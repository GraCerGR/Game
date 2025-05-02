using UnityEngine;
using TMPro;

public class Healt : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI HP; 
    [SerializeField] private float currentHP = 100f; 
    [SerializeField] private float maxHP = 100f;
    
    private void Start()
    {
        HP.text = "100%"; 
    }

    private void Update()
    {
        float hpPercentage = PlayerHealth.health;

        HP.text = $"{hpPercentage:0}%";
    }

   /* public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); 
    } */
}
