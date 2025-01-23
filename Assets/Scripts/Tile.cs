using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Map Properties")]
    public Sprite mapSprite;

    [Header("Direction Properties")]

    public bool goesTop;
    public bool goesRight;
    public bool goesBottom;
    public bool goesLeft;

    [Header("Edge Cases")]
    public bool isTopLeftAllowed;
    public bool isTopRightAllowed;
    public bool isBottomLeftAllowed;
    public bool isBottomRightAllowed;

    public bool isTopEdgeAllowed;
    public bool isBottomEdgeAllowed;
    public bool isLeftEdgeAllowed;
    public bool isRightEdgeAllowed;

    [Header("Neighbors")]
    public Tile[] upNeighbors;
    public Tile[] rightNeighbors;
    public Tile[] downNeighbors;
    public Tile[] leftNeighbors;

}
