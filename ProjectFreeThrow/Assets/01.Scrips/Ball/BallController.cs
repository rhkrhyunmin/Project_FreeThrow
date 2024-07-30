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
    private LineRenderer lineRenderer;

    public float forceMultiplier = 10f;
    private bool isOnGround;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
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
                    lineRenderer.enabled = true;
                    break;

                case TouchPhase.Moved:
                    endInputPosition = touch.position;
                    UpdateLineRenderer();
                    break;

                case TouchPhase.Ended:
                    endInputPosition = touch.position;
                    direction = (endInputPosition - startInputPosition) / Screen.dpi;
                    rb2D.velocity = direction * forceMultiplier;
                    lineRenderer.enabled = false;
                    break;
            }
        }

        // 마우스 입력 처리
        if (Input.GetMouseButtonDown(0))
        {
            startInputPosition = Input.mousePosition;
            lineRenderer.enabled = true;
        }

        if (Input.GetMouseButton(0))
        {
            endInputPosition = Input.mousePosition;
            UpdateLineRenderer();
        }

        if (Input.GetMouseButtonUp(0))
        {
            endInputPosition = Input.mousePosition;
            direction = (endInputPosition - startInputPosition) / Screen.dpi;
            rb2D.velocity = direction * forceMultiplier;
            lineRenderer.enabled = false;
        }
    }

    void UpdateLineRenderer()
    {
        Vector3 ballPosition = transform.position;
        Vector2 inputDirection = (endInputPosition - startInputPosition) / Screen.dpi;
        Vector3 targetPosition = ballPosition + new Vector3(inputDirection.x, inputDirection.y, 0) * forceMultiplier;

        lineRenderer.SetPosition(0, ballPosition);
        lineRenderer.SetPosition(1, targetPosition);
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
