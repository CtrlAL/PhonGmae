using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MassageDBControoler.CreateContactDB();
        MassageDBControoler.CreateChatMassageDB();
        MassageSeettingsDBController.CreateAppereanceDB();
        MassageSeettingsDBController.CreateNottificationDB();
        MassageSeettingsDBController.CreateNottificationList();

        //MassageSeettingsDBController.DropAll();
        //MassageDBControoler.DropChatMassageDB();
    }
}
