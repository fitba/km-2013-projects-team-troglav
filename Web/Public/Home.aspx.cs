using System;
using System.Data;
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
//using Lucene.Net.Search.Vectorhighlight;
using Data.Lucene;
using System.Drawing;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;


namespace Web.Public
{
    public partial class Home : System.Web.UI.Page
    {
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }
        public List<Post> listaMojihPitanja { get; set; }
        public List<Post> listaMojihClanaka { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                Directory lc = Data.Lucene.Indexing.GetDirectoryClanci(); // ovo brisati samo indeksiranje proba 
                Directory lp = Data.Lucene.Indexing.GetDirectoryPitanja(); // ovo brisati samo indeksiranje proba 

                txt_TrenutnoUredjujemo.Text = "Trenutno uređujemo " + temp.getCountAllPosts(1) + " članaka!";

                if (Request.QueryString["Pretraga"] != null)
                {
                    //Pretraga Wiki preporuke term & call function
                    string term = Request.QueryString["Pretraga"];
                    string func = "searchWithClick('" + term + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), term, func, true);
                   
                    dl_Clanci.DataSource = Data.Lucene.Pretraga.searchClanci(Request.QueryString["Pretraga"]);
                    dl_Clanci.DataBind();
                    dl_Pitanja.DataSource = Data.Lucene.Pretraga.searchPitanja(Request.QueryString["Pretraga"]);
                    dl_Pitanja.DataBind();
                    lbl_TrazenaRijec.Text = "Tražili ste pojam: " + Request.QueryString["Pretraga"];
                    lbl_BrojRezlutataPretrage.Text = "Za vas smo pronašli " + dl_Clanci.Items.Count + " rezultata ";
                    btn_Posljednje.BackColor = Color.LightGray;
                    btn_Istaknuti.BackColor = Color.LightGray;
                    btn_Hot.BackColor = Color.LightGray;
                    btn_OveSedmice.BackColor = Color.LightGray;
                    btn_OvogMjeseca.BackColor = Color.LightGray;
                }
                else if (Request.QueryString["TagID"] != null)
                {
                    //Tag Wiki preporuke term & call function 
                    string term = temp.getTagByID(Convert.ToInt32(Request.QueryString["TagID"])).Naziv;
                    string func = "searchWithClick('" + term + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), term, func, true);

                    dl_Clanci.DataSource = Data.Lucene.Pretraga.getClanciByTag(Convert.ToInt32(Request.QueryString["TagID"]));
                    dl_Clanci.DataBind();
                    dl_Pitanja.DataSource = Data.Lucene.Pretraga.getPitanjaByTag(Convert.ToInt32(Request.QueryString["TagID"]));
                    dl_Pitanja.DataBind();
                    lbl_TrazenaRijec.Text = "Tražili ste poveznicu: " + temp.getTagByID(Convert.ToInt32(Request.QueryString["TagID"])).Naziv;
                    lbl_BrojRezlutataPretrage.Text = "Za vas smo pronašli " + dl_Clanci.Items.Count + " rezultata";
                }
                else if (Request.QueryString["PodKategorijaID"] != null)
                {
                    //Pretraga Wiki preporuke term & call function
                    string term = temp.getPodKategorijaByID(Convert.ToInt32(Request.QueryString["PodKategorijaID"])).Naslov;
                    string func = "searchWithClick('" + term + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), term, func, true);

                    dl_Clanci.DataSource = temp.GetClanciByPodKategorija(Convert.ToInt32(Request.QueryString["PodKategorijaID"]));
                    dl_Clanci.DataBind();
                    dl_Pitanja.DataSource = temp.GetPitanjaByPodKategorija(Convert.ToInt32(Request.QueryString["PodKategorijaID"]));
                    dl_Pitanja.DataBind();
                    lbl_TrazenaRijec.Text = "Tražili ste članke sa podkategorijom: " + temp.getPodKategorijaByID(Convert.ToInt32(Request.QueryString["PodKategorijaID"])).Naslov;
                    lbl_BrojRezlutataPretrage.Text = "Za vas smo pronašli " + dl_Clanci.Items.Count + " rezultata";
                }
                else if (Request.QueryString["KategorijaID"] != null)
                {
                    //Pretraga Wiki preporuke term & call function
                    string term = temp.getPodKategorijaByID(Convert.ToInt32(Request.QueryString["KategorijaID"])).Naslov;
                    string func = "searchWithClick('" + term + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), term, func, true);

                    dl_Clanci.DataSource = temp.GetClanciByKategorija(Convert.ToInt32(Request.QueryString["KategorijaID"]));
                    dl_Clanci.DataBind();
                    dl_Pitanja.DataSource = temp.GetPitanjaByKategorija(Convert.ToInt32(Request.QueryString["KategorijaID"]));
                    dl_Pitanja.DataBind();
                    lbl_TrazenaRijec.Text = "Tražili ste članke sa kategorijom: " + temp.getKategorijaByID(Convert.ToInt32(Request.QueryString["KategorijaID"])).Naslov;
                    lbl_BrojRezlutataPretrage.Text = "Za vas smo pronašli " + dl_Clanci.Items.Count + " rezultata";
                }
                else
                {
                    korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];
                        if (korisnik != null)
                        {
                            //Sistem preporuke
                            listaMojihClanaka = temp.GetPreporukaClanaka(korisnik.id);
                            listaMojihPitanja = temp.GetPreporukaPitanja(korisnik.id);

                            if (listaMojihClanaka != null || listaMojihPitanja != null)
                            {
                                //Pretraga Wiki preporuke term & call function
                                string term = "Knowledge management";
                                string func = "searchWithClick('" + term + "');";
                                ClientScript.RegisterStartupScript(this.GetType(), term, func, true);

                                dl_Clanci.DataSource = listaMojihClanaka.Take(5);
                                dl_Clanci.DataBind();
                                dl_Pitanja.DataSource = listaMojihPitanja.Take(5);
                                dl_Pitanja.DataBind();
                            }
                            else
                            {
                                //Pretraga Wiki preporuke term & call function
                                string term = "Knowledge management";
                                string func = "searchWithClick('" + term + "');";
                                ClientScript.RegisterStartupScript(this.GetType(), term, func, true);

                                listaMojihClanaka = temp.getClanciByDate();
                                listaMojihPitanja = temp.getPitanjaByDate();

                                dl_Clanci.DataSource = listaMojihClanaka.Take(5);
                                dl_Clanci.DataBind();
                                dl_Pitanja.DataSource = listaMojihPitanja.Take(5);
                                dl_Pitanja.DataBind();
                            }
                        }
                        else
                        {
                            //Pretraga Wiki preporuke term & call function
                            string term = "Knowledge management";
                            string func = "searchWithClick('" + term + "');";
                            ClientScript.RegisterStartupScript(this.GetType(), term, func, true);

                            listaMojihClanaka = temp.getClanciByDate();
                            listaMojihPitanja = temp.getPitanjaByDate();

                            dl_Clanci.DataSource = listaMojihClanaka.Take(5);
                            dl_Clanci.DataBind();
                            dl_Pitanja.DataSource = listaMojihPitanja.Take(5);
                            dl_Pitanja.DataBind();
                        }

                        lbl_NaslovStranice.Text = "Za vas smo izabrali ";
                        btn_Posljednje.BackColor = Color.LightGray;
                        btn_Istaknuti.BackColor = Color.LightGray;
                        btn_Hot.BackColor = Color.LightGray;
                        btn_OveSedmice.BackColor = Color.LightGray;
                        btn_OvogMjeseca.BackColor = Color.LightGray;
                    
                }

                //DATA LISTE
                rpt_Tagovi.DataSource = temp.getTagoviClanciPopularni().Take(10);
                rpt_Tagovi.DataBind();

                rpt_TagoviQA.DataSource = temp.getTagoviPitanjaPopularni().Take(10);
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

               // img_Clanak_Photo

                Post p = temp.getPostByID(id);
                Data.EntityFramework.DAL.Korisnik k = temp.getKorisnikByID(p.VlasnikID.Value);

                System.Web.UI.WebControls.Image img_User= (System.Web.UI.WebControls.Image)e.Item.FindControl("img_User");
                img_User.ImageUrl = k.SlikaURL;

                System.Web.UI.WebControls.Image img_Clanak_Photo = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_Clanak_Photo");
                img_Clanak_Photo.ImageUrl = p.SlikaURL;

                Label reputacija = (Label)e.Item.FindControl("lbl_Reputacija");
                reputacija.Text = "Reputacija: " + k.Reputacija;

                PodKategorija pk = temp.getPodKategorijaByID(p.PodKategorija.Value);

                LinkButton lb_oPodKategorija = (LinkButton)e.Item.FindControl("lb_oPodKategorija");
                lb_oPodKategorija.Text =  pk.Naslov;
                lb_oPodKategorija.PostBackUrl = "/Public/QA_Pitanja.aspx?PodKategorijaID=" + pk.id;

                LinkButton lb_oKategorija = (LinkButton)e.Item.FindControl("lb_oKategorija");
                lb_oKategorija.Text =  temp.getKategorijaByID(pk.KategorijaID.Value).Naslov;
                lb_oKategorija.PostBackUrl = "/Public/QA_Pitanja.aspx?KategorijaID=" + temp.getKategorijaByID(pk.KategorijaID.Value).id;

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
                reputacija.Text = "Reputacija: " + k.Reputacija;

                PodKategorija pk = temp.getPodKategorijaByID(p.PodKategorija.Value);

                LinkButton lb_oPodKategorija = (LinkButton)e.Item.FindControl("lb_oPodKategorija");
                lb_oPodKategorija.Text = pk.Naslov;
                lb_oPodKategorija.PostBackUrl = "/Public/QA_Pitanja.aspx?PodKategorijaID=" + pk.id;

                LinkButton lb_oKategorija = (LinkButton)e.Item.FindControl("lb_oKategorija");
                lb_oKategorija.Text = temp.getKategorijaByID(pk.KategorijaID.Value).Naslov;
                lb_oKategorija.PostBackUrl = "/Public/QA_Pitanja.aspx?KategorijaID=" + temp.getKategorijaByID(pk.KategorijaID.Value).id;
                
            }

        }

        protected void btn_Posljednje_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                lbl_NaslovStranice.Text = "Najnoviji članci";
                dl_Clanci.DataSource = temp.getClanciByDate().Take(5);
                dl_Clanci.DataBind();
                btn_Posljednje.BackColor = Color.White;
                btn_Istaknuti.BackColor = Color.LightGray;
                btn_Hot.BackColor = Color.LightGray;
                btn_OveSedmice.BackColor = Color.LightGray;
                btn_OvogMjeseca.BackColor = Color.LightGray;
            }
        }
        protected void btn_Istaknuti_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Istaknuti članci";
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Clanci.DataSource = temp.getClanciByVotes().Take(5); 
                dl_Clanci.DataBind();
                btn_Posljednje.BackColor = Color.LightGray; 
                btn_Istaknuti.BackColor = Color.White;
                btn_Hot.BackColor = Color.LightGray;
                btn_OveSedmice.BackColor = Color.LightGray;
                btn_OvogMjeseca.BackColor = Color.LightGray;
            }
        }
        protected void btn_Hot_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Top članci";
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Clanci.DataSource = temp.getClanciByPregledi().Take(5);
                dl_Clanci.DataBind();
                btn_Posljednje.BackColor = Color.LightGray;
                btn_Istaknuti.BackColor = Color.LightGray;
                btn_Hot.BackColor = Color.White;
                btn_OveSedmice.BackColor = Color.LightGray;
                btn_OvogMjeseca.BackColor = Color.LightGray;
            }
        }
        protected void btn_OveSedmice_Click(object sender, EventArgs e)
        {
            
            using (TriglavBL temp = new TriglavBL())
            {
                lbl_NaslovStranice.Text = "Top članci ove sedmice";
                dl_Clanci.DataSource = temp.getClanciBySedmica().Take(5);
                dl_Clanci.DataBind();
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
                lbl_NaslovStranice.Text = "Top članci ovog mjeseca";
                dl_Clanci.DataSource = temp.getClanciByMjesec().Take(5);
                dl_Clanci.DataBind();
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