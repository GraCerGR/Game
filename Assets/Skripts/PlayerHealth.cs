using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] public static float health = 100;
    [SerializeField] public static float dustcount = 0;
    [SerializeField] private TextMeshProUGUI Dust2;

    private float nextActionTime = 0f;
    public float period = 0.1f;
    [SerializeField] private float floatcountofdust=1;

    [SerializeField] GameObject pistolImage;
    private Animator animator;

    float ADust = 0;


    private void Awake()
    {
        animator = pistolImage.GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        
        health -= amount;
        Debug.Log("Player HP: " + health);
        if (health <= 0)
        {
            // смерть
        }
    }
    public void TakeDustAngel(float a) 
    {
        ADust += a;
        //Dust2.text = $"{ADust:0}";
    }

    public void TakeHP(float amount)
    {
        health += amount;
        /*ADust += PlayerHealth.dustcount + amount;
        Dust2.text = $"{ADust:0}"*/
        ;

    }


    private void FixedUpdate()
    {
        if (ADust>0&&ADust<100)
        {
            //ADust -= Time.deltaTime;
            Debug.Log(Time.time);
            nextActionTime += period;
            ADust -= floatcountofdust* Time.deltaTime;
            Dust2.text = $"{ADust:0}";
            // execute block of code here
        }


        if (health > 75)
        {
            //animator.SetTrigger("100");
            animator.SetBool("100 0", true);
            animator.SetBool("75 0", false);
            animator.SetBool("25 0", false);
        }
        else if (health <= 75 && health > 25)
        {
            //animator.SetTrigger("75");
            animator.SetBool("100 0", false);
            animator.SetBool("75 0", true);
            animator.SetBool("25 0", false);
        }
        else
        {
            animator.SetBool("100 0", false);
            animator.SetBool("75 0", false);
            animator.SetBool("25 0", true);
            //animator.SetTrigger("25");
        }

    }

}
