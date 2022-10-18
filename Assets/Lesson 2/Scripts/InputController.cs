using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool leftButton; //переменная для запоминания нажата ли клавиша влево
    public bool rightButton; //переменная для запоминания нажата ли клавиша вправо
    public bool forwardButton; //переменная для запоминания нажата ли клавиша вперед
    public bool backwardsButton; //переменная для запоминания нажата ли клавиша назад
    public bool jumpButton; //переменная для запоминания нажата ли клавиша прыжка
    public bool dashButton; //переменная для запоминания нажата ли клавиша рывка

    // Update is called once per frame
    void Update()
    {
        //Проверяем зажаты ли нужные клавиши на клавиатуре
        leftButton = Input.GetKey(KeyCode.A);
        rightButton = Input.GetKey(KeyCode.D);
        forwardButton = Input.GetKey(KeyCode.W);
        backwardsButton = Input.GetKey(KeyCode.S);

        //Прыгать и ускоряться мы хотим лишь при первом нажатии, а не зажатии. Поэтому используем GetKeyDown вместо GetKey
        jumpButton = Input.GetKeyDown(KeyCode.Space);
        dashButton = Input.GetKeyDown(KeyCode.LeftShift);
    }
}
