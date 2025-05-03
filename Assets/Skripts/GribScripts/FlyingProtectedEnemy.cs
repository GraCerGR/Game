using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingProtectedEnemy : MonoBehaviour
{
    private Transform enemy;
    private Transform player;
    public float detectionRange = 40f;
    Vector3 cameraPlace = new Vector3(0f, 1.4f, 0f);
    private bool isAttacking = false;
    public float protectCooldown = 2f;

    private float lastProtectedTime;

    Animator animator;

    // ---- Враги ---- 
    private List<Transform> selectedEnemies = new List<Transform>();
    public int enemyCount = 3;
    public GameObject shieldPrefab;

    // ---- Старые щиты ---- 
    private List<GameObject> activeShields = new List<GameObject>();

    // ---- Луч ---- 
    public GameObject beamPrefab;
    private List<GameObject> activeBeams = new List<GameObject>();

    private void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        lastProtectedTime = Time.time - protectCooldown;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(player);
        if (Time.time - lastProtectedTime >= protectCooldown)
        {
            FindRandomEnemyInRange();
            ProtectEnemy();
            lastProtectedTime = Time.time;
        }
    }

    private void FindRandomEnemyInRange()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        List<GameObject> validEnemies = new List<GameObject>();

        foreach (GameObject enemy in allEnemies)
        {
            if (enemy == this.gameObject) continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist <= detectionRange)
            {
                validEnemies.Add(enemy);
            }
            Debug.Log("Враг найден " + enemy);
        }

        selectedEnemies.Clear();

        if (validEnemies.Count > 0)
        {
            Shuffle(validEnemies);
            Debug.Log("Враги" + validEnemies);

            int count = Mathf.Min(enemyCount, validEnemies.Count);
            for (int i = 0; i < count; i++)
            {
                selectedEnemies.Add(validEnemies[i].transform);
                Debug.Log("Selected enemy: " + validEnemies[i].name);
            }
        }
        else
        {
            Debug.Log("No enemies in range.");
        }
    }

    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            GameObject temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    private void ProtectEnemy()
    {
        animator.SetTrigger("Attack");

        // ---- Удаление старых щитов ---- 
        foreach (GameObject shield in activeShields)
        {
            if (shield != null)
                Destroy(shield);
        }
        activeShields.Clear();

        // ---- Удаление старых лучей ---- 
        foreach (GameObject beam in activeBeams)
        {
            if (beam != null)
                Destroy(beam);
        }
        activeBeams.Clear();

        // ---- Добавление щитов ---- 
        foreach (Transform enemy in selectedEnemies)
        {
            GameObject shield = Instantiate(shieldPrefab, enemy);
            shield.transform.localPosition = new Vector3(0f, 0.4f, 0f);

            BoxCollider collider = enemy.GetComponent<BoxCollider>();
            if (collider != null)
            {
                float maxSize = Mathf.Max(collider.size.x + 0.2f, collider.size.y + 0.2f, collider.size.z + 0.2f);
                shield.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
            }

            activeShields.Add(shield);

            StartCoroutine(SpawnAndFadeBeam(enemy));
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject shield in activeShields)
        {
            if (shield != null)
                Destroy(shield);
        }
        activeShields.Clear();

        foreach (GameObject beam in activeBeams)
        {
            if (beam != null)
                Destroy(beam);
        }
        activeBeams.Clear();
    }


    private IEnumerator SpawnAndFadeBeam(Transform target)
    {
        GameObject beam = Instantiate(beamPrefab);
        LineRenderer lr = beam.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position + new Vector3(0f, 1f, 0f));
            lr.SetPosition(1, target.position + new Vector3(0f, 1f, 0f));

            Gradient originalGradient = lr.colorGradient;
            GradientAlphaKey[] alphaKeys = originalGradient.alphaKeys;
            float initialAlpha = alphaKeys.Length > 0 ? alphaKeys[0].alpha : 1f;

            // Показываем луч 0.3 сек
            yield return new WaitForSeconds(0.1f);

            // Плавное затухание за 0.2 сек
            float fadeTime = 0.2f;
            float elapsed = 0f;

            while (elapsed < fadeTime)
            {
                elapsed += Time.deltaTime;
                float t = 1f - (elapsed / fadeTime);

                Gradient gradient = new Gradient();
                gradient.SetKeys(
                    originalGradient.colorKeys,
                    new GradientAlphaKey[] {
                    new GradientAlphaKey(initialAlpha * t, 0f),
                    new GradientAlphaKey(initialAlpha * t, 1f)
                    }
                );
                lr.colorGradient = gradient;

                yield return null;
            }
        }

        Destroy(beam);
    }
}
