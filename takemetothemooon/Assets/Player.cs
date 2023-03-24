using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Player : MonoBehaviour
{
    

  

    public bool talking = false;
    public void Talk(string orbName)
    {
        if(talking)
        {
            Debug.Log(orbName);
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


}
