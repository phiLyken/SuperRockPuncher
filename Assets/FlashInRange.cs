using UnityEngine;
using System.Collections;

public class FlashInRange : MonoBehaviour
{


    bool flashed = false;

    public GameObject Flash_Object;

    Player player;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }


    void Update()
    {
        if (!flashed && player != null)
        {
            GameObject obstacle;
            if (player.CanAttack(out obstacle))
            {
                if (obstacle == gameObject)
                {
                    flashed = true;

                    if (!player.IsInBooth)
                    {
                        Flash_Object.SetActive(true);
                    }
                }
            }
        }
    }
}
