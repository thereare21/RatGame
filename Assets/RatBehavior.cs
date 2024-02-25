using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);

            //increment score
           //ScoreManager.ratsKilled += 1;
        }
    }
}
