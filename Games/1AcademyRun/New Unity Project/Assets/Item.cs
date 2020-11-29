using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"OnTriggerEnter2D {collision.gameObject.name} - {collision.gameObject.layer}");

        if(collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log($"OnTriggerExit2D {collision.gameObject.name}");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"OnTriggerStay2D {collision.gameObject.name}");
    }
}
