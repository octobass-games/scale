using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float SecondsUntilExplosion;

    private float Timer;
    private bool IsTimerPaused = true;

    void Awake()
    {
        
    }

    public void PauseTimer()
    {
        IsTimerPaused = true;
    }

    public void UnpauseTimer()
    {
        IsTimerPaused = false;
    }

    void Update()
    {
        if (!IsTimerPaused)
        {
            Timer += Time.deltaTime;

            if (Timer > SecondsUntilExplosion)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        GameObject.FindGameObjectWithTag(TagComparer.GNOME).GetComponent<Inventory>().RemoveItem(this.gameObject);

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Pickupable>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;

        Timer = 0;
        PauseTimer();
    }
}
