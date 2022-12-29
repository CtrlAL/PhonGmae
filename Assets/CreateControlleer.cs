using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateControlleer : MonoBehaviour
{
    // Start is called before the first frame updat
    [SerializeField]
    TMP_InputField NameInput;
    [SerializeField]
    LoadContactList ContactLisstLoader;
    // Update is called once per frame
    public void CreateNewContact(){
        Contact c = new Contact(NameInput.text);
        MassageDBControoler.CreateContact(c);
        ContactNotificationInfo c_info;
        c_info.contact_id = c.contact_id;
        c_info.contact_name = c.contact_name;
        c_info.enable = true;
        MassageSeettingsDBController.InsertContact(c_info);
        ContactLisstLoader.CreateListElem(c);
    }
}
