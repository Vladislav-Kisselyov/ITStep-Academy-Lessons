using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public MovementController movement;
    public InputController input;

    // Update is called once per frame
    void Update()
    {

        //Если соответствующее условие выполняется, то вызываем метод Move(), передав в него соответствующее напрвление
        if (input.leftButton)
        {
            movement.Move(Vector3.left);
            if (input.dashButton)
            {
                movement.Dash(Vector3.left);
            }
        }
        if (input.rightButton)
        {
            movement.Move(Vector3.right);
            if (input.dashButton)
            {
                movement.Dash(Vector3.right);
            }
        }
        if (input.forwardButton)
        {
            movement.Move(Vector3.forward);
            if (input.dashButton)
            {
                movement.Dash(Vector3.forward);
            }
        }
        if (input.backwardsButton)
        {
            movement.Move(Vector3.back);
            if (input.dashButton)
            {
                movement.Dash(Vector3.left);
            }
        }

        if (input.jumpButton)
        {
            movement.Jump();
        }
    }
}
