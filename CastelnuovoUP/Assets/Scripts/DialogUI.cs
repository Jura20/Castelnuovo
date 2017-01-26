using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour {
    
    [SerializeField]
    private Transform dialogUILeft;
    [SerializeField]
    private Text leftName;
    [SerializeField]
    private Text leftContent;
    [SerializeField]
    private Image leftImage;

    [SerializeField]
    private Text[] options;

    [SerializeField]
    private Transform dialogUIRight;
    [SerializeField]
    private Text rightName;
    [SerializeField]
    private Text rightContent;
    [SerializeField]
    private Image rightImage;

    Dialog dialog;
    private bool dialogStarted = false;
    public bool DialogStarted
    {
        get { return dialogStarted; }
    }
    private int currentLine = 0;
    private float opinionMod = 0f;
    public float OpinionMod
    {
        get { return opinionMod; }
    }

    public void LoadDialog(Dialog _dialog, float startingOpinion)
    {
        dialog = _dialog;
        dialogStarted = true;
        opinionMod = startingOpinion;

        //First dialog line
        Line line = dialog.lines[0];
        if (line.Author.Equals("Player")) SetupLeftLine(line);
        else SetupRightLine(line);
    }

    //Returns false if the dialog ends
    public bool NextLine()
    {
        //Display next line
        currentLine++;
        if(currentLine < dialog.lines.Count )
        {
            Line line = dialog.lines[currentLine];
            if (line.Author.Equals("Player")) SetupLeftLine(line);
            else SetupRightLine(line);
            return true;
        }else
        {
            //End of dialog
            DisableDialogUI();
            return false;
        }
        
    }

    private void SetupLeftLine(Line line)
    {
        dialogUIRight.gameObject.SetActive(false);

        //Choice
        if (line.choices != null && line.choices.Count > 0)
        {
            leftContent.gameObject.SetActive(false);
            for(int i=0; i<line.choices.Count; i++)
            {
                options[i].gameObject.SetActive(true);
                options[i].text = "> " + line.choices[i].Text;
                if (i == 0)
                {
                    Button firstButton = options[i].gameObject.GetComponent<Button>();
                    firstButton.Select();
                    firstButton.OnSelect(null); //For the highlight to work well (workaround)
                }
            }
        }
        //Normal line
        else
        {
            if(options != null && options.Length > 0)
            {
                leftContent.gameObject.SetActive(true);
                for (int i = 0; i < options.Length; i++)
                    options[i].gameObject.SetActive(false);
            }
            leftName.text = line.Author;
            leftContent.text = line.Text;
        }

        dialogUILeft.gameObject.SetActive(true);
    }

    private void SetupRightLine(Line line)
    {
        dialogUILeft.gameObject.SetActive(false);
        
        rightName.text = line.Author;
        rightContent.text = line.Text;
        
        dialogUIRight.gameObject.SetActive(true);
    }

    public void DisableDialogUI()
    {
        dialogStarted = false;
        currentLine = 0;
        dialogUILeft.gameObject.SetActive(false);
        dialogUIRight.gameObject.SetActive(false);
    }


    public void Option0Clicked()
    {
        Line line = dialog.lines[currentLine];
        opinionMod += line.choices[0].OpinionMod;
        Debug.Log("OpinionMod in DialogUI: " + opinionMod);
    }
    public void Option1Clicked()
    {
        Line line = dialog.lines[currentLine];
        opinionMod += line.choices[1].OpinionMod;
    }
    public void Option2Clicked()
    {
        Line line = dialog.lines[currentLine];
        opinionMod += line.choices[2].OpinionMod;
    }


}
