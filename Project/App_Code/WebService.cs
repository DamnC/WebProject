using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //Sends file from ws to server in Byte[] 
    [WebMethod]
    public Byte[] Download(int audio_id)
    {
        string filepath = HttpContext.Current.Server.MapPath("~/Data/") + audio_id + ".mp3";
        if (File.Exists(filepath))
        {
            FileStream objfilestream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            int len = (int)objfilestream.Length;
            Byte[] file = new Byte[len];
            objfilestream.Read(file, 0, len);
            objfilestream.Close();
            AddDownload(audio_id);
            return file;
        }
        else
            return new Byte[0];
    }
    //returns file length
    [WebMethod]
    public int GetFileLen(int id)
    {
        string filepath = "G:\\Project\\Data\\" + id + ".mp3";
        FileStream objfilestream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        int len = (int)objfilestream.Length;
        objfilestream.Close();
        return len;
    } 
    //returns number of downloads for file id
    [WebMethod]
    public int GetFileDownloads(int audio_id)
    {
        string sql = string.Format("SELECT NUM FROM DOWNLOADS WHERE AUDIO_ID={0}", audio_id);
        if (WebServiceDal.ExecuteScalar(sql) == null)
        {
            sql = string.Format("INSERT INTO DOWNLOADS VALUES ({0},0)", audio_id);
            WebServiceDal.ExecuteNonQuery(sql);
            return 0;
        }
        else
            return (int)WebServiceDal.ExecuteScalar(sql);
    }
    //returns sum of downloads, used in admin stat page
    [WebMethod]
    public int GetNumberOfDownloads()
    {
        string sql = string.Format("SELECT SUM(NUM) FROM DOWNLOADS");
        return int.Parse(WebServiceDal.ExecuteScalar(sql).ToString());
    }
    //adds download in db for id
    public void AddDownload(int audio_id)
    {
        string sql = string.Format("UPDATE DOWNLOADS SET NUM={0} WHERE AUDIO_ID={1}",GetFileDownloads(audio_id)+1,audio_id);
        WebServiceDal.ExecuteNonQuery(sql);
    }
}