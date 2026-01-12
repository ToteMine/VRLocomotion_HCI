using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalTrigger : MonoBehaviour
{
    public AudioSource goalSound;
    public GameObject[] fireworks; // Zylinder-Objekte

    private float startTime;
    private bool finished = false;

    public GameObject startCube;

    public varDisplay display;

    void Start()
    {
        startTime = Time.time;
    }

    public void startTimer()
    {
        startTime = Time.time;
        finished = false;
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

            foreach (GameObject fw in fireworks)
            {
                if (fw != null)
                {
                    ParticleSystem[] psArray = fw.GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem ps in psArray)
                    {
                        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // sicherstellen, dass wir neu starten
                        ps.Play(); // jetzt starten
                    }
                }
            }

            Debug.Log("Ziel erreicht in " + endTime + " Sekunden");
        }
    }
}
