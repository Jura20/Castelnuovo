using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Transform graphics;

    [SerializeField]
    private float moveSpeed = 3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private string collidablesLayerName = "Collidable";

    //Components
    private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();

        if (graphics == null) Debug.LogError("Player graphics not set up.");
    }
	
	void Update () {
        Move();        
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


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer(collidablesLayerName)) velocity = Vector3.zero;
    }
}
