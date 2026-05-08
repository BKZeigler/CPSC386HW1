using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/Terrain Tile")]
public class TerrainTile : Tile // creates a tile asset that can be marked as terrain
{
    public bool isBlocked;
}
