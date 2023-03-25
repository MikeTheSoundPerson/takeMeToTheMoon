using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class NPC : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter NPCTalk;

    public Player player;


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
            Debug.Log("Talk to player");
        }
    }

    void OnEnable()
    {
        player.OnTalk += TalkToPlayer;
        Debug.Log("test");
    }

    void OnDisable()
    {
        player.OnTalk -= TalkToPlayer;
    }
}
