using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOnZAxis : MonoBehaviour
{
    [SerializeField] private int m_score;
    [HideInInspector] public GameManager m_gameManager;
    [SerializeField] private float m_speed = 20;
    private Transform m_transform;

    // Start is called before the first frame update

    private void Awake()
    {
        m_transform = transform;
    }
    void Start()
    {
        print(m_gameManager.name);
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.position += m_transform.forward * m_speed * Time.deltaTime;
        if (m_transform.position.z < -10)
        {
            m_gameManager.AddScore(m_score);
            Destroy(gameObject);
        }
        
    }
}
