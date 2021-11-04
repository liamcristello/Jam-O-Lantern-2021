using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHealth : MonoBehaviour
{
    public int baseHealth;
    private int health;
    public Color[] colors;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        health = baseHealth;
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.color = colors[health - 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetHealth()
    {
        return health;
    }

    public void TakeDamage()
    {
        health--;
        if (health < 1)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().BrickDestroyed(gameObject);
            Destroy(gameObject);
        }
        else
        {
            sr.color = colors[health - 1];
        }
    }
}
