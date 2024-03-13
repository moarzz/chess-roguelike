using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Manager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile TilePrefab ;
    [SerializeField] private Transform CamTransf;

    private Dictionary<Vector2, Tile> Tiles;

    private void Start()
    {
        GenerateGrid();
    }


    void GenerateGrid() {
        for (int x = 0; x < width; x++) {
            for ( int y = 0; y < height; y++) {
                var spawnedTile = Instantiate(TilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var IsOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(IsOffset);

                Tiles[new Vector2 (x, y)] = spawnedTile;
            }
        }
        CamTransf.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }


    public Tile GetTileAtPos(Vector2 pos)
    {
        if (Tiles.TryGetValue(pos, out Tile tile))
        {
            return tile;
        }
        return null;
    }
    
}

