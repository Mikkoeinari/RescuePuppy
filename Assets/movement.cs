using UnityEngine;
using System.Collections;


public class movement : MonoBehaviour
		{
			public float moveSpeed = 10f;
			public float turnSpeed = 10f;
			
			
			void Update ()
			{
				if(Input.GetKey(KeyCode.UpArrow))
					transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
				
				if(Input.GetKey(KeyCode.DownArrow))
					transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
				
				if(Input.GetKey(KeyCode.LeftArrow))
					transform.Translate(Vector3.left* moveSpeed * Time.deltaTime);
				
				if(Input.GetKey(KeyCode.RightArrow))
					transform.Translate(Vector3.right* moveSpeed * Time.deltaTime);
			}

	}

