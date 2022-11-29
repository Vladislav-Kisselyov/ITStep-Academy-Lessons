using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float horizontalMove;

    public bool jumpButton = false; //переменная для запоминания нажата ли клавиша прыжка
    public bool crouchButton = false; //переменная для запоминания нажата ли клавиша приседания

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        //Проверяем зажаты ли нужные клавиши на клавиатуре
        crouchButton = Input.GetKey(KeyCode.S);

        if (Input.GetKeyDown(KeyCode.Space))
            jumpButton = true;
        if (Input.GetKeyUp(KeyCode.Space))
            jumpButton = false;
    }
}
