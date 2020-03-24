using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet b = collision.gameObject.GetComponent<Bullet>();
        if (b)
        {
            health -= b.damage;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
