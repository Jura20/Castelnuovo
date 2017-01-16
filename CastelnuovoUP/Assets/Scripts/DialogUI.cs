using UnityEngine;

public class DialogUI : MonoBehaviour {

    private PlayerController playerController;
    [SerializeField]
    private Transform dialogUILeft;
    [SerializeField]
    private Transform dialogUIRight;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (playerController == null) Debug.LogError("Player controller not found in DialogUI");
    }

    public void InitiateDialog()
    {
        Debug.Log("Dialog initiated");
        playerController.SetBlocked(true);
        LoadDialog();
    }

    public void LoadDialog()
    {

    }

}
