using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnimator;
    Transform playerPos;
    NavMeshAgent playerAgent;
    public GameObject ClickTarget;
    private Vector3 previousPosition;
    public float defSpeed = 4f;
    public float speed;

    void Start()
    {
        previousPosition = transform.position;
        playerPos = GetComponent<Transform>();
        playerAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        speed = (transform.position - previousPosition).magnitude / Time.deltaTime;
        previousPosition = transform.position;

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
    
            if (Physics.Raycast(ray, out hit))
            {
                GameObject target = Instantiate(ClickTarget, hit.point, Quaternion.identity);
                target.GetComponent<Renderer>().material.color = Color.red;
                Destroy(target, 1f);
                playerAgent.SetDestination(target.transform.position);
            }
        }

        if (speed < 0.1f)
            {
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isRunning", false);
            } else if ( speed > 0.1f)
            {
                playerAnimator.SetBool("isWalking", true);
            } if(speed > 3.1f)
            {
                playerAnimator.SetBool("isWalking", false);
                playerAnimator.SetBool("isRunning", true);
            } if(speed < 3.1f && speed > 0.1f)
            {
                playerAnimator.SetBool("isWalking", true);
                playerAnimator.SetBool("isRunning", false);
            }
    }
}
