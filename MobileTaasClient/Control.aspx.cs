using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileTaasClient
{
    public partial class Control : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["emuid"] == null)
                {
                    Response.Redirect("Devices.aspx");
                }
                ViewState["emuid"] = Request.QueryString["emuid"];
                txtEmulatorId.Text = (String)ViewState["emuid"];
            }
            else
            {
                if (ViewState["emuid"] == null)
                {
                    Response.Redirect("Devices.aspx");
                }
            }
        }

        protected void btnRefreshImage_Click(object sender, EventArgs e)
        {
            byte[] data = com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager().GetScreenShot((int)Session["UserId"], (String)ViewState["emuid"]);
            imageData.ImageUrl = @"data:image/gif;base64," + Convert.ToBase64String(data);
        }

        protected void btnLaunchUrl_Click(object sender, EventArgs e)
        {
            com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager().LaunchUrl((int)Session["UserId"], (String)ViewState["emuid"], txtLaunchUrl.Text);
        }

        protected void btnLaunchIntent_Click(object sender, EventArgs e)
        {
            com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager().LaunchApp((int)Session["UserId"], (String)ViewState["emuid"], txtLaunchIntent.Text);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager().InstallApplication((int)Session["UserId"], (String)ViewState["emuid"], apkUpload.FileBytes);
        }

        protected void btnPowerOff_Click(object sender, EventArgs e)
        {
            com.sjsu.mobiletaas.resourcemanager.ResourceManager.GetResourceManager().StopEmulator((int)Session["UserId"], (String)ViewState["emuid"]);
            Response.Redirect("Devices.aspx");
        }
    }
}