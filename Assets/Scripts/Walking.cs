using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {
	public float movementswitch_delay = 1.0f;
	private float lastMovementUpdate = 0f;

	private readonly float Acceleration = 1;


	private movementType currMovement = movementType.PRIMARY_INDEX_TRIGGER;
	movementType[] allMovements = (movementType[])Enum.GetValues(typeof(movementType));
	int currentIndex = 0;
	// Use this for initialization
	void Start () {

		print("Num Of Movements");
		print(allMovements.Length);
	}
	
	// Update is called once per frame
	void Update () {

		//wenn movement switch button gedrückt und seit letztem press n sekunden vergangen sind
        if (OVRInput.Get(OVRInput.Button.Four) & Time.timeSinceLevelLoad - this.lastMovementUpdate > movementswitch_delay)
        {
			lastMovementUpdate = Time.timeSinceLevelLoad;
			currentIndex = (currentIndex + 1) % allMovements.Length;
			currMovement = allMovements[currentIndex];
			print("changed to movement:" + currMovement);
		}

        switch (currMovement)
        {
			case movementType.PRIMARY_INDEX_TRIGGER:
				primaryIndexTriggerMovement();
				break;
			case movementType.TELEPORT:
				teleport();
				break;
			case movementType.HEAD_BOBBING:
				headBobbing();
				break;
			default:
				break;
        }

    }


	private void primaryIndexTriggerMovement()
    {
		if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
		{
			this.transform.position += -transform.forward * Time.deltaTime * Acceleration;
		}
	}

	private void teleport()
    {

    }

	private void headBobbing()
    {

    }

	enum movementType
	{
		PRIMARY_INDEX_TRIGGER=0,
		TELEPORT=1,
		HEAD_BOBBING=2,
	}
}
