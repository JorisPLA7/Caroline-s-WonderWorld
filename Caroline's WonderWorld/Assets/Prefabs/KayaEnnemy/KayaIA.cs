using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KayaIA : MonoBehaviour
{

    NavMeshAgent kayaAgent;
    Animator kayaAnimator;
    Transform target;
    AudioSource kayaAudioSource;

    [SerializeField] float idleDistance = 10f, walkDistance = 7f, attackDistance = 1f;

    [SerializeField] AudioClip sndClaireHurt, sndPop;
    public float KayaDamage = 10f;
    public ProgressBar PbHealth;

    private GameObject particle;

    void Start()
    {
        kayaAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        kayaAnimator = GetComponent<Animator>();
        kayaAudioSource = GetComponent<AudioSource>();
        particle = transform.Find("Explode").gameObject;
    }

    void Update()
    {

        if (kayaAgent.remainingDistance > walkDistance)
        {
            kayaAgent.speed = 0;
            kayaAnimator.SetBool("walk", false);
            kayaAnimator.SetBool("attack", false);

            if (kayaAgent.remainingDistance > idleDistance)
            {
                kayaAnimator.SetBool("idle", false);
            }
            else
            {
                kayaAnimator.SetBool("idle", true);
            }
        }
        else
        {
            kayaAgent.speed = 1f;
            kayaAnimator.SetBool("walk", true);
            kayaAnimator.SetBool("attack", false);

            if (kayaAgent.remainingDistance < attackDistance)
            {
                kayaAnimator.SetBool("walk", false);
                kayaAnimator.SetBool("attack", true);
                kayaAgent.speed = 0;
            }
        }

        kayaAgent.SetDestination(target.position);
    }

    public void DamageToClaire()
    {
        PbHealth.Val -= KayaDamage;
        kayaAudioSource.PlayOneShot(sndClaireHurt);

        if (PbHealth.Val == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().ClaireDead();

            GameObject[] kaya = GameObject.FindGameObjectsWithTag("kaya");

            foreach (GameObject k in kaya)
            {
                k.GetComponent<KayaIA>().enabled = false;
                k.GetComponent<Animator>().SetBool("attack", false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            kayaAnimator.SetBool("attack",false);
            kayaAnimator.SetBool("idle", true);
            particle.SetActive(true);
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            kayaAudioSource.PlayOneShot(sndPop);
            Destroy(gameObject, sndPop.length);
        }
    }

    
}
