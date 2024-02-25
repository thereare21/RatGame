using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, GunModeInterface
{
    public GameObject shotgunPellet;

    [SerializeField]
    private float shotgunMaxCooldown = 0.5f;

    [Header("Bullet Stuff")]
    public float bulletSpeed = 15f;
    public int numBullets = 10;
    public float spread = 0.45f;

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
        maxCooldown = shotgunMaxCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = Mathf.Max(cooldown - Time.deltaTime, 0f);

    }

    void FireBullets()
    {
        for (int i = 0; i < numBullets; i++)
        {
            GameObject pellet = Instantiate(shotgunPellet, transform.GetChild(0).position, Quaternion.identity);

            Vector3 randomSpread = transform.forward +
                Random.Range(-spread, spread) * transform.right
                + Random.Range(-spread, spread) * transform.up;

            pellet.GetComponent<Rigidbody>().AddForce(bulletSpeed * randomSpread, ForceMode.VelocityChange);
        }
        
    }
}
