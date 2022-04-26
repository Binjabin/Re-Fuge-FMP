using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float thrustSpeed;
    Rigidbody rb;
    Vector3 targetDirection;
    [SerializeField] float maxIdealRange;
    [SerializeField] float minIdealRange;
    [SerializeField] float speedLimit;
    [SerializeField] float comfortableSpeed;
    bool breaking;
    bool attacking;

    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    GameObject trackingObject;
    public float radarDistance;

    public List<Transform> visibleTargets = new List<Transform>();

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        Debug.Log(targetsInViewRadius);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(target.position, transform.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    if (target.gameObject.tag == "Player")
                    {
                        visibleTargets.Add(target);
                    }
                            
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public Vector3 DirFromAngle(float angleInDeg, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindVisibleTargets();

        if (visibleTargets.Count > 0f)
        {
            trackingObject = visibleTargets[0].gameObject;
            Debug.Log("Found Player");
        }

        if (trackingObject != null)
        {

            if (Vector3.Distance(trackingObject.transform.position, transform.position) > radarDistance)
            {
                Debug.Log("Lost Player");
                trackingObject = null;
            }
            else
            {
                Transform player = trackingObject.transform;
                FaceTarget(player);
                if (Vector3.Dot(transform.forward, targetDirection) > 0.75f)
                {
                    if (targetDirection.magnitude < minIdealRange)
                    {
                        breaking = true;
                    }
                    else if (targetDirection.magnitude > maxIdealRange)
                    {
                        ThrustForwards();
                    }

                }
                if (rb.velocity.magnitude > speedLimit)
                {
                    breaking = true;
                    attacking = true;
                }
                if (breaking)
                {
                    attacking = false;
                    rb.AddForce(-transform.forward * thrustSpeed * 0.5f);
                    if (rb.velocity.magnitude < comfortableSpeed && targetDirection.magnitude > minIdealRange)
                    {
                        breaking = false;
                    }
                }
                else
                {
                    attacking = true;

                }
            }

            
        }
    }   
    void FaceTarget(Transform target)
    {
        targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0f;
        float rotationStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void ThrustForwards()
    {
        rb.AddForce(transform.forward * thrustSpeed);
    }
}
