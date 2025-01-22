using UnityEngine;

public class Room : MonoBehaviour
{
    public bool hasNorthDoor;
    public bool hasSouthDoor;
    public bool hasEastDoor;
    public bool hasWestDoor;

    public GameObject northDoorPrefab;
    public GameObject southDoorPrefab;
    public GameObject eastDoorPrefab;
    public GameObject westDoorPrefab;

    public void InitializeRoom(bool north, bool south, bool east, bool west)
    {
        hasNorthDoor = north;
        hasSouthDoor = south;
        hasEastDoor = east;
        hasWestDoor = west;

        // Instantiate doors based on connections
        if (hasNorthDoor) Instantiate(northDoorPrefab, new Vector3(0.06f, 5.893325f, 0), Quaternion.identity, transform);
        if (hasSouthDoor) Instantiate(southDoorPrefab, new Vector3(-0.04f, -6.25f, 0), Quaternion.identity, transform);
        if (hasEastDoor) Instantiate(eastDoorPrefab,  new Vector3(-9.125602f, -0.0290052f, 0), Quaternion.identity, transform);
        if (hasWestDoor) Instantiate(westDoorPrefab, new Vector3(-9.062102f, -0.158455f, 0), Quaternion.identity, transform);
    }

    void Start()
    {
        InitializeRoom(true, true, false, false);
    }

}
