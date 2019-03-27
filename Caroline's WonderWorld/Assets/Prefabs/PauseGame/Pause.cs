using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class Pause : MonoBehaviour {

    private Image imPause;
    [SerializeField] bool onPause = false;

    [SerializeField] AudioClip sndPause, sndUnPause;
	
	void Awake () {
        imPause = transform.Find("ImPause").GetComponent<Image>();
	}	
	
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            onPause = !onPause;

            if(onPause)
            {
                GetComponent<AudioSource>().PlayOneShot(sndPause);
                imPause.enabled = true;
                Time.timeScale = 0;
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(sndUnPause);
                imPause.enabled = false;
                Time.timeScale = 1;
            }
        }
	}
}
