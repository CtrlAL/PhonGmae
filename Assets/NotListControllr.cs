using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotListControllr : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Content, NetListElemPrefub;

    void Start()
    {
        CreateNotList();
    }

    private void CreateNotList(){
          foreach(ContactNotificationInfo c in ContactNotificationInfoList.not_info_list){
                GameObject not_list_elem =  Instantiate(NetListElemPrefub, Content.transform); 
                not_list_elem.transform.SetParent(Content.transform);
                not_list_elem.GetComponentInChildren<TextMeshProUGUI>().text = c.contact_name;
                not_list_elem.GetComponent<NotInfo>().contact_not = c;
                not_list_elem.GetComponentInChildren<Toggle>().isOn = c.enable;
                not_list_elem.GetComponentInChildren<ChangeEnableNot>().SetNotInfo(not_list_elem.GetComponent<NotInfo>());
          }    
    }
}
