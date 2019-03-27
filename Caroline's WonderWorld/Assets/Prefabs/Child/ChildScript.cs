using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChildScript : MonoBehaviour {

    GameObject particle;
    NavMeshAgent agentChild;
    Animator animatorChild;
    [SerializeField] Transform target;
    AudioSource audiosourceChild;
    [SerializeField] AudioClip sndExplosion;
    Transform player;
    public bool inCage = true;

	void Start () {
        particle = transform.Find("Particle").gameObject;
        agentChild = GetComponentInChildren<NavMeshAgent>();
        animatorChild = GetComponentInChildren<Animator>();
        audiosourceChild = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            inCage = false;
            audiosourceChild.PlayOneShot(sndExplosion);
            particle.SetActive(true);
            Destroy(transform.Find("Cage").gameObject);
            GetComponent<BoxCollider>().enabled = false;

            GameObject.Find("GameManager").GetComponent<UIslot>().AddChildInSlot();

        }
    }

    private void Update()
    {
       
        if(inCage)
        {
            agentChild.SetDestination(player.position);
            agentChild.speed = 0f;
        }
        else
        {
            animatorChild.SetBool("run", true);
            agentChild.SetDestination(target.position);
            agentChild.speed = 5f;
            Debug.Log(agentChild.remainingDistance);

            if (agentChild.remainingDistance<=agentChild.stoppingDistance)
            {
                agentChild.isStopped=true;
                animatorChild.SetBool("run", false);
                agentChild.transform.rotation = target.rotation;
            }
        }
    }

    
}
