using UnityEngine;
using System.Collections.Generic;

public class InteractNPC : MonoBehaviour {

    [SerializeField]
    private string id = "NPC_0";
    public string Id
    {
        get { return id; }
    }
    [SerializeField]
    private float opinion = 50f;
    public float Opinion
    {
        get { return opinion; }
        set { opinion = value; }
    }


    [SerializeField]
    private Transform interactMarker;
    private bool selected = false;
    public void SetSelected(bool value)
    {
        selected = value;
        if (selected == true) interactMarker.gameObject.SetActive(true);
        else interactMarker.gameObject.SetActive(false);
    }

    //Types of NPC
    // 0:Talker, 1:Chest
    [SerializeField]
    uint typeNPC = 0;
    [SerializeField]
    private Transform UI;

    //Dialog
    private List<Dialog> dialogs = new List<Dialog>();
    private DialogUI dialogUI;

    private void Start()
    {
        if (interactMarker == null) Debug.LogError("Interact marker not set for: " + transform.name);

        if (UI == null) Debug.LogError("UI not set for: " + transform.name);
        else dialogUI = UI.GetComponent<DialogUI>();

        if (dialogUI == null) Debug.LogError("UI has no DialogUI");
    }

#region Interaction

    public void ReceivePlayerInteraction()
    {
        SetSelected(true);
    }

    //Called when pressed "Jump" on PlayerController
    //Returns false if interaction ended
    public bool ActivateInteract()
    {
        switch (typeNPC)
        {
            case 0: //Talker
                if (!dialogUI.DialogStarted)
                {
                    //Decide appropiate dialog for current opinion
                    Dialog currentDialog = new Dialog();
                    float diff = 9999999f;
                    foreach(Dialog d in dialogs)
                    {
                        float newDiff = Mathf.Abs(d.MinOpinion - opinion);
                        if (newDiff < diff)
                        {
                            currentDialog = d;
                            diff = newDiff;
                        }
                    }
                    //Load dialog in dialogUI and show first line
                    dialogUI.LoadDialog(currentDialog, opinion);
                }
                else
                {
                    //Load next line
                    bool continues = dialogUI.NextLine();
                    if (!continues)
                    {
                        //End of dialog
                        opinion = dialogUI.OpinionMod; //Update NPC opinion
                        Debug.Log("NPC opinion: " + opinion);
                        return false;
                    }
                }
                break;
            default:
                Debug.LogError("NPC of no type: " + transform.name);
                break;
        }

        return true;
    }
    
#endregion

    public void AddDialog(Dialog _dialog)
    {
        //Add dialog
        dialogs.Add(_dialog);
    }

}
