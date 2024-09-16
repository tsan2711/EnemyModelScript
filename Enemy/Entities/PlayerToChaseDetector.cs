using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToChaseDetector : MonoBehaviour
{
    public bool PlayerInRange => detectPlayer != null;

    private List<Transform> playersInRange = new List<Transform>();
    public Transform detectPlayer {get; private set;}

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")) return;
        playersInRange.Add(other.transform);
        detectPlayer = other.transform;
    }

    private void OnTriggerExit(Collider other) {
        if(!other.CompareTag("Player")) return;
        playersInRange.Remove(other.transform);
        
        if(other.transform == detectPlayer) 
            detectPlayer = playersInRange.Count > 0 ? playersInRange[0] : null;

    }
}
