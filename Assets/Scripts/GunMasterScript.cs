using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunMasterScript : MonoBehaviour
{
    public Animator gunAnimator;

    public Slider slider;
    public GameObject sliderBackground;
    public GameObject sliderFill;



    public enum GunMode
    {
        PISTOL,
        SHOTGUN,
        BOMB
    }

    public GameObject gunObject;

    //make sure all data structures are the same length!

    [SerializeField]
    private Color[] colors;

    [SerializeField]
    private GunModeInterface[] gunModes;

    [SerializeField]
    private GunMode mode;

    [SerializeField]
    private int modesUnlocked;


    private ParticleSystem gunParticleSystem;
    private Material particleMaterial;
    

    



    // Start is called before the first frame update
    void Start()
    {
        modesUnlocked = 0;

        particleMaterial = gunObject.GetComponent<ParticleSystem>().GetComponent<Renderer>().material;
        particleMaterial.color = colors[(int)mode];

        gunModes = new GunModeInterface[3];
        gunModes[0] = GetComponent<Pistol>();
        gunModes[1] = GetComponent<Shotgun>();

        slider.value = 0;

        
    }

    // Update is called once per frame
    void Update()
    {

        //numerical inputs
        if (Input.GetKeyDown(KeyCode.Alpha1) && modesUnlocked >= 0) {
            mode = GunMode.PISTOL;
            Debug.Log(mode);
            Debug.Log((int)mode);
            particleMaterial.color = colors[(int)mode];
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && modesUnlocked >= 1) {
            mode = GunMode.SHOTGUN;
            Debug.Log(mode);
            Debug.Log((int)mode);
            particleMaterial.color = colors[(int)mode];
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && modesUnlocked >= 2)
        {
            mode = GunMode.BOMB;
            Debug.Log(mode);
            Debug.Log((int)mode);
            particleMaterial.color = colors[(int)mode];
        }

        //scroll wheel input

        // Get the scroll wheel movement
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        // Check if the scroll wheel moved
        if (scrollWheel != 0f)
        {
            // Output the scroll wheel movement
            //Debug.Log("Scroll Wheel Movement: " + scrollWheel);

            if ((int)mode == modesUnlocked)
            {
                mode = 0;
            } else
            {
                mode += 1;
            }

            particleMaterial.color = colors[(int)mode];
        }

        GunModeInterface currentGunMode = gunModes[(int)mode];


        //FIRE IN THE HOLEEEEE
        if (Input.GetButtonDown("Fire1"))
        {
            
            if (currentGunMode.cooldown < 0.02f)
            {
                currentGunMode.fireInTheHole();
                gunAnimator.SetTrigger("FireInTheHole");
                Debug.Log("fired in the hole");
                StartCoroutine(ShootAnimation());
                
            }
            else
            {
                //handle invalid firing
            }
        }

        //update chargeup UI

        
        Color selectedColor = colors[(int)mode];
        selectedColor.a = 1f;
        //float brightFactor = 5f;
        //Color bgColor = new Color(selectedColor.r + brightFactor,
        //    selectedColor.g + brightFactor,
        //    selectedColor.b + brightFactor);

        //sliderBackground.GetComponent<Image>().color = bgColor;

        Image sliderFillImage = sliderFill.GetComponent<Image>();

        sliderFillImage.color = selectedColor;

        slider.value = 1 - (currentGunMode.cooldown / currentGunMode.maxCooldown);

        //ebug.Log("slider value: " + slider.value);
        



    }

    IEnumerator ShootAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("return to idle");
        gunAnimator.SetTrigger("ReturnToIdle");

    }
}
