using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIslot : MonoBehaviour {


    [SerializeField] Sprite slotWithChild;
    [SerializeField] Image[] slots;
    private int x = 0;

    public bool SlotComplete = false;

    [SerializeField] GameObject EndPointParticle;

	public void AddChildInSlot()
    {
        slots[x].GetComponent<Image>().sprite = slotWithChild;
        x++;
        x = Mathf.Clamp(x, 0, slots.Length);

        if(x==slots.Length)
        {
            SlotComplete = true;
            EndPointParticle.SetActive(true);
        }
    }

    private void Awake()
    {
        EndPointParticle = GameObject.Find("Fireball_big_blue").gameObject;
        EndPointParticle.SetActive(false);
    }
}
