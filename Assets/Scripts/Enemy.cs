using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float m_speed = 1L;
    private int m_life = 100;
    private float m_roateSpeed;
    // 随机左右方向
    private int m_randomStart;
    // 撞击爆炸时造成的伤害
    private int m_boomPower = 10;
    private Transform m_transform;

    // Start is called before the first frame update
    void Start()
    {
        m_roateSpeed = Random.Range(25, 30);
        m_randomStart = Random.Range(-180, 180);

        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CrossBorder();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DefaultBullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                GetHurt(bullet.getPower());
                
            }
        } else if (other.tag == "Player")
        {
            GetHurt(m_life);
        }
    }

    protected virtual void Move()
    {
        float rx = Mathf.Sin(Time.time + m_randomStart) * Time.deltaTime;
        transform.Rotate(new Vector3(0, m_roateSpeed, 0), Space.World);
        transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime), Space.World);
    }

    /**
     * 越过底线销毁
     */
    private void CrossBorder()
    {
        if (m_transform.localPosition.z < -10)
        {
            Destroy(this.gameObject);
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

    public int getBoomPower()
    {
        return m_boomPower;
    }
}
