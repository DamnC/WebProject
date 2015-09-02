using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Comments
/// </summary>
public static class Comments
{
    //Gets number of comments on audio
    public static int GetNumberOfCommentsForAudio(int audio_id)
    {
        string sql = string.Format("SELECT COUNT(USER_ID) FROM COMMENTS WHERE AUDIO_ID ={0}", audio_id);
        return (int)Dal.ExecuteScalar(sql);
    }
    //Deletes a comment
    public static bool DeleteComment(int user_id, int audio_id)
    {
        try
        {
            string sql = string.Format("DELETE FROM COMMENTS WHERE AUDIO_ID ={0} AND USER_ID={1}", audio_id, user_id);
            Dal.ExecuteNonQuery(sql);
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
    //Deletes all comments by audio id
    public static bool DeleteAllCommentsOfAudio(int audio_id)
    {
        try
        {
            string sql = string.Format("DELETE FROM COMMENTS WHERE AUDIO_ID ={0}", audio_id);
            Dal.ExecuteNonQuery(sql);
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
    //Deletes all comments by user id
    public static bool DeleteAllCommentsOfUser(int user_id)
    {
        try
        {
            string sql = string.Format("DELETE FROM COMMENTS WHERE USER_ID ={0}", user_id);
            Dal.ExecuteNonQuery(sql);
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }
    //Gets all comments for audio by it's id
    public static DataSet GetCommentsByAudioId(int id)
    {
        string sql = string.Format("SELECT USERS.NICK, COMMENTS.USER_ID, COMMENTS.DATE_WRITTEN, COMMENTS.STRING FROM USERS,COMMENTS WHERE (((USERS.ID)=[COMMENTS].[USER_ID]) AND (COMMENTS.AUDIO_ID={0}))",id);
        return Dal.GetDataSet(sql);
    }   
    //Adds a comment
    public static void InsertComment(int user_id,int audio_id, DateTime date_w, string comment)
    {
        string sql = string.Format("INSERT INTO COMMENTS VALUES ({0},{1},#{2}#,#{3}#,'{4}')", user_id, audio_id, date_w, date_w, comment);
        Dal.ExecuteNonQuery(sql);
    }
    //Updates comment
    public static void UpdateComment(int user_id, int audio_id, DateTime date_w, string comment)
    {
        string sql = string.Format("UPDATE COMMENTS SET [STRING]='{0}', DATE_WRITTEN='{1}' WHERE (USER_ID={2}) AND (AUDIO_ID={3})",comment,date_w,user_id,audio_id);
        Dal.ExecuteNonQuery(sql);
    }
    //Checks if comment exists
    public static bool DoesCommentExist(int user_id, int audio_id)
    {
        string sql = string.Format("SELECT * FROM COMMENTS WHERE (USER_ID={0} AND AUDIO_ID={1})", user_id,audio_id);
        return Dal.ExecuteScalar(sql)!=null;
    }
}
