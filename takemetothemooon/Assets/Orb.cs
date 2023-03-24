using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Orb : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter collectOrb;
    public string orbName;
    public GameObject player;
    private Color startColor;

    private bool isClicked = false;
    private bool isFollowing = false;

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
        if(isFollowing){
            StartCoroutine(OnClick());
        }
    }

    IEnumerator OnClick()
    {
        isClicked = true;
        player.GetComponent<Player>().Talk(orbName);
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = startColor;
        isClicked = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" && !isFollowing)
        {
            isFollowing = true;
            if(collectOrb != null)
                collectOrb.Play();

            player.GetComponent<PlayerInformation>().numberOfOrbs++;
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        FollowPlayer followPlayer = GetComponent<FollowPlayer>();
        followPlayer.enabled = true;
        followPlayer.number = player.GetComponent<PlayerInformation>().numberOfOrbs;
        
    }
}
