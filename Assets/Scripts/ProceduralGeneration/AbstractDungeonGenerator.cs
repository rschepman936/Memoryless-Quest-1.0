using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
   [SerializeField]
   protected DungeonVisualizer dungeonVisualizer;
   [SerializeField]
   protected Vector2Int startPos = Vector2Int.zero;

   public void GenerateDungeon(){
    dungeonVisualizer.Clear();
    runProceduralGeneration();
   }

    protected abstract void runProceduralGeneration();

}
