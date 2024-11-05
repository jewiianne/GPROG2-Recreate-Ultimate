using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    public GameObject clones;
    public GameObject player;
    public GameObject coolDownText;

    private bool areClonesActive = false;
    private bool isOnCooldown = false;
    private float cooldownTime = 15f;

    void Start()
    {
        clones.SetActive(false);
        player.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!areClonesActive && !isOnCooldown)
            {
                StartCoroutine(spawnClones());
                StartCoroutine(invisiblePlayer());
            }
            else if (isOnCooldown)
            {
                coolDownText.SetActive(true); 
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            coolDownText.SetActive(false); 
        }
    }

    IEnumerator invisiblePlayer()
    {
        player.SetActive(false);
        yield return new WaitForSeconds(1f);
        player.SetActive(true);
    }

    IEnumerator spawnClones()
    {
        areClonesActive = true;
        isOnCooldown = true;
        coolDownText.SetActive(false);

        clones.SetActive(true);

        yield return new WaitForSeconds(15f);
        yield return StartCoroutine(despawnClones());

        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
        coolDownText.SetActive(false);
    }

    IEnumerator despawnClones()
    {
        clones.SetActive(false);
        areClonesActive = false;

        yield return null;
    }
}
