using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using System.Diagnostics;
using Mono.Data.Sqlite;


//List<ContactNotificationInfo> contat_not_info = new List<ContactNotificationInfo>();
public class ContactNotificationInfo{
    public string contact_id;
    public string contact_name;

    public bool enable;

    public ContactNotificationInfo(Contact c){
        contact_id = c.contact_id;
        contact_name = c.contact_name;
        enable = true;
    }

    public ContactNotificationInfo(string contactID, string ContactName, bool enableData){
        contact_id = contactID;
        contact_name = ContactName;
        enable = enableData;
    }
}
public static class ContactNotificationInfoList
{
    public static List<ContactNotificationInfo> not_info_list = new List<ContactNotificationInfo>();
}

public static class Appereance{

    public static ImageData backgroundImage;

    public static ImageData userAvatarImage;

    public static string message_color = "";

    public static void AddAppereanceToSqliteCommand(in SqliteCommand command){
        if(Appereance.backgroundImage == null || Appereance.userAvatarImage == null){
            return;
        }
        //AddAvatarImageParams(command);
        AddBackgroundImageeParams(command);
        command.Parameters.Add(new SqliteParameter("@id", 1));
        command.Parameters.Add(new SqliteParameter("@message_color", Appereance.message_color));
    }

    /*
    public static void AddAvatarImageParams(in SqliteCommand command){
        command.Parameters.Add(new SqliteParameter("@userAvatarByteCode", Appereance.userAvatarImage.imageData));
        command.Parameters.Add(new SqliteParameter("@avatar_width", Appereance.userAvatarImage.image_height));
        command.Parameters.Add(new SqliteParameter("@avatar_height", Appereance.userAvatarImage.image_height));
    }
    */

    public static void AddBackgroundImageeParams(in SqliteCommand command){
        command.Parameters.Add(new SqliteParameter("@backgound_bytecode", Appereance.backgroundImage.imageData));
        command.Parameters.Add(new SqliteParameter("@image_width", Appereance.backgroundImage.image_width));
        command.Parameters.Add(new SqliteParameter("@image_height", Appereance.backgroundImage.image_height));
    }

    public static void ReadAppereanceFromReaderSqlite(IDataReader reader){
        while (reader.Read()){          
                //Appereance.userAvatarImage = new ImageData((byte[])reader["userAvatarByteCode"], Convert.ToInt32(reader["avatar_widht"]), Convert.ToInt32(reader["avatar_height"])); 
                Appereance.backgroundImage = new ImageData((byte[])reader["backgound_bytecode"], Convert.ToInt32(reader["image_height"]), Convert.ToInt32(reader["image_width"]));

                Appereance.message_color = (string)reader["message_color"];
            }
    }
}

public static class MassageSeettingsDBController{
    private static string dbName = "URI=file:Assets/Resources/DataBases/MassagesDB.db";

    public static void CreateAppereanceDB(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){   
                command.CommandText = $@"CREATE TABLE IF NOT EXISTS appereance_setting(
                                            id INTEGER PRIMARY KEY,
                                            backgound_bytecode BLOB,
                                            image_width INTEGER,
                                            image_height INTEGER,
                                            userAvatarByteCode BLOB,
                                            avatar_width INTEGER,
                                            avatar_height INTEGER,
                                            message_color VARCHAR(30));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public static void CreateAppereancePresetRow(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"REPLACE INTO appereance_setting 
                                            (id) 
                                            VALUES (@id);";
                
                command.Parameters.Add(new SqliteParameter("id", 1));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public static void ReadAppereance(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){   
                command.CommandText = $@"SELECT * FROM appereance_setting;";

                using(IDataReader reader = command.ExecuteReader()){
                    Appereance.ReadAppereanceFromReaderSqlite(reader);
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /*
    public static void SaveAppereance(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"REPLACE INTO appereance_setting 
                                            (id ,backgound_bytecode ,image_width ,image_height ,userAvatarByteCode ,avatar_width ,avatar_height , massage_color) 
                                            VALUES (@id ,@backgound_bytecode ,@image_width ,@image_height ,@userAvatarByteCode ,@avatar_width, @avatar_height, @massage_color);";
                
                Appereance.AddAppereanceToSqliteCommand(command);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    */


    public static void UpdateBackground(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"UPDATE appereance_setting 
                                            SET backgound_bytecode = @backgound_bytecode , image_width = @image_width , image_height = @image_height
                                            WHERE id = 1;";
                
                Appereance.AddBackgroundImageeParams(command);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    /*
    public static void UpdateAvatar(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"UPDATE appereance_setting 
                                            SET userAvatarByteCode = @userAvatarByteCode , avatar_width = @avatar_width , avatar_height = @avatar_height
                                            WHERE id = {1};";
                
                Appereance.A(command);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    */

    public static void UpdateMessageColor(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"UPDATE appereance_setting 
                                            SET message_color = @message_color
                                            WHERE id = 1;";
                
                command.Parameters.Add(new SqliteParameter("message_color", Appereance.message_color));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    
    public static void CreateNottificationDB(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){   
                command.CommandText = $@"CREATE TABLE IF NOT EXISTS nottification_setting(
                                            id INTEGER PRIMARY KEY AUTOINCREMENT ,
                                            contact_id VARCHAR(40) UNIQUE,
                                            contact_name VARCHAR(40),
                                            enable INTEGER VARCHAR(30),
                                            CHECK (enable >= 0 AND enable <= 1),
                                            FOREIGN KEY(contact_id) REFERENCES contact_list(contact_id),
                                            FOREIGN KEY(contact_name) REFERENCES contact_list(ContactName));";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public static void InsertContact(ContactNotificationInfo c){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"REPLACE INTO nottification_setting 
                                            (contact_id, contact_name, enable) 
                                            VALUES ('{c.contact_id}', '{c.contact_name}', '{Convert.ToInt32(c.enable)}');";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public static void SaveNotification(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            foreach(ContactNotificationInfo c in ContactNotificationInfoList.not_info_list){
                using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"REPLACE INTO nottification_setting 
                                            (contact_id, contact_name, enable) 
                                            VALUES ('{c.contact_id}', '{c.contact_name}', '{Convert.ToInt32(c.enable)}');";
                command.ExecuteNonQuery();
                }
            } 
            connection.Close();
        }

    }

    public static void CreateNottificationList(){
        List<Contact> ContactList = MassageDBControoler.GetContactList();
        foreach(Contact c in ContactList){
            ContactNotificationInfo contact_not = new ContactNotificationInfo(c);
            InsertContact(contact_not);
        }
    }

    public static void LoadNotificationList(){
        
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"SELECT * FROM nottification_setting;";
                using(IDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        ContactNotificationInfo contact_not = new ContactNotificationInfo
                                                                    ((string)reader["contact_id"], 
                                                                    (string)reader["contact_name"], 
                                                                    Convert.ToBoolean(reader["enable"]));
                        ContactNotificationInfoList.not_info_list.Add(contact_not);
                    }
                    
                }
            }
            connection.Close();
        }
    }

    public static void DropAll(){
        
        using (var connection = new SqliteConnection(dbName)){
                connection.Open();
                using (var command = connection.CreateCommand()){    
                    command.CommandText = "DROP TABLE IF EXISTS appereance_setting";
                    command.ExecuteNonQuery();
                }

                using (var command = connection.CreateCommand()){    
                    command.CommandText = "DROP TABLE IF EXISTS nottification_setting";
                    command.ExecuteNonQuery();
                }

                connection.Close();
        }
    }
}