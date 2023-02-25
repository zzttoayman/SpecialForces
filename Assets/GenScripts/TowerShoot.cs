using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    public float towerFov = 15f;
    float maxTargetDist = 15f;
    public Transform playerTransform;
    public LayerMask playerMask;
    Vector3 lastPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPos = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPlayerPos = playerTransform.position;
        lastPlayerPos = currentPlayerPos;
        Vector3 playerNewPos = currentPlayerPos - lastPlayerPos;

        //if(Physics.SphereCast(transform.position, towerFov, playerDir, out RaycastHit hit, maxTargetDist, playerMask)){}    
    }
}
