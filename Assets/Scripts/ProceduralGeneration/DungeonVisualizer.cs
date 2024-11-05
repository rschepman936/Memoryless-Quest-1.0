using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTileMap;
    [SerializeField]
    private TileBase floorTile;
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPostions){
        PaintTiles(floorPostions, floorTileMap, floorTile);
    }

    public void PaintTiles(IEnumerable<Vector2Int> pos, Tilemap map, TileBase tile){
         foreach(var position in pos){
            PaintSingleTile(map, tile, position);
         }
    }
    public void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position){ 
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition,tile);
    }

    public void Clear(){
        floorTileMap.ClearAllTiles();
    }
}
