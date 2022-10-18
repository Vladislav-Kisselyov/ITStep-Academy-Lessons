using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float runSpeed; //переменная скорости бега
    public float jumpForce; //переменная силы прыжка
    public float dashForce; //переменная силы рывка
    public Rigidbody rigidBody; //переменная физического тела 

    private bool isGrounded = true; //для запоминания касается ли игрок земли в данный момент
    private bool isDoubleJumpDone = false; //для запоминания был ли сделан второй прыжок

    //Метод для реализации движения
    public void Move(Vector3 direction)
    {
        rigidBody.MovePosition(rigidBody.position + direction * runSpeed * Time.deltaTime);
        //Сдвигаем текущую позицию в определенном направлении 
        //transform.position += direction * runSpeed * Time.deltaTime;
    }

    //Метод для реализации прыжка
    public void Jump()
    {
        if (isGrounded == true)
        {
            //Сообщаем физическому телу силу, по направлению вверх. Получаем прыжок
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else if (isGrounded == false && isDoubleJumpDone == false)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isDoubleJumpDone = true; //запоминаем, что это был второй прыжок
        }
    }

    //Метод для реализации рывка
    public void Dash(Vector3 direction)
    {
        //Сообщаем физическому телу силу, по направлению direction. Получаем рывок
        rigidBody.AddForce(direction * dashForce, ForceMode.Impulse);
    }

    //Метод Unity, для обработки столкновения с другим твердым объектом
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " столкнулся с землей!");
        isGrounded = true; //запоминаем, что мы снова касаемся земли
        isDoubleJumpDone = false; //т.к. мы только что вернулись на землю, запоминаем что двойной прыжок ещё не был сделан.
    }

    //Метод Unity, для обработки ПРЕКРАЩЕНИЯ столкновения с другим твердым объектом
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log(name + " покинул землю!");
        isGrounded = false; //запоминаем, что мы больше НЕ касаемся земли
    }
}
