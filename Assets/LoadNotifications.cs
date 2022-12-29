using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNotifications : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    private void Load(){
        MassageSeettingsDBController.LoadNotificationList();
    }
}
