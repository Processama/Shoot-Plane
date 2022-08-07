using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float m_speed = 10.0f;
    private int m_power = 34;
    private Transform m_transform;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.Translate(new Vector3(0, 0, m_speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyUFO")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (this.enabled)
        {
            Destroy(this.gameObject);
        }
    }

    public int getPower()
    {
        return m_power;
    }
}
