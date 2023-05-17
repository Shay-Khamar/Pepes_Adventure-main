using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currentCheckpoint;
    private PlayerMovement player;
    // Start is called before the first frame update
    public float respawnDelay;

    public HealthManager healthManager;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        healthManager = FindObjectOfType<HealthManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer()
    {

        StartCoroutine("RespawnPlayerCo");
    }

    public IEnumerator RespawnPlayerCo()
    {

         Vector2 originalVelocity = player.PlayerRigidbody.velocity;
        player.PlayerRigidbody.velocity = Vector2.zero;
        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;
        Debug.Log("Player Respawned");
        player.transform.position = currentCheckpoint.transform.position;
        yield return new WaitForSeconds(respawnDelay);
        player.PlayerRigidbody.velocity = originalVelocity;
        healthManager.FullHealth();
        healthManager.isDead = false;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;

    }
    
}
