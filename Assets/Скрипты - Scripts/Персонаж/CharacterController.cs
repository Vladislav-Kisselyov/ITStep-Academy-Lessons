using UnityEngine;

[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(InputController))]
[RequireComponent(typeof(AnimationController))]
public class CharacterController : MonoBehaviour
{
    public MovementController movement;
    public InputController input;
    //Stats Характеристики

    private void FixedUpdate()
    {
        movement.Move(input.horizontalMove * Time.fixedDeltaTime, input.crouchButton, input.jumpButton);
    }
}
