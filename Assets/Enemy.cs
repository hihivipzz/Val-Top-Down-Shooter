using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float timeBetweenAttack;
    public float damage;

    public int pickupChange;
    public GameObject[] pickups;

    public GameObject deathEffect;

    [HideInInspector]
    public Transform player;

    // Start is called before the first frame update

    public void takeDame(int damageAmount) {
        health -= damageAmount;

        if(health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if(randomNumber < pickupChange)
            {
                GameObject randomPickup = pickups[Random.Range(0,pickups.Length)];
                Instantiate(randomPickup, transform.position,transform.rotation);
            }
            Instantiate(deathEffect,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
