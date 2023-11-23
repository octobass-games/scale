using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    public SpriteRenderer Arrow;

    public Sprite LeftArrow;
    public Sprite DownArrow;
    public Sprite UpArrow;
    public Sprite RightArrow;

    public Animator WindmillAnimator;

    public GameObject LeftWind;
    public GameObject RightWind;
    public GameObject UpWind;
    public GameObject DownWind;
    public ErrorDialogue ErrorDialogue;
    public string fmodEvent;


    public bool On = true;
    public enum Direction { Left, Right, Up, Down };
    public Direction direction = Direction.Right;
    private List<Direction> directions = new List<Direction>{  Direction.Left, Direction.Up, Direction.Right, Direction.Down };

    private FMOD.Studio.EventInstance instance;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        instance.start();
    }

    public void TurnOn()
    {
        On = true;
        DoThings();
        instance.start();
    }

    public void TurnOff()
    {
        On = false;
        DoThings();
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void Toggle()
    {
        On = !On;
        DoThings();
    }

    void Awake()
    {
        DoThings();
    }

    public void DoThings()
    {
        WindmillAnimator.SetBool("On", On);

        LeftWind.SetActive(On && direction == Direction.Left);
        RightWind.SetActive(On && direction == Direction.Right);
        UpWind.SetActive(On && direction == Direction.Up);
        DownWind.SetActive(On && direction == Direction.Down);

        Arrow.sprite = CalculateArrowSprite();
    }

    private Sprite CalculateArrowSprite()
    {
        switch (direction)
        {
            case Direction.Left:
                return LeftArrow;
            case Direction.Right:
                return RightArrow;
            case Direction.Up:
                return UpArrow;
            case Direction.Down:
                return DownArrow;
            default:
                return LeftArrow;
        }
    }

    public void ChangeArrowDirection()
    {
        if (On)
        {
            ErrorDialogue.ShowNoUseDialogue();
            return;
        }
        var index = directions.IndexOf(direction);
        if (index == directions.Count -1) {
            direction = directions[0];
        }else
        {
            direction = directions[index + 1];
        }
        DoThings();
    }
}
