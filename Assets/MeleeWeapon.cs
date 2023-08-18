using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float timeBetweenAttack;

    // Start is called before the first frame update
    private Animator weaponAnimator;
    private AudioSource source;
    public override void Start()
    {
        objectTransform = GetComponent<Transform>();
        weaponAnimator = objectTransform.GetChild(0).GetComponent<Animator>();
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public override void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
        transform.rotation = rotation;

        Vector3 newScale = transform.localScale;
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
        {
            newScale.y = -Math.Abs(newScale.y);
            objectTransform.localScale = newScale;
        }
        else
        {
            newScale.y = Math.Abs(newScale.y);
            objectTransform.localScale = newScale;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(Time.time >= shotTime)
            {
                source.Play();
                shotTime= Time.time+ timeBetweenAttack;
                weaponAnimator.SetTrigger("attack");
            }
        }
    }
}
