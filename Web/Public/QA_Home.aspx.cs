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
using Lucene.Net.Search.Function;
using Lucene.Net.Search.Payloads;
using Lucene.Net.Search.Spans;
using Lucene.Net.QueryParsers;
using Lucene.Net.Util.Cache;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Lucene.Net.Search.Highlight;
using Data.Lucene;
using System.Drawing;


namespace Web.Public
{
    public partial class QA_Home : System.Web.UI.Page
    {
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Button btn_Pitanja = (Button)Master.FindControl("btn_Pitanja");
            //btn_Pitanja.BackColor = Color.LightGreen;

            using (TriglavBL temp = new TriglavBL())
            {
                               
                if (Request.QueryString["Pretraga"] != null)
                {
                    dl_Pitanja.DataSource = Data.Lucene.Pretraga.searchPitanja(Request.QueryString["Pretraga"]);
                    dl_Pitanja.DataBind();
                }
                else if (Request.QueryString["TagID"] != null)
                {
                    dl_Pitanja.DataSource = Data.Lucene.Pretraga.getPitanjaByTag(Convert.ToInt32(Request.QueryString["TagID"]));
                    dl_Pitanja.DataBind();
                }
                else
                {
                    korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];
                        if (korisnik != null)
                        {
                            //Sistem preporuke
                            List<Post> listaMojihClanaka = temp.GetPreporukaClanaka(korisnik.id);
                            List<Post> listaMojihPitanja = temp.GetPreporukaPitanja(korisnik.id);

                            if (listaMojihClanaka != null || listaMojihPitanja != null)
                            {
                                dl_Pitanja.DataSource = listaMojihPitanja.Take(5);
                                dl_Pitanja.DataBind();
                            }
                            else
                            {
                                dl_Pitanja.DataSource = temp.getPitanjaByDate();
                                dl_Pitanja.DataBind();
                            }
                        }
                        else
                        {
                            dl_Pitanja.DataSource = temp.getPitanjaByDate();
                            dl_Pitanja.DataBind();
                        }
                }

                if (!IsPostBack)
                {
                    //ovdje ide sistem preporuke                      
                    lbl_NaslovStranice.Text = "Najnovija pitanja";
                    btn_Posljednje.BackColor = Color.LightGray;
                    btn_Istaknuti.BackColor = Color.LightGray;
                    btn_Hot.BackColor = Color.LightGray;
                    btn_OveSedmice.BackColor = Color.LightGray;
                    btn_OvogMjeseca.BackColor = Color.LightGray;
                }
                rpt_Tagovi.DataSource = temp.getTagoviClanciPopularni();
                rpt_Tagovi.DataBind();

                rpt_TagoviQA.DataSource = temp.getTagoviPitanjaPopularni();
                rpt_TagoviQA.DataBind();

                dl_Kategorije.DataSource = temp.getKategorijeAllPopular().Take(10);
                dl_Kategorije.DataBind();

                dl_Podkategorije.DataSource = temp.getPodKategorijaAllPopular().Take(10);
                dl_Podkategorije.DataBind();
            }

        }

        protected void dl_Clanci_ItemDataBound(object sender, DataListItemEventArgs e)
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
                reputacija.Text = "Reputacija: " + k.Reputacija;

                PodKategorija pk = temp.getPodKategorijaByID(p.PodKategorija.Value);

                LinkButton lb_oPodKategorija = (LinkButton)e.Item.FindControl("lb_oPodKategorija");
                lb_oPodKategorija.Text = pk.Naslov;
                lb_oPodKategorija.PostBackUrl = "/Public/QA_Pitanja.aspx?PodKategorijaID=" + pk.id;

                LinkButton lb_oKategorija = (LinkButton)e.Item.FindControl("lb_oKategorija");
                lb_oKategorija.Text = temp.getKategorijaByID(pk.KategorijaID.Value).Naslov;
                lb_oKategorija.PostBackUrl = "/Public/QA_Pitanja.aspx?KategorijaID=" + temp.getKategorijaByID(pk.KategorijaID.Value).id;

                

                //System.Web.UI.WebControls.Image img_BedzVlsanika = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_BedzVlsanika");
                //img_BedzVlsanika.ImageUrl = k.BedzSlika;

                //Label lbl_NazivBedzaVlasnika = (Label)e.Item.FindControl("lbl_NazivBedzaVlasnika");
                //lbl_NazivBedzaVlasnika.Text = k.BedzNaziv;
                //lbl_NazivBedzaVlasnika.ToolTip = k.BedzOpis;

                //Label lbl_Gold = (Label)e.Item.FindControl("lbl_Gold");
                //Label lbl_Silver = (Label)e.Item.FindControl("lbl_Silver");
                //Label lbl_Bronze = (Label)e.Item.FindControl("lbl_Bronze");
                //lbl_Gold.Text = "Zlatnika" + Convert.ToString(k.BrojZlatnih);
                //lbl_Silver.Text = "Dukata" + Convert.ToString(k.BrojSrebrenih);
                //lbl_Bronze.Text = "Groševa" + Convert.ToString(k.BrojBronzanih);
            }

        }
        protected void btn_Posljednje_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                lbl_NaslovStranice.Text = "Najnovija pitanja";
                dl_Pitanja.DataSource = temp.getPitanjaByDate();
                dl_Pitanja.DataBind();
                btn_Posljednje.BackColor = Color.White;
                btn_Istaknuti.BackColor = Color.LightGray;
                btn_Hot.BackColor = Color.LightGray;
                btn_OveSedmice.BackColor = Color.LightGray;
                btn_OvogMjeseca.BackColor = Color.LightGray;
            }
        }
        protected void btn_Istaknuti_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Istaknuta pitanja";
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Pitanja.DataSource = temp.getPitanjaByVotes();
                dl_Pitanja.DataBind();
                btn_Posljednje.BackColor = Color.LightGray;
                btn_Istaknuti.BackColor = Color.White;
                btn_Hot.BackColor = Color.LightGray;
                btn_OveSedmice.BackColor = Color.LightGray;
                btn_OvogMjeseca.BackColor = Color.LightGray;
            }
        }
        protected void btn_Hot_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Top pitanja";
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Pitanja.DataSource = temp.getPitanjaByPregledi();
                dl_Pitanja.DataBind();
                btn_Posljednje.BackColor = Color.LightGray;
                btn_Istaknuti.BackColor = Color.LightGray;
                btn_Hot.BackColor = Color.White;
                btn_OveSedmice.BackColor = Color.LightGray;
                btn_OvogMjeseca.BackColor = Color.LightGray;
            }
        }
        protected void btn_OveSedmice_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Top pitanja ove sedmice";
            using (TriglavBL temp = new TriglavBL())
            {
                Post p = temp.getPostByID(88);               
                dl_Pitanja.DataSource = temp.getPitanjaBySedmica();
                dl_Pitanja.DataBind();
                btn_Posljednje.BackColor = Color.LightGray;
                btn_Istaknuti.BackColor = Color.LightGray;
                btn_Hot.BackColor = Color.LightGray;
                btn_OveSedmice.BackColor = Color.White;
                btn_OvogMjeseca.BackColor = Color.LightGray;
            }
        }
        protected void btn_OvogMjeseca_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                lbl_NaslovStranice.Text = "Top pitanja ovog mjeseca";
                dl_Pitanja.DataSource = temp.getPitanjaByMjesec();
                dl_Pitanja.DataBind();
                btn_Posljednje.BackColor = Color.LightGray;
                btn_Istaknuti.BackColor = Color.LightGray;
                btn_Hot.BackColor = Color.LightGray;
                btn_OveSedmice.BackColor = Color.LightGray;
                btn_OvogMjeseca.BackColor = Color.White;
            }
        }

        protected void rpt_Tagovi_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                int tagCount = temp.getTagCount(id);
                Label lbl_brojtagova = (Label)e.Item.FindControl("lbl_BrojTagovanihPostova");
                lbl_brojtagova.Text = " x " + Convert.ToString(tagCount);
            }
        }
        protected void rpt_TagoviQA_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                int tagCount = temp.getTagCount(id);
                Label lbl_brojtagova = (Label)e.Item.FindControl("lbl_BrojTagovanihPostova");
                lbl_brojtagova.Text = " x " + Convert.ToString(tagCount);
            }
        }

        protected void dl_Kategorije_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                int ClanciCount = temp.getClanciCountBykategorija(id);
                int PitanjaCount = temp.getPitanjaCountBykategorija(id);

                LinkButton lb_BrojClanaka = (LinkButton)e.Item.FindControl("lb_BrojClanaka");
                lb_BrojClanaka.Text = " W > " + Convert.ToString(ClanciCount);
                lb_BrojClanaka.PostBackUrl = "/Public/Home.aspx?KategorijaID=" + id;


                LinkButton lb_BrojPitanja = (LinkButton)e.Item.FindControl("lb_BrojPitanja");
                lb_BrojPitanja.Text = " QA > " + Convert.ToString(PitanjaCount);
                lb_BrojPitanja.PostBackUrl = "/Public/QA_Pitanja.aspx?KategorijaID=" + id;
            }
        }

        protected void dl_Podkategorije_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                int ClanciCount = temp.getClanciCountByPodkategorija(id);
                int PitanjaCount = temp.getPitanjaCountByPodkategorija(id);

                LinkButton lb_BrojClanakaP = (LinkButton)e.Item.FindControl("lb_BrojClanakaP");
                lb_BrojClanakaP.Text = " W > " + Convert.ToString(ClanciCount);
                lb_BrojClanakaP.PostBackUrl = "/Public/Home.aspx?PodKategorijaID=" + id;


                LinkButton lb_BrojPitanjaP = (LinkButton)e.Item.FindControl("lb_BrojPitanjaP");
                lb_BrojPitanjaP.Text = " QA > " + Convert.ToString(PitanjaCount);
                lb_BrojPitanjaP.PostBackUrl = "/Public/QA_Pitanja.aspx?PodKategorijaID=" + id;
            }
        }        

    }
}
 