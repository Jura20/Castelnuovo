using UnityEngine;
using System.Xml;

public class DialogLoader : MonoBehaviour {

    [SerializeField]
    private TextAsset dialogsXml;

    [SerializeField]
    private InteractNPC[] NPCs;

    private void Start()
    {
        if (dialogsXml == null) return;
        //Detect NPCs
        LoadNPCs();
        LoadXml();
    }

    private void LoadNPCs()
    {
        GameObject[] npcGOs = GameObject.FindGameObjectsWithTag("NPC");
        if(npcGOs != null)
        {
            NPCs = new InteractNPC[npcGOs.Length];
            for (int i = 0; i < npcGOs.Length; i++)
            {
                NPCs[i] = npcGOs[i].GetComponentInChildren<InteractNPC>();
            }
        }
    }

    private void LoadXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dialogsXml.text); //Read file
        XmlNodeList npcList = xmlDoc.GetElementsByTagName("npc");

        foreach (XmlNode npc in npcList)
        {
            ParseNPC(npc);
        }
    }

    private void ParseNPC(XmlNode npc)
    {
        string npcID = "";
        //Attributes
        XmlAttributeCollection npcAttributes = npc.Attributes;
        foreach (XmlAttribute npcAttribute in npcAttributes)
        {
            if (npcAttribute.Name == "id")
            {
                npcID = npcAttribute.InnerText;
            }
        }
        if (!npcID.Equals(""))
        {
            //Children
            XmlNodeList npcDialogList = npc.ChildNodes;
            foreach (XmlNode dialog in npcDialogList)
            {
                Dialog npcDialog = ParseDialog(dialog);
                AssignDialog(npcID, npcDialog);
            }
        }
    }

    private Dialog ParseDialog(XmlNode dialog)
    {
        Dialog npcDialog = new Dialog();
        
        //Attributes
        XmlAttributeCollection dialogAttributes = dialog.Attributes;
        foreach (XmlAttribute dialogAttribute in dialogAttributes)
        {
            if (dialogAttribute.Name == "id")
            {
                npcDialog.Id = dialogAttribute.InnerText;
            }
            else
            {
                if (dialogAttribute.Name == "minOpinion")
                {
                    npcDialog.MinOpinion = float.Parse(dialogAttribute.InnerText);
                }
            }
        }
        //Children
        XmlNodeList dialogElems = dialog.ChildNodes;
        foreach (XmlNode line in dialogElems)
        {
            Line npcLine = ParseLine(line);
            AssignLine(npcLine, ref npcDialog);
        }

        return npcDialog;
    }

    private Line ParseLine(XmlNode line)
    {
        Line npcLine = new Line();
        
        //Attributes
        XmlAttributeCollection lineAttributes = line.Attributes;
        foreach (XmlAttribute lineAttribute in lineAttributes)
        {
            if (lineAttribute.Name == "author")
            {
                npcLine.Author = lineAttribute.InnerText;
            }
        }
        //Children
        XmlNodeList lineElems = line.ChildNodes;
        if (lineElems != null && lineElems.Count > 1)
        {
            //Choice
            npcLine.IsChoice = true;
            foreach (XmlNode choice in lineElems)
            {
                Choice npcChoice = ParseChoice(choice);
                AssignChoice(npcChoice, ref npcLine);
            }
        }
        else
        {
            //Normal line
            npcLine.IsChoice = false;
            npcLine.Text = line.InnerText;
        }

        return npcLine;
    }

    private Choice ParseChoice(XmlNode choice)
    {
        Choice npcChoice = new Choice();

        npcChoice.Text = choice.InnerText;
        XmlAttributeCollection choiceAttributes = choice.Attributes;
        if (choiceAttributes != null && choiceAttributes.Count > 0)
        {
            foreach (XmlAttribute choiceAttribute in choiceAttributes)
            {
                if (choiceAttribute.Name == "opinionMod") npcChoice.OpinionMod = float.Parse(choiceAttribute.InnerText);
            }
        }

        return npcChoice;
    }

    private void AssignChoice(Choice choice, ref Line line)
    {
        line.choices.Add(choice);
    }

    private void AssignLine(Line line, ref Dialog dialog)
    {
        dialog.lines.Add(line);
    }

    private void AssignDialog(string npcID, Dialog dialog)
    {
        //Find InteractNPC with npcID
        for(int i=0; i<NPCs.Length; i++)
        {
            if (NPCs[i].Id.Equals(npcID))
            {
                NPCs[i].AddDialog(dialog);
                break;
            }
        }
    }

}
