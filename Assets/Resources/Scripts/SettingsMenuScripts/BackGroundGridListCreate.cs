using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundGridListCreate : MonoBehaviour
{
   [SerializeField]
   GameObject BackGroundElementsGrid, BackGroundElementPrefub, BackGroundObject;

   public void CreateGrid(){
    Sprite[] sprite_lsit  =  Resources.LoadAll<Sprite>("Sprites/MassageApp/ChatBackground/");
        foreach(Sprite chat_background in sprite_lsit){
            GameObject grid_elem = Instantiate (BackGroundElementPrefub, BackGroundElementsGrid.transform);
            grid_elem.transform.SetParent(BackGroundElementsGrid.transform);
            grid_elem.GetComponent<BtttonChangeBackGround>().SetBackGoundObject(BackGroundObject);
            grid_elem.GetComponent<BtttonChangeBackGround>().SetImage(chat_background);
        }
   }
}
