using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private Color startColor;

    private bool isClicked = false;

    void OnMouseEnter()
    {
        if (!isClicked)
        {
        startColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    void OnMouseExit()
    {
        if (!isClicked)
        GetComponent<SpriteRenderer>().color = startColor;
    }

    void OnMouseDown()
    {
        StartCoroutine(OnClick());
    }

    IEnumerator OnClick()
    {
        isClicked = true;
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().color = startColor;
        isClicked = false;
    }
}
