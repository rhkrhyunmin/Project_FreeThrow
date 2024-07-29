using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public void GoalCheck()
    {

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.GameScore++;
            Debug.Log(GameManager.Instance.GameScore);
        }
    }
}
