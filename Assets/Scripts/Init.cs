using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

    public GameObject root;
    Octree<GameObject> octree;
    // Use this for initialization
    void Start () {
        GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        List<GameObject> listGameObject = new List<GameObject>(gameObjects);

        Bounds rootBound = root.GetComponent<BoxCollider>().bounds;
        octree = new Octree<GameObject>(rootBound.center, rootBound.size, 10, listGameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 画八叉树方框
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (octree != null)
        {
            DrawCube(octree.Root.m_ChildNodes);
        }
    } 

    private void DrawCube(OctreeNode<GameObject>[] octreeNode)
    {
        for (int i = 0; i < octreeNode.Length; i++)
        {
            if (octreeNode[i].m_ChildNodes != null)
            {
                DrawCube(octreeNode[i].m_ChildNodes);
            }
            octreeNode[i].m_Bounds.DrawBounds(new Color(0,0.7f,0));
        }
    }

#endif


}
