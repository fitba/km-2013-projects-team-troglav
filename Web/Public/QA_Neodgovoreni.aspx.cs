using Data.EntityFramework.BLL;
using Data.EntityFramework.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Public
{
    public partial class QA_Neodgovoreni : System.Web.UI.Page
    {
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }



        protected void Page_Load(object sender, EventArgs e)
        {
            korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];

            Button btn_Neodgovoreni = (Button)Master.FindControl("btn_Neodgovoreni");
            //btn_Neodgovoreni.BackColor = Color.LightGreen;


            if (!IsPostBack)
            {
                using (TriglavBL temp = new TriglavBL())
                {
                    if (korisnik != null)
                    {
                        string recenica = (string)temp.getPitanjaByKorisnikTags(korisnik);
                        if (recenica != "")
                        {
                            List<Post> listaMojihPostova = Data.Lucene.Pretraga.getPitanjaPretrage(recenica);
                            List<Post> listaMojihNeodgovorenihPostova = new List<Post>();
                            if (listaMojihPostova != null)
                            {
                                foreach (var post in listaMojihPostova)
                                {
                                    if (post.BrojOdgovora == 0)
                                        listaMojihNeodgovorenihPostova.Add(post);
                                }
                                dl_Pitanja.DataSource = listaMojihNeodgovorenihPostova;
                                dl_Pitanja.DataBind();
                            }
                            else
                            {
                                dl_Pitanja.DataSource = temp.getPitanjaNeodgovorena();
                                dl_Pitanja.DataBind();
                            }
                        }
                        else
                        {
                            dl_Pitanja.DataSource = temp.getPitanjaNeodgovorena();
                            dl_Pitanja.DataBind();
                        }

                        btn_MojiTagovi.BackColor = Color.LightGreen;
                        btn_Najnovija.BackColor = Color.LightGray;
                        btn_PoGlasovima.BackColor = Color.LightGray;
                    }
                    else
                    {
                        dl_Pitanja.DataSource = temp.getPitanjaNeodgovorena();
                        dl_Pitanja.DataBind();
                    }

                    btn_MojiTagovi.BackColor = Color.LightGreen;
                    btn_Najnovija.BackColor = Color.LightGray;
                    btn_PoGlasovima.BackColor = Color.LightGray;
                }
            }

        }
        protected void dl_Pitanja_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                List<Posts_Tags> listaPostsTags = temp.getPost_TagsByPostID(id);
                List<Tag> listaTagova = new List<Tag>();

                foreach (var pt in listaPostsTags)
                    listaTagova.Add(temp.getTagByID(Convert.ToInt32(pt.TagID)));

                DataList dl_Tags = (DataList)e.Item.FindControl("dl_Tagovi");
                dl_Tags.DataSource = listaTagova;
                dl_Tags.DataBind();

                Post p = temp.getPostByID(id);
                Data.EntityFramework.DAL.Korisnik k = temp.getKorisnikByID(p.VlasnikID.Value);
                System.Web.UI.WebControls.Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_User");
                //img.ImageUrl ="/Content/Users_Photo/DefaultUser.jpg";
                img.ImageUrl = k.SlikaURL;

                Label reputacija = (Label)e.Item.FindControl("lbl_Reputacija");
                reputacija.Text = "Reputacija" + k.Reputacija;

                System.Web.UI.WebControls.Image img_BedzVlsanika = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_BedzVlsanika");
                img_BedzVlsanika.ImageUrl = k.BedzSlika;

                Label lbl_NazivBedzaVlasnika = (Label)e.Item.FindControl("lbl_NazivBedzaVlasnika");
                lbl_NazivBedzaVlasnika.Text = k.BedzNaziv;
                lbl_NazivBedzaVlasnika.ToolTip = k.BedzOpis;

                Label lbl_Gold = (Label)e.Item.FindControl("lbl_Gold");
                Label lbl_Silver = (Label)e.Item.FindControl("lbl_Silver");
                Label lbl_Bronze = (Label)e.Item.FindControl("lbl_Bronze");
                lbl_Gold.Text = "Zlatnika" + Convert.ToString(k.BrojZlatnih);
                lbl_Silver.Text = "Dukata" + Convert.ToString(k.BrojSrebrenih);
                lbl_Bronze.Text = "Groševa" + Convert.ToString(k.BrojBronzanih);
            }
        }

        protected void btn_MojiTagovi_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                if (korisnik != null)
                {

                    string recenica = temp.getPitanjaByKorisnikTags(korisnik);
                    List<Post> listaMojihPostova = Data.Lucene.Pretraga.getPitanjaPretrage(recenica);
                    List<Post> listaMojihNeodgovorenihPostova = new List<Post>();
                    if (listaMojihPostova != null)
                    {
                        foreach (var post in listaMojihPostova)
                        {
                            if (post.BrojOdgovora == 0)
                                listaMojihNeodgovorenihPostova.Add(post);
                        }
                        dl_Pitanja.DataSource = listaMojihNeodgovorenihPostova;
                        dl_Pitanja.DataBind();

                        btn_MojiTagovi.BackColor = Color.LightGreen;
                        btn_Najnovija.BackColor = Color.LightGray;
                        btn_PoGlasovima.BackColor = Color.LightGray;
                    }
                    else
                    {
                        dl_Pitanja.DataSource = temp.getPitanjaNeodgovorena();
                        dl_Pitanja.DataBind();
                    }
                }

                else
                {
                    dl_Pitanja.DataSource = temp.getPitanjaNeodgovorena();
                    dl_Pitanja.DataBind();
                }
            }
        }
        protected void btn_Najnovija_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Pitanja.DataSource = temp.getPitanjaNeodgovorena();
                dl_Pitanja.DataBind();
                btn_MojiTagovi.BackColor = Color.LightGray;
                btn_Najnovija.BackColor = Color.LightGreen;
                btn_PoGlasovima.BackColor = Color.LightGray;
            }
        }
        protected void btn_PoGlasovima_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Pitanja.DataSource = temp.getPitanjaByVotes();
                dl_Pitanja.DataBind();
                btn_MojiTagovi.BackColor = Color.LightGray;
                btn_Najnovija.BackColor = Color.LightGray;
                btn_PoGlasovima.BackColor = Color.LightGreen;
            }
        }


    }
}