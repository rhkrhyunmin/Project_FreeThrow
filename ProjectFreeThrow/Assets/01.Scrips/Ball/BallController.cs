using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector2 startInputPosition;
    private Vector2 endInputPosition;
    private Vector2 direction;

    private Rigidbody2D rb2D;
    public PhysicsMaterial2D PhysicsMaterial2D;

    public float forceMultiplier = 10f;

    private bool isOnGround;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ��ġ �Է� ó��
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startInputPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endInputPosition = touch.position;
                    direction = (endInputPosition - startInputPosition) / Screen.dpi;
                    rb2D.velocity = direction * forceMultiplier;
                    break;
            }
        }

        // ���콺 �Է� ó��
        if (Input.GetMouseButtonDown(0))
        {
            startInputPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endInputPosition = Input.mousePosition;
            direction = (endInputPosition - startInputPosition) / Screen.dpi;
            rb2D.velocity = direction * forceMultiplier;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            rb2D.sharedMaterial = null;
            isOnGround = true;
        }

        //�� �տ��ٰ� triger������ ���� 
        if (collision.collider.CompareTag("bounce"))
        {
            rb2D.sharedMaterial = PhysicsMaterial2D;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
