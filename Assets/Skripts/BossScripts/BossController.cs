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
        
    }

    public IEnumerator ShiftEvent()
    {
        bossAnimator.SetBool("ShiftYes", true);
        bossAnimator.SetBool("IdleYes", false);
        
        yield return new WaitForSeconds(1f);
        
        bossAnimator.SetBool("ShiftYes", false);
        bossAnimator.SetBool("IdleYes", true);
    }

    public void ChangeToSecondStage()
    {
        bossAnimator.SetBool("Stage2Yes", true);
    }

    public void ChangeToThirdStage()
    {
        bossAnimator.SetBool("Stage3Yes", true);
    }

    public void ChangeToFourthStage()
    {
        bossAnimator.SetBool("Stage4Yes", true);
    }

    public IEnumerator SecondStage()
    {
        yield return new WaitForSeconds(1f); 
    }
    
}