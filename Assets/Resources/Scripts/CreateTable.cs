using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //DropAllDB();
        CreateAllDB();
    }

    public void CreateAllDB(){
        MassageDBControoler.CreateContactDB();
        MassageDBControoler.CreateChatMassageDB();
        MassageSeettingsDBController.CreateAppereanceDB();
        MassageSeettingsDBController.CreateNottificationDB();
        MassageSeettingsDBController.CreateNottificationList();
    }
    
    public void DropAllDB(){
        MassageDBControoler.DropChatMassageDB();
        MassageDBControoler.DropContactDB();
        MassageSeettingsDBController.DropAll();
    }
}
