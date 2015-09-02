<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        Application["connected"] = 0;
        Application["visits"] = 0;
        Application["visitors"] = 0;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        Application["connected"] = 0;
    }
    void Application_Error(object sender, EventArgs e) 
    {
    }
    void Session_Start(object sender, EventArgs e) 
    {
        Application.Lock();
        Application["visits"] = int.Parse(Application["visits"].ToString()) + 1;
        Application["visitors"] = int.Parse(Application["visitors"].ToString()) + 1;
        Application.UnLock();
    }
    void Session_End(object sender, EventArgs e) 
    {
        Application.Lock();
        Application["visitors"] = int.Parse(Application["visitors"].ToString())-1;
        Application.UnLock();
    }
</script>
