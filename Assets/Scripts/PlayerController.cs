using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int m_positionIndex = 1;
    private Vector3 m_targetPosition;

    private int[] m_possiblePositions =
    {
        -2, 0, 2
    };
    [SerializeField] float f = .15f;

    [SerializeField] private bool m_isJumping;
    float m_jumpSpeed;
    private Vector3 nextPosition;
    [SerializeField] private float m_maxJumpHeight;
    private bool m_isSliding;
    private float m_slideTimer;

    // Start is called before the first frame update
    void Start()
    {
        m_targetPosition = transform.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isJumping)
        {
            HandleJump();
            return;
        }
        
        if (m_isSliding)
        {
            HandleSlide();
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) m_positionIndex -= 1;
        if (Input.GetKeyDown(KeyCode.RightArrow)) m_positionIndex += 1;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_isJumping = true;
            m_jumpSpeed = Mathf.Sqrt(2*10*m_maxJumpHeight);
            nextPosition = transform.position; 
            nextPosition.y += m_jumpSpeed*Time.deltaTime;
            transform.position = nextPosition;
            return;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_isSliding = true;
            m_slideTimer = 1f;
            transform.position += Vector3.down * .5f;
            return;
        }


        m_positionIndex = Mathf.Clamp(m_positionIndex, 0, 2);
        m_targetPosition = new Vector3(m_possiblePositions[m_positionIndex], 0f, 0);
        transform.position = Vector3.Lerp(transform.position, m_targetPosition,f);

    }

    private void HandleSlide()
    {
        m_slideTimer -= Time.deltaTime;
        if (m_slideTimer > 0) return;
        transform.position-= Vector3.down * .5f;
        m_isSliding = false;
    }

    private void HandleJump()
    {
        m_jumpSpeed -= 10 * Time.deltaTime;
        nextPosition = transform.position;
        nextPosition.y += m_jumpSpeed * Time.deltaTime;
        transform.position = nextPosition;
        if (nextPosition.y > 0f) return;
        nextPosition.y = 0;
        transform.position = nextPosition;
        m_isJumping = false;
    }
}
