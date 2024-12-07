using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Tilemaps;

public class CorridorFirstDungeonGenerator : SimpleRamdomWalkGenerator
{
    [SerializeField]
    public whichEnemyTurn enemyTracker;
    
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;

    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercentage = 0.4f;

    public GameObject memoryShard;

    [SerializeField]
    public GameObject enemy;

    Vector3 memoryShardPos; 
    float adjustment = .5f;

    [SerializeField]
    private Tilemap roomTileMap;

    [SerializeField]
    protected Tilemap corridorMap;

    Vector3 start;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject playerMovePoint;

    public FloorTracker floorTracker;

    protected override void runProceduralGeneration(){
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration(){
        HashSet<Vector2Int> corridorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        List<List<Vector2Int>> corridors = CreateCorridors(corridorPositions, potentialRoomPositions);
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        List<Vector2Int>deadEnds = findAllDeadEnds(corridorPositions);
        
        CreateRoomsAtDeadEnd(deadEnds,roomPositions);

        for(int i = 0; i<corridors.Count;i++){
            corridors[i] = IncreaseCorridorSizeByThree(corridors[i]);
            corridorPositions.UnionWith(corridors[i]);
        }
        corridorPositions.Add(Vector2Int.zero);
        dungeonVisualizer.PaintFloorTiles(corridorPositions, corridorMap);
        roomPositions.Remove(Vector2Int.zero);
        dungeonVisualizer.PaintFloorTiles(roomPositions, roomTileMap);
    }

    public List<Vector2Int> IncreaseCorridorSizeByThree(List<Vector2Int> corridor){
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for(int i = 1; i<corridor.Count; i++){
            for(int x = -1; x<2;x++){
                for(int y = -1; y<2; y++){
                        newCorridor.Add(corridor[i-1] + new Vector2Int(x,y));
                }
            }
        }
        return newCorridor;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potRoomPos){
        var currentPosition = startPos;
        potRoomPos.Add(currentPosition);
        List<List<Vector2Int>> corridors = new  List<List<Vector2Int>>();

        Debug.Log(corridorCount * (floorTracker.floorNumber));

        for(int i =0;i<corridorCount * (floorTracker.floorNumber) ;i++){
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            corridors.Add(corridor);
            currentPosition = corridor[corridor.Count- 1];
            potRoomPos.Add(currentPosition);
            floorPos.UnionWith(corridor);
        }
        return corridors;
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors){
        foreach(var pos in deadEnds){
            if(roomFloors.Contains(pos) == false){
                var room = RunRandomWalk(randomWalkParameters, pos);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> findAllDeadEnds(HashSet<Vector2Int> corridorPos){
        List<Vector2Int> deadends = new List<Vector2Int>();
        foreach(var pos in corridorPos){
            int neighborsCount = 0;
            foreach(var direction in Direction2D.directionList){
                if(corridorPos.Contains(pos+direction)){
                    neighborsCount++;
                }
            }
            if(neighborsCount==1){
                deadends.Add(pos);
            }
        }
        return deadends;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions){
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomsToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count()*roomPercentage);
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomsToCreateCount).ToList();
        
        foreach(var roomPosition in roomsToCreate){
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
            List<Vector2Int> enemyPos = roomFloor.OrderBy(x => Guid.NewGuid()).Take(1).ToList();
            foreach(var loc in enemyPos){
                Vector3 post = new  Vector3(loc.x+.63f, loc.y+.57f,0);
                GameObject newEnemy = Instantiate(enemy,post,Quaternion.identity);
                enemyTracker.totalEnemies++;
                newEnemy.transform.GetChild(0).GetComponent<EnemyGridMovement>().enemyNumber = enemyTracker.totalEnemies;
                newEnemy.transform.GetChild(0).GetComponent<EnemyGridMovement>().goal = playerMovePoint.transform;
            }
        }
        List<Vector2Int> shardPos = roomPositions.OrderBy(x => Guid.NewGuid()).Take(3).ToList();
        foreach(var shard in shardPos){
            if(Vector2.Distance(shard, start) > Vector2.Distance(memoryShardPos, start)){
                memoryShard.transform.position = new Vector3(shard.x+adjustment, shard.y+adjustment, 0);
                memoryShardPos = new Vector3(shard.x+adjustment, shard.y+adjustment,0);
            }
        }
        return roomPositions;
    }

    public void Start(){
        dungeonVisualizer.Clear();
        enemyTracker.totalEnemies = 0;
        start.Set(0,0,0);
        memoryShardPos.Set(0,0,0);
        runProceduralGeneration();
        Destroy(enemy);
    }
}