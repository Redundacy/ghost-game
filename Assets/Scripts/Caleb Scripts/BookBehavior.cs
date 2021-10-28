using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{
    public float speed = 4.5f;
    public float platformTime;

    void Update()
    {
        Throwing();
        Timer();
    }
   
    private void Throwing()
    {
        transform.position += transform.right * Time.deltaTime * speed;
        platformTime = 4;
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
