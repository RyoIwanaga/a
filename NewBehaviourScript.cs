using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
	public GameObject player = null;
	public GameObject playerEnemy = null;
	public GameObject playerEnemyTarget = null;
	public GameObject eye = null;

	Vector3 _pointTouchFirst;
	bool _isTargeting = false;
	bool _isRotate = false;

	float eyeMax = 0.5f;
	float eyePosX = 0f;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		_isRotate = false;

		if (Input.GetMouseButtonDown(0)) {
			_pointTouchFirst = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0)) {
		}

		if (Input.GetMouseButton(0)) {
			var _subTouchFirst = Input.mousePosition - _pointTouchFirst;

			if (_subTouchFirst.x > 3) {
				_isRotate = true;

				var max = Screen.width / 4f;
				var move = Mathf.Min (_subTouchFirst.x, max);
				player.transform.Rotate (new Vector3(0, 
						move / max * 90 * Time.deltaTime, 0));

				eyePosX = Mathf.Min(eyeMax, eyePosX + Time.deltaTime * 0.1f);
				eye.transform.position = player.transform.TransformPoint(new Vector3(eyePosX, 0, 6));
			}
			else if (_subTouchFirst.x < -3) {
				_isRotate = true;

				var max = Screen.width / 4f;
				var move = Mathf.Min (- _subTouchFirst.x, max);
				player.transform.Rotate (new Vector3(0, 
						move / max * -90 * Time.deltaTime, 0));

				eyePosX = Mathf.Max(- eyeMax, eyePosX - Time.deltaTime * 0.1f);
				eye.transform.position = player.transform.TransformPoint(new Vector3(eyePosX, 0, 6));
			}

			if (_subTouchFirst.y > 3) {
				var max = Screen.width / 4f;
				var move = Mathf.Min (_subTouchFirst.y, max);
				player.transform.position += player.transform.forward * move / max * 6 * Time.deltaTime;
			}
			else if (_subTouchFirst.y < -3) {
				player.transform.position += player.transform.forward * -2 * Time.deltaTime;
			}
		}

		if (! _isRotate) {
			if (eyePosX > 0) {
				eyePosX = Mathf.Max(0, eyePosX - Time.deltaTime * 0.1f);
				eye.transform.position = player.transform.TransformPoint(new Vector3(eyePosX, 0, 6));
			}
			else if (eyePosX < 0) {
				eyePosX = Mathf.Min(0, eyePosX + Time.deltaTime * 0.1f);
				eye.transform.position = player.transform.TransformPoint(new Vector3(eyePosX, 0, 6));
			}
		}
	}

	void OnGUI ()
	{
		if (GUI.Button(new Rect(0, 0, 100, 100), "target")) {
			_isTargeting = true;
			playerEnemyTarget.SetActive(true);
		}

		if (GUI.Button(new Rect(100, 0, 100, 100), "target")) {
			_isTargeting = false;
			playerEnemyTarget.SetActive(false);
		}
	}
}
