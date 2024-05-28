using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FollowCube : MonoBehaviour
{
    
    [SerializeField] private Transform m_target;
    [SerializeField] private Vector3 m_offset;
    [SerializeField] private float m_ratio=.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = Vector3.Lerp(transform.position, m_target.position + m_offset, m_ratio);
        transform.position = position;
    }
}
