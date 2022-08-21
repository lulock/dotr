using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{ 
    public NavMeshAgent agent;

    public float rotateSpeedMovement = 0.075f;
    public float rotateVelocity;

    public HeroCombat heroCombatScript;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {

        if(heroCombatScript.targetedEnemy != null)
        {
            if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null)
            {
                if (!heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    heroCombatScript.targetedEnemy = null;
                }
            }
        }

        bool RMB = Input.GetMouseButtonDown(1);

        if (RMB)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Floor")
            {
                //MOVEMENT
                agent.SetDestination(hit.point);
                heroCombatScript.targetedEnemy = null;
                agent.stoppingDistance = 0;

                //ROTATION
                Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);

                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationToLookAt.eulerAngles.y,
                    ref rotateVelocity,
                    rotateSpeedMovement * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);

            }

            //if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Enemy")
            //{
                // attack

            //}
        }
    }
}
