using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Vector2 startInputPosition;
    private Vector2 endInputPosition;
    private Vector2 direction;

    LineDraw lineDraw;

    private Rigidbody2D rb2D;
    public PhysicsMaterial2D PhysicsMaterial2D;

    public float forceMultiplier = 10f;
    private bool isOnGround;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lineDraw = GetComponent<LineDraw>();
    }

    void Update()
    {
        // 터치 입력 처리
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startInputPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    endInputPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endInputPosition = touch.position;
                    direction = (startInputPosition - endInputPosition) / Screen.dpi;
                    rb2D.velocity = direction * forceMultiplier;
                    break;
            }
        }

        // 마우스 입력 처리
        if (Input.GetMouseButtonDown(0))
        {
            startInputPosition = Input.mousePosition;
            lineDraw.DrawLine();
        }

        if (Input.GetMouseButton(0))
        {
            endInputPosition = Input.mousePosition;
            lineDraw.FreeDraw();
        }

        if (Input.GetMouseButtonUp(0))
        {
            endInputPosition = Input.mousePosition;
            direction = (startInputPosition - endInputPosition) / Screen.dpi;
            rb2D.velocity = direction * forceMultiplier;
            lineDraw.ClearLine();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            rb2D.sharedMaterial = null;
            isOnGround = true;
        }

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
