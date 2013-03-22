using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;
using Data.EntityFramework.DAL;
using Data.EntityFramework.BLL;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Data.Lucene;


namespace Web.Public
{
    public partial class WikiTriglav : System.Web.UI.MasterPage
    {
        Data.EntityFramework.DAL.Korisnik korisnik;

        protected void Page_Load(object sender, EventArgs e)
        {
            korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];
            if (korisnik != null)
            {
                //  Response.Write("Trenutno logiran " + korisnik.Nadimak);
                lbl_TrenutnoLogiran.Text = "Trenutno logiran " + korisnik.Nadimak;
                lb_Login.Visible = false;
                lb_Logout.Visible = true;
                if (Request.QueryString["ReturnUrl"] != null)
                    Response.Redirect(Request.QueryString["ReturnUrl"]);
            }
        }

        protected void btn_NoviClanak_Click(object sender, EventArgs e)
        {
            if (korisnik != null)
            {
                Response.Redirect("/Public/ClanakNovi.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "Login()", true);
            }
        }
        protected void btn_NovoPitanje_Click(object sender, EventArgs e)
        {
            if (korisnik != null)
            {
                Response.Redirect("/Public/QA_PitanjeNovo.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "Login()", true);
            }
        }
        protected void lb_Reg_Click(object sender, EventArgs e)
        {
            Response.Redirect("/RegistrationForm.aspx");
        }
        protected void lb_Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Login.aspx");
        }
        protected void lb_Logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Login.aspx");
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_Trazi.Text != String.Empty)
                Response.Redirect("/Public/Home.aspx?Pretraga=" + txt_Trazi.Text);
        }
    }
}