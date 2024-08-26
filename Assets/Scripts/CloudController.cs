using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField]
    Rigidbody mRigidbody;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("player out");
            mRigidbody.velocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
