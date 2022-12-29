using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeEnableNot : MonoBehaviour
{
    // Start is called before the first frame update
    private NotInfo not_info;
    [SerializeField] Toggle togle;
    public void Change(){
        if(not_info){
            not_info.contact_not.enable = togle.isOn;
            Debug.Log(not_info.contact_not.enable);
        }  
    }

    public void SetNotInfo(NotInfo _not_info){
        not_info = _not_info;
    }
}
