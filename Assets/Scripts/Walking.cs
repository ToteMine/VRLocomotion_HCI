using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {
	public float movementswitch_delay = 1.0f;
	private float lastMovementUpdate = 0f;

	public OVRPlayerController playerController;


	private movementType currMovement = movementType.CONTROLLER_SHAKING;
	movementType[] allMovements = (movementType[])Enum.GetValues(typeof(movementType));
	int currentIndex = 0;
	// Use this for initialization
	void Start () {

		print("Num Of Movements");
		print(allMovements.Length);
	}
	
	// Update is called once per frame
	void Update () {
		OVRInput.Update();
		
		//jump
		if (OVRInput.Get(OVRInput.Button.One))
        {
			playerController.Jump();
        }
		
		//wenn movement switch button gedrückt und seit letztem press n sekunden vergangen sind
        if (OVRInput.Get(OVRInput.Button.Four) & Time.timeSinceLevelLoad - this.lastMovementUpdate > movementswitch_delay)
        {
			lastMovementUpdate = Time.timeSinceLevelLoad;
			currentIndex = (currentIndex + 1) % allMovements.Length;
			currMovement = allMovements[currentIndex];
			print("changed to movement:" + currMovement);
		}

		print(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch));

		switch (currMovement)
        {
			case movementType.PRIMARY_INDEX_TRIGGER:
				primaryIndexTriggerMovement();
				break;
			case movementType.TELEPORT:
				teleport();
				break;
			case movementType.CONTROLLER_SHAKING:
				controllerShaking();
				break;
			default:
				break;
        }

    }


	private void primaryIndexTriggerMovement()
    {
		if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
		{
			this.transform.position += -transform.forward * Time.deltaTime * 1;
		}
	}

	private void teleport()
    {

    }

	public float ControllerShakingMagnitude = 1.0f;
	private float acceleration_delta = 0.5f;
	private void controllerShaking()
    {
		Vector3 velocity_left = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
		Vector3 velocity_right = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);

		print("vel left " + velocity_left + " -- " + "vel right " + velocity_right);

		print("mag left " + velocity_left.sqrMagnitude + " -- " + "mag right " + velocity_right.sqrMagnitude);

		if(velocity_left.sqrMagnitude > ControllerShakingMagnitude & velocity_right.sqrMagnitude > ControllerShakingMagnitude)
        {
			float acceleration = (velocity_left.sqrMagnitude + velocity_right.sqrMagnitude) / 2;

			this.transform.position += -transform.forward * Time.deltaTime * acceleration * acceleration_delta;
		}



	}

	enum movementType
	{
		PRIMARY_INDEX_TRIGGER=0,
		TELEPORT=1,
		CONTROLLER_SHAKING=2,
	}
}
