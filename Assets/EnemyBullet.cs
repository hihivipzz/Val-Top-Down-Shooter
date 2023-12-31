using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Player playerScript;
    private Vector2 targetPosition;
    public float speed;
    public float damage;
    // Start is called before the first frame update
    public GameObject blood;
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards(transform.position,targetPosition,speed*Time.deltaTime);
        }
        else
        {
            Instantiate(blood,transform.position,transform.rotation);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDame(damage);
            Instantiate(blood, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
