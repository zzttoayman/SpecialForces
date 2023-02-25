using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinAI : MonoBehaviour
{
    GameObject target;
    NavMeshAgent goblin;

    // Start is called before the first frame update
    void Start()
    { 
        target = GameObject.FindGameObjectWithTag("Player");
        goblin = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        goblin.SetDestination(target.transform.position);
    }
}
