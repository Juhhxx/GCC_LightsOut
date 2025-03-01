using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Incase I need to implement new pathfinding methods we make a an abstract pathfinder
/// </summary>
public abstract class Pathfinder
{
    private readonly HexagonTabletop _tabletop;
    public Pathfinder(HexagonTabletop tabletop)
    {
        _tabletop = tabletop;
    }

    public abstract Stack<HexagonCell> FindPath(HexagonCell start, HexagonCell objective);
}
