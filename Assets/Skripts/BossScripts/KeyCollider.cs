using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyCollider : MonoBehaviour
{
    private void Start()
    {
        // Ensure the collider is set as a trigger
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object touching the collider has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Load scene 1
            SceneManager.LoadScene("ThisISTheEnd");
        }
    }
}