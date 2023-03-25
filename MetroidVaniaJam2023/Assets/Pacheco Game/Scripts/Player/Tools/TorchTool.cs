using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTool : MonoBehaviour
{
    [SerializeField] float torchSpeed = 10f;
    [SerializeField] float torchAngle = 10f;
    PlayerMovement player;
    Rigidbody2D torchRB;
    BoxCollider2D boxCollider2D;
    float direction;


    private void Start() {
        player = FindObjectOfType<PlayerMovement>();
        torchRB = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        direction = player.transform.localScale.x * torchSpeed;

        torchRB.velocity = new Vector2(direction, torchAngle);
        //StartCoroutine(Curve());
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "DreadEvent") {
            //Enter trigger dread event logic here
        }
        if (other.tag == "Ground") {
            Destroy(gameObject);
        }
    }


/*
    private IEnumerator Curve() {
        Vector2 velocity = torchRB.velocity;

        for(int i = 0; i < 180; i++) {
            transform.Rotate(0,0,1);
            torchRB.velocity = Quaternion.Euler(0, 0, i) * velocity;
            yield return new WaitForSeconds(0.01f);
        }

        
    }*/


}
