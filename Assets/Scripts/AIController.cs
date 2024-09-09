using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    NavMeshAgent nav;
    [SerializeField]
    GameObject targetObj;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(targetObj.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
