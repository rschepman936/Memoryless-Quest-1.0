using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CorridorFirstDungeonGenerator : SimpleRamdomWalkGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercentage = 0.4f;

    public GameObject memoryShard;
   
    protected override void runProceduralGeneration(){
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration(){
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        floorPositions.UnionWith(roomPositions);

        dungeonVisualizer.PaintFloorTiles(floorPositions);
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions){
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomsToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count()*roomPercentage);
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomsToCreateCount).ToList();
        List<Vector2Int> shardPos = roomsToCreate.OrderBy(x => Guid.NewGuid()).Take(1).ToList();
        foreach(var roomPosition in roomsToCreate){
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        foreach(var shard in shardPos){
            memoryShard.transform.position = new Vector3(shard.x, shard.y, 0);
        }
        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions){
        var currentPosition = startPos;
        potentialRoomPositions.Add(currentPosition);

        for(int i = 0;i<corridorCount;i++){
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }

    }

    public void Start(){
        runProceduralGeneration();
    }
    
}