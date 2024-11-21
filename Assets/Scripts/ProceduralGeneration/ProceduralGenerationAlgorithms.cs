using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms 
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength){
        HashSet<Vector2Int> path = new  HashSet<Vector2Int>();
        var prevPos = startPos;

        for(int i = 0; i<walkLength;i++)
        {
            var newPos = prevPos + Direction2D.RandomDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

    public static List<Vector2Int>RandomWalkCorridor(Vector2Int startPosition, int corridorLength){
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.RandomDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);
        for(int i = 0; i<corridorLength; i++){
            currentPosition+= direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }
}

public static class Direction2D{
    public static List<Vector2Int> directionList= new List<Vector2Int>{
        new Vector2Int(0,1),//Up
        new Vector2Int(1,0),//Right
        new Vector2Int(0,-1),//Down
        new Vector2Int(-1,0),//Left
    };

    public static Vector2Int RandomDirection(){
        return directionList[Random.Range(0,directionList.Count)];
    }

}
