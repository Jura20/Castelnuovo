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

    private int interactableMask;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerGraphics = playerController.GetGraphics();
        
        interactableMask = LayerMask.GetMask(interactableLayerName);
    }

    //Raycasting
    void FixedUpdate () {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerGraphics.forward, out hit, interactableDistance, interactableMask))
        {
            Debug.Log("Mask");
            interactNPC = hit.collider.gameObject.GetComponentInChildren<InteractNPC>();
            if (interactNPC != null)
            {
                Debug.Log("NPC");

                interactNPC.ReceivePlayerInteraction();
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
