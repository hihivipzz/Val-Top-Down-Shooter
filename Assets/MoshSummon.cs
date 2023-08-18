using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoshSummon : MonoBehaviour
{

    public float explosionTime;
    public float damage;
    private bool isTrigger=false;
    Player player;

    private IEnumerator DestroyWithDelay()
    {
        // Chờ một khoảng thời gian trước khi hủy đối tượng
        yield return new WaitForSeconds(explosionTime);
        if (isTrigger) {
            
            player.TakeDame(damage);
        }

        // Hủy đối tượng
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            isTrigger= true;
        }
        else
        {
            isTrigger= false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            isTrigger = false;
        }
    }
}
