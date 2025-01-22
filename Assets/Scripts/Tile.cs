using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isTopLeftAllowed;
    public bool isTopRightAllowed;
    public bool isBottomLeftAllowed;
    public bool isBottomRightAllowed;

    public bool isTopEdgeAllowed;
    public bool isBottomEdgeAllowed;
    public bool isLeftEdgeAllowed;
    public bool isRightEdgeAllowed;

    public Tile[] upNeighbors;
    public Tile[] rightNeighbors;
    public Tile[] downNeighbors;
    public Tile[] leftNeighbors;
}
