using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{

    public enum HeroAttackType { Melee, Ranged};
    public HeroAttackType heroAttackType;

    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    private Movement moveScript;
    private Stats statScript;
    private Animator anim;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Movement>();
        statScript = GetComponent<Stats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetedEnemy != null)
        {
            if (Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
            {
                moveScript.agent.SetDestination(targetedEnemy.transform.position);
                moveScript.agent.stoppingDistance = attackRange;

                
            }
            else
            {
                if(heroAttackType == HeroAttackType.Melee)
                {
                    //ROTATION
                    Quaternion rotationToLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);

                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationToLookAt.eulerAngles.y,
                        ref moveScript.rotateVelocity,
                        rotateSpeedForAttack * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);

                    if (performMeleeAttack)
                    {
                        Debug.Log("Attack Minion");
                        // coroutine to attack
                        StartCoroutine(MeleeAttackInterval());
                    }
                }
            }
        }
    }

    IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        anim.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(statScript.attackTime / ((100 + statScript.attackTime) * 0.01f));

        if (targetedEnemy == null)
        {
            anim.SetBool("Basic Attack", false);
            performMeleeAttack = true;
        }
    }

    public void MeleeAttack()
    {
        if(targetedEnemy != null)
        {
            if(targetedEnemy.GetComponent<Targetable>().enemyType == Targetable.EnemyType.Minion)
            {
                targetedEnemy.GetComponent<Stats>().health -= statScript.attackDmg;
            }
        }
        performMeleeAttack = true;
    }
}