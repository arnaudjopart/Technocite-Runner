using UnityEngine;

using static UnityEngine.Input;
using static UnityEngine.KeyCode;

public class PlayerMovement : MonoBehaviour
{
    private int[] m_positions =
    {
        -2,0,2 // index 0,1,2
    };

    private enum STATE
    {
        DEFAULT = 0, //0;
        MOVING,//1
        JUMPING, //2
        SLIDING, //3
        FALLING//4
    }

    [SerializeField] private STATE m_currentState;

    private int m_currentPositionIndex;
    private Vector3 m_targetPosition;
    [SerializeField] private float m_lerpValue =50;
    private float m_lerpThreshold =.1f;
    private float m_initialJumpSpeed;
    private float m_currentVerticalSpeed;
    private const float m_gravity = 9.8f;
    [SerializeField] private float m_maxHeight = 2;
    [SerializeField] private float m_doubleJumpImpulse;

    private float m_slideTimer;

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
        switch (m_currentState)
        {
            case STATE.DEFAULT:

                if (GetKeyDown(LeftArrow)) MoveLeft();
                if (GetKeyDown(RightArrow)) MoveRight();
                if (GetKeyDown(UpArrow)) Jump();
                if (GetKeyDown(DownArrow)) Slide();

                break;
            case STATE.MOVING:
                //Move Logic
                transform.position = Vector3.Lerp(transform.position, m_targetPosition, m_lerpValue * Time.deltaTime);

                if (Vector3.Distance(transform.position, m_targetPosition) < m_lerpThreshold)
                {
                    transform.position = m_targetPosition;
                    m_currentState = STATE.DEFAULT;
                }
                break;
            case STATE.JUMPING:
                
                if (GetKeyDown(UpArrow)) DoubleJump();

                m_currentVerticalSpeed -= m_gravity * Time.deltaTime;
                transform.position += Vector3.up * m_currentVerticalSpeed * Time.deltaTime;

                if (m_currentVerticalSpeed < 0)
                {
                    m_currentState = STATE.FALLING;
                }
              
                break;
            case STATE.SLIDING:

                m_slideTimer -= Time.deltaTime;
                if (m_slideTimer <= 0)
                {
                    m_currentState = STATE.DEFAULT;
                    transform.localScale = new Vector3(1, 1f, 1);
                }

                break;
            case STATE.FALLING:
                m_currentVerticalSpeed -= m_gravity * 10 * Time.deltaTime;
                transform.position += Vector3.up * m_currentVerticalSpeed * Time.deltaTime;
                if (transform.position.y <= 0)
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    m_currentState = STATE.DEFAULT;
                }
                break;
            default:
                break;
        }

    }

    private void DoubleJump() => m_currentVerticalSpeed += m_doubleJumpImpulse;

    private void Slide()
    {
        m_slideTimer = .7f;
        transform.localScale = new Vector3(1, .5f, 1);
        m_currentState = STATE.SLIDING;
    }

    private void Jump()
    {
        m_initialJumpSpeed = Mathf.Sqrt(2 * m_gravity * m_maxHeight);
        m_currentVerticalSpeed = m_initialJumpSpeed;
        transform.position += Vector3.up * m_currentVerticalSpeed * Time.deltaTime;
        m_currentState = STATE.JUMPING;
    }

    private void MoveRight()
    {

        if (m_currentPositionIndex < 2)
        {
            m_currentPositionIndex++;
            m_targetPosition = new Vector3(m_positions[m_currentPositionIndex], 0, 0);
            m_currentState = STATE.MOVING;
        }
    }

    private void MoveLeft()
    {
        if (m_currentPositionIndex > 0)
        {
            m_currentPositionIndex--;
            m_targetPosition = new Vector3(m_positions[m_currentPositionIndex], 0, 0);
            m_currentState = STATE.MOVING;
        }

    }
}