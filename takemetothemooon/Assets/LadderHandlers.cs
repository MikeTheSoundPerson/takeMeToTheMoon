using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHandlers : MonoBehaviour
{
    void Awake()
    {
        float width = GetComponent<BoxCollider2D>().size.x;
        float height = GetComponent<BoxCollider2D>().size.y;
        Transform topHandler = transform.GetChild(0).transform;
        Transform bottomHandler = transform.GetChild(1).transform;
        topHandler.position = new Vector3(transform.position.x, transform.position.y + height / 2, 0);
        bottomHandler.position = new Vector3(transform.position.x, transform.position.y - height / 2, 0);
        GetComponent<BoxCollider2D>().offset = Vector2.zero;
        GetComponent<BoxCollider2D>().size = new Vector2(width, height);

    }
}
