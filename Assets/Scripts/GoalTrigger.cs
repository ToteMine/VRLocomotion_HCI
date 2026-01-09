using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GoalTrigger : MonoBehaviour
{ 
    public AudioSource goalSound;

    private float startTime;
    private bool finished = false;

    public varDisplay display;

    void Start()
    {
        startTime = Time.time;
    }

    void OnTriggerEnter(Collider other)
    {
        if (finished) return;


        if (other.CompareTag("Player"))
        {
            finished = true;

            float endTime = Time.timeSinceLevelLoad;

            if (display != null)
            {
                display.finisherText = "Ziel erreicht!\nZeit: " 
                                + endTime.ToString("F2") + " Sekunden";
            }

            if (goalSound != null)
            {
                goalSound.Play();
            }

            Debug.Log("Ziel erreicht in " + endTime + " Sekunden");
        }
    }
}
