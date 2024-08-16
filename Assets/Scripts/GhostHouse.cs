using System.Collections.Generic;
using UnityEngine;

public class GhostHouse : MonoBehaviour
{
    List<GhostAI> _allGhost;
    float _leaveHouseTimer;
    public float LeaveHouseInterval;

    private void Awake()
    {
        _allGhost = new List<GhostAI>();
        _leaveHouseTimer = LeaveHouseInterval;
       
    }
    private void Update()
    {
        if (_allGhost.Count > 0) 
        {
            _leaveHouseTimer -= Time.deltaTime;

            if (_leaveHouseTimer <= 0) 
            {
                _leaveHouseTimer += LeaveHouseInterval;
                _allGhost[0].LeaveHouse();
                _allGhost.RemoveAt(0);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var ghost = other.GetComponent<GhostAI>();
        ghost.Recovery();

        if (_allGhost.Count == 0) 
        {
            _leaveHouseTimer = LeaveHouseInterval;
        }
        _allGhost.Add(ghost);

    }
}
