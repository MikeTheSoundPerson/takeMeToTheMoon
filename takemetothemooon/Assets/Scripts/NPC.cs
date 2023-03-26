using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class NPC : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter NPCGreenRequest;
    [SerializeField] private StudioEventEmitter NPCGreenThanks;
    [SerializeField] private StudioEventEmitter NPCRedRequest;
    [SerializeField] private StudioEventEmitter NPCRedThanks;
    [SerializeField] private StudioEventEmitter NPCOrangeRequest;
    [SerializeField] private StudioEventEmitter NPCOrangeThanks;
    [SerializeField] private StudioEventEmitter NPCBlueRequest;
    [SerializeField] private StudioEventEmitter NPCBlueThanks;

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
            NPCGreenRequest.Play();
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
            DisplayOrb(orb.GetComponent<Orb>(), new Vector2(i-1, 2), i);
            i++;
        }
    }

    private void DisplayOrb(Orb orb, Vector2 offset, int index = 0)
    {
        GameObject displayOrb = new GameObject();
        displayOrb.name = orb.orbName;
        displayOrb.AddComponent<SpriteRenderer>();
        Wobble wobble = displayOrb.AddComponent<Wobble>();
        wobble.offset = new Vector2(0, 0.4f);
        wobble.index = index;
        displayOrb.AddComponent<Rigidbody2D>();
        displayOrb.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        displayOrb.GetComponent<Rigidbody2D>().gravityScale = 0;
        displayOrb.GetComponent<Rigidbody2D>().freezeRotation = true;
        displayOrb.GetComponent<SpriteRenderer>().sprite = orb.orbSprite;
        displayOrb.GetComponent<SpriteRenderer>().sortingOrder = 10;

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
