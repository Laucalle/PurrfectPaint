using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public Transform target;
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}
    private void Move()
    {
        Vector2 dir = new Vector2(target.position.x - transform.position.x,
            target.position.y - transform.position.y);
        dir.Normalize();
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
}
