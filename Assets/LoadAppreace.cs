using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAppreace : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Load(){
        MassageSeettingsDBController.ReadAppereance();
    }
}