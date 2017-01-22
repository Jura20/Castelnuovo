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
    
    public void LoadDialog(Dialog _dialog)
    {
        dialog = _dialog;
        dialogStarted = true;
        Line line = dialog.lines[0];

        if (line.Author.Equals("Player")) SetupLeftLine(line);
        else SetupRightLine(line);
    }

    //Returns false if the dialog ends
    public bool NextLine()
    {
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

        }
        //Normal line
        else
        {
            leftName.text = line.Author;
            leftContent.text = line.Text;
        }


        dialogUILeft.gameObject.SetActive(true);
    }

    private void SetupRightLine(Line line)
    {
        dialogUILeft.gameObject.SetActive(false);

        //Choice
        if (line.choices != null && line.choices.Count > 0)
        {

        }
        //Normal line
        else
        {
            rightName.text = line.Author;
            rightContent.text = line.Text;
        }

        dialogUIRight.gameObject.SetActive(true);
    }


    public void DisableDialogUI()
    {
        dialogStarted = false;
        currentLine = 0;
        dialogUILeft.gameObject.SetActive(false);
        dialogUIRight.gameObject.SetActive(false);
    }

}
