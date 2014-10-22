using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MobileTaasClient
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["UserId"] == null)
            {
                panelLoggedIn.Visible = false;
                panelLoggedOut.Visible = true;
            }
            else
            {
                panelLoggedIn.Visible = true;
                panelLoggedOut.Visible = false;
            }
        }
    }
}