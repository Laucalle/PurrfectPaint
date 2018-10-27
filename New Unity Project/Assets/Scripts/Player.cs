using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject stain;
    public float speed = 5f;

    private void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        if(transform.rotation.eulerAngles.z <= 360 && transform.rotation.eulerAngles.z >= 275)
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
        else if(transform.rotation.eulerAngles.z >= 180 && transform.rotation.eulerAngles.z < 275)
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 steinPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            steinPos.z = 0;
            Instantiate(stain, steinPos, Quaternion.identity);
        }

    }
}
