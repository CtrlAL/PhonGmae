using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteeAllChild : MonoBehaviour
{
   [SerializeField] Transform ObjectForDeleteChild;

   public void DeleteChilds(){
        foreach(Transform child in ObjectForDeleteChild ){
            GameObject.Destroy(child.gameObject);
        }
   }
}
