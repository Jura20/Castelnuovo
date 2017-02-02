using UnityEngine;

public class FollowerCombat : MonoBehaviour {

    //[SerializeField]
    //private float health = 100f;
    //public float Health
    //{
    //    get { return health; }
    //}
    //[SerializeField]
    //private float speed = 2f;

    private Transform target;
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }


    public void PositionFollower()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float currentDist = 999999f;
        for (int j = 0; j < enemies.Length; j++)
        {
            float enemyDist = Mathf.Abs(Vector3.Distance(enemies[j].transform.position, transform.position));
            Debug.Log("Enemy dist: " + enemyDist);
            if (enemyDist < currentDist)
            {
                currentDist = enemyDist;
                target = enemies[j].transform;
                target.LookAt(enemies[j].transform);
            }
        }
    }


}
