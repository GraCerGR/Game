using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{ 
    public Animator bossAnimator;

    void Awake()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            bossAnimator.SetBool("ShiftYes", false);
            bossAnimator.SetBool("IdleYes", true);
        }
    }

    public IEnumerator ShiftEvent()
    {
        bossAnimator.SetBool("ShiftYes", true);
        bossAnimator.SetBool("IdleYes", false);
        
        yield return new WaitForSeconds(1f);
        
        bossAnimator.SetBool("ShiftYes", false);
        bossAnimator.SetBool("IdleYes", true);
    }
    
}