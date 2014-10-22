using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileTaasClient
{
    public partial class Billing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void ObjectDataSource1_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager();
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager().Pay((int)Session["UserId"]);
            GridView1.DataBind();
        }
    }
}