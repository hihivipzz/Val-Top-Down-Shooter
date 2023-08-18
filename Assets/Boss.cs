
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float health;
    public Enemy[] enemies;
    public float spawnOffset;
    public float damage;

    public GameObject skill;
    public int numberOfBullet;
    public float timeBetweenAttack;
    private float attackTime;

    public GameObject deathEffect;

    private float halfHealth;
    private Animator anim;
    private float degreeBetweenAttack;
    private Player player;

    private Slider healthBar;

    private SceneTransitions sceneTransitions;
    private float dameTakeBetweenSummon;
    private float dameTakeToSummon ;

    public void TakeDame(float damageAmount)
    {
        health -= damageAmount;
        dameTakeToSummon += damageAmount;
        healthBar.value = health;
        if (health <= 0)
        {
            Instantiate(deathEffect,transform.position,transform.rotation);
            Destroy(gameObject);
            healthBar.gameObject.SetActive(false);
            sceneTransitions.LoadWinScene("Win");
        }

        if(health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        if(dameTakeToSummon > dameTakeBetweenSummon)
        {
            float n = dameTakeToSummon / dameTakeBetweenSummon;
            for(int i =0; i < n; i++)
            {
                Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
                Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
                dameTakeToSummon -= dameTakeBetweenSummon;

            }
            
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        degreeBetweenAttack = 360 / numberOfBullet;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthBar = GameObject.FindObjectOfType<Slider>();
        healthBar.maxValue= health;
        healthBar.value= health;
        sceneTransitions = FindObjectOfType<SceneTransitions>();
        dameTakeBetweenSummon = health / 20;
        dameTakeToSummon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if(Time.time > attackTime)
            {
                anim.SetTrigger("attacking");
                attackTime = Time.time + timeBetweenAttack;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDame(damage);
        }
    }

    public void Attack()
    {
        for(int i=0;i<numberOfBullet; i++)
        {
            Instantiate(skill, transform.position, Quaternion.Euler(0, 0, i * degreeBetweenAttack));
        }
    }
}
