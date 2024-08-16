using UnityEngine;

public class PacmanView : MonoBehaviour
{
    public CharacterMotor CharacterMotor;
    public GameManager GameManager;
    public Life CharacterLife;
    public Animator Animator;

    public AudioSource audioSourcePacman;
    public AudioClip lifeLostSound;
    void Start()
    {
        CharacterMotor.OnDirectionChanged += CharacterMotor_OnDirectionChanged;
        CharacterMotor.OnResetPosition += CharacterMotor_OnResetPosition;
        CharacterMotor.OnDisabled += CharacterMotor_OnDisabled;
        CharacterLife.OnRemovedLives += CharacterLife_OnRemovedLives;
        Animator.SetBool("Moving", false);
        Animator.SetBool("Dead", false);


    }



    private void CharacterMotor_OnDisabled()
    {
        Animator.speed = 0f;
    }

    private void CharacterMotor_OnResetPosition()
    {

        Animator.SetBool("Moving", false);
        Animator.SetBool("Dead", false);
    }

    private void CharacterLife_OnRemovedLives(int _)
    {
        transform.Rotate(0, 0, -90);
        Animator.speed = 1f;
        audioSourcePacman.PlayOneShot(lifeLostSound);
        Animator.SetBool("Moving", false);
        Animator.SetBool("Dead", true);

    }

    private void CharacterMotor_OnDirectionChanged(Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                Animator.SetBool("Moving", false);
                break;

            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                Animator.SetBool("Moving", true);

                break;

            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                Animator.SetBool("Moving", true);

                break;

            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                Animator.SetBool("Moving", true);

                break;

            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Animator.SetBool("Moving", true);

                break;
        }
    }


}
