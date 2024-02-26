using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheeseBehavior : MonoBehaviour
{
    public static float cheeseHealth;

    public float startingCheeseHealth = 100f;

    public float damagePerRat = 0.5f;

    public float damageTimeInterval = 1f;

    [SerializeField]
    private float scanRadius = 2f;

    private float damageTimer;

    private Slider cheeseSlider;

    // Start is called before the first frame update
    void Start()
    {
        cheeseHealth = startingCheeseHealth;
        damageTimer = 0f;

        cheeseSlider = GetComponentInChildren<Slider>();
        cheeseSlider.value = (cheeseHealth / startingCheeseHealth);

    }

    // Update is called once per frame
    void Update()
    {
        //count number of rats in its vicinity every interval
        //lose specific health points based on how many rats are in the collider trigger

        damageTimer += Time.deltaTime;

        if (damageTimer >= damageTimeInterval)
        {
            cheeseHealth -= damagePerRat * ScanForRats();
            damageTimer = 0f;
            //Debug.Log("cheese health: " + cheeseHealth);
        }

        if (cheeseHealth <= 0)
        {
            //game over handle
            //Debug.Log("GAME OVER THE RATS HAVE WON THE RATS HAVE WON");
        }

        cheeseSlider.value = cheeseHealth / startingCheeseHealth;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            RectTransform canvas = GetComponentInChildren<RectTransform>();

            canvas.LookAt(player.transform);
        }



    }

    int ScanForRats()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, scanRadius);

        int ratCountInVicinity = 0;
        
        foreach (Collider collider in colliders)
        {
            // Get the GameObject associated with the collider
            GameObject obj = collider.gameObject;

            if (obj.CompareTag("Rat"))
            {
                ratCountInVicinity++;
            }

            // Do something with the GameObject (e.g., access its components, apply logic)
            Debug.Log("Found object: " + obj.name);
            
        }
        return ratCountInVicinity;


    }

}
