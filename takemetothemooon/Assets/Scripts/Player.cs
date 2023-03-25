using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class Player : MonoBehaviour
{
    

  

    public bool talking = false;

    public Action<Orb> OnTalk;

    private PlayerInformation playerInformation;

    private void Start()
    {
        playerInformation = GetComponent<PlayerInformation>();
    }
    public void Talk(Orb orb)
    {
        OnTalk?.Invoke(orb);
        if(talking)
        {
            Debug.Log(orb.name);
        }
        else
        {
            Debug.Log("No one to talk to");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "NPC")
        {
            talking = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "NPC")
        {
            talking = false;
        }
    }

    public int CollectOrb(Orb orb)
    {
        playerInformation.AddOrb();
        orb.OnOrbClicked += Talk;
        Debug.Log("Collected Orb");
        return playerInformation.numberOfOrbs;
    }

    


}
