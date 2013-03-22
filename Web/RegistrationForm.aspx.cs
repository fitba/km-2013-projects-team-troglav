using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Data.EntityFramework.BLL;
using Data.EntityFramework.DAL;

namespace Web
{
    public partial class RegistrationForm : System.Web.UI.Page
    {
        Korisnik korisnik = new Korisnik();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_uploadFoto_Click(object sender, EventArgs e)
        {
            if (FuFoto.PostedFile != null)
            {
                string fileName = FuFoto.FileName;
                string defaultVirtualPhotoLocation = "/Content/Users_Photo/";
                string photoVirtualPath = defaultVirtualPhotoLocation + fileName;
                string photoPhysicalPath = Server.MapPath(photoVirtualPath);

                FuFoto.PostedFile.SaveAs(photoPhysicalPath);
                img_Foto.ImageUrl = photoVirtualPath;
            }
        }

        protected void btn_SaveKorisnik_Click(object sender, EventArgs e)
        {
            if (ValidacijaKorisnika())
            {
                using (TriglavBL temp = new TriglavBL())
                {
                    
                    korisnik.Nadimak = txt_KorisnickoIme.Text;
                    korisnik.Lozinka = txt_Lozinka.Text;
                    korisnik.BrojGodina = Convert.ToInt32(txt_BrojGodina.Text);
                    korisnik.OMeni = txt_OMeni.Text;
                    korisnik.DatumKreiranja = DateTime.Now;
                    korisnik.SlikaURL = img_Foto.ImageUrl;
                    korisnik.Reputacija = 0;
                    korisnik.Pregleda = 0;
                    korisnik.Likes = 0;
                    korisnik.Unlikes = 0;
                    korisnik.BrojZlatnih = 0;
                    korisnik.BrojSrebrenih = 0;
                    korisnik.BrojBronzanih = 0;
                    temp.SaveKorisnik(korisnik);
                    ResetPoljaRegistracija();
                    PostaviKorisnikaUSesiju();
                }
            }            
        }

        private void PostaviKorisnikaUSesiju()
        {
            using (TriglavBL temp = new TriglavBL())
            {
               
                if (korisnik != null)
                {
                    Session.Add("LogiraniKorisnik", korisnik);
                    if (Request.QueryString != null)
                    {
                        if (Request.QueryString["ReturnUrl"] != null) Response.Redirect(Request.QueryString["ReturnUrl"]);
                        else Response.Redirect("/Public/Home.aspx");
                    }
                    else Response.Redirect("/Public/Home.aspx");
                }
                
            }
        }

        private void ResetPoljaRegistracija()
        {            
            txt_KorisnickoIme.Text = String.Empty;
            txt_Lozinka.Text  = String.Empty;
            txt_BrojGodina.Text = String.Empty;
            txt_OMeni.Text = String.Empty;
            img_Foto.ImageUrl = "Content/Users_Photo/DefaultUser.jpg"; 
        }

        private bool ValidacijaKorisnika()
        {   
           
            if (rfv_KorisnickoIme.IsValid == false ||
                rfv_Lozinka.IsValid == false ||
                rfv_OMeni.IsValid == false ||
                rv_BrojGodina.IsValid == false)
                return false;

            using (TriglavBL temp = new TriglavBL())
            {
                if (temp.ProvjeriIme(txt_KorisnickoIme.Text) != null)
                {
                    Response.Write("Greška! :: Korisničko ime je već registrirano.");
                    ResetPoljaRegistracija();
                    return false;
                }
            }
            return true;
        }
    }
}