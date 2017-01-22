using System.Collections.Generic;

public class Choice
{
    private string text;
    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    private float opinionMod;
    public float OpinionMod
    {
        get { return opinionMod; }
        set { opinionMod = value; }
    }
}

public class Line
{
    private string author;
    public string Author
    {
        get { return author; }
        set { author = value; }
    }

    private bool isChoice = false;
    public bool IsChoice
    {
        get { return isChoice; }
        set { isChoice = value; }
    }

    //Normal line of text
    private string text;
    public string Text
    {
        get { return text; }
        set { text = value; }
    }

    //Choice
    public List<Choice> choices = new List<Choice>();
}


public class Dialog {

    private string id;
    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    private float minOpinion;
    public float MinOpinion
    {
        get { return minOpinion; }
        set { minOpinion = value; }
    }

    public List<Line> lines = new List<Line>();
}
