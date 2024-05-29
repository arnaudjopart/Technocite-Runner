using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] m_obstaclePrefabCollection; 
    [SerializeField] private float m_spawnFrequency = 5f;
    private float m_timer;
    [SerializeField] private GameManager m_myGameManager;


    // Start is called before the first frame update
    void Start()
    {
        /*InvokeRepeating("SpawnObstacle", 2, 5);
        CancelInvoke("SpawnObstacle");*/

        //Invoke("GameOver", 5);
        m_timer = m_spawnFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer-=Time.deltaTime;
        if(m_timer <= 0)
        {
            SpawnObstacle();
            m_timer = m_spawnFrequency;
        }
    }

    private void SpawnObstacle()
    {
        //GameObject newInstance = Instantiate(m_obstaclePrefab);
        GameObject newInstance = Instantiate(m_obstaclePrefabCollection[Random.Range(0,m_obstaclePrefabCollection.Length)], new Vector3(0,0,200),Quaternion.Euler(0,180,0));
        newInstance.transform.parent = transform;
        newInstance.GetComponent<MovementOnZAxis>().m_gameManager = m_myGameManager;
    }
}
