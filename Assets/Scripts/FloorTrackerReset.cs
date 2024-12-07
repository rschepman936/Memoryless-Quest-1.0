using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrackerReset : MonoBehaviour
{
    [SerializeField]
    FloorTracker tracker;

    void Start()
    {
        tracker.floorNumber = 0;
    }

}
