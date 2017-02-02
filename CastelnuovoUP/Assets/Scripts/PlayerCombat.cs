using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerCombat : MonoBehaviour {

    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;
    public float Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    [Header("DASH")]
    [SerializeField]
    private float dashSpeedBonus = .3f;
    [SerializeField]
    private float dashTime = .2f;
    [SerializeField]
    private float dashDamage = 20f;
    [SerializeField]
    private float dashForce = 2f;
    private bool currentDash = false;

    private PlayerController playerController;

	void Start ()
    {
        playerController = GetComponent<PlayerController>();
        currentHealth = maxHealth;
	}

    private void Update()
    {
        if (Input.GetButtonDown("Dash") && !currentDash) Dash();
    }


    private void Dash()
    {
        currentDash = true;
        playerController.MoveSpeed += dashSpeedBonus;
        StartCoroutine("ResetDash");
    }
    private IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(dashTime);
        playerController.MoveSpeed -= dashSpeedBonus;
        currentDash = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Hit enemy with dash
            if (currentDash)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.Push(transform.forward * dashForce);
                enemy.Damage(dashDamage);
            }
        }
    }
}