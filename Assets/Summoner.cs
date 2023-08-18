using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private float summonTime;

    private Vector2 targetPosition;
    private Animator anim;

    public MoshSummon enemyToSummon;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition= new Vector2(randomX, randomY);
    }

    // Update is called once per frame
    void Update()
    {
        if(player!= null)
        {
            if(Vector2.Distance(transform.position,targetPosition)> .5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed*Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);

                if(Time.time >= summonTime)
                {
                    summonTime= Time.time + timeBetweenAttack;
                    anim.SetTrigger("summon");
                }
            }
        }
    }

    public void Summon()
    {
        if(player!= null)
        {
            Instantiate(enemyToSummon, player.position, player.rotation);
        }
    }
}
