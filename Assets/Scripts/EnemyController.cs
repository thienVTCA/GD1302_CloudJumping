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
        if(other.tag.Equals("playerAttack"))
        {
            //add for player score and destroy enemy
            Destroy(gameObject);
        }
        Debug.Log("Player take dam");
        var playerController = other.gameObject.GetComponent<PlayerController>();
        if(playerController != null)
        {
            playerController.SubtractHealth(1);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
    }
}
