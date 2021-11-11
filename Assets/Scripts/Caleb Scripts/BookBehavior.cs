using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehavior : MonoBehaviour
{
    public float speed = 4.5f;
    public float platformTime;
    public float maxDistance;
    private Vector3 startingPosition;
    private bool thrown = false;
    private bool falling = false;

    void Start() {
        IEnumerator coroutine = Timer();
        StartCoroutine(coroutine);
        startingPosition = transform.position;
    }

    void Update()
    {
        Throwing();
    }
   
    private void Throwing()
    {
        if (Mathf.Abs(transform.position.x - startingPosition.x) <= maxDistance) {
            transform.position += transform.right * Time.deltaTime * speed;
            if (!thrown) {
                thrown = true;
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            }
        }
        else {
            if (!falling) {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        // platformTime = 4;
    }


    private IEnumerator Timer() {
        yield return new WaitForSeconds(platformTime);
        Fall();
    }

    public void Fall() {
        falling = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 8) {
            Destroy(gameObject);
        }
    }
}
