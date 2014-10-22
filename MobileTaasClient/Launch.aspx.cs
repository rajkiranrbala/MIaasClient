using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileTaasClient
{
    public partial class Launch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnLaunch_Click(object sender, EventArgs e)
        {
            com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager().StartEmulator((int)Session["UserId"], Int32.Parse(cmbAndroidVersion.SelectedValue),
                Int32.Parse(cmbMemory.SelectedValue));
            Response.Redirect("Tasks.aspx");
        }
    }
}