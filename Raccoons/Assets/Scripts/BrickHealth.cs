using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickHealth : MonoBehaviour
{
    public int baseHealth;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
