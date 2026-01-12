using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class varDisplay : MonoBehaviour
{
    public Image[] panels;
    public Color activeColor = Color.white;
    public Color inactiveColor = new Color(0.5f, 0.5f, 0.5f, 1f);

    public Walking walking;
    private MovementType lastMovement;


    public TMP_Text textComponent;

    public string finisherText = "";

    void Start()
    {
        textComponent.fontSize = 20;
        textComponent.outlineWidth = 0.3f;
        textComponent.outlineColor = Color.black;

        lastMovement = walking.currMovement;

        UpdateText();
        UpdatePanels();
    }

    void Update()
    {
        if (walking.currMovement != lastMovement)
        {
            lastMovement = walking.currMovement;
            UpdatePanels();
        }
        // myVariable hier updaten, z.B. von Controller-Velocity
        // myVariable = velocity.magnitude;
        UpdateText();
    }

    void UpdateText()
    {

        if(finisherText != "")
        {
            print("FINISHERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR");
            textComponent.text = finisherText;
            return;
        }

        print("NNOFINISHER");

        //textComponent.text = "Gewaehltes Movement: " + walking.currMovement.ToString();  // F1 = 1 Dezimal
    }

    void UpdatePanels()
    {
        if (panels == null || panels.Length == 0)
            return;

        for (int i = 0; i < panels.Length; i++)
        {
            if ((MovementType)i == walking.currMovement)
                panels[i].color = activeColor;
            else
                panels[i].color = inactiveColor;
        }
    }
}
