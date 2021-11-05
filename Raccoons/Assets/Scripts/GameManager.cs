using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject[] victoryElements;
    public GameObject[] defeatElements;
    public GameObject readyText;
    public GameObject paddle;
    private AudioSource audioSource;
    public AudioClip missClip;
    public AudioClip victoryJingle;
    public AudioClip defeatJingle;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        audioSource = gameObject.GetComponent<AudioSource>();
        livesLeft = baseLives;
        foreach (GameObject brick in GameObject.FindGameObjectsWithTag("Brick"))
        {
            activeBricks.Add(brick);
        }
        StartCoroutine(SetUpBall(false));
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
            audioSource.PlayOneShot(missClip);
            LoseLife();
        }
    }

    public void BrickDestroyed(GameObject brick)
    {
        activeBricks.Remove(brick);
        if (activeBricks.Count < 1)
        {
            StartCoroutine(EndGame(true));
        }
    }

    private void LoseLife()
    {
        livesLeft--;
        UpdateLives();
        if (livesLeft == 0)
        {
            StartCoroutine(EndGame(false));
        }
        else
        {
            StartCoroutine(SetUpBall(true));
        }
    }

    IEnumerator SetUpBall(bool lostlife)
    {
        paddle.GetComponent<PaddleController>().Reset();
        paddle.SetActive(false);
        paddle.GetComponent<PaddleController>().canControl = false;
        if (lostlife)
        {
            yield return new WaitForSeconds(2.5f);
        }
        readyText.SetActive(true);
        paddle.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        readyText.SetActive(false);
        paddle.GetComponent<PaddleController>().canControl = true;
        SpawnBall();
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

    public IEnumerator EndGame(bool win)
    {
        paddle.GetComponent<PaddleController>().canControl = false;
        yield return new WaitForSeconds(0.5f);
        paddle.SetActive(false);
        foreach (GameObject ball in activeBalls)
        {
            Destroy(ball);
        }
        paddle.SetActive(false);
        gameOverCanvas.gameObject.SetActive(true);
        if (win)
        {
            foreach (GameObject obj in victoryElements)
            {
                obj.SetActive(true);
            }
            audioSource.clip = victoryJingle;
        }
        else
        {
            foreach (GameObject obj in defeatElements)
            {
                obj.SetActive(true);
            }
            audioSource.clip = defeatJingle;
        }
        audioSource.PlayOneShot(audioSource.clip);
        Debug.Log("GAME OVER");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
