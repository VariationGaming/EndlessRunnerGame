using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class FindNewAngle : MonoBehaviour {

	static public Vector2 myStartPoint;
	static public Vector2 myFinishPoint;


	void Start () {
		myStartPoint = new Vector2 (0,0);
		myFinishPoint = new Vector2 (1, 1);
	}

	void Update () {
		if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0))) { // this is the jumping
			myFinishPoint = Input.mousePosition;
			Debug.Log(Mathf.Atan2(myFinishPoint.y,myFinishPoint.x)-Mathf.Atan2(myStartPoint.y,myStartPoint.x));
			Debug.DrawLine (myStartPoint,myFinishPoint);
		}
	}
}