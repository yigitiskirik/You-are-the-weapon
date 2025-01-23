using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool collapsed;
    public Tile[] tileOptions;
    public bool visited;

    public void CreateCell(bool collapseState, Tile[] tiles)
    {
        collapsed = collapseState;
        tileOptions = tiles;
        visited = false;
    }

    public void RecreateCell(Tile[] tiles)
    {
        tileOptions = tiles;
    }

    public void VisitCell()
    {
        visited = true;
    }
}
