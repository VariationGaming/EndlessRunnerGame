using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public PlayerControl thePlayer;

	public new Camera camera;
	public Vector3 cameraPos;

	private Vector3 lastPlayerPosition;
	private float distanceToMove;


	void Start () {
		thePlayer = FindObjectOfType<PlayerControl> ();
		lastPlayerPosition = thePlayer.transform.position;
		camera = GetComponent<Camera> ();
	}

	void Update () {
		cameraPos = camera.ScreenToWorldPoint (Input.mousePosition);
		distanceToMove = thePlayer.transform.position.x - lastPlayerPosition.x;
		transform.position = new Vector3 (transform.position.x + distanceToMove, transform.position.y,transform.position.z);
		lastPlayerPosition = thePlayer.transform.position;
	}
}
