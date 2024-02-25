using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletTravel : MonoBehaviour
{
    public float timeTravelling;

    private bool destroyable = false;

    private void Start()
    {
        destroyable = false;
        StartCoroutine(ActivateDestroyable());
    }

    IEnumerator ActivateDestroyable()
    {
        yield return new WaitForSeconds(0.05f);
        destroyable = true;
        yield return new WaitForSeconds(timeTravelling);
        Destroy(gameObject);
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (destroyable)
        {
            Destroy(gameObject);

        }
    }
}
