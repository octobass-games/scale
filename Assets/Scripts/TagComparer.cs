

using UnityEngine;

public class TagComparer
{
    public static string GIANT = "Giant";
    public static string GNOME = "Gnome";
    public static bool IsPlayer(string tag)
    {
        return tag == GIANT || tag == GNOME;
    }

    public static bool IsPlayer(Collider2D col)
    {
        return col.CompareTag(GIANT) || col.CompareTag(GNOME);
    }

    public static bool IsGnome(Collider2D col)
    {
        return  col.CompareTag(GNOME);
    }

    public static bool IsGnome(string tag)
    {
        return tag == GNOME;
    }

    public static bool IsGiant(Collider2D col)
    {
        return col.CompareTag(GIANT);
    }

    public static bool IsGiant(string tag)
    {
        return tag == GIANT;
    }


    public static string GetTagBySize(bool isGiant)
    {
        return isGiant ? GIANT : GNOME;
    }

}

