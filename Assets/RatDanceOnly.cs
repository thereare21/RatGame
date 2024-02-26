using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDanceOnly : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("CloseToCheese", true);
        animator.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("CloseToCheese", true);
    }
}
