using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class NPC : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter NPCTalk;

    public Player player;

    public List<GameObject> wantedOrbs = new List<GameObject>();

    public Action OnRecievedOrbs;


    public bool talking;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            talking = true;
            NPCTalk?.Play();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            talking = false;
        }
    }

    void TalkToPlayer(Orb orb)
    {
        if(talking)
        {
            if(wantedOrbs.Count == 0)
            {
                Debug.Log("No orbs wanted");
                return;
            }
            Orb wantedOrb = wantedOrbs?[0]?.GetComponent<Orb>();

            if(wantedOrb?.name == orb?.name)
            {
                
                wantedOrbs.RemoveAt(0);
                if(wantedOrbs.Count == 0)
                {
                    Debug.Log("All orbs collected");
                    OnRecievedOrbs?.Invoke();
                }
            }
            else
            {
                Debug.Log("Wrong orb");
            }
            
            
        }
    }

    void OnEnable()
    {
        player.OnTalk += TalkToPlayer;
    }

    void OnDisable()
    {
        player.OnTalk -= TalkToPlayer;
    }
}
