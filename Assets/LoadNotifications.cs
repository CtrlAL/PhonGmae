using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNotifications : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] NotListControllr notListController;
    void Start()
    {
        Load();
        notListController.CreateNotList();
    }

    // Update is called once per frame
    private void Load(){
        MassageSeettingsDBController.LoadNotificationList();
    }
}
