using UnityEngine;

[RequireComponent(typeof(CharacterMotor))]
public class PacmanInput : MonoBehaviour
{

    private CharacterMotor _motor;
    private void Start()
    {
        _motor = GetComponent<CharacterMotor>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _motor.SetMoveDirection(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _motor.SetMoveDirection(Direction.Left);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _motor.SetMoveDirection(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _motor.SetMoveDirection(Direction.Right);
        }
    }
}
