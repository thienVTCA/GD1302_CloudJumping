using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 10;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("col name " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger col name " + other.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        var movement = new Vector3(h, 0, v);
        characterController.Move(movement*moveSpeed*Time.deltaTime);
    }
}
