using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController2 : MonoBehaviour
{

    //public Transform targetObjpos;
    //public float updateInterval;
    //public float updateInterval2;
    //public int speed;
    //private double lastInterval;
    //private double lastInterval2;
    public Transform targetObj;

    public Animator animator;
    public AudioSource footsteps;
    public AudioSource triggerSound;
    public AudioSource triggerStart;
    public AudioSource triggerEnd;
    private NavMeshAgent nav;
    public bool enemyChase;
    private Vector3 navStart;
    public Transform[] patrolPoints;
    public bool hiding;

    public int targetPoint;
    public float viewRadius;
    public float viewAngle;

    public LayerMask targetPlayer;
    public LayerMask obstacleMask;
    public float hearRadius;
    private PlayerMovement playerScript;
    public EnemyRaycast raycastScript;
    private bool moving;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = targetObj.GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        enemyChase = false;
        nav = GetComponent<NavMeshAgent>();
        moving = true;
        triggerSound.Stop();
        triggerStart.Stop();
        triggerEnd.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (moving == false)
        {
            animator.SetBool("Walking", false);
        }
        else
        {
            animator.SetBool("Walking", true);
        }
        PlaySound();
        if (hiding == false) PlayerDetection();
        if (moving == false && enemyChase == false)
        {
            nav.speed = 0;
        }
        else
            nav.speed = 3.5f;

        if (enemyChase == true)
        {
            nav.SetDestination(targetObj.position);
        }
        else
        {
            if (nav.transform.position == new Vector3(patrolPoints[targetPoint].position.x, transform.position.y, patrolPoints[targetPoint].position.z))
            {
                StartCoroutine(EnemyWaiting());
                increaseTargetInt();
            }
            nav.SetDestination(patrolPoints[targetPoint].position);
        }


    }
    void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
            targetPoint = 0;
    }
    public void stopChase()
    {
        raycastScript.monsterAwareness = 0;
        enemyChase = false;
        StartCoroutine(QuietLong(triggerSound));

    }
    public void startChase()
    {
        //triggerSound.Play(); REMEMBER THIS SCARY
        StartCoroutine(PlayLong(triggerSound));
        raycastScript.monsterAwareness = raycastScript.awarenessCap / 2;
        enemyChase = true;
    }

    void PlaySound()
    {
        if (nav.speed > 0)
        {
            StartCoroutine(Play(footsteps));
        }
        else
        {
            StartCoroutine(Quiet(footsteps));
        }

    }
    void PlayerDetection()
    {
        Vector3 playerTarget = (targetObj.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetObj.transform.position);
            if (distanceToTarget < viewRadius)
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacleMask) == false)
                {
                    startChase();
                }
            }
        }
        float hearDistance = Vector3.Distance(transform.position, targetObj.transform.position);
        if ((playerScript.footsteps.isPlaying || playerScript.running.isPlaying) && hearDistance < hearRadius)
        {

            startChase();
        }
    }

    //IEnumerators
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///
    IEnumerator EnemyWaiting()
    {
        moving = false;
        yield return new WaitForSeconds(3);
        moving = true;
    }
    IEnumerator Play(AudioSource A)
    {
        float defaultVolume = 1;
        float percentage = 0;
        float transitionTime = 0.25f;

        if (A.isPlaying == false) A.Play();
        A.UnPause();
        while (A.volume < defaultVolume)
        {
            A.volume = Mathf.Lerp(0, defaultVolume, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
    }
    IEnumerator Quiet(AudioSource A)
    {
        float defaultVolume = 1;
        float percentage = 0;
        float transitionTime = 0.50f;

        while (A.volume > 0)
        {
            A.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
        A.volume = 0;
        A.Pause();
    }
    IEnumerator PlayLong(AudioSource A)
    {

        float defaultVolume = 1;
        float percentage = 0;
        float transitionTime = 2f;

        if (A.isPlaying == false) { triggerStart.Play(); A.Play(); }
        A.UnPause();
        while (A.volume < defaultVolume)
        {
            A.volume = Mathf.Lerp(0, defaultVolume, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
    }

    IEnumerator QuietLong(AudioSource A)
    {
        float defaultVolume = 1;
        float percentage = 0;
        float transitionTime = 5f;

        triggerEnd.Play();
        while (A.volume > 0)
        {
            A.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }

        A.Stop();
        A.volume = 1;
    }

}

