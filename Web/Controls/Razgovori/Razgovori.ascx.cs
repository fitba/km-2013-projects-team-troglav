using Data.EntityFramework.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Controls.Razgovori
{
    public partial class Razgovori : System.Web.UI.UserControl
    {
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }
        public int PostId
        {
            get
            {
                if (Session["PostID"] == null)
                {
                    Session.Add("PostId", 0);
                    return 0;
                }
                else
                {
                    return (int)Session["PostID"];
                }
            }
            set
            {
                if (Session["PostID"] == null)
                {
                    Session.Add("PostId", value);
                }
                else
                {
                    Session["PostID"] = value;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];

                if (korisnik != null)
                {
                    //img_LogiraniKorisnik.ImageUrl = temp.getSlikaByKorisnikId(korisnik.id);
                    btn_SaveKomentar.Enabled = true;
                }
                else
                {
                    //img_LogiraniKorisnik.ImageUrl = "~/Content/Users_Photo/DefaultUser.jpg";
                    btn_SaveKomentar.Enabled = false;
                }
            }
        }
        protected void btn_SaveKomentar_Click(object sender, EventArgs e)
        {
            if (txt_Sadrzaj.Text == String.Empty)
            {
                return;
            }
            else
            {
                Data.EntityFramework.DAL.Korisnik korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];
                if (korisnik == null)
                {
                    Response.Redirect("/Login.aspx?ReturnUrl=" + Request.Url);
                }
                else
                {
                    if (PostId != 0)
                    {
                        using (TriglavBL temp = new TriglavBL())
                        {
                            Data.EntityFramework.DAL.Komentari komentar = new Data.EntityFramework.DAL.Komentari();
                            komentar.Sadrzaj = txt_Sadrzaj.Text;
                            komentar.DatumKreiranja = DateTime.Now;
                            komentar.Likes = 0;
                            komentar.Unlikes = 0;
                            komentar.PostID = PostId;
                            komentar.KorisnikID = korisnik.id;
                            komentar.isRazgovor = 1;
                            temp.SaveKomentar(komentar);
                            Data.EntityFramework.DAL.Post post = temp.getPostByID(PostId);
                            post.Broj_Razgovora++;
                            temp.UpdatePost(post);
                            ResetirajPolja();
                        }
                    }
                }
            }
        }
        private void ResetirajPolja()
        {
            txt_Sadrzaj.Text = String.Empty;
            Response.Redirect("/Public/ClanakRazgovor.aspx?PostId=" + PostId);
        }
    }
}