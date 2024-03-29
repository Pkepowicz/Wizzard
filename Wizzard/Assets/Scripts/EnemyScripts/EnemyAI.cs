using UnityEngine;
// Note this line, if it is left out, the script won't know that the class 'Path' exists and it will throw compiler errors
// This line should always be present at the top of scripts which use pathfinding
using Pathfinding;

[HelpURL("http://arongranberg.com/astar/docs/class_partial1_1_1_astar_a_i.php")]
public class EnemyAI : Fighter
{
    
    
    public  int value;
    [Header("AI settings")] 
    public Transform targetPosition;

    [SerializeField] public Transform hand;

    public float maxHandTurnSpeed;

    private Seeker seeker;

    public Path path;

    public float nextWaypointDistance = 3; // Distance from waypoint that allows ai to mark it as reached

    private int currentWaypoint = 0;

    public float repathRate = 0.5f; // How often check for repath
    private float lastRepath = float.NegativeInfinity;

    public float stopRadius; // How far from target ai should stop

    public bool reachedEndOfPath;

    public Vector3 dir;

    protected override void Start()
    {
        targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
        base.Start();
        seeker = gameObject.GetComponent<Seeker>();
        targetPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();

        // Start a new path to the targetPosition, call the the OnPathComplete function
        // when the path has been calculated (which may take a few frames depending on the complexity)
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
    }

    public void FixedUpdate()
    {
        if (Time.time > lastRepath + repathRate && seeker.IsDone())
        {
            lastRepath = Time.time;
            // Start a new path to the targetPosition, call the the OnPathComplete function
            // when the path has been calculated (which may take a few frames depending on the complexity)
            seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
        }
        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }


        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        // Rotate hand into player direction
        if (hand != null)
        {
            hand.transform.up = Vector2.Lerp(hand.transform.up, targetPosition.transform.position - transform.position, maxHandTurnSpeed);
        }

        // Move only if target is outside of desired range
        if (Vector2.Distance(transform.position, targetPosition.position) >= stopRadius && canMove)
        {
            UpdateMotor(dir * speedFactor);
        } 
        // Setting animation to idle, setting to walking is done inside update motor
        else
        {
            anim.SetBool("moving", false);
        }
    }

    protected override void Death()
    {
        base.Death();
        ScoreManager.current.AddPoints(value);
        
    }
}