using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;


public class SimpleRamdomWalkGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;

    [SerializeField]
    protected Tilemap corridorTileMap;
    
    [SerializeField]
    protected override void runProceduralGeneration(){

    Vector2Int startPosition = Vector2Int.zero;
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition);
        dungeonVisualizer.Clear();
        dungeonVisualizer.PaintFloorTiles(floorPositions, corridorTileMap);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO randomWalkParameters, Vector2Int position){
        var currentPos = position;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for(int i = 0; i< randomWalkParameters.iterations ;i++){
            var path =ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, randomWalkParameters.walkLength);
            floorPos.UnionWith(path);
            if(randomWalkParameters.startRandomlyEachIteration){
                currentPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
            }
        }
        return floorPos;
    }

}
