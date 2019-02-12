using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour 
{
	public Camera cam;
	public Rigidbody2D rb2d;

	private float maxWidth;
	private bool canControl;

	void Start () 
	{
		canControl = false;
		rb2d = GetComponent<Rigidbody2D>();
		if (cam == null) 
		{
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = GetComponentInChildren<Renderer> ().bounds.extents.x;
		maxWidth = targetWidth.x - hatWidth;
	}
	

	void FixedUpdate () 
	{
		if (canControl) 
		{
			Vector3 rawPosition = cam.ScreenToWorldPoint (Input.mousePosition);
			Vector3 targetPosition = new Vector3 (rawPosition.x, 0f, 0f);
			float targetWidth = Mathf.Clamp (targetPosition.x, -maxWidth, maxWidth);
			targetPosition = new Vector3 (targetWidth, 0f, 0f);
			rb2d.MovePosition (targetPosition);
		}
	}

	public void ToggleControl(bool toggle)
	{
		canControl = toggle;
	}

}
