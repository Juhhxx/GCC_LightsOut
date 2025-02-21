using System.Collections.Generic;
using UnityEngine;

public class HexagonTabletop : MonoBehaviour
{
    private Dictionary<Vector2Int, HexagonCell> _grid;

    private void Awake()
    {
        _grid = new Dictionary<Vector2Int, HexagonCell>();
    }

    public void CreateCell(Vector2Int vec, HexagonCell cell)
    {
        _grid[vec] = cell;
    }

    public HexagonCell GetCell(Vector2Int vec)
    {
        return _grid[vec];
    }
}
