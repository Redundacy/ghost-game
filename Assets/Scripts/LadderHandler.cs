using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHandler : MonoBehaviour {
    public float LadderSize = 3;
    public bool MovingLadder = false;

    public float LadderBoundsLeft = -10,
        LadderBoundsRight = 10;

    private BoxCollider2D _boxCollider2D;

    void Awake() {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, Mathf.Clamp(LadderSize, 2, LadderSize+1));
        transform.GetChild(0).localPosition = new Vector3(0, (LadderSize - 1) / 2);
        transform.GetChild(1).GetComponent<SpriteRenderer>().size =
            new Vector2(transform.GetChild(1).GetComponent<SpriteRenderer>().size.x, LadderSize - 2);
        transform.GetChild(2).localPosition = new Vector3(0, -(LadderSize - 1) / 2);
    }

    void OnValidate() {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxCollider2D.size = new Vector2(_boxCollider2D.size.x, Mathf.Clamp(LadderSize, 2, LadderSize + 1));
        transform.GetChild(0).localPosition = new Vector3(0, (LadderSize - 1) / 2);
        transform.GetChild(1).GetComponent<SpriteRenderer>().size =
            new Vector2(transform.GetChild(1).GetComponent<SpriteRenderer>().size.x, LadderSize - 2);
        transform.GetChild(2).localPosition = new Vector3(0, -(LadderSize - 1) / 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
