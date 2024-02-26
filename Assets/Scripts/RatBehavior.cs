using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehavior : MonoBehaviour
{

    private Animator animator;
    private GameObject cheese;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cheese = GameObject.FindGameObjectWithTag("Cheese");

        if (animator == null || cheese == null)
        {
            Debug.Log("rat animator or cheese object not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, cheese.transform.position) < 2f)
        {
            animator.SetBool("CloseToCheese", true);
        } else
        {
            animator.SetBool("CloseToCheese", false);
        }
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
