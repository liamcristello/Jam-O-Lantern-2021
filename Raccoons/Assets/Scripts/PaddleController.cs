using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public SpriteRenderer cloth;
    public Sprite clothUp;
    public Sprite clothDown;
    private AudioSource audioSource;
    public bool canControl;
    //private Transform start;
    // Start is called before the first frame update
    void Start()
    {
        //start = transform;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControl) return;

        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if (transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    public void Reset()
    {
        Debug.Log("Resetting");
        cloth.sprite = clothDown;
        transform.localPosition = new Vector3(0f, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            StartCoroutine(ClothAnim());
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    private IEnumerator ClothAnim()
    {
        cloth.sprite = clothUp;
        yield return new WaitForSeconds(0.2f);
        cloth.sprite = clothDown;
    }
}
