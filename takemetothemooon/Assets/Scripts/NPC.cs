using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class NPC : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter NPCTalk;
    public bool talking;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            talking = true;
            //only play if reference is set
            if (NPCTalk != null)
                NPCTalk.Play();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            talking = false;
        }
    }
}
