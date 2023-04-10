using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    [SerializeField] int pointsForCoinPickup = 10;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
