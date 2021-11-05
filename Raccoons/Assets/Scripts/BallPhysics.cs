using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallPhysics : MonoBehaviour
{
    [SerializeField]
    private float X_MIN;
    [SerializeField]
    private float X_MAX;
    [SerializeField]
    private float Y_MIN;
    [SerializeField]
    private float Y_MAX;
    //UnityEvent OutOfBounds;
    private Rigidbody2D rb;
    public GameObject explosion;
    public AudioClip[] bounceClips;
    public AudioClip[] brickBreakClips;
    public AudioClip[] brickShatterClips;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * Random.Range(Y_MIN, Y_MAX));
        rb.AddForce(Vector2.right * (Random.Range(X_MIN, X_MAX) * (Random.Range(0, 2) * 2 - 1)));
        //OutOfBounds = new UnityEvent();
        //OutOfBounds.AddListener(GameManager.CallTest);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x + 0.003f) * Mathf.Sign(rb.velocity.x),
        //    Mathf.Abs(rb.velocity.y + 0.003f) * Mathf.Sign(rb.velocity.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OutOfBounds"))
        {
            //OutOfBounds.Invoke();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().BallLost(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            collision.gameObject.GetComponent<BrickHealth>().TakeDamage();
            if (collision.gameObject.GetComponent<BrickHealth>().GetHealth() < 1)
            {
                audioSource.clip = brickShatterClips[Random.Range(0, brickShatterClips.Length)];
                GameObject exp = Instantiate(explosion, collision.transform.position, collision.transform.rotation);
                Destroy(exp, 2.5f);
            }
            else
            {
                audioSource.clip = brickBreakClips[Random.Range(0, brickBreakClips.Length)];
            }
        }
        else
        {
            audioSource.clip = bounceClips[Random.Range(0, bounceClips.Length)];
        }
        audioSource.PlayOneShot(audioSource.clip);
    }
}
