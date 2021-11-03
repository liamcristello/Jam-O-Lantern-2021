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
    [SerializeField]
    private List<GameObject> activeBricks;
    public GameObject[] lives;
    public int baseLives;
    private int livesLeft;
    public Canvas gameOverCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        livesLeft = baseLives;
        foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick"))
        {
            activeBricks.Add(brick);
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
            LoseLife();
        }
    }

    public void BrickDestroyed(GameObject brick)
    {
        activeBricks.Remove(brick);
        if (activeBricks.Count < 1)
        {
            EndGame(true);
        }
    }

    private void LoseLife()
    {
        livesLeft--;
        UpdateLives();
        if (livesLeft == 0)
        {
            EndGame(false);
        }
    }

    private void GainLife()
    {
        if (livesLeft < baseLives)
        {
            livesLeft++;
            UpdateLives();
        }
    }

    private void UpdateLives()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (livesLeft > i) { lives[i].gameObject.SetActive(true); }
            else { lives[i].gameObject.SetActive(false); }
        }
    }

    public void EndGame(bool win)
    {
        gameOverCanvas.gameObject.SetActive(true);
        Debug.Log("GAME OVER");
    }
}
