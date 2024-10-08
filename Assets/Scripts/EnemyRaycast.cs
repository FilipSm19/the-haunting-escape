using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{
    public int rayLength = 5;
    public LayerMask layerMaskInteract;
    public EnemyController enemyScript;
    public Transform enemy;
    private bool couroutineBool = false;
    private bool cooldownBool = false;
    public int monsterAwareness = 0;
    public int awarenessCap = 20;
    public LayerMask obstacleMask;
    public float viewAngle;
    public float viewRadius;
    public EnemyTrigger enemytrigger;

    void Update()
    { /*Vector3 mousePostion = Input.mousePosition;
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * rayLength, Color.green);*/
        if (enemytrigger.triggered)
        {
            Vector3 enemyTarget = (enemy.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, enemyTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToTarget < viewRadius)
                {

                    if (Physics.Raycast(transform.position, enemyTarget, distanceToTarget, obstacleMask) == false)
                    {
                        if (couroutineBool == false)
                        {
                            StartCoroutine(WatchEnemy());
                        }
                    }
                }
            }
            if (monsterAwareness > 0 && cooldownBool == false && couroutineBool == false)
            {
                StartCoroutine(Cooldown());
            }
        }

    }



    IEnumerator WatchEnemy()
    {
        couroutineBool = true;
        yield return new WaitForSeconds(0.25f);
        if (monsterAwareness < awarenessCap)
            monsterAwareness++;
        if (monsterAwareness >= awarenessCap)
        {
            enemyScript.startChase();
        }
        Debug.Log(monsterAwareness);
        couroutineBool = false;
    }
    IEnumerator Cooldown()
    {
        cooldownBool = true;
        yield return new WaitForSeconds(2);
        monsterAwareness--;
        if (monsterAwareness == 0 && enemyScript.enemyChase == true)
        {
            enemyScript.stopChase();
        }
        Debug.Log(monsterAwareness);
        cooldownBool = false;
    }
}
