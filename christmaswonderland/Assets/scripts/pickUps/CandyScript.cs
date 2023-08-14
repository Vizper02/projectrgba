using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playermovement player = other.gameObject.GetComponent<playermovement>();
            if(player != null)
            {
                if(player.life < 3)
                {
                    player.life++;
                    Destroy(gameObject);
                }
            }
        }
    }
}
