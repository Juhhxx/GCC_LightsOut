using System.Collections.Generic;
using UnityEngine;

public class LayoutGroup3D : MonoBehaviour
{
    [SerializeField] private bool _horizontal;
    [SerializeField] private bool _centerObjects;
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
            // Get the position Vector of the parent
            Vector3 offsetVector = transform.position;

            foreach (GameObject child in _children)
            {
                if (child.transform.GetSiblingIndex() > 0)
                {
                    // Define offsetValue variable as zero
                    Vector3 offsetValue = Vector3.zero;
                    
                    // Get the current child objects size
                    Vector3 childSize = GetChildSize(child);
                    
                    // Check if we wznt horizontal or vertical alignment and offset the right value
                    if (_horizontal)
                        offsetValue.x = childSize.x + _spacing;
                    else
                        offsetValue.y = childSize.y + _spacing;

                    // Add the offsetValue to the offsetVector
                    offsetVector += offsetValue;
                }

                // Apply the offsetVector to the childs position
                child.transform.position = offsetVector;

                Debug.Log($"Child {child.transform.GetSiblingIndex()} offseted by {offsetVector}");
            }

            // Align the children to the middle

            if (_centerObjects)
            {
                foreach (GameObject child in _children)
                {
                    if (transform.childCount > 1)
                    {
                        // Divide the final offsetVector by 2 (get half the length)
                        Vector3 alignedVector = offsetVector / 2;
                        
                        // Move the child object by the alignedVector value
                        child.transform.position -= alignedVector;
                    }
                }
            }
        }
    }
    private Vector2 GetChildSize(GameObject child)
    {
        Renderer childRender = child.GetComponentInChildren<Renderer>();

        return childRender.bounds.size;
    }
}
