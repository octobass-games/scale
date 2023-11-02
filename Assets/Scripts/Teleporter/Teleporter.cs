using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public TeleporterEntrance FirstEntrance;
    public TeleporterEntrance SecondEntrance;

    public void Teleport(TeleporterEntrance teleporterEntrance, Collider2D collider2D)
    {
        var exit = GetExit(teleporterEntrance);

        var rb2d = collider2D.GetComponent<Rigidbody2D>();

        if (Input.GetKeyDown(KeyCode.Space)) {
            rb2d.MovePosition(exit.ExitPoint.position);
        }
    }

    private TeleporterEntrance GetExit(TeleporterEntrance teleporterEntrance)
    {
        return teleporterEntrance == FirstEntrance ? SecondEntrance : FirstEntrance;
    }
}
