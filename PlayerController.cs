using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	static Vector3 SPHERE_CENTER = new Vector3(0f, 0f, 0f);
	const float IGNORE = 5f;

	GameObject camera;
	Rigidbody 	rBody;
	float 		speed = 10f;
	float 		gravity = -9.8f;
	Vector3 	posFirstTouch;
	Vector3 	vectorMove = Vector3.zero;

	void Start () 
	{
		rBody = GetComponent<Rigidbody>();
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(0)) {
			posFirstTouch = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0)) {
			vectorMove = Vector3.zero;
		}

		if (Input.GetMouseButton(0)) {
			var width = Screen.width / 4f;
			var vector = Input.mousePosition - posFirstTouch;
			var fraction_x = Reu.Position1ToFraction2(vector.x, IGNORE, width, - IGNORE, - width);
			var fraction_y = Reu.Position1ToFraction2(vector.y, IGNORE, width, - IGNORE, - width);

			vectorMove = new Vector3(fraction_x, 0, fraction_y);
		}
	}

	void FixedUpdate ()
	{
		/*** Gravity ***/

		var vector_up = (transform.position - SPHERE_CENTER).normalized;
		var vector_local_up = transform.up;

		rBody.AddForce(vector_up * gravity);
		rBody.rotation = Quaternion.FromToRotation(vector_local_up, vector_up) * transform.rotation;

		/*** Move ***/

		var vector_local_move = transform.TransformDirection(vectorMove) * speed * Time.fixedDeltaTime;
		rBody.MovePosition(transform.position + vector_local_move);

		/*** Change direction ***/

		if (vectorMove != Vector3.zero)
			transform.eulerAngles = vector_local_move;
	}
}
