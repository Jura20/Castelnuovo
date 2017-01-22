using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class InteractPlayer : MonoBehaviour {

    [SerializeField]
    private string interactableLayerName = "Interactable";
    [SerializeField]
    private float interactableDistance = 1f;
    private InteractNPC interactNPC;

    private PlayerController playerController;
    private Transform playerGraphics;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerGraphics = playerController.GetGraphics();
    }

    //Raycasting
    void FixedUpdate () {
        RaycastHit hit;
        Vector3 direction = playerGraphics.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, direction, out hit, interactableDistance))
        {
            Transform interactInfo = hit.collider.transform.Find("InteractInfo");
            if(interactInfo != null && LayerMask.LayerToName(interactInfo.gameObject.layer).Equals(interactableLayerName))
            {
                interactNPC = hit.collider.transform.Find("InteractInfo").GetComponent<InteractNPC>();
                if (interactNPC != null)
                {
                    interactNPC.ReceivePlayerInteraction();
                }
            }
        }else
        {
            //Nothing hit
            if (interactNPC != null)
            {
                interactNPC.SetSelected(false);
                interactNPC = null;
            }
        }
    }

    //Called when we have pressed "Jump" in PlayerController
    public void ActivateInteract()
    {
        if (interactNPC != null)
        {
            playerController.SetBlocked(true);
            if (!interactNPC.ActivateInteract())
            {
                //Dialog ended
                playerController.SetBlocked(false);
                interactNPC = null;
            }
        }
    }
}
