using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
	
	public float walkSpeed = 2;
	public float runSpeed = 6;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	public float gravity;
	float speedSmoothVelocity;
	float currentSpeed;

	Animator animator;
	Transform cameraT;
	CharacterController controller;

	void Start()
	{
		animator = GetComponent<Animator>();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;

		//This will prevent the rotation to snap back 
		if (inputDir != Vector2.zero)
		{
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		
		bool running = Input.GetKey(KeyCode.LeftShift);
		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
		Vector3 velocity = transform.forward * currentSpeed;
		velocity.y -= gravity;

		controller.Move(velocity * Time.deltaTime);

		float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;

		//If you are looking for a simple animation add an animator component and animator controller to your
		//model. Then create a blend tree in the animator with 3 animation blends: Idle(0)-Walk(.5)-run(1)
		
		if (animator != null)
		{
			//Create a float parameter named speedPercent in the animator controller or change it to
			// say what ever you like just ensure the parameter is written the same as the string bellow.
			animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
		}

	}
}
