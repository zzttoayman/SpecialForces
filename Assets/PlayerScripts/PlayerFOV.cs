using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFOV : MonoBehaviour
{
    public float viewRadius = 15f;
    float viewAngle = 360f;

    public LayerMask ObstacleLayer;
    public LayerMask EnemyLayer;
    public List<Transform> visibleTargets;
    public float meshResolution;

    public float edgeDstThreshold;

    public int edgeResolveIterations;
    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    void LateUpdate()
    {
        DrawFieldOfView();  
    }

    IEnumerator FindTargetsWithDelay(float delay){
        while(true)
        {
            yield return new WaitForSeconds (delay);
            FindVisibleTargets();
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, EnemyLayer);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius [i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleLayer))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int StepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / StepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= StepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle/2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if(i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if(oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if(edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);                        
                    }
                    if(edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.point);
            oldViewCast = ViewCast(angle);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertice = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertice[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertice[i+1] = transform.InverseTransformPoint(viewPoints[i]);

            if(i < vertexCount - 2)
            {
                triangles[i*3] = 0;
                triangles[i*3+1] = i+1;
                triangles[i*3+2] = i+2;  
            }
            
        }

        viewMesh.Clear();

        viewMesh.vertices = vertice;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle)/2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if(newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            } else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, viewRadius, ObstacleLayer))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        } else 
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo 
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
