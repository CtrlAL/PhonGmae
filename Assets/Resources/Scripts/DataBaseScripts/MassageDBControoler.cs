using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using Mono.Data.Sqlite;

enum myTextureFormats{
    //RGBA64 = TextureFormat.RGBA64,
    DXT1 = TextureFormat.DXT1,
    RGB24 = TextureFormat.RGB24,
    //RGBA32 = TextureFormat.RGBA32
}

public class ImageData{
    public byte[] imageData = new byte[] {};
    public int image_width;
    public int image_height;

    public ImageData(byte[] _imageData, int iw, int ih){
        imageData = _imageData;
        image_width = iw;
        image_height = ih;
    }

    public ImageData(Image unityImageObject){
        if (unityImageObject == null){
            return;
        }
        UnityEngine.Debug.Log(unityImageObject.sprite.texture.format);
        Texture2D tex2D = unityImageObject.sprite.texture;
        imageData = tex2D.GetRawTextureData();
        image_width = tex2D.width;
        image_height = tex2D.height;
    }

    public Sprite SpriteFromImageByte(){
        Texture2D texCopy = new Texture2D(this.image_width, this.image_height,  TextureFormat.DXT1, false);
        texCopy.LoadRawTextureData(this.imageData);
        texCopy.Apply();
        Sprite mySprite = Sprite.Create(texCopy, new Rect(0.0f, 0.0f, this.image_width, this.image_height), new Vector3(0.5f, 0.5f, 100));
        return mySprite; 
    }
}

public class Contact{  
    public ImageData contactImage;  
    public string contact_id;
    public string contact_name;

    public Contact(){
        contact_id = "";
        contact_name = "";
    }

    public Contact(string _contact_name){
        contact_name = _contact_name;
        Guid contact_uuid = Guid.NewGuid();
        contact_id = contact_uuid.ToString();
    }

    public void AddContactToSqliteCommand(in SqliteCommand command){
        command.Parameters.Add(new SqliteParameter("@contactAvatarImage", contactImage.imageData));
        command.Parameters.Add(new SqliteParameter("@image_width", contactImage.image_width));
        command.Parameters.Add(new SqliteParameter("@image_height", contactImage.image_height));
        command.Parameters.Add(new SqliteParameter("@contactName", contact_name));
        command.Parameters.Add(new SqliteParameter("@contact_id", contact_id));
        //command.Parameters.Add(new SqliteParameter("@contact_id", ));
    }
}

public class Massage{
    public int own;
    public string massage_text;
}

public static class MassageDBControoler 
{
    // Start is called before the first frame update
    private static string dbName = "URI=file:Assets/Resources/DataBases/MassagesDB.db";


    /*
    private static bool EmptyCheck(string table_name){//Здесь вообще тру фолс можно или на 2 проверки поделить
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){    
                command.CommandText = $"SELECT count(*) FROM {table_name};";

                using(IDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        int count = Convert.ToInt32(reader["count(*)"]);
                        if(count == 0){
                            connection.Close();
                            return true;
                        }else{
                            connection.Close();
                            return false;
                        }
                    }
                }
            }
        }
        return false;
    }
    */

    public static void CreateContactDB(){
        string t_name = "contact_list";
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){   
                command.CommandText = $@"CREATE TABLE IF NOT EXISTS {t_name} (
                                            id INTEGER PRIMARY KEY AUTOINCREMENT ,
                                            contact_id VARCHAR(40) UNIQUE,
                                            contactName VARCHAR(30),
                                            contactAvatarImage BLOB,
                                            image_width INTEGER,
                                            image_height INTEGER);";
                                            //PhoneOwnerName VARCHAR(30)
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public static void DropContactDB(){
        
        using (var connection = new SqliteConnection(dbName)){
                connection.Open();
                using (var command = connection.CreateCommand()){    
                    command.CommandText = "DROP TABLE IF EXISTS contact_list";
                    command.ExecuteNonQuery();
                }
                connection.Close();
        }
    }

    
    public static void CreateContact(Contact c){   
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"REPLACE INTO contact_list 
                                            (contactName, contact_id, contactAvatarImage, image_width, image_height) 
                                            VALUES (@contactName, @contact_id, @contactAvatarImage, @image_width, @image_height);";
                c.AddContactToSqliteCommand(command);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /*
    public static void CreateContacts(string[] contact_name_list){ 
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            foreach (string name in contact_name_list){
                using (var command = connection.CreateCommand()){   
                    try{
                    Contact c = new Contact(name);
                    command.CommandText = $@"REPLACE INTO contact_list 
                                                (contactName, contact_id) 
                                                VALUES ('{c.contact_name}',
                                                '{c.contact_id}');";
                    command.ExecuteNonQuery();
                    }catch(SqliteException){
                        UnityEngine.Debug.Log("Already Exist");
                        return;
                    }
            }
                connection.Close();
            }
            
        }
    }
    */

    
    public static void CreateChatMassageDB(){   
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){    
                command.CommandText = $@"CREATE TABLE IF NOT EXISTS chat_massages_db (
                                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            chat_id VARCHAR(30) , 
                                            MassageText VARCHAR(30),
                                            my_or_not_my_massage INTEGER,
                                            FOREIGN KEY(chat_id) REFERENCES contact_list(contact_id),
                                            CHECK (my_or_not_my_massage >= 0 AND my_or_not_my_massage <= 1) );";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }   

    public static void DropChatMassageDB(){
        
        using (var connection = new SqliteConnection(dbName)){
                connection.Open();
                using (var command = connection.CreateCommand()){    
                    command.CommandText = "DROP TABLE IF EXISTS chat_massages_db";
                    command.ExecuteNonQuery();
                }
                connection.Close();
        }
    }


    public static void CreateMassage(string chat_id, string massage_text, int own){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"INSERT INTO chat_massages_db 
                                            (chat_id, MassageText, my_or_not_my_massage) 
                                            VALUES ('{chat_id}', '{massage_text}', '{own}');";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }


    public static List<Contact> GetContactList(){
        List<Contact> contact_list = new List<Contact>();
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){    
                command.CommandText = $"SELECT * FROM contact_list;";     
                using(IDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        Contact contact = new Contact();
                        contact.contact_id = reader["contact_id"].ToString();
                        contact.contact_name = reader["ContactName"].ToString();
                        ImageData contactAvatarImage = new ImageData((byte[])reader["contactAvatarImage"], Convert.ToInt32(reader["image_width"]), Convert.ToInt32(reader["image_height"]));
                        contact.contactImage = contactAvatarImage;
                        contact_list.Add(contact);
                    }
                }
            }
            connection.Close();
            return contact_list;
        }
    }

    public static List<Massage> GetMassageList(string chat_id){
        List<Massage> massage_list = new List<Massage>();
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){    
                command.CommandText = $"SELECT * FROM chat_massages_db;";     
                using(IDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){    
                        if(chat_id == reader["chat_id"].ToString()){
                            Massage massage = new Massage();
                            massage.massage_text = reader["MassageText"].ToString();
                            UnityEngine.Debug.Log(reader["my_or_not_my_massage"].ToString());
                            massage.own = Convert.ToInt32(reader["my_or_not_my_massage"]);
                            massage_list.Add(massage);
                        }      
                    }
                }
            }
            connection.Close();
            return massage_list;
        }
    }
}
/*
        Texture2D newTextureFromDiferrentFormats(){
            Texture2D texCopy;
            try{
                texCopy = new(Appereance.userAvatarImage.image_width, Appereance.userAvatarImage.image_height, TextureFormat.DXT1, false);
                texCopy.LoadRawTextureData(Appereance.userAvatarImage.imageData);
                texCopy.Apply();
            }catch{
                return null;
            }

            foreach(TextureFormat f in Enum.GetValues(typeof(myTextureFormats))){
                try{
                    texCopy = new(Appereance.userAvatarImage.image_width, Appereance.userAvatarImage.image_height, f, false);
                    texCopy.LoadRawTextureData(Appereance.userAvatarImage.imageData);
                    texCopy.Apply();
                }catch{
                    texCopy = null;
                    continue;
                }
            }
            
            return texCopy;    
        }
        */