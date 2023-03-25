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

    public Sprite orbSprite;
    public GameObject orbLight;

    private bool isClicked = false;
    private bool isFollowing = false;

    public Action<Orb> OnOrbClicked;
    public Action<Orb> OnOrbCollected;


    void Start()
    {
        player = GameObject.Find("Player");
        orbSprite = GetComponent<SpriteRenderer>()?.sprite;
    }
    void OnMouseEnter()
    {
        if (!isClicked && isFollowing)
        {
            startColor = GetComponent<SpriteRenderer>().color;
            orbLight.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (!isClicked && isFollowing)
        orbLight.SetActive(false);
    }

    void OnMouseDown()
    {
        if(isFollowing)
        {
            OnOrbClicked?.Invoke(this);
            StartCoroutine(OnClick());
        }
    }

    IEnumerator OnClick()
    {
        isClicked = true;
        //GetComponent<SpriteRenderer>().color = Color.green;
        orbLight.SetActive(true);
        yield return new WaitForSeconds(1f);
        //GetComponent<SpriteRenderer>().color = startColor;
        orbLight.SetActive(false);
        isClicked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !isFollowing)
        {
            OnOrbCollected?.Invoke(this);
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
