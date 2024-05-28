using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private int[] m_positions =
    {
        -2,0,2 // index 0,1,2
    };

    private int m_currentPositionIndex;
    private Vector3 m_targetPosition;
    private bool m_listenToInput;
    [SerializeField] private float m_lerpValue =50;
    private float m_lerpThreshold =.1f;

    // Start is called before the first frame update
    void Start()
    {
        m_currentPositionIndex = 1;
        Vector3 startPosition = new Vector3(m_positions[m_currentPositionIndex], 0, 0);
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_listenToInput ==true)
        {
            if (Input.GetMouseButtonDown(0)) MoveLeft();

            if (Input.GetMouseButtonDown(1)) MoveRight();

        }
        
        transform.position = Vector3.Lerp(transform.position,m_targetPosition, m_lerpValue*Time.deltaTime);
        
        if(Vector3.Distance(transform.position, m_targetPosition) < m_lerpThreshold)
        {
            m_listenToInput = true;
        }

    }

    private void MoveRight()
    {
        if (m_currentPositionIndex > 0)
        {
            m_currentPositionIndex--;
            m_targetPosition = new Vector3(m_positions[m_currentPositionIndex], 0, 0);
            m_listenToInput = false;
        }
    }

    private void MoveLeft()
    {
        if (m_currentPositionIndex < 2)
        {
            m_currentPositionIndex++;
            m_targetPosition = new Vector3(m_positions[m_currentPositionIndex], 0, 0);
            m_listenToInput = false;
        }
    }
}
