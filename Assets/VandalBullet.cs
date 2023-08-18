using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class VandalBullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    public GameObject explosion;

    public GameObject soundObject;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifeTime);
        Instantiate(soundObject,transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right* speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().takeDame(damage);
            Destroy(gameObject);
        }

        else if (collision.tag == "Boss")
        {
            collision.transform.parent.GetComponent<Boss>().TakeDame(damage);
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
