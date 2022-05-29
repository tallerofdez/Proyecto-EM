using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script para las fisicas y cuerpo de la bala
public class bulletScript : MonoBehaviour
{

    public float speed = 10f;
    public Rigidbody2D rb;
    LayerMask _layer;

    private void Awake()
    {
        
        // Configure LayerMask
        _layer = LayerMask.GetMask("Obstacles");

        //configure rigidbody
        rb = GetComponent<Rigidbody2D>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bala");
        rb.velocity = transform.right * speed;
       // this.transform.rotation = Quaternion.identity;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.gameObject.Equals(this))
        {

        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
