using UnityEngine;

public class TabletopMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _cellLayer;
    private HexagonCell _hoveredCell;
    [SerializeField] private HexagonCell _currentCell;

    // Update is called once per frame
    private void Update()
    {
        CheckForHover();
        CheckForSelection();
    }

    private void CheckForHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit ;

        if (Physics.Raycast(ray, out hit, _cellLayer)
            && hit.transform.TryGetComponent<HexagonCell>(out var newCell))
            _hoveredCell = newCell;
        else
            _hoveredCell = null;
    }

    private void CheckForSelection()
    {
        // Chose up, so if the player hovers and buttons down the wrong button,
        // they can still navigate to another button so select it

        if (_hoveredCell != null && Input.GetButtonUp("Select"))
        {
            _currentCell = _hoveredCell;
            Move();
        }

    }

    private void Move()
    {
        // do movement based on that path finding shit i saw
    }
}
