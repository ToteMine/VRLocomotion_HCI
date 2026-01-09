using UnityEngine;
using UnityEngine.UI;

public class GoalTrigger : MonoBehaviour
{
    public Text timerText;
    public AudioSource goalSound;

    private float startTime;
    private bool finished = false;

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

            float endTime = Time.time - startTime;

            if (timerText != null)
            {
                timerText.text = "Ziel erreicht!\nZeit: " 
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
