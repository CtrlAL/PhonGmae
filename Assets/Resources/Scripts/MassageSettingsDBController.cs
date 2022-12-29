using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using System.Diagnostics;
using Mono.Data.Sqlite;


//List<ContactNotificationInfo> contat_not_info = new List<ContactNotificationInfo>();

public struct ContactNotificationInfo{
    public bool enable;
    public string contact_id;
    public string contact_name;
}
public static class ContactNotificationInfoList
{
    public static List<ContactNotificationInfo> not_info_list = new List<ContactNotificationInfo>();
}

public static class Appereance{
    public static byte[] backgound_bytecode = {};
    public static int image_width = 0;
    public static int image_height = 0;

    public static float pivot_x = 0;

    public static float pivot_y = 0;

    public static string massage_color = "";
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
                                            pivot_x REAL,
                                            pivot_y REAL,
                                            massage_color VARCHAR(30));";
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
                    while (reader.Read()){          
                        Appereance.backgound_bytecode = (byte[])reader["backgound_bytecode"];
                        Appereance.image_height = Convert.ToInt32(reader["image_height"]);
                        Appereance.image_width = Convert.ToInt32(reader["image_width"]);
                        Appereance.pivot_x = (float)reader["pivot_x"];
                        Appereance.pivot_y = (float)reader["pivot_y"];
                        Appereance.massage_color = (string)reader["massage_color"];
                    }
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public static void SaveAppereance(){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"REPLACE INTO appereance_setting 
                                            (id ,backgound_bytecode, image_width, image_height, pivot_x, pivot_y, massage_color) 
                                            VALUES (@id , @backgound_bytecode, @image_width, @image_height, @pivot_x, @pivot_y, @massage_color);";
                command.Parameters.Add(new SqliteParameter("@backgound_bytecode", Appereance.backgound_bytecode));
                command.Parameters.Add(new SqliteParameter("@id", 1));
                command.Parameters.Add(new SqliteParameter("@image_width", Appereance.image_width));
                command.Parameters.Add(new SqliteParameter("@image_height", Appereance.image_height));
                command.Parameters.Add(new SqliteParameter("@pivot_x", Appereance.pivot_x));
                command.Parameters.Add(new SqliteParameter("@pivot_y", Appereance.pivot_y));
                command.Parameters.Add(new SqliteParameter("@massage_color", ""));
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
            ContactNotificationInfo contact_not;
            contact_not.contact_id = c.contact_id;
            contact_not.contact_name = c.contact_name;
            contact_not.enable = true;
            InsertContact(contact_not);
        }
    }

    public static void LoadNotificationList(){
        
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){ 
                command.CommandText = $@"SELECT * FROM nottification_setting;";
                using(IDataReader reader = command.ExecuteReader()){
                    ContactNotificationInfo contact_not = new ContactNotificationInfo();
                    while(reader.Read()){
                        contact_not.enable = Convert.ToBoolean(reader["enable"]);
                        contact_not.contact_id = (string)reader["contact_id"];
                        contact_not.contact_name = (string)reader["contact_name"];
                    }
                    ContactNotificationInfoList.not_info_list.Add(contact_not);
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