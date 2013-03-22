using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data.EntityFramework.BLL;
using Data.EntityFramework.DAL;


namespace Web.Public
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        //BUTTON LOGIRANJE

        protected void btn_Logiranje_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                Data.EntityFramework.DAL.Korisnik korisnik = temp.Autentifikacija_Usera(txt_KorisnickoIme.Text, txt_Lozinka.Text);
                if (korisnik != null)
                {
                    Session.Add("LogiraniKorisnik", korisnik);
                    korisnik.DatumZadnjegPristupa = DateTime.Now;
                    temp.UpdateKorisnik(korisnik);

                    if (Request.QueryString != null)
                    {
                        if (Request.QueryString["ReturnUrl"] != null)   
                            Response.Redirect(Request.QueryString["ReturnUrl"]);                        
                        else    
                            Response.Redirect("/Public/Home.aspx");                        
                    }
                    else Response.Redirect("/Public/Home.aspx");  
                }  
                else     ResetirajPolja();               
            }

        }

        private void ResetirajPolja()
        {
            txt_KorisnickoIme.Text = String.Empty;
            txt_Lozinka.Text = String.Empty;
        }
    }
}