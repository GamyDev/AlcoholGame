using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{
   [SerializeField] private GameObject[] _vfx;

    public void PlayVFX()
    {
        int rnd = Random.Range(0, 23);
        _vfx[rnd].gameObject.SetActive(true);
        Invoke("DeletVFX", 2f);
    }


    void DeletVFX()
    {
        for (int i = 0; i < 23; i++)
        {
            _vfx[i].gameObject.SetActive(false);
        }
    }
}
 

