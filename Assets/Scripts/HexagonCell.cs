using System;
using UnityEngine;

public class HexagonCell : MonoBehaviour
{
    public Vector2Int CellValue { get; private set; }
    private HexagonTabletop _tabletop;
    private void Start()
    {
        _tabletop = GetComponentInParent<HexagonTabletop>();

        CellValue = new Vector2Int(
            Convert.ToInt32(transform.localPosition.x / _tabletop.GetComponent<Grid>().cellSize.x),
            Convert.ToInt32(transform.localPosition.y / _tabletop.GetComponent<Grid>().cellSize.y));

        _tabletop.CreateCell(CellValue, this);
    }

    public void HoverCell()
    {

    }

    public void SelectCell()
    {
        
    }
}
