using UnityEngine;
using System.Collections.Generic;

// 八叉树节点类
public struct OctreeNode<T> {
    // 节点包围盒的尺寸和信息
    public Bounds m_Bounds;

    // 节点当前深度
    public int m_CurrentDepth;

    // 节点包含对象列表
    public  List<GameObject> m_ObjectList;

    // 节点的子节点
    public OctreeNode<T>[] m_ChildNodes;
}

public class Octree<T>
{
    /// <summary>
    /// 根节点
    /// </summary>
    private OctreeNode<T> m_Root;
    public OctreeNode<T> Root
    {
        get
        {
            return m_Root;
        }

        set
        {
            m_Root = value;
        }
    }

    /// <summary>
    /// 最大深度
    /// </summary>
    private int m_MaxDepth;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="center">树中心</param>
    /// <param name="size">树区域大小</param>
    /// <param name="maxDepth">树最大深度</param>
    public Octree(Vector3 center, Vector3 size, int maxDepth, List<GameObject> m_ObjectList)
    {
        // 初始化根节点
        this.m_MaxDepth = maxDepth;
        this.m_Root = new OctreeNode<T>();
        this.m_Root.m_Bounds = new Bounds(center, size);
        this.m_Root.m_CurrentDepth = 0;
        this.m_Root.m_ChildNodes = new OctreeNode<T>[8];
        this.m_Root.m_ObjectList = new List<GameObject>();
        this.m_Root.m_ObjectList = m_ObjectList;


        SplitBounds(ref this.m_Root, m_Root.m_ObjectList, this.m_Root.m_CurrentDepth);

    }

    /// <summary>
    /// 分八个区
    /// </summary>
    /// <param name="childNodes">八个子节点</param>
    /// <param name="oneBounds">当前节点的信息</param>
    private void SplitBounds(ref OctreeNode<T> currentNode, List<GameObject> objectList, int deep)
    {
        if (deep < m_MaxDepth)
        {
            currentNode.m_ObjectList = new List<GameObject>();
            // 1、判断是否相交 相交就添加到当前节点的列表里
            foreach (var gameObject in objectList)
            {
                if (gameObject.GetComponent<SphereCollider>() != null)
                {
                    var gameObjectBounds = gameObject.GetComponent<Collider>().bounds;
                    // 如果gameobject与分区相交 就添加到当前分区的列表里
                    if (currentNode.m_Bounds.Intersects(gameObjectBounds))
                    {
                        currentNode.m_ObjectList.Add(gameObject);
                    }
                }
            }

            // 2、分八个区域
            currentNode.m_ChildNodes = new OctreeNode<T>[8];
            currentNode.m_CurrentDepth = deep;

            var oneBounds = currentNode.m_Bounds;
            Vector3 half2Vector = oneBounds.size / 4;
            // 八块空间的位置
            currentNode.m_ChildNodes[0].m_Bounds.center = oneBounds.center - half2Vector;
            currentNode.m_ChildNodes[0].m_Bounds.size = oneBounds.size / 2;
            currentNode.m_ChildNodes[1].m_Bounds.center = currentNode.m_ChildNodes[0].m_Bounds.center + new Vector3(oneBounds.size.x / 2, 0, 0);
            currentNode.m_ChildNodes[1].m_Bounds.size = oneBounds.size / 2;
            currentNode.m_ChildNodes[2].m_Bounds.center = currentNode.m_ChildNodes[1].m_Bounds.center + new Vector3(0, 0, oneBounds.size.z / 2);
            currentNode.m_ChildNodes[2].m_Bounds.size = oneBounds.size / 2;
            currentNode.m_ChildNodes[3].m_Bounds.center = currentNode.m_ChildNodes[0].m_Bounds.center + new Vector3(0, 0, oneBounds.size.z / 2);
            currentNode.m_ChildNodes[3].m_Bounds.size = oneBounds.size / 2;
            currentNode.m_ChildNodes[4].m_Bounds.center = currentNode.m_ChildNodes[0].m_Bounds.center + new Vector3(0, oneBounds.size.y / 2, 0);
            currentNode.m_ChildNodes[4].m_Bounds.size = oneBounds.size / 2;
            currentNode.m_ChildNodes[5].m_Bounds.center = currentNode.m_ChildNodes[1].m_Bounds.center + new Vector3(0, oneBounds.size.y / 2, 0);
            currentNode.m_ChildNodes[5].m_Bounds.size = oneBounds.size / 2;
            currentNode.m_ChildNodes[6].m_Bounds.center = currentNode.m_ChildNodes[2].m_Bounds.center + new Vector3(0, oneBounds.size.y / 2, 0);
            currentNode.m_ChildNodes[6].m_Bounds.size = oneBounds.size / 2;
            currentNode.m_ChildNodes[7].m_Bounds.center = currentNode.m_ChildNodes[3].m_Bounds.center + new Vector3(0, oneBounds.size.y / 2, 0);
            currentNode.m_ChildNodes[7].m_Bounds.size = oneBounds.size / 2;

            // 3、遍历8个区域 递归调用
            for (int i = 0; i < currentNode.m_ChildNodes.Length; i++)
            {
                // 递归调用
                if (currentNode.m_ObjectList.Count != 0)
                {
                    // check if child node intersect
                    bool isIntersects = false;
                    currentNode.m_ChildNodes[i].m_ObjectList = new List<GameObject>();
                    foreach (var gameObject in currentNode.m_ObjectList)
                    {
                        if (gameObject.GetComponent<SphereCollider>() != null)
                        {
                            var gameObjectBounds = gameObject.GetComponent<Collider>().bounds;
                            if (currentNode.m_ChildNodes[i].m_Bounds.Intersects(gameObjectBounds))
                            {
                                currentNode.m_ChildNodes[i].m_ObjectList.Add(gameObject);
                                isIntersects = true;
                            }
                        }
                    }
                    if (isIntersects) {
                        if (currentNode.m_CurrentDepth != 0
                            && currentNode.m_ObjectList.Count == currentNode.m_ChildNodes[i].m_ObjectList.Count)
                        {
                            continue;
                        }
                        currentNode.m_CurrentDepth++;
                        SplitBounds(ref currentNode.m_ChildNodes[i], currentNode.m_ObjectList, currentNode.m_CurrentDepth);
                    }
                }
            }
        }
    }
}