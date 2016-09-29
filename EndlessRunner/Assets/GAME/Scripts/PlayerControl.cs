using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float moveSpeed; // the main movement speed
	private float moveSpeedTemp; // temp movement speed to call after jumping 

	private float jumpForceX; // the force of the jump between calculations
	private float jumpForceY;

	public float jumpForceXMax;
	public float jumpForceYMax;

	private bool inAir;

	public float speedMultiplier; // how much the speed will increase by every interval

	public float speedIncreasePoint; // the interval distance
	private float speedPoint; // the accumulated distance traveled

	private Vector3 clickStart; // the mouse position of the inital click
	private Vector3 clickFinish; // last mouse position click
	private float clickAngle;
	private Vector3 linePos;
	private Vector3 mouseDistance;

	public bool grounded; // is the player on the ground
	public LayerMask whatIsGround; // the layer of ground
	public Transform groundCheck; // checking if the ground is there
	public float groundCheckRad; // the distance between the character and the ground 

	private Rigidbody2D myRigidBody;  
	private Animator myAnimator;
	public GameManager theGameManager;
	private ScoreManager theScoreManager;
	public LineRenderer theLineRenderer;
	private CameraControl theCamera;

	private float moveSpeedStore; // reset variables
	private float speedPointStore;
	private float speedIncreaseStore;
	private Vector3 mouseDistanceStore;
	 
	public AudioSource jumpSound; // audio sources
	public AudioSource deathSound;

	void Start () { // initialise everything
		myRigidBody = GetComponent<Rigidbody2D> (); 
		myAnimator = GetComponent<Animator> ();
		theScoreManager = FindObjectOfType<ScoreManager> ();
		speedPoint = speedIncreasePoint;
		moveSpeedStore = moveSpeed;
		speedPointStore = speedPoint;
		speedIncreaseStore = speedIncreasePoint;
		moveSpeedTemp = moveSpeed;
		theCamera = FindObjectOfType<CameraControl> ();
		mouseDistanceStore = mouseDistance;
		theLineRenderer.sortingOrder = 2;
	}

	void Update () {
		Jump ();
	}

	void Jump(){

		grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRad,whatIsGround); // Grounded is the ground layer checked with a radius connected to the player

		if (transform.position.x > speedPoint) { // The speed multiplier over time.
			speedPoint += speedIncreasePoint;
			speedIncreasePoint = speedIncreasePoint * speedMultiplier;
			moveSpeed = moveSpeed * speedMultiplier;
			moveSpeedTemp = moveSpeed;
		}

		if (grounded) {
			myRigidBody.velocity = new Vector2 (moveSpeed, myRigidBody.velocity.y); // updating the running speed
		}
			
		if ((Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) && grounded) { // this is the jumping
			theScoreManager.scoreIncreasing = false;
			clickStart = Input.mousePosition;
			moveSpeed = 0;
			myRigidBody.velocity = new Vector2 (moveSpeed, myRigidBody.velocity.y);// stop the character
			inAir = false;
		}

		if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0)) && grounded && !inAir) { // end the jump
			
			clickFinish = Input.mousePosition;

			jumpForceX = clickStart.x - clickFinish.x ; 
			jumpForceY = clickStart.y - clickFinish.y ;

			clickAngle = CalculateAngle (clickStart, clickFinish);

			if(Input.GetAxis("Mouse X")<0){
				mouseDistance.x++;
			}
			if(Input.GetAxis("Mouse X")>0){
				mouseDistance.x--;
			}
			if(Input.GetAxis("Mouse Y")<0){
				mouseDistance.y++;
			}
			if(Input.GetAxis("Mouse Y")>0){
				mouseDistance.y--;
			}
				
			theLineRenderer.SetPosition (0,myRigidBody.transform.position);
			theLineRenderer.SetPosition (1,myRigidBody.transform.position - mouseDistance/20);
		
			if (jumpForceX > jumpForceXMax) { // if the force is too much slow them down
				jumpForceX = jumpForceXMax;
			}
			if (jumpForceY > jumpForceYMax) {
				jumpForceY = jumpForceYMax;
			}
		}
		if ((Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0)) && grounded && !inAir) {// end the jump
			if(clickAngle >= 90 && clickAngle <= 180){
				myRigidBody.velocity = new Vector2 (jumpForceX, jumpForceY); // jump based on the force
				jumpSound.Play (); // play the sound 
			}
			moveSpeed = moveSpeedTemp; // go back to normal speed
			theScoreManager.scoreIncreasing = true;
			inAir = true;
			mouseDistance = mouseDistanceStore;

		}
		myAnimator.SetFloat ("Speed", moveSpeed); // animations
		myAnimator.SetBool ("Grounded", grounded);
	}

	void OnCollisionEnter2D (Collision2D other){ // Death and reset everything to starting variables, most done in game manager
		if (other.gameObject.tag == "KillBox") {
			deathSound.Play ();
			theGameManager.RestartGame();
			moveSpeed = moveSpeedStore;
			speedPoint = speedPointStore;
			speedIncreasePoint = speedPointStore;
			moveSpeedTemp = moveSpeedStore;
		}
	}
	public static float CalculateAngle(Vector3 from, Vector3 to) { // Courtesy of https://gist.github.com/shiwano/0f236469cd2ce2f4f585

		return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;

	}
}