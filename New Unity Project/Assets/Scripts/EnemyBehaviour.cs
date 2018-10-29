using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public Transform target;
    public float speed;
    public GameObject corpse;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (target) {
            Move();
        }
	}
    private void Move()
    {
        Vector2 dir = new Vector2(target.position.x - transform.position.x,
            target.position.y - transform.position.y);
        dir.Normalize();
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(gameObject.name + "colisiona con " + collider.gameObject.name);
        if (collider.gameObject.layer == LayerMask.NameToLayer("Stain") && collider.gameObject.tag == gameObject.tag) {
            Instantiate(corpse, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
