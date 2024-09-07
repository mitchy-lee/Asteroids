using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    private float rotation;
    public float acc;
    private float maxVel = 100;
    private float moving;
    private Vector2 oldPos;
    private SpriteRenderer fireRender;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()){
            if (sr.gameObject.name == "fire") {
                fireRender = sr;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        fireRender.enabled = moving > 0f;
    }

    void FixedUpdate() {
        transform.Rotate(0, 0, -5 * rotation, Space.Self);
        
        rb.AddForce(transform.up * moving * acc, ForceMode2D.Force);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVel);

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

        //Invoke("OnTriggerEnter2D", 3);
    }

    void OnMove(InputValue value) {
        //Debug.Log("moving " + value);
        moving = value.Get<float>();
    }

    void OnRotate(InputValue value) {
        //Debug.Log("rotating: " + value.Get<float>());
        rotation = value.Get<float>();
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Asteroid")) {
            transform.position = Vector2.zero;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            StartCoroutine(RespawnAfterDelay(3f)); // Start the coroutine with a 3-second delay
        }
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Respawn the player at the origin
        transform.position = Vector2.zero;
    }
}
