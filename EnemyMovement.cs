using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D enemyRig;
    BoxCollider2D enemyFlipCollider;
    void Start()
    {
        enemyRig = GetComponent<Rigidbody2D>();
        enemyFlipCollider = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        enemyRig.velocity = new Vector2(moveSpeed, 0f);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipFacingEnemy();
    }
    void FlipFacingEnemy()
    {
        // bool enemyFlipper = Mathf.Abs(enemyRig.velocity.x) > Mathf.Epsilon;
        /*if (enemyFlipper)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(enemyRig.velocity.x)), 1f);
        }*/
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRig.velocity.x)), 1f);

    }

}
