using System;
using System.Collections.Generic;
using System.Linq;

public static class StringExtension
{
    public static List<DialogueItem> ParseToDialogue(this String text)
    {
        var items = new List<DialogueItem>();
        var lines = text.Split('\n');
        foreach (var line in lines)
        {
            if (line.Trim() != "")
            {
                var split = line.Split(":");
                if (split.Length == 3)
                {

                    items.Add(new DialogueItem(split[0].Trim(), split[1].Trim(), split[2].Trim()));
                }
                else
                {
                    items.Add(new DialogueItem(split[0].Trim(), split[1].Trim()));

                }
            }
        }
        return items;
    }
}