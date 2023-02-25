using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyExtraMusic : MonoBehaviour
{
    private GameObject[] MusicManagers;
    // Start is called before the first frame update
    void Start()
    {
        MusicManagers = GameObject.FindGameObjectsWithTag("Music");
        if (MusicManagers.Length >= 2)
        {
            Destroy(MusicManagers[1]);
        }
    }
}
