using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, GunModeInterface
{

    public GameObject pistolPellet;

    [Header("Bullet Physics")]
    public float bulletSpeed = 15f;

    public float cooldown { get; set; }
    public float maxCooldown { get; set; }


    public void fireInTheHole()
    {
        Debug.Log("fire in the hole!");

        FireBullets();

        cooldown = maxCooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        maxCooldown = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = Mathf.Max(cooldown - Time.deltaTime, 0f);

    }

    void FireBullets()
    {
        GameObject pellet = Instantiate(pistolPellet, transform.GetChild(0).position, Quaternion.identity);



        pellet.GetComponent<Rigidbody>().AddForce(bulletSpeed * transform.forward, ForceMode.VelocityChange);
    }

    
}
