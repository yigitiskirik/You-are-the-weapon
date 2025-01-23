using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isMapReady = false;
    public int mapSize;
    public Vector2Int startPoint;
    public Vector2Int endPoint;
    public List<Cell> mapGrid;

    public GameObject northDoorPrefab;
    public GameObject southDoorPrefab;
    public GameObject eastDoorPrefab;
    public GameObject westDoorPrefab;
    public GameObject playerObject;

    public Vector2Int playerPosition; // Remove public

    enum Orientation {
        Top,
        Bottom,
        Left,
        Right
    }

    public IEnumerator WaitForReady()
    {
        // Wait until isReady becomes true
        yield return new WaitUntil(() => isMapReady);

        // Continue with the rest of the logic when isReady is true
        Debug.Log("isMapReady is true! Continuing...");

        playerPosition = startPoint;
        GoToCoordinates(playerPosition);
    }

    void GoToCoordinates(Vector2Int coordinates)
    {
        Tile selectedTile = ReturnCellDetails(coordinates);
        ToggleDoors(selectedTile);
    }

    Tile ReturnCellDetails(Vector2Int cellCoordinates)
    {
        int index = cellCoordinates.y * mapSize + cellCoordinates.x;
        return mapGrid[index].tileOptions[0];
    }

    void ToggleDoors(Tile selectedTile)
    {
        northDoorPrefab.SetActive(selectedTile.goesTop);
        southDoorPrefab.SetActive(selectedTile.goesBottom);
        eastDoorPrefab.SetActive(selectedTile.goesRight);
        westDoorPrefab.SetActive(selectedTile.goesLeft);
    }

    void Start()
    {
        StartCoroutine(WaitForReady());
    }

    void MoveToLocation(Orientation orientation)
    {
        GoToCoordinates(playerPosition);
        switch (orientation)
        {
            case Orientation.Left:
                playerObject.transform.position = new Vector2(-7.5f, 0);
                break;
            case Orientation.Right:
                playerObject.transform.position = new Vector2(7.8f, 0);
                break;
            case Orientation.Top:
                playerObject.transform.position = new Vector2(4.9f, 0);
                break;
            case Orientation.Bottom:
                playerObject.transform.position = new Vector2(-5f, 0);
                break;
        }
    }

    public void EnterBottomDoor()
    {
        playerPosition += Vector2Int.down;
        MoveToLocation(Orientation.Top);
    }

    public void EnterTopDoor()
    {
        playerPosition += Vector2Int.up;
        MoveToLocation(Orientation.Bottom);
    }

    public void EnterRightDoor()
    {
        playerPosition += Vector2Int.right;
        MoveToLocation(Orientation.Left);
    }

    public void EnterLeftDoor()
    {
        playerPosition += Vector2Int.left;
        MoveToLocation(Orientation.Right);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
