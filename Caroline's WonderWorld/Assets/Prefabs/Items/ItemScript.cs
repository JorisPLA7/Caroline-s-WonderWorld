using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    [SerializeField] ProgressBar pb;
    [SerializeField] int itemVal = 10;
    [SerializeField] MeshRenderer meshRenderer;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            AudioSource audiosource = GetComponent<AudioSource>();
            pb.Val += itemVal;
            audiosource.Play();

            GetComponent<BoxCollider>().enabled = false;
            meshRenderer.enabled = false;
            

            Destroy(gameObject, audiosource.clip.length);
        }
    }
}
