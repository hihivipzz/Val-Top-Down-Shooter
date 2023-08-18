using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShot;

    [HideInInspector]
    public float shotTime;
    public Transform objectTransform;

    private Animator cameraAni;

    // Start is called before the first frame update
    public virtual void Start()
    {
        objectTransform = GetComponent<Transform>();
        cameraAni = Camera.main.GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        Vector3 newScale = transform.localScale;
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            newScale.y = - Math.Abs(newScale.y);
            objectTransform.localScale = newScale;
        }
        else
        {
            newScale.y = Math.Abs(newScale.y);
            objectTransform.localScale = newScale;
        }

        if (Input.GetMouseButton(0))
        {
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                cameraAni.SetTrigger("shake");
                shotTime = Time.time + timeBetweenShot;
            }
        }
        else
        {
            //Ngưng bắn
        }
    }
}
 