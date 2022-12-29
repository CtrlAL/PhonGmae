using System.Collections;
using System.Collections.Generic;
using System.Data;
using System;
using UnityEngine;
using System.Diagnostics;
using Mono.Data.Sqlite;

enum BoolBaseState{
    _FALSE,
    _TRUE,
    _TABLE_NOT_EXIST
}

public class Contact{   
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
}

public class Massage{
    public int own;
    public string massage_text;
}

public static class MassageDBControoler 
{
    // Start is called before the first frame update
    private static string dbName = "URI=file:Assets/Resources/DataBases/MassagesDB.db";

    private static BoolBaseState EmptyCheck(string table_name){
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){    
                command.CommandText = $"SELECT count(*) FROM {table_name};";

                using(IDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        int count = Convert.ToInt32(reader["count(*)"]);
                        if(count == 0){
                            connection.Close();
                            return BoolBaseState._TRUE;
                        }else{
                            connection.Close();
                            return BoolBaseState._FALSE;
                        }
                    }
                }
            }
        }
        return BoolBaseState._TABLE_NOT_EXIST;
    }

    public static void CreateContactDB(){   
        string t_name = "contact_list";
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            using (var command = connection.CreateCommand()){   
                command.CommandText = $@"CREATE TABLE IF NOT EXISTS {t_name} (
                                            id INTEGER PRIMARY KEY AUTOINCREMENT ,
                                            contact_id VARCHAR(40) UNIQUE,
                                            ContactName VARCHAR(30));";
                                            //PhoneOwnerName VARCHAR(30)
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        if(EmptyCheck(t_name) == BoolBaseState._TRUE){
            string[] defult_contact = {"Мама", "Папа"};
            CreateContacts(defult_contact);
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
                                            (ContactName, contact_id) 
                                            VALUES ('{c.contact_name}', '{c.contact_id}');";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public static void CreateContacts(string[] contact_name_list){ 
        using (var connection = new SqliteConnection(dbName)){
            connection.Open();
            foreach (string name in contact_name_list){
                using (var command = connection.CreateCommand()){   
                    try{
                    Contact c = new Contact(name);
                    command.CommandText = $@"REPLACE INTO contact_list 
                                                (ContactName, contact_id) 
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
