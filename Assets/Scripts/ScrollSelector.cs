using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ScrollSelector : MonoBehaviour
{
    bool clicked = false;

    public void ToggleClick(RaycastHit hit)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            resetColors();
            if (!this.clicked)
            {
                this.clicked = true;
                DoClick(hit.collider.gameObject);
            }

            else
            {
                DoClick(hit.collider.gameObject);
                clicked = false;
            }
        }
    }

    public virtual void DoClick(GameObject clicker)
    {
        clicker.GetComponent<Renderer>().material.color = Color.grey;
    }

    public virtual void resetColors() {
    }

}

