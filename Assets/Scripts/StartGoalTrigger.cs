using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGoalTrigger : MonoBehaviour
{
    public GoalTrigger goalTrigger;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goalTrigger.startTimer();
            Debug.Log("Startzeit gesetzt!");
        }
    }
}
