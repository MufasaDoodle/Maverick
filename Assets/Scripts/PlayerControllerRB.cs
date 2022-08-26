using FishNet;
using FishNet.Object;
using FishNet.Object.Prediction;
using UnityEngine;

public class PlayerControllerRB : NetworkBehaviour
{

	private Vector3 playerMovementInput;
	private Vector2 playerMouseInput;
	private float xRot;

	[SerializeField] private Rigidbody rb;
	[SerializeField] private Transform playerCamera;
	[Space]
	[SerializeField] private float speed;
	[SerializeField] private float sensitivity;
	[SerializeField] private float jumpForce;


	public override void OnStartNetwork()
	{
		base.OnStartNetwork();

		if (base.Owner.IsLocalClient)
		{
			var cam = playerCamera.gameObject.AddComponent<Camera>();
			cam.tag = "MainCamera";
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!IsOwner) return; //if we are not the local player, get out

		playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

		MovePlayer();
		MovePlayerCamera();
	}

	private void MovePlayer()
	{
		Vector3 moveVector = transform.TransformDirection(playerMovementInput) * speed;
		rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

		if (Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	private void MovePlayerCamera()
	{
		xRot -= playerMouseInput.y * sensitivity;

		transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
		playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
	}
}
