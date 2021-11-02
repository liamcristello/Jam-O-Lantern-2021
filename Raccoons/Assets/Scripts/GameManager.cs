using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ballPrefab;
    public Transform ballSpawnpoint;
    [SerializeField]
    private List<GameObject> activeBalls;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    public void SpawnBall()
    {
        GameObject ball = GameObject.Instantiate(ballPrefab, ballSpawnpoint);
        activeBalls.Add(ball);
    }

    public void BallLost(GameObject ball)
    {
        activeBalls.Remove(ball);
        if (activeBalls.Count < 1)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER");
    }
}
