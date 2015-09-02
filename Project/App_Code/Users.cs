using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for User
/// </summary>
static public class Users
{
    //  מחזיר 1 אם הוסיף משתמש אחרת מחזיר את הבעיה
    public static string AddUser(string nick,string mail, string password,string username, string realname)
    {
        string exists = Exists(username, mail);
        if (exists=="")
        {
            string sql = string.Format("INSERT INTO USERS ([NICK],[MAIL],[PASSWORD],[USERNAME],[REALNAME],[ISADMIN]) VALUES ('{0}','{1}','{2}','{3}','{4}', 0)",
                nick, mail, password, username, realname);
            return Dal.ExecuteNonQuery(sql).ToString();
        }
        return exists;
    }
    // בודק אם קיים כבר מייל או שם משתמש
    public static String Exists(string username,string mail)
    {
        string error="";
        string sql = string.Format("SELECT USERNAME FROM USERS WHERE USERNAME='{0}'", username);
        if (Dal.ExecuteScalar(sql) != null)
            error += "username in use ";
        sql = string.Format("SELECT MAIL FROM USERS WHERE MAIL='{0}'", mail);
        if (Dal.ExecuteScalar(sql) != null)
            error += "mail in use";
        return error;
    }
    //deletes a user and all related songs, comments and likes.
    public static bool DeleteUser(int id)
    {
        try
        {
            AudioFiles.DeleteAllFileOfUser(id);
            Comments.DeleteAllCommentsOfUser(id);
            Likes.DeleteAllLikesOfUser(id);
            string sql = string.Format("DELETE FROM USERS WHERE ID={0}", id);
            Dal.ExecuteNonQuery(sql);
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
    //מחזיר אמת אם הצליח להתחבר, אחרת שקר
    public static bool LoginSuccessful(string username, string pass)
    {
        string sql = string.Format("SELECT USERNAME FROM USERS WHERE PASSWORD= '{0}' AND USERNAME='{1}'", pass, username);
        return Dal.ExecuteScalar(sql) != null;
    }
    //returns true if user is admin, else false
    public static bool IsAdmin(int id)
    {
        string sql = string.Format("SELECT ISADMIN FROM USERS WHERE ID={0}",id);
        return (bool)Dal.ExecuteScalar(sql);
    }
    //returns user nick by his id
    public static string GetUserNickById(int id) //asks for id
    {
        string sql = string.Format("SELECT NICK FROM USERS WHERE ID={0}", id);
        return Dal.ExecuteScalar(sql).ToString();
    }
    //returns user id by his username
    public static int GetIdByUsername(string username)
    {
        string sql = string.Format("SELECT ID FROM USERS WHERE USERNAME='{0}'", username);
        return (int)Dal.ExecuteScalar(sql);
    }
    //returns all users
    public static DataSet GetAllUsers()
    {
        string sql = string.Format("SELECT * FROM USERS");
        return Dal.GetDataSet(sql);
    }
    //updates user info
    public static void UpdateUser(int id, string nick, string pass, string realname)
    {
        string sql = string.Format("UPDATE USERS SET NICK='{0}',PASSWORD='{1}',REALNAME='{2}' WHERE (ID={3})",nick,pass,realname,id);
        Dal.ExecuteNonQuery(sql);
    }
}