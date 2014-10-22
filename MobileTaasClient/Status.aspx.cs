using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileTaasClient
{
    public partial class Status : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude("raphel", "js/raphael.2.1.0.min.js");
            Page.ClientScript.RegisterClientScriptInclude("jusgage", "js/justgage.1.0.1.min.js");
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager();
        }
    }

}