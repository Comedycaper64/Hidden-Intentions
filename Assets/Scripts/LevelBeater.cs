using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBeater : MonoBehaviour
{
    [SerializeField] GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            winScreen.SetActive(true);
            collision.GetComponent<PlayerMovement>().canMove = false;
        }
    }
}
