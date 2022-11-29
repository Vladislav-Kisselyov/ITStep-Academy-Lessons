using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    [Header("Настройки Бега")]
    [Range(10, 400f)][SerializeField] private float m_RunSpeed = 400f; //Скорость Бега
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f; //Сглаживание Бега

    [Header("Настройки Прыжка")]
    [SerializeField] private float m_JumpForce = 400f; //Сила Прыжка
    [SerializeField] private bool m_AirControl = false; //Можно ли двигаться в воздухе
    [SerializeField] private Transform m_GroundCheck; //Ссылка на Ноги персонажа
    [SerializeField] private LayerMask m_WhatIsGround; //Маска, считающаяся землей

    [Header("Настройки Приседания")]
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f; //Множитель скорости при приседании
    [SerializeField] private Transform m_CeilingCheck; //Ссылка на Голову Персонажа
    [SerializeField] private Collider2D m_CrouchDisableCollider; //Коллайдер, который отключается при приседании

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [Header("События")]
    [Space]

    public BoolEvent OnCrouchEvent; //Событие при приседании
    public UnityEvent OnLandEvent; //Событие при приземлении

    //Константы
    const float k_GroundedRadius = .2f; //Радиус для проверки земли под ногами
    const float k_CeilingRadius = .2f; //Радиус для проверки земли над головой

    private Rigidbody2D m_Rigidbody2D; //Ссылка на компонент RigidBody
    private Vector3 m_Velocity = Vector3.zero; //Скорость персонажа
    private bool m_Grounded; //Находится ли персонаж на земле
    private bool m_FacingRight = true;  //Смотрит ли персонаж Вправо

    private bool m_wasCrouching = false; //Приседал ли персонаж на предыдущем кадре

    private void Awake()
    {
        //Поймать ссылку на Компонент RigidBody
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // Отсылаем лучи во все стороны, и проверяем пересеклись ли он с землей
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 
            k_GroundedRadius, m_WhatIsGround);
        //Проверям соприкосновение с кусочками земли
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float direction, bool crouch, bool jump)
    {
        //Умножаем направление на скорость
        direction *= m_RunSpeed;

        // Если мы уже приседаем, и над головой есть объект мешающий встать
        if (crouch == false)
        {
            // Запускаем лучи из головы и проверяем есть ли земля над головой
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //Перемещаться влево-вправо только если мы на земле или включен AirControl
        if (m_Grounded || m_AirControl)
        {

            // Если приседаем
            if (crouch)
            {
                //И если до этого не приседали
                if (!m_wasCrouching)
                {
                    //запустить событие о том, что только что присели
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Замедлить скорость игрока если приседаем
                direction *= m_CrouchSpeed;

                // Отключить коллайдер головы
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Включаем коллайдер головы
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                // Запустить событие о том, что мы перестали приседать
                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            //Влияем на скорость по горизонтали
            Vector3 targetVelocity = new Vector2(direction, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, 
                targetVelocity, ref m_Velocity, m_MovementSmoothing);

            //Запоминаем последнее направление, в которое смотрим
            if (direction > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (direction < 0 && m_FacingRight)
            {
                Flip();
            }
        }
        // Если игрок находится на земле и нажата клавиша прыжка, то прыгаем
        if (m_Grounded && jump)
        {
            //Применить вертикальную силу для прыжка
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Изменить переменную о Направлении на противоположное значение
        m_FacingRight = !m_FacingRight;
        // Умножить Scale.x на -1, отразив тем самым объект целиком
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}