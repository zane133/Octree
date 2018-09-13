using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 球和球碰撞
public class TestColider : MonoBehaviour {
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GameObject[] gameObjects =  FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.transform.name != transform.transform.name && gameObject.transform.tag!= "Octree")
            {
                var colider = gameObject.GetComponent<Collider>();
                if (colider)
                {
                    //Vector3 pos1 = transform.GetComponent<Collider>().bounds.center;
                    //float radius1 = transform.GetComponent<Collider>().bounds.size.x / 2;
                    //Vector3 pos2 = gameObject.GetComponent<Collider>().bounds.center;
                    //float radius2 = gameObject.GetComponent<Collider>().bounds.size.x / 2;

                    //float posDistance = (pos1 - pos2).magnitude;

                    ////Debug.Log(radius1 + " " + radius2);

                    //if (posDistance <= radius1 + radius2)
                    //{
                    //    Debug.Log("bang");
                    //}
                    //else
                    //{

                    //}

                    // 这个方法很好 ^_^
                    if (transform.GetComponent<Collider>().bounds.Intersects(gameObject.GetComponent<Collider>().bounds))
                    {
                        Debug.Log("bang");
                    }
                }
            }
        }
    }
}
