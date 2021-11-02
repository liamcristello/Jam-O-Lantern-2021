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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * Random.Range(Y_MIN, Y_MAX));
        rb.AddForce(Vector2.right * (Random.Range(X_MIN, X_MAX) * (Random.Range(0, 2) * 2 - 1)));
        //OutOfBounds = new UnityEvent();
        //OutOfBounds.AddListener(GameManager.CallTest);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(explosion, 2.5f);
            Destroy(collision.gameObject);
        }
    }
}
