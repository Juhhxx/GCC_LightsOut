using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletopMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _cellLayer;
    private HexagonCell _hoveredCell;
    [SerializeField] private HexagonCell _currentCell;
    private HexagonCell _selectedCell;
    private Pathfinder _pathfinder;
    private HexagonTabletop _tabletop;


    private bool _moving;

    private void Start()
    {
        if ( _currentCell == null )
            _currentCell = FindFirstObjectByType<HexagonCell>();
        transform.position = new Vector3(_currentCell.transform.position.x, transform.position.y, _currentCell.transform.position.z);

        _tabletop = FindFirstObjectByType<HexagonTabletop>();
        _pathfinder = new AStarPathfinder(_tabletop);
    }
    private void Update()
    {
        CheckForHover();
        CheckForSelection();
    }

    private void CheckForHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if ( Physics.Raycast(ray, out RaycastHit hit, _cellLayer)
            && hit.transform.TryGetComponent<HexagonCell>(out var newCell))
        {
            if ( _hoveredCell != newCell && newCell != _currentCell )
            {
                _hoveredCell?.StopHoverCell();
                _hoveredCell = newCell;
                _hoveredCell.HoverCell();
            }
        }
        else if ( _hoveredCell != null )
        {
            _hoveredCell.StopHoverCell();
            _hoveredCell = null;
        }
    }

    private void CheckForSelection()
    {
        if (_moving) return;
        // Chose up, so if the player hovers and buttons down the wrong button,
        // they can still navigate to another button so select it

        if (_hoveredCell != null && Input.GetButtonUp("Select"))
        {
            _selectedCell = _hoveredCell;
            StartCoroutine(Move());
        }

    }

    private IEnumerator Move()
    {
        Debug.Log("Starting movement from " + _currentCell + " to " + _selectedCell );
        _moving = true;

        Stack<HexagonCell> path = _pathfinder.FindPath(_currentCell, _selectedCell);

        if ( path == null )
        {
            _moving = false;
            Debug.Log("Can't move there. ");
            yield break;
        }

        HexagonCell next;
        do
        {
            yield return new WaitForSeconds(0.2f);

            next = path.Pop();
            _currentCell = next;

            transform.position = new Vector3(next.transform.position.x, transform.position.y, next.transform.position.z);

            if ( path.Count > 0 )
            {
                next = path.Peek();

                Vector3 target = next.transform.position;
                target.y = transform.position.y;

                transform.LookAt(target);
            }
        }
        while ( _currentCell != _selectedCell );

        _selectedCell = null;

        _moving = false;
    }

    private void OnDisable()
    {
        _moving = false;        
    }
}
