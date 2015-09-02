using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

/// <summary>
/// Summary description for AudioFiles
/// </summary>
public static class AudioFiles
{
    //Returns all audio files for user page
    public static DataSet GetAllFilesByUser(int userid)
    {
        string sql = string.Format("SELECT USERS.USERNAME,USERS.NICK,AUDIO_FILE.ID, AUDIO_FILE.AUDIO_NAME, AUDIO_FILE.DESCRIPTION, AUDIO_FILE.LISTENS, AUDIO_FILE.UP_DATE FROM USERS, AUDIO_FILE WHERE (((USERS.ID)=[AUDIO_FILE].[USER_ID]) AND USERS.ID = {0})", userid);
        return Dal.GetDataSet(sql);
    }
    //Returns all audio files for explore view
    public static DataSet GetAllFiles()
    {
        string sql = string.Format("SELECT USERS.USERNAME,USERS.NICK,AUDIO_FILE.ID, AUDIO_FILE.AUDIO_NAME, AUDIO_FILE.LISTENS, AUDIO_FILE.UP_DATE FROM USERS, AUDIO_FILE WHERE (((USERS.ID)=[AUDIO_FILE].[USER_ID]))");
        return Dal.GetDataSet(sql);
    }
    //Returns audios with name that fits search string
    public static DataSet Search(string SearchString)
    {
        string sql = string.Format("SELECT USERS.USERNAME,USERS.NICK,AUDIO_FILE.ID, AUDIO_FILE.AUDIO_NAME, AUDIO_FILE.DESCRIPTION, AUDIO_FILE.LISTENS, AUDIO_FILE.UP_DATE FROM USERS, AUDIO_FILE WHERE (((USERS.ID)=[AUDIO_FILE].[USER_ID]) AND AUDIO_NAME LIKE '%{0}%')", SearchString);
        return Dal.GetDataSet(sql);
    }
    //Upload method
    public static string UploadFile(FileUpload FileUpload1,string AudioName,string AudioDesc,string UserId)
    {
        string label1="";
        if (CreateFileDB(AudioName,AudioDesc,UserId,FileUpload1.FileName) != 0)
        {
            try
            {
                FileUpload1.SaveAs(HttpContext.Current.Server.MapPath("~/Data/") +GetIdByFileName(FileUpload1.FileName).ToString()+".mp3");
                label1 = "File name: " +
                     AudioName + "<br>" +
                     (int)(FileUpload1.PostedFile.ContentLength)/1024 + " Kb<br>" +
                     "Content type: " +
                     FileUpload1.PostedFile.ContentType;
            }
            catch (Exception ex)
            {
                label1 = "ERROR: " + ex.Message.ToString();
            }
        }
        return label1;
    }
    //Adds file to DB then returns id
    public static int CreateFileDB(string AudioName, string AudioDesc, string UserId, string FileName)
    {
        string sql = string.Format("INSERT INTO AUDIO_FILE ([AUDIO_NAME],[USER_ID],[DESCRIPTION],[LISTENS],[UP_DATE],[FILE_NAME]) VALUES ('{0}','{1}','{2}',0,'{3}','{4}')",
                AudioName, UserId, AudioDesc, System.DateTime.Now , FileName);
        return (int)Dal.ExecuteNonQuery(sql);
    }
    //Retruns id by filename
    public static int GetIdByFileName(string filename)
    {
        string sql = string.Format("SELECT * FROM AUDIO_FILE WHERE FILE_NAME='{0}'",filename); 
        return (int)Dal.ExecuteScalar(sql);
    }
    //Retruns filename by id
    public static string GetFilenameById(int id)
    {
        string sql = string.Format("SELECT FILE_NAME FROM AUDIO_FILE WHERE ID={0}", id);
        return Dal.ExecuteScalar(sql).ToString();
    }
    //Retruns num of listens for id
    public static int GetListens(int audio_id)
    {
        string sql = string.Format("SELECT LISTENS FROM AUDIO_FILE WHERE (ID = {0})", audio_id);
        return (int)Dal.ExecuteScalar(sql);
    }
    //Adds a listen to file id 
    public static void AddListen(int audio_id)
    {
        string sql = string.Format("UPDATE AUDIO_FILE SET LISTENS={0} WHERE (ID = {1})",GetListens(audio_id)+1 ,audio_id);
        Dal.ExecuteNonQuery(sql);
    }
    //Retruns a file by his id
    public static DataSet GetFileById(int id)
    {
        string sql = string.Format("SELECT USERS.NICK,USERS.USERNAME,AUDIO_FILE.ID, AUDIO_FILE.AUDIO_NAME, AUDIO_FILE.DESCRIPTION, AUDIO_FILE.LISTENS, AUDIO_FILE.UP_DATE FROM USERS, AUDIO_FILE WHERE (((USERS.ID)=[AUDIO_FILE].[USER_ID]) AND AUDIO_FILE.ID={0})", id);
        return Dal.GetDataSet(sql);
    }
    //Deletes file by his id 
    public static bool DeleteFile(int id)
    {
        Comments.DeleteAllCommentsOfAudio(id);
        Likes.DeleteAllLikesOfAudio(id);
        string sql = string.Format("DELETE FROM AUDIO_FILE Where id={0}", id);
        Dal.ExecuteNonQuery(sql);
        if (File.Exists(HttpContext.Current.Server.MapPath("~/Data/") + id + ".mp3"))
        {
            File.Delete(HttpContext.Current.Server.MapPath("~/Data/") + id + ".mp3");
        }
        return true;
    }
    //Retruns AudioFile data for admin page
    public static DataSet GetFilesAdmin()
    {
        string sql = string.Format("SELECT USERS.NICK,AUDIO_FILE.ID, AUDIO_FILE.AUDIO_NAME, AUDIO_FILE.LISTENS, AUDIO_FILE.UP_DATE,AUDIO_FILE.DESCRIPTION,AUDIO_FILE.FILE_NAME FROM USERS, AUDIO_FILE WHERE (((USERS.ID)=[AUDIO_FILE].[USER_ID]))");
        return Dal.GetDataSet(sql);
    }
    //Retruns File in Byte[] form
    public static Byte[] Download(int id)
    {
        ServiceReference1.WebServiceSoapClient ws = new ServiceReference1.WebServiceSoapClient();
        Byte[] file = ws.Download(id);
        return file;
    }
    //Updates AudioFile details by his id 
    public static void UpdateAudio(int id, string audio_name,int listens, DateTime UpDate, string filename, string desc)
    {
        string sql = string.Format("UPDATE AUDIO_FILE SET AUDIO_NAME='{0}',LISTENS={1},UP_DATE='{2}',FILE_NAME='{3}',DESCRIPTION='{4}' WHERE (ID={5})",audio_name,listens,UpDate,filename,desc, id);
        Dal.ExecuteNonQuery(sql);
    }
    //Retruns sum of listens
    public static int GetSumOfListens()
    {
        string sql = string.Format("SELECT SUM(LISTENS) FROM AUDIO_FILE");
        return int.Parse(Dal.ExecuteScalar(sql).ToString());
    }
    //Deletes all files that a user has uploaded
    public static void DeleteAllFileOfUser(int user_id)
    {
        DataSet ds = GetAllFilesByUser(user_id);
        int id;
        int num = ds.Tables[0].Rows.Count;
        for (int i = 0; i < num; i++)
        {
            id=ds.Tables[0].Rows[i].Field<int>("ID");
            DeleteFile(id);
        }
    }
}