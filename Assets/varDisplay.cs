using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class varDisplay : MonoBehaviour
{

    public Walking walking;

    public TMP_Text textComponent;

    void Start()
    {
        textComponent.fontSize = 20;
        textComponent.outlineWidth = 0.3f;
        textComponent.outlineColor = Color.black;

        UpdateText();
    }

    void Update()
    {
        // myVariable hier updaten, z.B. von Controller-Velocity
        // myVariable = velocity.magnitude;
        UpdateText();
    }

    void UpdateText()
    {

        textComponent.text = "Gewähltes Movement: " + walking.currMovement.ToString();  // F1 = 1 Dezimal
    }
}
