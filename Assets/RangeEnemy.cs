using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public float stopDistance;

    public Transform shotPoint;

    public GameObject enemyBullet;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private float attackTime;

    private Animator anim;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (Vector2.Distance(transform.position, player.position) > stopDistance || !isInside())
            {

                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if (Time.time > attackTime)
            {
                attackTime = Time.time + timeBetweenAttack;
                anim.SetTrigger("attack");
            }
        }
       

        
    }

    public void RangeAttack()
    {
        if(player!=null&& isInside())
        {
            Vector2 direction = player.position - shotPoint.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            shotPoint.rotation = rotation;

            Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);
        }
        
    }

    private bool isInside()
    {
        if(transform.position.x >minX && transform.position.x < maxX && transform.position.y > minY && transform.position.y<maxY)
        {
            return true;
        }
        return false;
    }
}
