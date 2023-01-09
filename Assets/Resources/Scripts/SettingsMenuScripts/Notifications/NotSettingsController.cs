using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotSettingsController : MonoBehaviour
{     

    [SerializeField]
    GameObject Content; 
    public void LoadContactNotList(){
        List<Contact> adwqad  = MassageDBControoler.GetContactList();
    }
}
