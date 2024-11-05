using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRamdomWalkGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool starRandomlyEachIteration = true;
    
    [SerializeField]
    protected override void runProceduralGeneration(){
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        dungeonVisualizer.Clear();
        dungeonVisualizer.PaintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk(){
        var currentPos = startPos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for(int i = 0; i<iterations ;i++){
            var path =ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPos, walkLength);
            floorPos.UnionWith(path);
            if(starRandomlyEachIteration){
                currentPos = floorPos.ElementAt(Random.Range(0,floorPos.Count));
            }
        }
        return floorPos;
    }
}
