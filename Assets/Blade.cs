using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public int damage;
    private Animator ani;
    
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            collision.GetComponent<Enemy>().takeDame(damage);
        }else if(collision.tag == "Boss" && ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            collision.transform.parent.GetComponent<Boss>().TakeDame(damage);
        }
    }
}
