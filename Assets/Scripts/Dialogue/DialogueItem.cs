
[System.Serializable]
public class DialogueItem
{
    public string Name;
    public string Text;
    public string animatorTrigger;
    public DialogueItem(string name, string text, string animatorTrigger = null)
    {
        Name = name;
        Text = text;
        this.animatorTrigger = animatorTrigger;
    }
}