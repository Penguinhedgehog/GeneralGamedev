using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropthroughPlatformLogic : MonoBehaviour
{
    private GameObject currentOneWayPlatform;

    [SerializeField] private CapsuleCollider2D playerColliderRemove;
    [SerializeField] private CircleCollider2D playerCollider;
    [SerializeField] float dropDownTime = .5f;

    Animator myAnimator;
    PlayerMovement player;
    bool isCrouching;


    private void Start() {
        myAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
    }


    private void OnPlayerDropDown() {
        isCrouching = !isCrouching;
        myAnimator.SetBool("isCrouching", isCrouching);
        playerColliderRemove.enabled = !isCrouching;
        player.SetHasControl(!isCrouching);
        if (currentOneWayPlatform != null ) {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DropDownPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DropDownPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(dropDownTime);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }


}
