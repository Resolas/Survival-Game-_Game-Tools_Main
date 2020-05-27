using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myScoutAI = GetComponent<EnemyScoutAI>();
        myAttackAI = GetComponent<AttackDroneAI>();
    }

    public float health = 30f;
    public bool isDead = false;
    public GameObject deathEffect;

    private AudioSource myAudio;
    private EnemyScoutAI myScoutAI;
    private AttackDroneAI myAttackAI;
    // Update is called once per frame
    void Update()
    {
        checkStatus();
    }

    public void checkStatus()
    {
        if (myScoutAI != null)
        {
            Vector3 distance = myScoutAI.targetObject.position - transform.position;
            float distMg = distance.sqrMagnitude / 1000;

            if (myScoutAI.targetObject && distMg < myScoutAI.range)
            {
                myAudio.enabled = true;
            }
            else
            {
                myAudio.enabled = true;
            }
        }

        if (myAttackAI != null)
        {
            Vector3 distance = myAttackAI.targetObject.position - transform.position;
            float distMg = distance.sqrMagnitude / 1000;

            if (myAttackAI.targetObject && distMg < myAttackAI.range)
            {
                myAudio.enabled = true;
            }
            else
            {
                myAudio.enabled = true;
            }
        }

        if (health <= 0)
        {
            isDead = true;

        }

        if (isDead)
        {
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
