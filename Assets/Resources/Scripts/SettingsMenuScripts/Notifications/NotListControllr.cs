using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotListControllr : MonoBehaviour
{
    // Start is called before the first frame update
    private bool createdStatus;
    [SerializeField] GameObject Content, NetListElemPrefub;

    public void CreateNotList(){
        Debug.Log(ContactNotificationInfoList.not_info_list.Count);
        foreach(ContactNotificationInfo c in ContactNotificationInfoList.not_info_list){         
            InstantiateNotObject(c);
        }   
        createdStatus = true; 
    }

    private void InstantiateNotObject(ContactNotificationInfo cInfo){
        GameObject not_list_elem =  Instantiate(NetListElemPrefub, Content.transform); 
        not_list_elem.transform.SetParent(Content.transform);
        not_list_elem.GetComponentInChildren<TextMeshProUGUI>().text = cInfo.contact_name;
        not_list_elem.GetComponent<NotInfo>().contact_not = cInfo;
        not_list_elem.GetComponentInChildren<Toggle>().isOn = cInfo.enable;
        not_list_elem.GetComponentInChildren<ChangeEnableNot>().SetNotInfo(not_list_elem.GetComponent<NotInfo>());
    }

    public void AddToNotList(Contact c){
        if(createdStatus){ 
            ContactNotificationInfo cInfo = new ContactNotificationInfo(c);
            MassageSeettingsDBController.InsertContact(cInfo); 
            InstantiateNotObject(cInfo);
        }
    }
}
