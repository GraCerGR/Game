using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource Source;

    public AudioClip dieEnemySound;
    public AudioClip hitEnemySound;
    public AudioClip hitPlayerSound;
    public AudioClip gunSound;
    public AudioClip emptyGunSound;
    public AudioClip dustSound;
    public AudioClip footstepClips;

    public void PlayHitEnemySound(AudioClip clip = null)
    {
        if (clip != null)
            Source.PlayOneShot(clip);
        else
            Source.PlayOneShot(hitEnemySound, 1.75f);
    }

    public void PlayDieSound(AudioClip clip = null)
    {
        if (clip != null)
            Source.PlayOneShot(clip);
        else
            Source.PlayOneShot(dieEnemySound, 1.75f);
    }

    public void PlayHitPlayerSound(AudioClip clip = null)
    {
        if (clip != null)
            Source.PlayOneShot(clip);
        else
            Source.PlayOneShot(hitPlayerSound);
    }
    public void PlayGunSound(AudioClip clip = null)
    {
        if (clip != null)
            Source.PlayOneShot(clip);
        else
            Source.PlayOneShot(gunSound, 0.75f);
    }
    public void PlayEmptyGunSound(AudioClip clip = null)
    {
        if (clip != null)
            Source.PlayOneShot(clip);
        else
            Source.PlayOneShot(emptyGunSound, 0.75f);
    }
    public void PlayDustSound(AudioClip clip = null)
    {
        if (clip != null)
            Source.PlayOneShot(clip);
        else
            Source.PlayOneShot(dustSound);
    }
    public void PlayFootstepSound()
    {
        Source.PlayOneShot(footstepClips, 3f);
    }
}