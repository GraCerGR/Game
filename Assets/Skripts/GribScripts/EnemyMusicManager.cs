using UnityEngine;
using System.Collections;

public class EnemyMusicManager : MonoBehaviour
{
    public static EnemyMusicManager Instance;

    public AudioSource mainAudioSource;
    public AudioSource secondaryAudioSource;

    public AudioClip calmMusic;
    public AudioClip combatMusic;

    private bool isInCombat = false;
    private Coroutine returnToCalmCoroutine;
    public float calmDelay = 2.0f;
    public float crossfadeDuration = 1.0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (mainAudioSource != null && calmMusic != null)
        {
            mainAudioSource.clip = calmMusic;
            mainAudioSource.loop = true;
            mainAudioSource.volume = 1f;
            mainAudioSource.Play();
        }
    }

    private void Update()
    {
        EnemyAwareness[] enemies = FindObjectsOfType<EnemyAwareness>();
        bool anyAttacking = false;

        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.isSeeYou)
            {
                anyAttacking = true;
                break;
            }
        }

        if (anyAttacking)
        {
            if (!isInCombat)
            {
                isInCombat = true;
                if (returnToCalmCoroutine != null)
                {
                    StopCoroutine(returnToCalmCoroutine);
                }
                StartCoroutine(CrossfadeToMusic(combatMusic));
            }
        }
        else
        {
            if (isInCombat && returnToCalmCoroutine == null)
            {
                returnToCalmCoroutine = StartCoroutine(ReturnToCalmAfterDelay());
            }
        }
    }

    private IEnumerator ReturnToCalmAfterDelay()
    {
        yield return new WaitForSeconds(calmDelay);

        EnemyAwareness[] enemies = FindObjectsOfType<EnemyAwareness>();
        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.isSeeYou)
            {
                returnToCalmCoroutine = null;
                yield break;
            }
        }

        isInCombat = false;
        StartCoroutine(CrossfadeToMusic(calmMusic));
        returnToCalmCoroutine = null;
    }

    private IEnumerator CrossfadeToMusic(AudioClip newClip)
    {
        if (mainAudioSource.clip == newClip || newClip == null) yield break;

        secondaryAudioSource.clip = newClip;
        secondaryAudioSource.volume = 0f;
        secondaryAudioSource.loop = true;
        secondaryAudioSource.Play();

        float time = 0f;

        while (time < crossfadeDuration)
        {
            float t = time / crossfadeDuration;
            mainAudioSource.volume = Mathf.Lerp(1f, 0f, t);
            secondaryAudioSource.volume = Mathf.Lerp(0f, 1f, t);
            time += Time.deltaTime;
            yield return null;
        }

        var temp = mainAudioSource;
        mainAudioSource = secondaryAudioSource;
        secondaryAudioSource = temp;

        secondaryAudioSource.Stop();
        secondaryAudioSource.clip = null;
        secondaryAudioSource.volume = 1f;
        mainAudioSource.volume = 1f;
    }
}
