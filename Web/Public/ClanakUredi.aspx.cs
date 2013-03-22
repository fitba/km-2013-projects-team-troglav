using Data.EntityFramework.BLL;
using Data.EntityFramework.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Public
{
    public partial class ClanakUredi : System.Web.UI.Page
    {

        public Data.EntityFramework.DAL.Post post { get; set; }
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }
        public string tinytext { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];

            using (TriglavBL temp = new TriglavBL())
            {
                if (Request.QueryString != null)
                {
                    if (Request.QueryString["PostID"] != null)
                    {
                        int postId = Int32.Parse(Request.QueryString["PostID"]);
                        post = temp.getPostByID(postId);
                    }
                    else
                    {
                        post = temp.getPostByID(2);
                    }
                }
                else
                {
                    post = temp.getPostByID(2);
                }

                if (!IsPostBack)
                {
                    post.BrojPregleda++; //dodavanje pregleda
                    temp.UpdatePost(post);
                }
                LoadClanak(post.id);
            }
            btn_GlavnaStranica.BackColor = System.Drawing.Color.LightBlue;            
            btn_Razgovor.BackColor = System.Drawing.Color.White;
            btn_Citaj.BackColor = System.Drawing.Color.White;
            btn_VidiIzvornik.BackColor = System.Drawing.Color.LightBlue;
            btn_VidiIzmjene.BackColor = System.Drawing.Color.White; 
        }

        private void LoadClanak(int p)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                lbl_ClanakIzvorni.Text = post.Naslov;


                if (!IsPostBack)
                    txt_Sadrzaj.Text = post.Sadrzaj;

                lbl_Promjenjeni.Text = txt_Sadrzaj.Text;


            }
        }
        
       public void btn_SacuvajIzmjeneClanka_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                if (String.Compare( txt_Sadrzaj.Text, post.Sadrzaj) == 0 )
                {
                    lbl_Obavjestenje.Text = "Molimo vas unesite izmjene";
                }
                else
                {
                    Data.EntityFramework.DAL.Post PostEdit = new Data.EntityFramework.DAL.Post();
                    PostEdit.Naslov = post.Naslov;
                    PostEdit.Sadrzaj = txt_Sadrzaj.Text;
                    PostEdit.Tagovi = post.Tagovi;
                    PostEdit.DatumKreiranja = DateTime.Now;
                    PostEdit.DatumZadnjeIzmjene = DateTime.Now;
                    PostEdit.DatumZadnjeAktivnosti = DateTime.Now;
                    PostEdit.VlasnikID = korisnik.id;
                    PostEdit.VlasnikNadimak = korisnik.Nadimak;
                    Response.Write("POST ID JE " + post.id);
                    PostEdit.RoditeljskiPostID = post.id;
                    PostEdit.PodKategorija = post.PodKategorija;
                    PostEdit.PostVrsta = 2;
                    PostEdit.PrihvacenaIzmjena = 0;
                    

                    post.PromijenioID = korisnik.id;
                    post.DatumKreiranja = DateTime.Now;
                    post.DatumZadnjeIzmjene = DateTime.Now;
                    post.DatumZadnjeAktivnosti = DateTime.Now;
                    post.BrojOdgovora++;

                    temp.SavePost(PostEdit);
                    temp.UpdatePost(post);
                    Response.Write("<script>alert('Uspješno ste sačuvali izmjene');</script>");
                    Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
                }
               
            }           
            

        }

        protected void btn_Odustani_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }
        protected void btn_Preview_Click(object sender, EventArgs e)
        {
            if (lbl_Promjenjeni.Visible == false)
                lbl_Promjenjeni.Visible = true;
            else
                lbl_Promjenjeni.Visible = false;
        }


        protected void btn_GlavnaStranica_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }
        protected void btn_Razgovor_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakRazgovor.aspx?id=" + post.id);
        }
        protected void btn_Citaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }
        protected void btn_VidiIzvornik_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakUredi.aspx?PostID=" + post.id);
        }
        protected void btn_VidiIzmjene_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakStareIzmjene.aspx?PostID=" + post.id);
        }

        
    }
}