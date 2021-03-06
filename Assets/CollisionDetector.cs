﻿using UnityEngine;
using System.Collections;

public class CollisionDetector : MonoBehaviour
{
		public float MovingForce;
		Vector3 StartPoint;
		Vector3 Origin;
		public int NoOfRays = 10;
		int i;
		RaycastHit HitInfo;
		float LengthOfRay, DistanceBetweenRays, DirectionFactor;
		float margin = 0.015f;
		Ray ray;
	
		void Start ()
		{
				//Length of the Ray is distance from center to edge
				LengthOfRay = GetComponent<Collider>().bounds.extents.y;	
				//Initialize DirectionFactor for upward direction
				DirectionFactor = Mathf.Sign (Vector3.up.y);	
				
		}

		void Update ()
		{
				// First ray origin point for this frame
				StartPoint = new Vector3 (GetComponent<Collider>().bounds.min.x + margin, transform.position.y, transform.position.z); 
				if (!IsCollidingVertically ()) {						
						transform.Translate (Vector3.up * MovingForce * Time.deltaTime * DirectionFactor);
				}
		}

		bool IsCollidingVertically ()
		{
				Origin = StartPoint;		
				DistanceBetweenRays = (GetComponent<Collider>().bounds.size.x - 2 * margin) / (NoOfRays - 1); 
				for (i = 0; i<NoOfRays; i++) {
						// Ray to be casted.
						ray = new Ray (Origin, Vector3.up * DirectionFactor);
						//Draw ray in scene view to see visually. Remember visual length is not actual length
						Debug.DrawRay (Origin, Vector3.up * DirectionFactor, Color.yellow);
						if (Physics.Raycast (ray, out HitInfo, LengthOfRay)) {
								print ("Collided With " + HitInfo.collider.gameObject.name);	
								// Negate the Directionfactor to reverse the moving direction of colliding cube(here cube2)
								DirectionFactor = -DirectionFactor;			
								return true;
						}
						Origin += new Vector3 (DistanceBetweenRays, 0, 0);
				}
				return false;

		}

		
}

