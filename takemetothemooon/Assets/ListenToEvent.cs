using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToEvent : MonoBehaviour
{
    public NPC npc;

    public GameObject[] objectsToActivate;
    
    void OnEnable()
    {
        npc.OnRecievedOrbs += Activate;
    }

    void OnDisable()
    {
        npc.OnRecievedOrbs -= Activate;
    }

    void Activate()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}
