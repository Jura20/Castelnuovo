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
    private float moveSpeed = 3f;
    private Vector3 velocity = Vector3.zero;
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
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        interactPlayer = GetComponent<InteractPlayer>();

        if (graphics == null) Debug.LogError("Player graphics not set up.");
    }
	
	void Update ()
    {
        if (!blocked)
        {
            Move();
            Interact();
        } 
    }

    private void Move()
    {
        //Input
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");
        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        //Animation movement
        //...
        
        //Movement
        velocity = (movHorizontal + movVertical) * moveSpeed;
        rb.MovePosition(rb.position + velocity * Time.deltaTime);

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
        if (Input.GetButtonDown("Jump")){
            interactPlayer.ActivateInteract();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer(collidablesLayerName)) velocity = Vector3.zero;
    }
}
