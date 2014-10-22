using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileTaasClient
{
    public partial class Devices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void DeviceSources_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Response.Redirect("Control.aspx?emuid=" + e.CommandArgument);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}