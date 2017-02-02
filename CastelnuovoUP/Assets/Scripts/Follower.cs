using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

    [SerializeField]
    private Transform graphics;

    [SerializeField]
    private Transform followTarget;
    [SerializeField]
    private float minDistance = 1f;
    [SerializeField]
    private float followDelay = 1f;

    private bool following = false;

    [SerializeField]
    private float moveSpeed = 3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private float rotationSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (followTarget != null)
        {
            float distance = Vector3.Distance(followTarget.position, transform.position);
            if (distance > minDistance)
            {
                if (!following) StartCoroutine("StartFollowing");
                else Follow();
            }else
            {
                following = false;
            }
        }
    }

    private IEnumerator StartFollowing()
    {
        yield return new WaitForSeconds(followDelay);
        following = true;
    }

    private void Follow()
    {
        //Movement
        Vector3 dir = followTarget.position - transform.position;
        velocity = (dir.normalized) * moveSpeed;
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

}
