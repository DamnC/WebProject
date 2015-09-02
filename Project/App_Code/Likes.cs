using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Likes
/// </summary>
public static class Likes
{
    //returns amount of likes
    public static int GetLikesForAudio(int audio_id)
    {
        string sql = string.Format("SELECT COUNT(USER_ID) FROM LIKES WHERE AUDIO_ID ={0}", audio_id);
        return (int)Dal.ExecuteScalar(sql);
    }
    //returns true if use liked audio, else false
    public static bool DidUserLikeAudio(int user_id, int audio_id)
    {
        string sql = string.Format("SELECT USER_ID FROM LIKES WHERE AUDIO_ID ={0} AND USER_ID={1}", audio_id, user_id);
        return Dal.ExecuteScalar(sql) != null;
    }
    //Likes an audio
    public static void Like(int user_id, int audio_id)
    {
        string sql = string.Format("INSERT INTO LIKES ([USER_ID],[AUDIO_ID]) VALUES ({0},{1}) ", user_id, audio_id);
        Dal.ExecuteNonQuery(sql);
    }
    //unikes an audio
    public static bool Unlike(int user_id, int audio_id)
    {
        string sql = string.Format("DELETE FROM LIKES WHERE AUDIO_ID ={0} AND USER_ID={1}", audio_id, user_id);
        Dal.ExecuteNonQuery(sql);
        return true;
    }
    // Gets user liked songs
    public static DataSet GetLikesForUser(int user_id)
    {
        string sql = string.Format("SELECT AUDIO_FILE.AUDIO_NAME, AUDIO_FILE.LISTENS, AUDIO_FILE.UP_DATE, USERS.NICK, USERS.USERNAME, AUDIO_FILE.ID FROM USERS INNER JOIN (AUDIO_FILE INNER JOIN LIKES ON AUDIO_FILE.ID = LIKES.AUDIO_ID) ON USERS.ID = LIKES.USER_ID WHERE (((LIKES.USER_ID)={0}));", user_id);
        return Dal.GetDataSet(sql);
    }
    // Deletes all likes related to audio. used in song deletion.
    public static void DeleteAllLikesOfAudio(int audio_id)
    {
        string sql = string.Format("DELETE FROM LIKES WHERE AUDIO_ID ={0}", audio_id);
        Dal.ExecuteNonQuery(sql);
    }
    // Deletes all likes related to user. used in user deletion.
    public static void DeleteAllLikesOfUser(int user_id)
    {
        string sql = string.Format("DELETE FROM LIKES WHERE USER_ID ={0}", user_id);
        Dal.ExecuteNonQuery(sql);
    }
}