using System;
using UnityEngine;


public enum GhostState
{
    Active,
    Vulnerability,
    VulnerabilityEnding,
    Defeated
}
[RequireComponent(typeof(GhostMove))]
public class GhostAI : MonoBehaviour
{
    private GhostMove _ghostMove;
    private GhostState _ghostState;
    public float _vulnerabilityEndingTimer;
    private float _vulnerabilityTimer;
    private bool _leaveHouse;

    private Transform _pacman;

    public event Action<GhostState> OnGhostStateChanged;
    public event Action OnDefeated;
    void Start()
    {
        _ghostMove = GetComponent<GhostMove>();
        _ghostMove.OnUpdateMoveTarget += GhostMove_OnUpdateMoveTarget;
        _pacman = GameObject.FindWithTag("Player").transform;
        _ghostState = GhostState.Active;
        _leaveHouse = false;
    }
    private void Update()
    {
        switch (_ghostState)
        {
            case GhostState.Vulnerability:
                _vulnerabilityTimer -= Time.deltaTime;
                if (_vulnerabilityTimer <= _vulnerabilityEndingTimer)
                {
                    _ghostState = GhostState.VulnerabilityEnding;
                    OnGhostStateChanged?.Invoke(_ghostState);
                }
                break;
            case GhostState.VulnerabilityEnding:
                _vulnerabilityTimer -= Time.deltaTime;
                if (_vulnerabilityTimer <= 0)
                {
                    _ghostState = GhostState.Active;
                    OnGhostStateChanged?.Invoke(_ghostState);
                }
                break;

        }
    }
    public void ResetPosition()
    {
        _ghostMove.CharacterMotor.ResetPosition();
        _ghostState = GhostState.Active;
        OnGhostStateChanged?.Invoke(_ghostState);
        _leaveHouse = false;
    }
    public void StartMoving()
    {
        _ghostMove.CharacterMotor.enabled = true;
    }
    public void StopMoving()
    {
        _ghostMove.CharacterMotor.enabled = false;
    }
    public void SetVulnerability(int duration)
    {
        _vulnerabilityTimer = duration;
        _ghostState = GhostState.Vulnerability;
        OnGhostStateChanged?.Invoke(_ghostState);
        _ghostMove.AllowReverseDirection();
    }
    public void Recovery()
    {
        _ghostMove.CharacterMotor.CollideWithGates(true);
        _ghostState = GhostState.Active;
        OnGhostStateChanged?.Invoke(_ghostState);
        _leaveHouse = false;
    }

    public void LeaveHouse() 
    {
        _ghostMove.CharacterMotor.CollideWithGates(false);
        _leaveHouse = true;
    }
    private void GhostMove_OnUpdateMoveTarget()
    {
        switch (_ghostState)
        {
            case GhostState.Active:
                if (_leaveHouse)
                {
                    if (transform.position == new Vector3(0, 3, 0))
                    {
                        _leaveHouse = false;
                        _ghostMove.CharacterMotor.CollideWithGates(true);
                        _ghostMove.SetTargetMoveLocation(_pacman.position);

                    }
                    else 
                    {
                        _ghostMove.SetTargetMoveLocation(new Vector3(0, 3, 0));
                        
                    }
                }
                else 
                {
                    _ghostMove.SetTargetMoveLocation(_pacman.position);
                }
               
                break;
            case GhostState.Vulnerability:
            case GhostState.VulnerabilityEnding:
                _ghostMove.SetTargetMoveLocation((transform.position - _pacman.position) * 2);
                break;
            case GhostState.Defeated:
                _ghostMove.SetTargetMoveLocation(Vector2.zero);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        switch (_ghostState)
        {
            case GhostState.Active:
                if (other.CompareTag("Player"))
                {
                    other.GetComponent<Life>().RemoveLives();
                }
                break;

            case GhostState.Vulnerability:
            case GhostState.VulnerabilityEnding:
                if (other.CompareTag("Player"))
                {
                    OnDefeated?.Invoke();
                    _ghostState = GhostState.Defeated;
                    OnGhostStateChanged?.Invoke(_ghostState);
                    _ghostMove.CharacterMotor.CollideWithGates(false);

                }
                break;
        }

    }
}
