using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCube : MonoBehaviour
{
    public GameObject Prefab;
    public int Scale = 20;

    public Material Blue;
    public Material Yellow;
    public Material Green;

    private List<GameObject> m_CubeList;

    // Start is called before the first frame update
    void Start()
    {
        m_CubeList = new List<GameObject>();
    }

    private void BuildTerrian()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                Vector3 newPosition = transform.position + transform.right * i;
                newPosition = newPosition + transform.forward * j;
                var height = Mathf.PerlinNoise(i / (float)Scale, j / (float)Scale);
                height *= 20f;
                height = Mathf.RoundToInt(height);
                Debug.Log(height);

                newPosition = newPosition + transform.up * height;

                var newCube = Instantiate(Prefab, newPosition, transform.rotation);
                m_CubeList.Add(newCube);

                if (height <= 5)
                {
                    newCube.GetComponent<Renderer>().material = Yellow;
                }
                else if (5 < height && height < 8)
                {
                    newCube.GetComponent<Renderer>().material = Blue;
                }
                else if (height >= 8)
                {
                    newCube.GetComponent<Renderer>().material = Green;
                }
            }
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new UnityEngine.Rect(10, 20, 100, 40), "Build"))
        {
            foreach (var item in m_CubeList)
            {
                Destroy(item);
            }
            BuildTerrian();
        }
    }
}
