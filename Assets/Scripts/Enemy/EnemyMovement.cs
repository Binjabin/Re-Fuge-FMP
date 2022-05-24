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
    GameObject trackingPlayer;
    public float radarDistance;

    public List<Transform> visibleTargets = new List<Transform>();
    public List<Transform> invisibleTargets = new List<Transform>();

    [SerializeField] GameObject weapons;
    ParticleSystem[] weaponParticles;
    bool collided;
    Transform player;
    public List<Vector3> patrolPoints;
    int patrolIndex;
    bool avoiding;
    Vector3 currentAim;

    [SerializeField] bool isHeavy;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] float missileAttackTime;
    float timeSinceMissile;

    public bool isTracking;
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        invisibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

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
                        if(!target.GetComponent<PlayerMovement>().inactive)
                        {
                            if (!target.gameObject.GetComponent<Stealth>().stealthOn)
                            {
                                visibleTargets.Add(target);
                            }
                            else
                            {
                                invisibleTargets.Add(target);
                            }
                        }
                        
                    }
                            
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        weaponParticles = weapons.GetComponentsInChildren<ParticleSystem>();
        patrolIndex = 0;
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
        isTracking = trackingPlayer != null;
        if (visibleTargets.Count > 0f)
        {
            trackingPlayer = visibleTargets[0].gameObject;
        }
        if (trackingPlayer != null)
        {
            currentAim = trackingPlayer.transform.position;
        }
        else
        {
            currentAim = patrolPoints[patrolIndex];
        }
        RaycastHit hit;
        Vector3 aimDir = (currentAim - transform.position).normalized;
        Debug.DrawRay(transform.position, aimDir * maxIdealRange, Color.red);
        if (Physics.Raycast(transform.position, aimDir, out hit, maxIdealRange) && hit.transform.gameObject.tag != "player")
        {
            GameObject avoidingObject = hit.transform.gameObject;
            Vector3 avoidanceDirection = Quaternion.AngleAxis(90, Vector3.up) * aimDir;
            Debug.DrawRay(transform.position, avoidanceDirection * maxIdealRange, Color.green);
            FaceDirection(avoidanceDirection);
            if (Vector3.Dot(transform.forward, avoidanceDirection) > 0.75f)
            {
                ThrustForwards();
                breaking = false;
            }
            else
            {
                breaking = true;
            }


            if (breaking)
            {
                rb.drag = 3.0f;
                if (rb.velocity.magnitude < comfortableSpeed && targetDirection.magnitude > minIdealRange)
                {
                    breaking = false;
                }
            }
            else
            {
                rb.drag = 0.1f;
            }
        }

        else if (trackingPlayer != null)
        {
            if(trackingPlayer.tag == "Player")
            {
                
                float distThisFrame = Vector3.Distance(trackingPlayer.transform.position, transform.position);
                if(invisibleTargets.Count > 0){collided = false;}
                if (distThisFrame > radarDistance)
                {
                    trackingPlayer = null;
                    
                }
                else if(invisibleTargets.Count < 1f && trackingPlayer.GetComponent<Stealth>().stealthOn && !collided)
                {
                    trackingPlayer = null;
                    
                }
                else
                {
                    
                    player = trackingPlayer.transform;
                    FaceTarget(player.position);
                    if (Vector3.Dot(transform.forward, targetDirection.normalized) > 0.75f)
                    {
                        if (targetDirection.magnitude < minIdealRange)
                        {
                            attacking = false;
                            rb.AddForce(-transform.forward * thrustSpeed);
                        }
                        else if (targetDirection.magnitude > maxIdealRange)
                        {
                            attacking = false;
                            if(Vector3.Dot(rb.velocity.normalized, targetDirection.normalized) > 0.75f)
                            {
                                if(rb.velocity.magnitude < comfortableSpeed)
                                {
                                    ThrustForwards();
                                }
                            }
                            else
                            {
                                ThrustForwards();
                            }
                            
                            
                        }
                        else
                        {
                            attacking = true;
                        }
                    }
                    if (rb.velocity.magnitude > speedLimit)
                    {
                        breaking = true;
                        attacking = false;
                    }
                    if (breaking)
                    {
                        rb.drag = 3.0f;
                        if (rb.velocity.magnitude < comfortableSpeed && targetDirection.magnitude > minIdealRange)
                        {
                            breaking = false;
                        }
                    }
                    else
                    {
                        rb.drag = 0.1f;
                    }
                }
            }
        }
        else
        {
            attacking = false;
            
            Vector3 point = patrolPoints[patrolIndex];
            FaceTarget(point);
            if (Vector3.Dot(transform.forward, targetDirection.normalized) > 0.75f)
            {
                if (targetDirection.magnitude > 10f)
                {
                    breaking = false;
                    if(Vector3.Dot(rb.velocity.normalized, targetDirection.normalized) > 0.3f)
                    {
                        breaking = false;
                        if(rb.velocity.magnitude < comfortableSpeed)
                        {
                            ThrustForwards();
                        }
                    }
                    else
                    {
                        ThrustForwards();
                        breaking = true;
                    }
                    
                    
                }
                else if(targetDirection.magnitude < 10f)
                {
                    breaking = true;
                    patrolIndex++;
                    if(patrolIndex >= patrolPoints.Count){patrolIndex = 0;}
                }
                else
                {
                    breaking = true;
                    if(Vector3.Dot(rb.velocity.normalized, targetDirection.normalized) > 0.75f)
                    {
                        if(rb.velocity.magnitude < comfortableSpeed)
                        {
                            ThrustForwards();
                        }
                    }
                    else
                    {
                        ThrustForwards();
                        
                    }
                }
            }
            if (rb.velocity.magnitude > speedLimit)
            {
                breaking = true;
                attacking = false;
            }
            if (breaking)
            {
                rb.drag = 3.0f;
                if (rb.velocity.magnitude < comfortableSpeed && targetDirection.magnitude > minIdealRange)
                {
                    breaking = false;
                }
            }
            else
            {
                rb.drag = 0.1f;
            }
            
        }
            
        
        
        if(attacking)
        {
            for (int i = 0; i < weaponParticles.Length; i++)
            {
                if (!weaponParticles[i].isPlaying)
                {
                    weaponParticles[i].Play();
                }
            }
            if(isHeavy)
            {
                timeSinceMissile += Time.deltaTime;
                if(timeSinceMissile > missileAttackTime)
                {
                    Debug.Log("missile");
                    Vector3 instantiatePos = transform.position + transform.forward * 2f;
                    GameObject obj = Instantiate(missilePrefab, instantiatePos, Quaternion.identity);
                    obj.GetComponent<Rigidbody>().velocity = rb.velocity;
                    timeSinceMissile = 0f;
                }
            }

        }
        else
        {
            for(int i = 0; i < weaponParticles.Length; i++)
            {
                if(weaponParticles[i].isPlaying)
                {
                    weaponParticles[i].Stop();
                }
                
            }
        }
    }   
    void FaceTarget(Vector3 target)
    {
        targetDirection = target - transform.position;
        targetDirection.y = 0f;
        float rotationStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void FaceDirection(Vector3 direction)
    {
        float rotationStep = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void ThrustForwards()
    {
        rb.AddForce(transform.forward * thrustSpeed);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            trackingPlayer = other.gameObject;
            collided = true;
        }
    }
}
