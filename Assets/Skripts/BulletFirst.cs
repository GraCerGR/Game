using UnityEngine;

public class BulletFirst : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // уничтожить через X секунд
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject); // уничтожить при попадании в любую поверхность
        }
    }
}
