using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;


public class Orb : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter collectOrb;
    public string orbName;
    public GameObject player;
    private Color startColor;

    private bool isClicked = false;
    private bool isFollowing = false;

    public Action<string> OnOrbClicked;
    public Action<string> OnOrbCollected;


    void Start()
    {
        player = GameObject.Find("Player");
    }
    void OnMouseEnter()
    {
        if (!isClicked && isFollowing)
        {
            startColor = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void OnMouseExit()
    {
        if (!isClicked && isFollowing)
        GetComponent<SpriteRenderer>().color = startColor;
    }

    void OnMouseDown()
    {
        if(isFollowing)
        {
            OnOrbClicked?.Invoke(name);
            StartCoroutine(OnClick());
        }
    }

    IEnumerator OnClick()
    {
        isClicked = true;
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = startColor;
        isClicked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !isFollowing)
        {
            OnOrbCollected?.Invoke(name);
            isFollowing = true;
            
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        
        FollowPlayer followPlayer = GetComponent<FollowPlayer>();
        followPlayer.number = player.GetComponent<Player>().CollectOrb(this);
        followPlayer.enabled = true;
        
        
    }
}
