using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{
    public float speed = 4.5f;
    private Rigidbody2D _rb;
    public LayerMask groundLayer;
    public float platformTime;
    // Update is called once per frame
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Throwing()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
        platformTime = 6;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private void Timer()
    {
        if(platformTime > 0)
        {
            platformTime -= Time.deltaTime;
        }else
        {
            Destroy(gameObject);
        }
    }
}
