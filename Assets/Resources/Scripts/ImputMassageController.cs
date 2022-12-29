using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ImputMassageController : MonoBehaviour
{
    [SerializeField] 
    GameObject OwnMassagePrefub;

    [SerializeField] 
    GameObject CompanionMassagePrefub;

    [SerializeField]
    GameObject Content;

    [SerializeField]
    TextMeshProUGUI ChatName;

    [SerializeField]
    TMP_InputField MassageInput;

    [SerializeField]
    Scrollbar VerticalScrollbar;
    private Contact cur_contact;
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return)){    
            SendMassage();
        }   
    }

    void SendMassage(){
        if(!MassageInput.isFocused){
                return;
            }
            //string new_massage_text = MassageInput.GetComponentInChildren<TMP_InputField>().text;
            string new_massage_text = MassageInput.text;

            CreateMassageBox(MassageInput.text, 1);
            MassageDBControoler.CreateMassage(cur_contact.contact_id, new_massage_text, 1);
    }

    public void SendCompanionMassage(string massage_text){
        CreateMassageBox(massage_text, 0);
        MassageDBControoler.CreateMassage(cur_contact.contact_id, massage_text, 0);
    }

    public void LoadChat(Contact contact, List<Massage> massage_list){  
        ChatName.text = contact.contact_name;
        cur_contact = contact;
        LoadMassageList();
        Debug.Log(VerticalScrollbar.value);
        VerticalScrollbar.SetValueWithoutNotify(0);
        void LoadMassageList(){
            Debug.Log(OwnMassagePrefub.GetComponentInChildren<Image>().color);
            //Debug.Log(contact.contact_name);
            //Debug.Log(contact.contact_id);
            foreach(Massage m in massage_list){
                CreateMassageBox(m.massage_text, m.own);
            }
        }
    }

    void CreateMassageBox(string massage_text, int own){
        //Debug.Log(OwnMassagePrefub.GetComponentInChildren<Image>().color);
        if(own == 1){
            GameObject new_massage = Instantiate (OwnMassagePrefub, Content.transform);
            new_massage.transform.SetParent(Content.transform);
            new_massage.GetComponentInChildren<TextMeshProUGUI>().text = massage_text;
        }else if (own == 0){
            GameObject new_massage = Instantiate (CompanionMassagePrefub, Content.transform);
            new_massage.transform.SetParent(Content.transform);
            new_massage.GetComponentInChildren<TextMeshProUGUI>().text = massage_text;
        }
            
    }

    public GameObject GetOwnMassaagePrefub(){
        return OwnMassagePrefub;
    }

    public void SetMassagePrefub(GameObject new_prefub){
        OwnMassagePrefub = new_prefub;
    }

}
