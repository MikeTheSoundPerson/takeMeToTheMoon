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

    void Start()
    {
        player = GameObject.Find("Player")?.GetComponent<Player>();
        DisplayOrbs();
    }
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
                DisplayOrbs();
                
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

    private void DisplayOrbs()
    {

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int i = 0;
        foreach(GameObject orb in wantedOrbs)
        {
            DisplayOrb(orb.GetComponent<Orb>(), new Vector2(i-1, 1));
            i++;
        }
    }

    private void DisplayOrb(Orb orb, Vector2 offset)
    {
        GameObject displayOrb = new GameObject();
        displayOrb.name = orb.orbName;
        displayOrb.AddComponent<SpriteRenderer>();
        displayOrb.GetComponent<SpriteRenderer>().sprite = orb.orbSprite;

        displayOrb.transform.position = transform.position + (Vector3)offset;
        displayOrb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        displayOrb.transform.parent = transform;
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
