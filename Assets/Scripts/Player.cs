using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour
{
    private int m_life = 100;
    private float m_speed = 5.0f;
    private Transform m_transform;

    public GameObject m_bullet;
    // 攻速 颗/s
    private float m_bulletAtkSpeed = 5;
    // 每颗子弹前摇时长
    private float m_bulletAtkWait;
    // 发射一枚子弹后发射下一枚前摇计时器
    private float m_bulletTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;

        m_bulletAtkWait = 1 / m_bulletAtkSpeed;
        m_bulletTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyUFO")
        {
            GetHurt(other.GetComponent<Enemy>().getBoomPower());
        }
    }

    private void Move()
    {
        // 纵向移动距离
        float MoveV = 0;
        // 横向移动距离
        float MoveH = 0;

        if (Input.GetKey(KeyCode.W))
        {
            MoveV += m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveV -= m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveH -= m_speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveH += m_speed * Time.deltaTime;
        }
        m_transform.Translate(new Vector3(MoveH, 0, MoveV));
    }

    private void Shoot()
    {
        // 每一帧计时器减去该帧时长直到前摇为0,便可发射下一枚
        m_bulletTimer -= Time.deltaTime;
        if (m_bulletTimer <= 0)
        {
            m_bulletTimer = 0;
            if (Input.GetKey(KeyCode.J) || Input.GetMouseButton(0))
            {
                Instantiate(m_bullet, m_transform.position, m_transform.rotation);
                // 每发射一枚子弹,计时器重置为前摇时长
                m_bulletTimer = m_bulletAtkWait;
            }
        }
    }

    private void GetHurt(int damage)
    {
        m_life -= damage;
        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
