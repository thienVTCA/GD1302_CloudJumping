using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag.Equals("playerAttack"))
        {
            var playerControllerAttack = other.gameObject.GetComponentInParent<PlayerController>();
            //Debug.Log("player take score");
            if (playerControllerAttack != null)
            {
                playerControllerAttack.AddScore();
            }
            //add for player score and destroy enemy
            Destroy(gameObject);
        }
        var playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            //Debug.Log("Player take dam");
            playerController.SubtractHealth(1);
            Destroy(gameObject);
        }
        if (other.tag.Equals("wallend"))
        {
            //add for player score and destroy enemy
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
    }
}
