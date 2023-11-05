using System.Collections.Generic;
using UnityEngine;

public class SwitchTagChecker : MonoBehaviour
{
    public List<string> ValidUserTags = new List<string>();

    public bool IsValidUser(string userTag)
    {
        return ValidUserTags.Contains(userTag);
    }
}
