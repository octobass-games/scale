using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float SecondsUntilExplosion;
    public string explosionFmodEvent;
    public string timerFmodEvent;

    private float Timer;
    private bool IsTimerPaused = true;
    private FMOD.Studio.EventInstance instance;
    private bool isTimerSoundPlaying = true;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(timerFmodEvent);
    }

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

            if(isTimerSoundPlaying == false)
            {
                instance.start();
                isTimerSoundPlaying = true;
            }
            

            if (Timer > SecondsUntilExplosion)
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                Explode();
            }
        }
    }
}
