using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float lifeTime = 10f;

    Vector2 lastPosition;


    private void Start() {
        Destroy(gameObject, lifeTime);
    }

    

    private void FixedUpdate() {
        lastPosition = gameObject.transform.position;
        gameObject.transform.position = lastPosition + new Vector2(speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Ground") {
            Destroy(gameObject);
        }
    }



}
