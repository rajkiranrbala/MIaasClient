using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileTaasClient
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Response.Redirect("Launch.aspx");
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            txtError.Text = "";
            try
            {
                Session["UserId"] = com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager().AuthenticateUser(txtUserName.Text, txtPassword.Text);
                Response.Redirect("Launch.aspx");
            }
            catch (Exception ex)
            {
                txtError.Text = ex.Message;
            }
            
        }
    }
}