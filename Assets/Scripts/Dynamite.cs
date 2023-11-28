using FMODUnity;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float SecondsUntilExplosion;
    public string explosionFmodEvent;
    public StudioEventEmitter SoundEmitter;

    private float Timer;
    private bool IsTimerPaused = true;
    private bool isTimerSoundPlaying = true;

    public void PauseTimer()
    {
        IsTimerPaused = true;
    }

    public void UnpauseTimer()
    {
        IsTimerPaused = false;
        isTimerSoundPlaying = false;
    }

    public void Explode()
    {
        GameObject.FindGameObjectWithTag(TagComparer.GNOME).GetComponent<Inventory>().RemoveItem(this.gameObject);
        SoundEmitter.Stop();
        FMODUnity.RuntimeManager.PlayOneShot(explosionFmodEvent);

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Pickupable>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;

        Timer = 0;
        PauseTimer();
    }

    void Update()
    {
        if (!IsTimerPaused)
        {
            Timer += Time.deltaTime;

            if (isTimerSoundPlaying == false)
            {
                SoundEmitter.Play();
                isTimerSoundPlaying = true;
            }
            

            if (Timer > SecondsUntilExplosion)
            {
                SoundEmitter.Stop();
                Explode();
            }
        }
    }
}
