using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InteractPlayer))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Transform graphics;
    public Transform GetGraphics()
    {
        return graphics;
    }

    //Movement
    [SerializeField]
    private float moveSpeed = .1f;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private string collidablesLayerName = "Collidable";

    //Components
    private Rigidbody rb;
    private InteractPlayer interactPlayer;

    private bool blocked = false; // Avoid moving when in dialog, etc.
    public bool GetBlocked()
    {
        return blocked;
    }
    public void SetBlocked(bool value)
    {
        blocked = value;
        if (blocked) rb.velocity = Vector3.zero;
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        interactPlayer = GetComponent<InteractPlayer>();

        if (graphics == null) Debug.LogError("Player graphics not set up.");
    }
	
	void Update ()
    {
        Interact();
    }

    private void FixedUpdate()
    {
        if (!blocked)
        {
            
            MoveInterpolated();
        }
    }
    
    private void MoveInterpolated()
    {
        //Input
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");
        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;
        Vector3 velocity = (movHorizontal + movVertical) * moveSpeed;
        
        //Animation movement
        //...

        //Movement
        rb.velocity = new Vector3(velocity.x/Time.fixedDeltaTime, 0, velocity.z/Time.fixedDeltaTime);

        //Rotation (graphics only)
        if (velocity != Vector3.zero)
        {
            graphics.transform.rotation = Quaternion.Slerp(
                graphics.transform.rotation,
                Quaternion.LookRotation(velocity),
                Time.deltaTime * rotationSpeed
            );
        }
    }


    private void Interact()
    {
        if (Input.GetButtonDown("Submit")){
            interactPlayer.ActivateInteract();
        }
    }


}
