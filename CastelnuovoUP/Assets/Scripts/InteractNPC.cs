using UnityEngine;

public class InteractNPC : MonoBehaviour {

    [SerializeField]
    private string id = "NPC_0";
    public string Id
    {
        get { return id; }
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
    private Dialog dialog;
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
                if (!dialogUI.DialogStarted) dialogUI.LoadDialog(dialog);
                else
                {
                    bool continues = dialogUI.NextLine();
                    if (!continues) return false;
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
        dialog = _dialog;
    }

}
