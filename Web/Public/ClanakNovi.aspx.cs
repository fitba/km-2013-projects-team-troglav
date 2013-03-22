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
    public partial class ClanakNovi : System.Web.UI.Page
    {

        public Data.EntityFramework.DAL.Post post { get; set; }
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                if (Request.QueryString != null)
                {
                    if (Request.QueryString["NazivClanka"] != null)
                    {
                        post = new Data.EntityFramework.DAL.Post();
                        txt_Naslov.Text = Request.QueryString["NazivClanka"];
                        
                    }
                    else
                    {
                        post = new Data.EntityFramework.DAL.Post();
                    }
                }
                else
                {
                    post = new Data.EntityFramework.DAL.Post();
                }

                LoadClanak(post.id);
            }

        }

        private void LoadClanak(int p)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                if (!IsPostBack)
                {
                    ddl_Kategorija.DataValueField = "id";
                    ddl_Kategorija.DataTextField = "Naslov";
                    ddl_Kategorija.DataSource = (List<Kategorija>)temp.getKategorijeAll();
                    ddl_Kategorija.DataBind();

                    ddl_Podkategorija.DataValueField = "id";
                    ddl_Podkategorija.DataTextField = "Naslov";
                    Int32 KID = Convert.ToInt32(ddl_Kategorija.SelectedValue);
                    ddl_Podkategorija.DataSource = temp.getPodkategorijaByKategorija(KID);
                    ddl_Podkategorija.DataBind();
                }
                else
                {
                    if (ddl_Kategorija.SelectedValue != null)
                    {
                        ddl_Podkategorija.DataValueField = "id";
                        ddl_Podkategorija.DataTextField = "Naslov";
                        Int32 KID = Convert.ToInt32(ddl_Kategorija.SelectedValue);
                        ddl_Podkategorija.DataSource = temp.getPodkategorijaByKategorija(KID);
                        ddl_Podkategorija.DataBind();
                    }
                    else
                    {
                        ddl_Podkategorija.Text = String.Empty;
                    }
                }

            }
        }

        protected void btn_uploadFoto_Click(object sender, EventArgs e)
        {
            if (FuFoto.PostedFile != null)
            {
                string fileName = FuFoto.FileName;
                string defaultVirtualPhotoLocation = "/Content/Clanci_Photo/";
                string photoVirtualPath = defaultVirtualPhotoLocation + fileName;
                string photoPhysicalPath = Server.MapPath(photoVirtualPath);

                FuFoto.PostedFile.SaveAs(photoPhysicalPath);
                img_Foto.ImageUrl = photoVirtualPath;
            }
        }

        protected void btn_SaveClanak_Click(object sender, EventArgs e)
        {

            if (SaveValidation())
            {
                using (TriglavBL temp = new TriglavBL())
                {
                    post.PostVrsta = 1;
                    post.PodKategorija = Convert.ToInt32(ddl_Podkategorija.SelectedValue);
                    post.Naslov = txt_Naslov.Text;
                    post.Sazetak = txt_Sazetak.Text;
                    post.Sadrzaj = txt_Sadrzaj.Text;
                    post.Tagovi = txt_Tagovi.Text;                   
                    post.BrojKomentara = 0;
                    post.BrojOdgovora = 0;
                    post.BrojOmiljenih = 0;
                    post.BrojPoena = 0;
                    post.BrojPregleda = 0;
                    post.BrojRangiranja = 0;
                    post.DatumKreiranja = DateTime.Now;
                    post.DatumZadnjeAktivnosti = DateTime.Now;
                    post.DatumZadnjeIzmjene = DateTime.Now;
                    post.Likes = 0;
                    post.Unlikes = 0;
                    post.PrihvacenaIzmjena = 0;
                    post.PrihvaceniOdgovori = 0;
                    post.SlikaURL = img_Foto.ImageUrl;

                    korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];
                    if (korisnik != null)
                    {
                        post.VlasnikID = korisnik.id;
                        post.VlasnikNadimak = korisnik.Nadimak;
                    }
                    else
                    {
                        Response.Write("<script>alert('Greška! Molimo vas da se logirate!');</script>");
                        //Response.Redirect("/Login.aspx");
                        return;
                    }                    
                    Response.Write("<script>alert(Vaš članak je uspješno sačuvan!');</script>");
                    temp.SavePost(post);
                    PoveziTagove();
                    Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
                }
            }
            else
            {
                return;
            }
        }

        private void PoveziTagove()
        {

            if (txt_Tagovi.Text != String.Empty)
            {
                using (TriglavBL temp = new TriglavBL())
                {   
                    string[] tags = txt_Tagovi.Text.Split(',');                   

                    foreach (var item in tags)
                    {
                        Data.EntityFramework.DAL.Tag tag = new Data.EntityFramework.DAL.Tag();    
                        tag.Naziv = item.Trim();

                        if (temp.getTagByName(tag.Naziv) == null)
                        {
                            tag.DatumKreiranja = DateTime.Now;
                            temp.SaveTag(tag);
                        }
                        else
                        {
                            tag = temp.getTagByName(tag.Naziv);
                        }

                        if (temp.getPosts_TagsByIDS(post.id, tag.id) == null)
                        {
                            Data.EntityFramework.DAL.Posts_Tags posts_tags = new Data.EntityFramework.DAL.Posts_Tags();
                            posts_tags.PostID = post.id;
                            posts_tags.TagID = tag.id;                            
                            temp.SavePosts_Tags(posts_tags);
                        }
                    }
                }
            }
        }


        private bool SaveValidation()
        {
            using (TriglavBL temp = new TriglavBL())
            {
                List<Post> listaClanaka = temp.getAllPosts(1);
                foreach (var clanak in listaClanaka)
                {
                    if (String.Compare(txt_Naslov.Text, Convert.ToString(clanak.Naslov), true) == 0)
                    {
                        Response.Write("<script>alert('Greška! Članak pod ovim imenom već postoji.\n Pokušajte promijeniti naslov članka.');</script>");
                        return false;
                    }
                }

                if (txt_Naslov.Text.Count() < 3)
                {
                    Response.Write("<script>alert('Greška! Naziv mora biti duži od 3 karaktera.');</script>");
                    return false;
                }
                if (txt_Sazetak.Text.Count() < 3)
                {
                    Response.Write("<script>alert('Greška! Sažetak mora biti duži od 3 karaktera.');</script>");
                    return false;
                }
                if (txt_Sadrzaj.Text.Count() < 3)
                {
                    Response.Write("<script>alert('Greška! Sadržaj mora biti duži od 3 karaktera.');</script>");
                    return false;
                }
                if (txt_Tagovi.Text.Count() < 3)
                {
                    Response.Write("<script>alert('Greška! Tagovi moraju biti duži od 3 karaktera.');</script>");
                    return false;
                }
                return true;
            }
        }


        protected void btn_Dodaj_Poglavlje_Click(object sender, EventArgs e)
        {

        }

        protected void btn_SacuvajPoglavlje_Click(object sender, EventArgs e)
        {

        }




    }
}

