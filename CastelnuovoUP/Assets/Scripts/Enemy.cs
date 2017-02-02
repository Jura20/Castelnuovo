using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
    }


    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }

    //[SerializeField]
    //private float speed = 2f;


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag.Equals("Player"))
    //    {
    //        //Start combat
    //        enemyGroup.StartCombat(this);
    //    }
    //}

    public void Damage(float damage)
    {
        currentHealth -= damage;
        //Debug.Log("Current health: " + currentHealth);
        if (currentHealth <= 0f) Die();
    }


    public void Push(Vector3 force)
    {
        rb.AddForce(force);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }



}
