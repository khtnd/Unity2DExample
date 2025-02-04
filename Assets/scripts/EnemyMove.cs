using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int nextMove;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Think();
    }

    private void FixedUpdate() {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVector = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVector, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVector, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null) {
            Turn();
        }
    }

    void Think() {
        //nextMove = Random.Range(-1, 2);
        nextMove = -1;
        NextInvoke();
    }

    void Turn() {
        CancelInvoke();
        nextMove = -nextMove;
        NextInvoke();
    }

    void NextInvoke() {
        animator.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0) {
            spriteRenderer.flipX = nextMove == 1;
        }

        Invoke("Think", 2f);
    }
}
