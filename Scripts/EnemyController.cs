using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CodeMonkey.HealthSystemCM;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject getHealthSystemGameObject;
    [SerializeField] private Animator animator;

    [Header("Drops")]
    public GameObject ammo;
    [Range(10, 50)]
    public int ammoDropChance = 30;
    public GameObject health;
    [Range(10, 50)]
    public int healthDropChance = 30;

    public GameObject powerup;
    [Range(5, 10)]
    public int powerupDropChance = 5;
    // health drop prefab needs to be given the tag "health" and referenced here in the HPcharacter Prefab.

    [Header("Gun")]
    public enemyGun gun;

    [Header("Navigation")]
    [Range(0, 50)]
    public int detectionDistance;
    [Range(0, 50)]
    public int weaponRange;
    [Range(0, 5)]
    public int speed;

    private GameObject goal;
    private HealthSystem healthSystem;
    private NavMeshAgent agent;
    private bool isDead = false;
    private Transform self;

    [Range(0, 12)]
    public float patrolRange = 10.0f;

    bool RandomPoint(float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (HealthSystem.TryGetHealthSystem(getHealthSystemGameObject, out HealthSystem system))
        {
            healthSystem = system;
        }

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.enabled = true;
        goal = GameObject.FindWithTag("Player");
        self = gameObject.transform;
    }

    void LookAtPlayer()
    {
        Transform target = goal.transform;

        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (healthSystem.IsDead())
        {
            if (!isDead)
            {
                gun.canFire = false;
                isDead = true;
                animator.SetTrigger("die");
                agent.enabled = false;
            }
            else
            {
                StartCoroutine(DestroyAfterDeath());
            }
            return;
        }

        Vector3 targetPos = goal.transform.position;
        Vector3 pos = self.position;
        Vector3 point;

        float distanceToTarget = Vector3.Distance(targetPos, pos);
        float heightDiff = targetPos.y - (pos.y + 0.5f);
        

        // navmeshagent and animation control.
        if (distanceToTarget <= weaponRange && Mathf.Abs(heightDiff) < 0.5f)
        {
            LookAtPlayer();
            gun.canFire = true;
            animator.SetBool("inRange", true);
            animator.SetBool("alert", true);
            agent.speed = 0;
        }
        else if (distanceToTarget <= detectionDistance)
        {
            gun.canFire = false;
            agent.speed = speed * 2;
            agent.destination = targetPos;
            animator.SetBool("alert", true);
            animator.SetBool("inRange", false);
        }
        else 
        {
            gun.canFire = false;
            if (RandomPoint(patrolRange, out point) && !agent.hasPath)
            {
                agent.destination = point;
            }
            animator.SetBool("alert", false);
            animator.SetBool("inRange", false);
        }

    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(3);

        int dropChance = Random.Range(0, 100);
        if (dropChance < ammoDropChance)
        {
            Instantiate(ammo, self.position, self.rotation);
        }
        else if (dropChance > (100 - healthDropChance))
        {
            Instantiate(health, self.position, self.rotation);
        } else
        {
            int powerUpChance = Random.Range(0, 100);
            if(powerUpChance < powerupDropChance)
            {
                Instantiate(powerup, self.position, self.rotation);
            }
        }
        Destroy(gameObject);
    }
}
