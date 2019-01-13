using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rect
{
    public Rect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        this.P1 = p1;
        this.P2 = p2;
        this.P3 = p3;
        this.P4 = p4;
    }
    public Rect(Vector2 center, Vector2 max)
    {
        this.Center = center;
        this.SizeMax = max;
    }
    public Vector2 P1;
    public Vector2 P2;
    public Vector2 P3;
    public Vector2 P4;
    public Vector2 Center;
    public Vector2 SizeMax;

}

public class Circle
{
    public Circle(Vector2 center, float radius)
    {
        this.Center = center;
        this.Radius = radius;
    }
    public Vector2 Center;
    public float Radius;
}

public class TestColider2 : MonoBehaviour
{
    public BoxCollider Cube;
    public SphereCollider Sphere;
    private bool IsIntersect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (cube.bounds.Intersects(sphere.bounds))
        //{
        //    print("相交");
        //}

        var center = Cube.bounds.center;
        var extends = Cube.bounds.extents;
        //var p0 = new Vector3(center.x - extends.x, center.y - extends.y, center.z - extends.z);
        //var p1 = new Vector3(center.x - extends.x, center.y + extends.y, center.z - extends.z);
        //var p2 = new Vector3(center.x + extends.x, center.y + extends.y, center.z - extends.z);
        //var p3 = new Vector3(center.x + extends.x, center.y - extends.y, center.z - extends.z);
        //var p4 = new Vector3(center.x - extends.x, center.y + extends.y, center.z + extends.z);
        //var p5 = new Vector3(center.x + extends.x, center.y + extends.y, center.z + extends.z);
        //var p6 = new Vector3(center.x + extends.x, center.y - extends.y, center.z + extends.z);
        //var p7 = new Vector3(center.x - extends.x, center.y - extends.y, center.z + extends.z);

        //var toP1 = new Vector2(p0.z, p0.y);
        //var toP2 = new Vector2(p1.z, p1.y);
        //var toP3 = new Vector2(p4.z, p4.y);
        //var toP4 = new Vector2(p7.z, p7.y);

        var centerVec2 = new Vector2(center.x, center.z);
        var maxVec2 = new Vector2(Cube.bounds.max.x, Cube.bounds.max.z);
        Rect rect1 = new Rect(centerVec2, maxVec2);

        //toP1 = new Vector2(p0.z, p0.x);
        //toP2 = new Vector2(p7.z, p7.x);
        //toP3 = new Vector2(p6.z, p6.x);
        //toP4 = new Vector2(p3.z, p3.x);

        centerVec2 = new Vector2(center.z, center.y);
        maxVec2 = new Vector2(Cube.bounds.max.z, Cube.bounds.max.y);
        Rect rect2 = new Rect(centerVec2, maxVec2);


        //toP1 = new Vector2(p0.x, p0.y);
        //toP2 = new Vector2(p1.x, p1.y);
        //toP3 = new Vector2(p2.x, p2.y);
        //toP4 = new Vector2(p3.x, p3.y);

        centerVec2 = new Vector2(center.x, center.y);
        maxVec2 = new Vector2(Cube.bounds.max.x, Cube.bounds.max.y);
        Rect rect3 = new Rect(centerVec2, maxVec2);


        Circle circle1 = new Circle(new Vector2(Sphere.transform.position.x, Sphere.transform.position.z), Sphere.radius);
        Circle circle2 = new Circle(new Vector2(Sphere.transform.position.z, Sphere.transform.position.y), Sphere.radius);
        Circle circle3 = new Circle(new Vector2(Sphere.transform.position.x, Sphere.transform.position.y), Sphere.radius);

        IsIntersect = IntersectRectAndCircle(rect1, circle1) && 
                      IntersectRectAndCircle(rect2, circle2) &&
                      IntersectRectAndCircle(rect3, circle3);
        //if (IsIntersect)
        //{
        //    print("相交");

        //}
    }

    // 判断圆与矩形相交
    private bool IntersectRectAndCircle(Rect rect, Circle circle)
    {
        // 四个中点
        //Vector2 c1 = new Vector2(0.5f * (rect.P1.x + rect.P2.x), 0.5f * (rect.P1.y + rect.P2.y));
        //Vector2 c2 = new Vector2(0.5f * (rect.P2.x + rect.P3.x), 0.5f * (rect.P2.y + rect.P3.y));
        //Vector2 c3 = new Vector2(0.5f * (rect.P3.x + rect.P4.x), 0.5f * (rect.P3.y + rect.P4.y));
        //Vector2 c4 = new Vector2(0.5f * (rect.P4.x + rect.P1.x), 0.5f * (rect.P4.y + rect.P1.y));

        var rectCenter = new Vector2(Mathf.Abs(rect.Center.x), Mathf.Abs(rect.Center.y));
        var circleCenter = new Vector2(Mathf.Abs(circle.Center.x), Mathf.Abs(circle.Center.y));


        var v = circleCenter - rectCenter;
        var h = rect.SizeMax - rectCenter;
        var u = v - h;

        u = new Vector2(u.x < 0 ? 0 : u.x, u.y < 0 ? 0 : u.y);

        print(rectCenter + " " + circleCenter + " " + rect.SizeMax + " " + u);

        if (u.sqrMagnitude <= circle.Radius * circle.Radius)
        {
            return true;
        }
        return false;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.background = null;                 //这是设置背景填充的
        style.normal.textColor = new Color(1, 0, 0);    //设置字体颜色的
        style.fontSize = 40;                            //当然，这是字体颜色

        if (IsIntersect)
        {
            GUI.Label(new UnityEngine.Rect(10, 10, 200, 20), "True", style);
        }
        else
        {
            GUI.Label(new UnityEngine.Rect(10, 10, 200, 20), "False", style);
        }
    }




}
