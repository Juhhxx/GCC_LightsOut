using System.Collections.Generic;
using UnityEngine;

public class LayoutGroup3D : MonoBehaviour
{
    [SerializeField] private bool _horizontal;
    [SerializeField] private float _spacing;
    private List<GameObject> _children;

    private void Start()
    {
        _children = new List<GameObject>();
    }
    public void AddChildren(GameObject child)
    {
        child.transform.SetParent(transform);
        _children.Add(child);
        AlignChildren();
    }
    public void RemoveChildren(GameObject child)
    {
        child.transform.SetParent(null);
        _children.Remove(child);
        AlignChildren();
    }
    private void AlignChildren()
    {
        if (transform.childCount > 0)
        {
            Vector3 offsetVector = transform.position;

            foreach (GameObject child in _children)
            {
                if (child.transform.GetSiblingIndex() > 0)
                {
                    child.transform.position = offsetVector;

                    Vector3 offsetValue = Vector3.zero;
                    
                    Vector3 childSize = GetChildSize(child);
                    
                    if (_horizontal)
                        offsetValue.x = childSize.x + _spacing;
                    else
                        offsetValue.y = childSize.y + _spacing;

                    offsetVector += offsetValue;
                }

                child.transform.position = offsetVector;

                Debug.Log($"Child {child.transform.GetSiblingIndex()} offseted by {offsetVector}");
            }
        }
    }
    private Vector2 GetChildSize(GameObject child)
    {
        Renderer childRender = child.GetComponentInChildren<Renderer>();

        return childRender.bounds.size;
    }
}
