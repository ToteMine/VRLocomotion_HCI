using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour {
	public float movementswitch_delay = 1.0f;
	
	
	private float lastMovementUpdate = 0f;

	public OVRPlayerController playerController;
	CharacterController cc;

	public OVRCameraRig cameraRig;

	public  MovementType currMovement = MovementType.SUPERMAN;


	public float stickDeadzone = 0.6f;
	public float stickSwitchDelay = 0.5f;
	private float lastStickSwitch = 0f;
	private bool stickWasCentered = true;
	private bool jumpPressedLastFrame = false;



	MovementType[] allMovements = (MovementType[])Enum.GetValues(typeof(MovementType));
	int currentIndex = 0;
	// Use this for initialization
	void Start () {
    	cc = playerController.GetComponent<CharacterController>();
		print("Num Of Movements");
		print(allMovements.Length);
	}
	
	// Update is called once per frame
	void Update () {
		OVRInput.Update();
		
		//jump
		bool jumpPressedNow = OVRInput.Get(OVRInput.Button.One);

		if (jumpPressedNow && !jumpPressedLastFrame)
		{
			Debug.Log("JUMP BUTTON DOWN SIMULATED");
			playerController.Jump();
		}
		jumpPressedLastFrame = jumpPressedNow;

		joystickMovementSelector();
		
		//wenn movement switch button gedrückt und seit letztem press n sekunden vergangen sind
        if (OVRInput.Get(OVRInput.Button.Four) && Time.timeSinceLevelLoad - this.lastMovementUpdate > movementswitch_delay)
        {
			lastMovementUpdate = Time.timeSinceLevelLoad;
			currentIndex = (currentIndex + 1) % allMovements.Length;
			currMovement = allMovements[currentIndex];
			print("changed to movement:" + currMovement);
		}


		switch (currMovement)
        {
			case MovementType.PRIMARY_INDEX_TRIGGER:
				primaryIndexTriggerMovement();
				break;
			case MovementType.TELEPORT:
				teleport();
				break;
			case MovementType.CONTROLLER_SHAKING:
				controllerShaking();
				break;
			case MovementType.SUPERMAN:
				superman();
				break;
			default:
				break;
        }

    }

	private readonly float minYlevelSM = -0.1f;
	private readonly float maxYlevelSM = 0.1f;
	private void superman()
    {

		Vector3 hmdpos = cameraRig.centerEyeAnchor.transform.position;
		Vector3 leftWorldPos = cameraRig.leftHandAnchor.transform.position;
		Vector3 rightWorldPos = cameraRig.rightHandAnchor.transform.position;
		print(hmdpos + " - " + leftWorldPos + " - " + rightWorldPos);



		Vector3 leftPosLocal = hmdpos - leftWorldPos;
		Vector3 rightPosLocal = hmdpos - rightWorldPos;

		if (maxYlevelSM > leftPosLocal.y && leftPosLocal.y > minYlevelSM && maxYlevelSM > rightPosLocal.y && rightPosLocal.y > minYlevelSM)
        {
			this.transform.position += -transform.forward * Time.deltaTime * 1;
		}

		//print("left " + leftPosLocal + " -- " + "right " + rightPosLocal);
	}

	private void primaryIndexTriggerMovement()
	{
		float leftGrip  = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
		float rightGrip = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);

		float grip = Mathf.Max(leftGrip, rightGrip);

		if (grip > 0.1f)
		{
			this.transform.position += -transform.forward * grip * Time.deltaTime * 2f;
		}
	}

	private float lastGrip = 0f;

	private void teleport()
	{
		float grip = Mathf.Max(
			OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger),
			OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)
		);

		if (grip > 0.8f && lastGrip <= 0.8f)
		{
			this.transform.position += -transform.forward * 1.0f;
		}

		lastGrip = grip;
	}

	public float ControllerShakingMagnitude = 1.0f;
	private float acceleration_delta = 0.5f;
	private void controllerShaking()
    {
		Vector3 velocity_left = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
		Vector3 velocity_right = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);

		//print("vel left " + velocity_left + " -- " + "vel right " + velocity_right);

		//print("mag left " + velocity_left.sqrMagnitude + " -- " + "mag right " + velocity_right.sqrMagnitude);

		if(velocity_left.sqrMagnitude > ControllerShakingMagnitude || velocity_right.sqrMagnitude > ControllerShakingMagnitude)
        {
			float acceleration = (velocity_left.sqrMagnitude + velocity_right.sqrMagnitude) / 2;

			this.transform.position += -transform.forward * Time.deltaTime * acceleration * acceleration_delta;
		}



	}

	private void joystickMovementSelector()
	{
		Vector2 stick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
		Debug.Log("Thumbstick: " + stick);

		if (stick.magnitude < stickDeadzone)
		{
			stickWasCentered = true;
			return;
		}

		if (!stickWasCentered)
			return;

		stickWasCentered = false;

		if (Mathf.Abs(stick.y) > Mathf.Abs(stick.x))
			currMovement = stick.y > 0 ? MovementType.PRIMARY_INDEX_TRIGGER
									: MovementType.CONTROLLER_SHAKING;
		else
			currMovement = stick.x > 0 ? MovementType.TELEPORT
									: MovementType.SUPERMAN;

		Debug.Log("Joystick switched to: " + currMovement);
	}

}


public enum MovementType
{
	PRIMARY_INDEX_TRIGGER = 0,
	TELEPORT = 1,
	CONTROLLER_SHAKING = 2,
	SUPERMAN = 3,
}