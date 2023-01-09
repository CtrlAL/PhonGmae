using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeColor : MonoBehaviour
{
    // Start is called before the first frame update

   [SerializeField]
   ImputMassageController MassageControler;

   [SerializeField]
   GameObject Example;
   public void ChangeColor(){
        GameObject new_colored_prefub = MassageControler.GetOwnMassaagePrefub();
        new_colored_prefub.GetComponentInChildren<Image>().color = gameObject.GetComponent<Image>().color;
        MassageControler.SetMassagePrefub(new_colored_prefub);
        //MassageSeettingsDBController.SaveMassageColor(gameObject.GetComponent<Image>().color.ToString());
   }

   public void ChangeExampleColor(){
        Example.GetComponent<Image>().color = gameObject.GetComponent<Image>().color;
   }
}
