using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player is near");
        }
    }
}
