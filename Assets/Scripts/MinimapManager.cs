using UnityEngine;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{
    public RectTransform minimapContainer;  // The container RectTransform
    public Sprite playerIndicatorSprite;
    public float gridCellSize = 20f;       // Size of each grid cell
    public Vector2Int gridSize;            // Total grid size (columns, rows)
    public float maxWidth = 200f;          // Maximum minimap width
    public float maxHeight = 200f;         // Maximum minimap height

    private Vector2 gridOffset;
    private GameObject playerIndicator;    // Player indicator instance

    public void InitializeMinimap()
    {
        // Calculate grid bounds
        float gridWidth = gridSize.x * gridCellSize;
        float gridHeight = gridSize.y * gridCellSize;

        // Calculate scale to fit minimap within the container
        float scaleX = maxWidth / gridWidth;
        float scaleY = maxHeight / gridHeight;
        float scale = Mathf.Min(scaleX, scaleY, 1f);

        // Apply scaling
        minimapContainer.localScale = new Vector3(scale, scale, 1f);

        // Center the grid in the container
        gridOffset = new Vector2(gridWidth / 2f, gridHeight / 2f);

        CreatePlayerIndicator();
    }

    public void AddMapSprite(Vector2 gridPosition, Sprite mapSprite)
    {
        // Convert grid position to local position within the minimap
        Vector2 localPosition = new(
            (gridPosition.x * gridCellSize) - gridOffset.x,
            (gridPosition.y * gridCellSize) - gridOffset.y
        );

        // Instantiate the map sprite
        GameObject minimapImage = new("MinimapImage");
        Image image = minimapImage.AddComponent<Image>();
        image.sprite = mapSprite;
        GameObject sprite = Instantiate(minimapImage, minimapContainer);
        sprite.transform.SetSiblingIndex(0);
        RectTransform spriteRect = sprite.GetComponent<RectTransform>();
        spriteRect.anchoredPosition = localPosition;
        spriteRect.sizeDelta = Vector2.one * gridCellSize;  // Match grid cell size
    }

    private void CreatePlayerIndicator()
    {
        // Create the player indicator object
        playerIndicator = new GameObject("PlayerIndicator");
        Image image = playerIndicator.AddComponent<Image>();
        image.sprite = playerIndicatorSprite;

        // Set the player indicator as a child of the minimap container
        playerIndicator.transform.SetParent(minimapContainer);

        // Configure RectTransform properties
        RectTransform playerRect = playerIndicator.GetComponent<RectTransform>();
        playerRect.pivot = new Vector2(0.5f, 0.5f);
        playerRect.sizeDelta = Vector2.one * gridCellSize; // Match grid cell size
    }

    public void UpdatePlayerIndicator(Vector2 gridPosition)
    {
        // Convert grid position to local position within the minimap
        Vector2 localPosition = new(
            (gridPosition.x * gridCellSize) - gridOffset.x,
            (gridPosition.y * gridCellSize) - gridOffset.y
        );

        // Update the player indicator position
        RectTransform playerRect = playerIndicator.GetComponent<RectTransform>();
        playerRect.anchoredPosition = localPosition;
    }
}