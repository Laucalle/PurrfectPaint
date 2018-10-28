using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject stain;
    public float speed = 5f;

    public float coneAngle = 15;
    public float distance = 7.5f;

    private void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Todo esto se puede resumir en un transform.LookAt pero sin el tiempo de retardo de seguimiento
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
            for (int i = 0; i < 50; i++)
            {
                float minAngle = this.transform.rotation.eulerAngles.z - coneAngle;
                float maxAngle = this.transform.rotation.eulerAngles.z + coneAngle;
                //Debug.Log("Angulo: " + this.transform.rotation.eulerAngles + " Min: " + minAngle + " Max: " + maxAngle);

                float newAngle = RandomFromDistribution.RandomRangeNormalDistribution(minAngle, maxAngle, RandomFromDistribution.ConfidenceLevel_e._95);

                if (newAngle <= 360 && newAngle >= 275)
                {
                    newAngle = 0;
                }
                else if (newAngle >= 180 && newAngle < 275)
                {
                    newAngle = 180;
                }

                newAngle = newAngle * Mathf.Deg2Rad;

                float newDistance = RandomFromDistribution.RandomRangeExponential(0, distance, 1, RandomFromDistribution.Direction_e.Right);

                float catetoX = Mathf.Cos(newAngle) * newDistance;
                float catetoY = Mathf.Abs(Mathf.Sin(newAngle) * newDistance);
                //Debug.Log("NewAngle: " + newAngle + " NewDistance: " + newDistance + " CatetoX: " + catetoX + " CatetoY: " + catetoY);

                Vector3 newPoint = this.transform.position + new Vector3(catetoX, catetoY, 0);

                Instantiate(stain, newPoint, Quaternion.identity);
            }

        }



    }
}
