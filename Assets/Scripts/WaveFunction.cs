using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaveFunction : MonoBehaviour
{
    public int dimensions;
    public Tile[] tileObjects;
    public List<Cell> gridComponents;
    public Cell cellObj;

    int iterations = 0;

    bool IsTopLeft(int x, int y) => x == 0 && y == dimensions - 1;
    bool IsTopRight(int x, int y) => x == dimensions - 1 && y == dimensions - 1;
    bool IsBottomLeft(int x, int y) => x == 0 && y == 0;
    bool IsBottomRight(int x, int y) => x == dimensions - 1 && y == 0;

    bool IsTopEdge(int x, int y) => y == dimensions - 1 && x > 0 && x < dimensions - 1;
    bool IsBottomEdge(int x, int y) => y == 0 && x > 0 && x < dimensions - 1;
    bool IsLeftEdge(int x, int y) => x == 0 && y > 0 && y < dimensions - 1;
    bool IsRightEdge(int x, int y) => x == dimensions - 1 && y > 0 && y < dimensions - 1;

    void Awake()
    {
        gridComponents = new List<Cell>();
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                Cell newCell = Instantiate(cellObj, new Vector2(x, y), Quaternion.identity);
                List<Tile> validTiles = new(tileObjects);

                if (IsTopLeft(x, y))
                {
                    validTiles = validTiles.Where(tile => tile.isTopLeftAllowed).ToList();
                }
                else if (IsTopRight(x, y))
                {
                    validTiles = validTiles.Where(tile => tile.isTopRightAllowed).ToList();
                }
                else if (IsBottomLeft(x, y))
                {
                    validTiles = validTiles.Where(tile => tile.isBottomLeftAllowed).ToList();
                }
                else if (IsBottomRight(x, y))
                {
                    validTiles = validTiles.Where(tile => tile.isBottomRightAllowed).ToList();
                }
                else if (IsTopEdge(x, y))
                {
                    validTiles = validTiles.Where(tile => tile.isTopEdgeAllowed).ToList();
                }
                else if (IsBottomEdge(x, y))
                {
                    validTiles = validTiles.Where(tile => tile.isBottomEdgeAllowed).ToList();
                }
                else if (IsLeftEdge(x, y))
                {
                    validTiles = validTiles.Where(tile => tile.isLeftEdgeAllowed).ToList();
                }
                else if (IsRightEdge(x, y))
                {
                    validTiles = validTiles.Where(tile => tile.isRightEdgeAllowed).ToList();
                }
                newCell.CreateCell(false, validTiles.ToArray());
                gridComponents.Add(newCell);
            }
        }

        StartCoroutine(CheckEntropy());
    }

    IEnumerator CheckEntropy()
    {
        List<Cell> tempGrid = new(gridComponents);

        tempGrid.RemoveAll(c => c.collapsed);
        tempGrid.Sort((a, b) => { return a.tileOptions.Length - b.tileOptions.Length; });

        int arrLength = tempGrid[0].tileOptions.Length;
        int stopIndex = default;

        for (int i = 1; i < tempGrid.Count; i++)
        {
            if (tempGrid[i].tileOptions.Length > arrLength)
            {
                stopIndex = i;
                break;
            }
        }

        if (stopIndex > 0)
        {
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
        }

        yield return new WaitForSeconds(0.01f);

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<Cell> tempGrid)
    {
        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);
        Cell cellToCollapse = tempGrid[randIndex];

        cellToCollapse.collapsed = true;
        int randCollapseIndex = UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length);
        Tile selectedTile = cellToCollapse.tileOptions[randCollapseIndex];
        cellToCollapse.tileOptions = new Tile[] { selectedTile };

        Tile foundTile = cellToCollapse.tileOptions[0];
        Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity);

        UpdateGeneration();
    }

    void UpdateGeneration()
    {
        List<Cell> newGenerationCell = new(gridComponents);

        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                var index = x + y * dimensions;
                if (gridComponents[index].collapsed)
                {
                    newGenerationCell[index] = gridComponents[index];
                }
                else
                {
                    List<Tile> options = new(tileObjects);

                    if (IsTopLeft(x, y))
                    {
                        options = options.Where(tile => tile.isTopLeftAllowed).ToList();
                    }
                    else if (IsTopRight(x, y))
                    {
                        options = options.Where(tile => tile.isTopRightAllowed).ToList();
                    }
                    else if (IsBottomLeft(x, y))
                    {
                        options = options.Where(tile => tile.isBottomLeftAllowed).ToList();
                    }
                    else if (IsBottomRight(x, y))
                    {
                        options = options.Where(tile => tile.isBottomRightAllowed).ToList();
                    }
                    else if (IsTopEdge(x, y))
                    {
                        options = options.Where(tile => tile.isTopEdgeAllowed).ToList();
                    }
                    else if (IsBottomEdge(x, y))
                    {
                        options = options.Where(tile => tile.isBottomEdgeAllowed).ToList();
                    }
                    else if (IsLeftEdge(x, y))
                    {
                        options = options.Where(tile => tile.isLeftEdgeAllowed).ToList();
                    }
                    else if (IsRightEdge(x, y))
                    {
                        options = options.Where(tile => tile.isRightEdgeAllowed).ToList();
                    }

                    if (y > 0)
                    {
                        Cell up = gridComponents[x + (y - 1) * dimensions];
                        List<Tile> validOptions = new();

                        foreach (Tile possibleOptions in up.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].upNeighbors;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    if (x < dimensions - 1)
                    {
                        Cell right = gridComponents[x + 1 + y * dimensions].GetComponent<Cell>();
                        List<Tile> validOptions = new();

                        foreach (Tile possibleOptions in right.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].leftNeighbors;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    if (y < dimensions - 1)
                    {
                        Cell down = gridComponents[x + (y + 1) * dimensions];
                        List<Tile> validOptions = new();

                        foreach (Tile possibleOptions in down.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].downNeighbors;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    if (x > 0)
                    {
                        Cell left = gridComponents[x - 1 + y * dimensions].GetComponent<Cell>();
                        List<Tile> validOptions = new();

                        foreach (Tile possibleOptions in left.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].rightNeighbors;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    Tile[] newTileList = new Tile[options.Count];

                    for (int i = 0; i < options.Count; i++)
                    {
                        newTileList[i] = options[i];
                    }

                    newGenerationCell[index].RecreateCell(newTileList);
                }
            }
        }

        gridComponents = newGenerationCell;
        iterations++;

        if (iterations < dimensions * dimensions)
        {
            StartCoroutine(CheckEntropy());
        }
    }

    void CheckValidity(List<Tile> optionList, List<Tile> validOption)
    {
        for (int newX = optionList.Count - 1; newX >= 0; newX--)
        {
            var element = optionList[newX];
            if (!validOption.Contains(element))
            {
                optionList.RemoveAt(newX);
            }
        }
    }
}
