using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GunModeInterface
{

    //cooldown - when 0, ready to fire, when > 0, not ready
    public float cooldown { get; set; }

    //max value the cooldown can be (right after it fires)
    public float maxCooldown { get; set; }


    //function to run fire method
    public abstract void fireInTheHole();
}
