using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactController : MonoBehaviour
{
    private Contact _contact;

    private GameObject ChatScreenObject;

    private GameObject ContactScreenObject;

    private GameObject Fotter;

    public void OpenChat(){
        //Debug.Log(ContactScreenObject);
        ContactScreenObject.SetActive(false);
        ChatScreenObject.SetActive(true);
        List<Massage> massage_list = MassageDBControoler.GetMassageList(_contact.contact_id);
        Fotter.SetActive(false);
        //Debug.Log();
        if(ChatScreenObject){
            ChatScreenObject.GetComponent<ImputMassageController>().LoadChat(_contact, massage_list);
            ChatScreenObject.GetComponent<ImputMassageController>().SendCompanionMassage("Здарова");
        }   
    }

    public void SetChatObjects(GameObject chat_screen){
        ChatScreenObject = chat_screen;
    }

    public void SetContactScreenObject(GameObject contact_scrren){
        ContactScreenObject = contact_scrren;
    }

    public void SetContact(Contact contact){
        _contact = contact;
    }

    public void SetFooter(GameObject footer){
        Fotter = footer;
    }
}
