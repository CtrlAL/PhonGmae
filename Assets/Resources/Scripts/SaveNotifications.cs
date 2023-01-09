using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNotifications : MonoBehaviour
{

    [SerializeField] Transform not_list_controllr;
    // Start is called before the first frame update
    public void Save(){
        List<ContactNotificationInfo> cont_not_list = new List<ContactNotificationInfo>();
        foreach(Transform n in not_list_controllr){
            cont_not_list.Add(n.GetComponent<NotInfo>().contact_not);
        }
        ContactNotificationInfoList.not_info_list = cont_not_list;
        MassageSeettingsDBController.SaveNotification();
    }
}
