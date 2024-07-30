using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject drawingPrefab;
    public Material material;
    private Color randomColor;

    public void DrawLine()
    {
        // 기존 라인 초기화
        ClearLine();

        GameObject drawing = Instantiate(drawingPrefab);
        lineRenderer = drawing.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        Randomize();
        lineRenderer.startColor = randomColor;
        lineRenderer.endColor = randomColor;

        // 마우스 위치로 초기 위치 설정
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, GameManager.Instance.BallController.transform.position);
    }

    void Randomize()
    {
        int randomInt = Random.Range(1, 5);

        switch (randomInt)
        {
            case 4:
                material.SetFloat("width", 5);
                material.SetFloat("heigth", 2);
                randomColor = Color.yellow;
                break;
            case 3:
                material.SetFloat("width", 5);
                material.SetFloat("heigth", 2);
                randomColor = Color.cyan;
                break;
            case 2:
                material.SetFloat("width", 5);
                material.SetFloat("heigth", 2);
                randomColor = Color.green;
                break;
            case 1:
                material.SetFloat("width", 5);
                material.SetFloat("heigth", 2);
                randomColor = Color.red;
                break;
        }
    }

    public void FreeDraw()
    {
        if (lineRenderer == null)
        {
            DrawLine();
        }

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, worldPos);
    }

    public void ClearLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
            Destroy(lineRenderer.gameObject);
            lineRenderer = null;
        }
    }
}
