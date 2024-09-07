using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 randomDirection;
    private Vector2 oldPos;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        float angle = Random.Range(0f, 360f);
        randomDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
        rb.velocity = randomDirection * speed / 2;
        
        oldPos = transform.position;
        if (transform.position.x > 10) {
            transform.position = new Vector2(-10, oldPos.y);
        }
        if (transform.position.x < -10) {
            transform.position = new Vector2(10, oldPos.y);
        }
        if (transform.position.y > 5) {
            transform.position = new Vector2(oldPos.x, -5);
        }
        if (transform.position.y < -5) {
            transform.position = new Vector2(oldPos.x, 5);
        }
    }
}
