using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 250.0f;
    public float jumpForce = 12f;
    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _box;

    // Start is called before the first frame update
    void Start()
    {
        _box = GetComponent<BoxCollider2D>();
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y); // задаём только горизонтальное движение
        _body.velocity = movement;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f); // проверяем значения минимальной Y координаты коллайдера
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if (hit != null) {
            grounded = true; // если под персонажем обнаружен коллайдер
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space)) {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // сила добавляется только при нажатии клавиши Пробел
        }

        _anim.SetFloat("speed", Mathf.Abs(deltaX)); // скорость больше нуля даже при отрицательных значениях velocity
        if (!Mathf.Approximately(deltaX, 0)) { // числа типа float не всегда полностью совпадают, поэтому сравним их методом Approximately()
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1); // в процессе движения масштабируем положительную или отрицательную единицу для поворота направо или налево
        }
    }
}
