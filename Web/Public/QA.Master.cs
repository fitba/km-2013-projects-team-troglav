using Data.EntityFramework.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Public
{
    public partial class QA : System.Web.UI.MasterPage
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
                Response.Redirect("/Public/QA_Home.aspx?Pretraga=" + txt_Trazi.Text);
        }

        protected void btn_Pitanja_Click(object sender, EventArgs e)
        {
            btn_Pitanja.BackColor = Color.Aqua;
            btn_Tagovi.BackColor = Color.LightGray;
            btn_Korisnici.BackColor = Color.LightGray;
            btn_Bedzevi.BackColor = Color.LightGray;
            btn_Neodgovoreni.BackColor = Color.LightGray;
            btn_NovoPitanje.BackColor = Color.LightGray;
            Response.Redirect("/Public/QA_Pitanja.aspx");
        }

        protected void btn_Tagovi_Click(object sender, EventArgs e)
        {
            btn_Pitanja.BackColor = Color.LightGray;
            btn_Tagovi.BackColor = Color.LawnGreen;
            btn_Korisnici.BackColor = Color.LightGray;
            btn_Bedzevi.BackColor = Color.LightGray;
            btn_Neodgovoreni.BackColor = Color.LightGray;
            btn_NovoPitanje.BackColor = Color.LightGray;
            Response.Redirect("/Public/QA_Tagovi.aspx");
        }

        protected void btn_Korisnici_Click(object sender, EventArgs e)
        {
            btn_Pitanja.BackColor = Color.LightGray;
            btn_Tagovi.BackColor = Color.LightGray;
            btn_Korisnici.BackColor = Color.LawnGreen;
            btn_Bedzevi.BackColor = Color.LightGray;
            btn_Neodgovoreni.BackColor = Color.LightGray;
            btn_NovoPitanje.BackColor = Color.LightGray;
            Response.Redirect("/Public/QA_Korisnici.aspx");
        }

        protected void btn_Bedzevi_Click(object sender, EventArgs e)
        {
            btn_Pitanja.BackColor = Color.LightGray;
            btn_Tagovi.BackColor = Color.LightGray;
            btn_Korisnici.BackColor = Color.LightGray;
            btn_Bedzevi.BackColor = Color.LawnGreen;
            btn_Neodgovoreni.BackColor = Color.LightGray;
            btn_NovoPitanje.BackColor = Color.LightGray;
            Response.Redirect("/Public/QA_Bedzevi.aspx");
        }

        protected void btn_Neodgovoreni_Click(object sender, EventArgs e)
        {
            btn_Pitanja.BackColor = Color.LightGray;
            btn_Tagovi.BackColor = Color.LightGray;
            btn_Korisnici.BackColor = Color.LightGray;
            btn_Bedzevi.BackColor = Color.LightGray;
            btn_Neodgovoreni.BackColor = Color.LawnGreen;
            btn_NovoPitanje.BackColor = Color.LightGray;
            Response.Redirect("/Public/QA_Neodgovoreni.aspx");
        }

        protected void btn_PitanjeNovo_Click(object sender, EventArgs e)
        {
            btn_Pitanja.BackColor = Color.LightGray;
            btn_Tagovi.BackColor = Color.LightGray;
            btn_Korisnici.BackColor = Color.LightGray;
            btn_Bedzevi.BackColor = Color.LightGray;
            btn_Neodgovoreni.BackColor = Color.LightGray;
            btn_NovoPitanje.BackColor = Color.LawnGreen;
            Response.Redirect("/Public/QA_PitanjeNovo.aspx");
        }

       
        protected void img_Logo2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("/Public/QA_Home.aspx");
        }
    }
}