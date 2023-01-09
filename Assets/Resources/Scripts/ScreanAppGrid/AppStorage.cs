using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class AppStorage
{
    // Start is called before the first frame update
   public static GameObject[] appList;
   public static GameObject[] appIconList;

   public static void saveID(string app_id){
        //appIDList.Add(app_id);
   }
   public static void loadAppList(){
     if(appList == null){
          appList = Resources.LoadAll("Prefub/App", typeof(GameObject)).Cast<GameObject>().ToArray();
          appIconList = Resources.LoadAll("Prefub/AppIcon", typeof(GameObject)).Cast<GameObject>().ToArray();
     }         
   }
   //public static void InstallApp(){ 
   //}
}
