using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOnZAxis : MonoBehaviour
{
    [SerializeField] private float m_speed = 20;
    private Transform m_transform;

    // Start is called before the first frame update

    private void Awake()
    {
        m_transform = transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.position += m_transform.forward * m_speed * Time.deltaTime;
        if(m_transform.position.z<-10) Destroy(gameObject);
    }
}
