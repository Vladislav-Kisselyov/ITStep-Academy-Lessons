namespace Lesson2
{
    using UnityEngine;

    public class Controller : MonoBehaviour
    {
        //Атрибут SerializeField позволяет изменить значение переменной из Инспектора Unity
        [SerializeField] private float runSpeed; //переменная скорости бега
        [SerializeField] private float jumpForce; //переменная силы прыжка
        [SerializeField] private float dashForce; //переменная силы рывка
        [SerializeField] private Rigidbody rigidBody; //переменная физического тела 

        private bool leftButton; //переменная для запоминания нажата ли клавиша влево
        private bool rightButton; //переменная для запоминания нажата ли клавиша вправо
        private bool forwardButton; //переменная для запоминания нажата ли клавиша вперед
        private bool backwardsButton; //переменная для запоминания нажата ли клавиша назад
        private bool jumpButton; //переменная для запоминания нажата ли клавиша прыжка
        private bool dashButton; //переменная для запоминания нажата ли клавиша рывка

        private bool isGrounded = true; //для запоминания касается ли игрок земли в данный момент
        private bool isDoubleJumpDone = false;

        private void Update()
        {
            //Проверяем зажаты ли нужные клавиши на клавиатуре
            leftButton = Input.GetKey(KeyCode.A);
            rightButton = Input.GetKey(KeyCode.D);
            forwardButton = Input.GetKey(KeyCode.W);
            backwardsButton = Input.GetKey(KeyCode.S);

            //Прыгать и ускоряться мы хотим лишь при первом нажатии, а не зажатии. Поэтому используем GetKeyDown вместо GetKey
            jumpButton = Input.GetKeyDown(KeyCode.Space);
            dashButton = Input.GetKeyDown(KeyCode.LeftShift);

            //Если соответствующее условие выполняется, то вызываем метод Move(), передав в него соответствующее напрвление
            if (leftButton) 
            {
                Move(Vector3.left);
                if (dashButton)
                {
                    Dash(Vector3.left, dashForce);
                }
            }
            if (rightButton)
            {
                Move(Vector3.right);
                if (dashButton)
                {
                    Dash(Vector3.right, dashForce);
                }
            }
            if (forwardButton)
            {
                Move(Vector3.forward);
                if (dashButton)
                {
                    Dash(Vector3.forward, dashForce);
                }
            }
            if (backwardsButton)
            {
                Move(Vector3.back);
                if (dashButton)
                {
                    Dash(Vector3.left, dashForce);
                }
            }

            if (jumpButton && isGrounded)
            {
                Jump(jumpForce);
            }
            else if (jumpButton && isGrounded == false && isDoubleJumpDone == false)
            {
                isDoubleJumpDone = true; //запоминаем, что это был второй прыжок
                Jump(jumpForce);
            }
        }

        //Метод для реализации движения
        private void Move(Vector3 direction)
        {
            rigidBody.MovePosition(rigidBody.position + direction * runSpeed * Time.deltaTime);
            //Сдвигаем текущую позицию в определенном направлении 
            //transform.position += direction * runSpeed * Time.deltaTime;
        }

        //Метод для реализации прыжка
        private void Jump(float forceAmount)
        {
            //Сообщаем физическому телу силу, по направлению вверх. Получаем прыжок
            rigidBody.AddForce(Vector3.up * forceAmount, ForceMode.Impulse);
        }

        //Метод для реализации рывка
        private void Dash(Vector3 direction, float forceAmount)
        {
            //Сообщаем физическому телу силу, по направлению direction. Получаем рывок
            rigidBody.AddForce(direction * forceAmount, ForceMode.Impulse);
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
}
