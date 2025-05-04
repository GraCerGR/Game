using UnityEngine;

public class BloodDisapear : MonoBehaviour
{
    [SerializeField] float lifeTime = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
