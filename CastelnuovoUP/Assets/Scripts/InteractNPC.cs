using UnityEngine;

public class InteractNPC : MonoBehaviour {

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

    private void Start()
    {
        if (interactMarker == null) Debug.LogError("Interact marker not set for: " + transform.name);
        if (UI == null) Debug.LogError("UI not set for: " + transform.name);
    }


    public void ReceivePlayerInteraction()
    {
        SetSelected(true);
    }

    public void ActivateInteract()
    {
        Debug.Log("NPC: " + transform.name + " activated.");
        switch (typeNPC)
        {
            case 0: //Talker
                DialogUI dialogUI = UI.GetComponent<DialogUI>();
                break;
            default:
                break;
        }
    }

}
