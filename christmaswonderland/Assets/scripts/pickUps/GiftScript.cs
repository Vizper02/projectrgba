using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScript : MonoBehaviour
{
    public Sprite[] sprites;
    public float speedIncrease;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playermovement player = other.gameObject.GetComponent<playermovement>();
            if (player != null)
            {
                player.increasePickUps(speedIncrease);
                Destroy(gameObject);
            }
        }
    }
}
