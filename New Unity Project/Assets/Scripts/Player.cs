using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Animator catAnimator;
    public GameObject[] stain;
    public float moveSpeed = 5f;

    public float coneAngle = 15;
    public float distance = 7.5f;

    public float fireSpeed = 5f;

    private int color = 0;
    private float nextTimeToFire = 0f;

    private void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Todo esto se puede resumir en un transform.LookAt pero sin el tiempo de retardo de seguimiento
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, moveSpeed * Time.deltaTime);

        if(transform.rotation.eulerAngles.z <= 360f && transform.rotation.eulerAngles.z >= 275f)
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        }
        else if(transform.rotation.eulerAngles.z >= 180f && transform.rotation.eulerAngles.z < 275f)
        {
            transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 180f);
        }

        AnimateCat();

        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireSpeed;
            CheckColor();
            if (color != -1)
            {
                GenerateCone(color);
            }

        }

    }

    private void AnimateCat()
    {
        if (transform.rotation.eulerAngles.z >= 0f && transform.rotation.eulerAngles.z < 60f)
        {
            catAnimator.Play("RightCat");
        }
        else if (transform.rotation.eulerAngles.z >= 60f && transform.rotation.eulerAngles.z < 120)
        {
            catAnimator.Play("BackCat");
        }
        else if (transform.rotation.eulerAngles.z >= 120 && transform.rotation.eulerAngles.z <= 180)
        {
            catAnimator.Play("LeftCat");
        }
    }

    private void CheckColor()
    {
        if(Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2) && Input.GetKey(KeyCode.Alpha3))
        {
            color = -1;
            return;
        } 
        
        if(Input.GetKey(KeyCode.Alpha1))
        {
            if (Input.GetKey(KeyCode.Alpha2))
            {
                color = 3;
                return;
            }

            if(Input.GetKey(KeyCode.Alpha3))
            {
                color = 5;
                return;
            }

            color = 0;
            return;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (Input.GetKey(KeyCode.Alpha3))
            {
                color = 4;
                return;
            }

            color = 1;
            return;
        }

        if(Input.GetKey(KeyCode.Alpha3))
        {
            color = 2;
            return;
        }
        color = -1;
        return;

    }

    private void GenerateCone(int color)
    {
        for (int i = 0; i < 25; i++)
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
            int shift = Random.Range(0, 2);
            Instantiate(stain[color*2+shift], newPoint, Quaternion.identity);
        }
    }
}
