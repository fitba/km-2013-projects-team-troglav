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
    public partial class QA_Pitanje : System.Web.UI.Page
    {
        private int postId;
        public Data.EntityFramework.DAL.Post post { get; set; }
        public Data.EntityFramework.DAL.Post odgovor { get; set; }
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }
        public Data.EntityFramework.DAL.Korisnik vlasnikPosta { get; set; }
        public Data.EntityFramework.DAL.User_Likes user_likes { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            Button btn_Pitanja = (Button)Master.FindControl("btn_Pitanja");
            //btn_Pitanja.BackColor = Color.LightGreen;

            korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];
            using (TriglavBL temp = new TriglavBL())
            {
                if (Request.QueryString != null)
                {
                    if (Request.QueryString["PostID"] != null)
                    {
                        postId = Int32.Parse(Request.QueryString["PostID"]);
                        post = temp.getPostByID(postId);
                        if (post.PostVrsta == 6)
                            post = temp.getPostByID(post.RoditeljskiPostID.Value);
                    }
                    else
                    {
                        Response.Redirect("/Public/QA_Pitanja.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/Public/QA_Pitanja.aspx");
                }

                if (!IsPostBack)
                {
                    post.BrojPregleda++; //dodavanje pregleda
                    temp.UpdatePost(post);
                }
                LoadClanak(post.id);
            }
        }
        private void LoadClanak(int postId)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                vlasnikPosta = temp.getKorisnikByID(post.VlasnikID.Value);
                post = temp.getPostByID(postId);
                lbl_Naslov.Text = post.Naslov;
                lit_Sadrzaj.Text = post.Sadrzaj;
                // lbl_BrojKomentara.Text = "Broj Komentara: " + Convert.ToString(post.BrojKomentara.Value); 
                lbl_BrojPregleda.Text = Convert.ToString(post.BrojPregleda.Value);
                lbl_BrojPrihvacenihodgovora.Text = Convert.ToString(post.BrojOdgovora.Value);
                lbl_KorisnikNadimak.Text = Convert.ToString(post.VlasnikNadimak);
                lbl_KorisnikNadimak.GetRouteUrl("/Public/Korisnik.aspx?id=" + post.VlasnikID.Value);
                lbl_Kreirano.Text = "Kreirano: " + Convert.ToString(post.DatumKreiranja.Value);
                lbl_Reputacija.Text = Convert.ToString(post.Korisnik.Reputacija.Value);
                img_BedzVlsanika.ImageUrl = vlasnikPosta.BedzSlika;
                lbl_NazivBedzaVlasnika.Text = vlasnikPosta.BedzNaziv;
                lbl_NazivBedzaVlasnika.ToolTip = vlasnikPosta.BedzOpis;

                GetVotesInfo(); //Votes info
                lbl_VotesScore.Text = Convert.ToString(post.BrojPoena);
                GetRatesInfo(); //Rates info
                //KomentariBox.PostId = postId;

                lb_oPodKategorija.Text = "Tema: " + temp.getPodKategorijaByID(post.PodKategorija.Value).Naslov;
                lb_oPodKategorija.PostBackUrl = "/Public/QA_Pitanja.aspx?PodKategorijaID=" + post.PodKategorija.Value;

                lb_oKategorija.Text = "Oblast:" + temp.getKategorijaByID(temp.getPodKategorijaByID(post.PodKategorija.Value).KategorijaID.Value).Naslov;
                lb_oKategorija.PostBackUrl = "/Public/QA_Pitanja.aspx?KategorijaID=" + temp.getKategorijaByID(temp.getPodKategorijaByID(post.PodKategorija.Value).KategorijaID.Value).id;

                //LISTA ODGOVORA

                dl_odgovori.DataSource = temp.getOdgovoriByPitanjeID(post.id);
                dl_odgovori.DataBind();

                dl_WikiPoveznice.DataSource = Data.Lucene.Pretraga.getClanciPretrage(post.Tagovi).Take(5);
                dl_WikiPoveznice.DataBind();

                dl_QAPoveznice.DataSource = Data.Lucene.Pretraga.getPitanjaPretrage(post.Tagovi).Take(5);
                dl_QAPoveznice.DataBind();

                //Preporuka korisnici

                List<Tag> lt = temp.getTagoviPitanja(post.id);
                List<Data.EntityFramework.DAL.Korisnik> lk = new List<Data.EntityFramework.DAL.Korisnik>();
                foreach (var t in lt)
                {
                    lk.AddRange(temp.GetKorisniciRelatedToPitanjeTags(t));
                }
                dl_Korisnici.DataSource = lk.OrderByDescending(x => x.Reputacija).Distinct();
                dl_Korisnici.DataBind();


            }
        }
        /// <summary>
        /// USER VOTES POSTS/////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        private void GetVotesInfo()
        {

            using (TriglavBL temp = new TriglavBL())
            {
                if (korisnik != null)
                {
                    user_likes = temp.getUserLikes(korisnik.id, post.id);
                    if (user_likes != null)
                    {
                        if (user_likes.isLiked == 0)
                        {
                            lbl_Likes.Text = Convert.ToString(post.Likes.Value) + " osoba je glasovalo za! ";
                            lbl_Unlikes.Text = Convert.ToString(post.Unlikes.Value) + " osoba je glasovalo protiv! ";
                            btn_Like.Enabled = true;
                            btn_Unlike.Enabled = true;
                        }
                        if (user_likes.isLiked == 1)
                        {
                            lbl_Likes.Text = "Vi i još " + Convert.ToString(post.Likes.Value - 1) + " osoba je glasovalo za! ";
                            lbl_Unlikes.Text = Convert.ToString(post.Unlikes.Value) + " osoba je glasovalo protiv! ";
                            btn_Like.Enabled = false;
                            btn_Unlike.Enabled = true;
                        }
                        if (user_likes.isLiked == 2)
                        {
                            lbl_Likes.Text = Convert.ToString(post.Likes.Value) + " osoba je glasovalo za! ";
                            lbl_Unlikes.Text = "Vi i još " + Convert.ToString(post.Unlikes.Value - 1) + " osoba ste glasovali protiv! ";
                            btn_Like.Enabled = true;
                            btn_Unlike.Enabled = false;
                        }
                    }
                    else
                    {
                        lbl_Likes.Text = Convert.ToString(post.Likes.Value) + " osoba je glasovalo za! ";
                        lbl_Unlikes.Text = Convert.ToString(post.Unlikes.Value) + " osoba je glasovalo protiv! ";
                        btn_Like.Enabled = true; // btn vote up
                        btn_Unlike.Enabled = true; // btn vote down  
                    }

                }
                else
                {
                    lbl_Likes.Text = Convert.ToString(post.Likes.Value) + " osoba je glasovalo za! ";
                    lbl_Unlikes.Text = Convert.ToString(post.Unlikes.Value) + " osoba je glasovalo protiv! ";
                    btn_Like.Enabled = false; // btn vote up
                    btn_Unlike.Enabled = false; // btn vote down  
                }
            }
        }

        protected void btn_Like_Click(object sender, ImageClickEventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                user_likes = temp.getUserLikes(korisnik.id, post.id);

                if (user_likes == null)
                {
                    user_likes = new Data.EntityFramework.DAL.User_Likes();
                    user_likes.PostId = post.id;
                    user_likes.UserId = korisnik.id;
                    user_likes.DaumRated = DateTime.Now;
                    user_likes.DatumLajkanja = DateTime.Now;
                    user_likes.isLiked = 1;
                    user_likes.Rate = 0;
                    temp.SaveUser_Likes(user_likes);
                }
                else
                {
                    Int32 LikesBefore = temp.getVoteUserLikes(user_likes.UserId.Value, user_likes.PostId.Value);
                    user_likes.DatumLajkanja = DateTime.Now;
                    user_likes.isLiked = 1;
                    temp.UpdateUser_Likes(user_likes, LikesBefore);
                }
            }
            LoadClanak(post.id);
        }
        protected void btn_Unlike_Click(object sender, ImageClickEventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                user_likes = temp.getUserLikes(korisnik.id, post.id);
                if (user_likes == null)
                {
                    user_likes = new Data.EntityFramework.DAL.User_Likes();
                    user_likes.PostId = post.id;
                    user_likes.UserId = korisnik.id;
                    user_likes.DaumRated = DateTime.Now;
                    user_likes.DatumLajkanja = DateTime.Now;
                    user_likes.isLiked = 2;
                    user_likes.Rate = 0;
                    temp.SaveUser_Unlikes(user_likes);
                }
                else
                {
                    Int32 LikesBefore = temp.getVoteUserLikes(user_likes.UserId.Value, user_likes.PostId.Value);
                    user_likes.DatumLajkanja = DateTime.Now;
                    user_likes.isLiked = 2;
                    temp.UpdateUser_Likes(user_likes, LikesBefore);
                }
            }
            LoadClanak(post.id);

        }
        protected void btn_oLike_Click(object sender, ImageClickEventArgs e)
        {

            if (korisnik != null)
            {

                ImageButton _sender = (ImageButton)sender;
                HiddenField hid = (HiddenField)_sender.FindControl("hf");


                using (TriglavBL temp = new TriglavBL())
                {
                    Post odgovorNaLIsti = temp.getOdgovorByID(Convert.ToInt32(hid.Value));

                    if (odgovorNaLIsti != null)
                    {
                        user_likes = temp.getUserLikes(korisnik.id, odgovorNaLIsti.id);

                        if (user_likes == null)
                        {
                            user_likes = new Data.EntityFramework.DAL.User_Likes();
                            user_likes.PostId = odgovorNaLIsti.id;
                            user_likes.UserId = korisnik.id;
                            user_likes.DaumRated = DateTime.Now;
                            user_likes.DatumLajkanja = DateTime.Now;
                            user_likes.isLiked = 1;
                            user_likes.Rate = 0;
                            temp.SaveUser_Likes(user_likes);
                        }
                        else
                        {
                            Int32 LikesBefore = temp.getVoteUserLikes(user_likes.UserId.Value, user_likes.PostId.Value);
                            user_likes.DatumLajkanja = DateTime.Now;
                            user_likes.isLiked = 1;
                            temp.UpdateUser_Likes(user_likes, LikesBefore);
                        }
                    }
                }
                LoadClanak(post.id);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "Login()", true);
            }



        }
        protected void btn_oUnlike_Click(object sender, ImageClickEventArgs e)
        {
            if (korisnik != null)
            {
                ImageButton _sender = (ImageButton)sender;
                HiddenField hid = (HiddenField)_sender.FindControl("hf");


                using (TriglavBL temp = new TriglavBL())
                {
                    Post odgovorNaLIsti = temp.getOdgovorByID(Convert.ToInt32(hid.Value));

                    if (odgovorNaLIsti != null)
                    {
                        user_likes = temp.getUserLikes(korisnik.id, odgovorNaLIsti.id);

                        if (user_likes == null)
                        {
                            user_likes = new Data.EntityFramework.DAL.User_Likes();
                            user_likes.PostId = post.id;
                            user_likes.UserId = korisnik.id;
                            user_likes.DaumRated = DateTime.Now;
                            user_likes.DatumLajkanja = DateTime.Now;
                            user_likes.isLiked = 2;
                            user_likes.Rate = 0;
                            temp.SaveUser_Unlikes(user_likes);
                        }
                        else
                        {
                            Int32 LikesBefore = temp.getVoteUserLikes(user_likes.UserId.Value, user_likes.PostId.Value);
                            user_likes.DatumLajkanja = DateTime.Now;
                            user_likes.isLiked = 2;
                            temp.UpdateUser_Likes(user_likes, LikesBefore);
                        }

                    }
                }
                LoadClanak(post.id);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "Login()", true);
            }

        }


        /// <summary>
        /// USER RATES POSTS/////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        private void GetRatesInfo()
        {
            using (TriglavBL temp = new TriglavBL())
            {
                if (korisnik != null)
                {
                    if (temp.getUserLikes(korisnik.id, post.id) == null) //zapis u bazi ne postoji
                    {
                        EnableAllRateButtons();
                    }
                    else //zapis u bazi postoji
                    {
                        if (temp.getRateUserLikes(korisnik.id, post.id) == 0)
                        {
                            EnableAllRateButtons();
                        }
                        else
                        {
                            int userRate = temp.getRateUserLikes(korisnik.id, post.id);
                            SwitchRateButton(userRate);
                        }
                    }
                }
                else
                {
                    DisableAllRateButtons();
                }
            }
            if (post.BrojRangiranja > 0)
                lbl_Rejting.Text = Convert.ToString(post.BrojOmiljenih.Value / post.BrojRangiranja.Value);
            else
                lbl_Rejting.Text = Convert.ToString(0);

        }
        private void EnableAllRateButtons()
        {
            btn_RateThis_01.Enabled = true;
            btn_RateThis_02.Enabled = true;
            btn_RateThis_03.Enabled = true;
            btn_RateThis_04.Enabled = true;
            btn_RateThis_05.Enabled = true;
            lbl_RateThis.Text = "Ocjenite pitanje";
        }
        private void DisableAllRateButtons()
        {
            btn_RateThis_01.Enabled = false;
            btn_RateThis_02.Enabled = false;
            btn_RateThis_03.Enabled = false;
            btn_RateThis_04.Enabled = false;
            btn_RateThis_05.Enabled = false;
            lbl_RateThis.Text = "Za ocjenjivanje clanaka morate se logirati";
        }
        private void SwitchRateButton(int rated)
        {
            switch (rated)
            {
                case 1:
                    {
                        btn_RateThis_01.Enabled = false;
                        btn_RateThis_02.Enabled = true;
                        btn_RateThis_03.Enabled = true;
                        btn_RateThis_04.Enabled = true;
                        btn_RateThis_05.Enabled = true;
                        lbl_RateThis.Text = "You rated this Article with 1 star";
                    } break;
                case 2:
                    {
                        btn_RateThis_01.Enabled = true;
                        btn_RateThis_02.Enabled = false;
                        btn_RateThis_03.Enabled = true;
                        btn_RateThis_04.Enabled = true;
                        btn_RateThis_05.Enabled = true;
                        lbl_RateThis.Text = "You rated this Article with 2 star";
                    } break;
                case 3:
                    {
                        btn_RateThis_01.Enabled = true;
                        btn_RateThis_02.Enabled = true;
                        btn_RateThis_03.Enabled = false;
                        btn_RateThis_04.Enabled = true;
                        btn_RateThis_05.Enabled = true;
                        lbl_RateThis.Text = "You rated this Article with 3 star";
                    } break;
                case 4:
                    {
                        btn_RateThis_01.Enabled = true;
                        btn_RateThis_02.Enabled = true;
                        btn_RateThis_03.Enabled = true;
                        btn_RateThis_04.Enabled = false;
                        btn_RateThis_05.Enabled = true;
                        lbl_RateThis.Text = "You rated this Article with 4 star";
                    } break;
                case 5:
                    {
                        btn_RateThis_01.Enabled = true;
                        btn_RateThis_02.Enabled = true;
                        btn_RateThis_03.Enabled = true;
                        btn_RateThis_04.Enabled = true;
                        btn_RateThis_05.Enabled = false;
                        lbl_RateThis.Text = "You rated this Article with 5 star";
                    } break;

                default:
                    {
                        btn_RateThis_01.Enabled = true;
                        btn_RateThis_02.Enabled = true;
                        btn_RateThis_03.Enabled = true;
                        btn_RateThis_04.Enabled = true;
                        btn_RateThis_05.Enabled = true;
                    } break;
            }
        }
        protected void btn_RateThis_01_Click(object sender, EventArgs e)
        {
            RateThis(1);
        }
        protected void btn_RateThis_02_Click(object sender, EventArgs e)
        {
            RateThis(2);
        }
        protected void btn_RateThis_03_Click(object sender, EventArgs e)
        {
            RateThis(3);
        }
        protected void btn_RateThis_04_Click(object sender, EventArgs e)
        {
            RateThis(4);
        }
        protected void btn_RateThis_05_Click(object sender, EventArgs e)
        {
            RateThis(5);
        }
        private void RateThis(int rate)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                user_likes = temp.getUserLikes(korisnik.id, post.id);

                if (user_likes == null) //ako ne postoji zapis
                {
                    user_likes = new Data.EntityFramework.DAL.User_Likes();
                    user_likes.PostId = post.id;
                    user_likes.UserId = korisnik.id;
                    user_likes.DatumLajkanja = DateTime.Now;
                    user_likes.DaumRated = DateTime.Now;
                    user_likes.isLiked = 0;
                    user_likes.Rate = rate; // setujemo rate
                    temp.SaveUser_Likes_Rates(user_likes);
                }
                else
                {
                    Int32 RateBefore = temp.getRateUserLikes(user_likes.UserId.Value, user_likes.PostId.Value);
                    user_likes.DatumLajkanja = DateTime.Now;
                    user_likes.DaumRated = DateTime.Now;
                    user_likes.Rate = rate;
                    temp.UpdateUser_Likes_Rates(user_likes, RateBefore);
                }
            }
            LoadClanak(post.id);
        }

        protected void btn_SaveOdgovor_Click(object sender, EventArgs e)
        {
            if (korisnik != null)
            {
                if (txt_oSadrzaj.Text.Count() > 10)
                {
                    odgovor = new Data.EntityFramework.DAL.Post();
                    using (TriglavBL temp = new TriglavBL())
                    {
                        odgovor.PodKategorija = post.PodKategorija;
                        odgovor.PostVrsta = 6;    // pitanje               
                        odgovor.Naslov = "Odgovor";
                        odgovor.Sadrzaj = txt_oSadrzaj.Text;
                        odgovor.Sazetak = String.Empty;
                        odgovor.Tagovi = String.Empty;
                        odgovor.BrojKomentara = 0;
                        odgovor.BrojOdgovora = 0;
                        odgovor.BrojOmiljenih = 0;
                        odgovor.BrojPoena = 0;
                        odgovor.BrojPregleda = 0;
                        odgovor.BrojRangiranja = 0;
                        odgovor.DatumKreiranja = DateTime.Now;
                        odgovor.DatumZadnjeAktivnosti = DateTime.Now;
                        odgovor.DatumZadnjeIzmjene = DateTime.Now;
                        odgovor.Likes = 0;
                        odgovor.Unlikes = 0;
                        odgovor.PrihvacenaIzmjena = 0;
                        odgovor.PrihvaceniOdgovori = 0;
                        odgovor.RoditeljskiPostID = post.id;

                        korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];
                        if (korisnik != null)
                        {
                            odgovor.VlasnikID = korisnik.id;
                            odgovor.VlasnikNadimak = korisnik.Nadimak;
                        }
                        else
                        {
                            Response.Write("<script>alert('Greška! Molimo vas da se logirate!');</script>");
                            //Response.Redirect("/Login.aspx");
                            return;
                        }
                        Response.Write("<script>alert(Vaš članak je uspješno sačuvan!');</script>");
                        temp.SavePost(odgovor);
                        post.BrojOdgovora++;
                        post.DatumZadnjeAktivnosti = DateTime.Now;
                        post.DatumZadnjeIzmjene = DateTime.Now;
                        post.PromijenioID = korisnik.id;
                        temp.UpdatePost(post);
                        Response.Redirect("/Public/QA_Pitanje.aspx?PostID=" + post.id);
                    }
                }
                else return;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "Login()", true);
            }

        }

        protected void dl_odgovori_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                Post p = temp.getPostByID(id);
                Data.EntityFramework.DAL.Korisnik k = temp.getKorisnikByID(p.VlasnikID.Value);
                System.Web.UI.WebControls.Image img_User = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_User");
                img_User.ImageUrl = k.SlikaURL;

                Label reputacija = (Label)e.Item.FindControl("lbl_Reputacija");
                reputacija.Text = "Reputacija: " + k.Reputacija;

                System.Web.UI.WebControls.Image img_Bedz = (System.Web.UI.WebControls.Image)e.Item.FindControl("img_Bedz");
                img_Bedz.ImageUrl = k.BedzSlika;

                Label lbl_NazivBedza = (Label)e.Item.FindControl("lbl_NazivBedza");
                lbl_NazivBedza.Text = k.BedzNaziv;
                lbl_NazivBedza.ToolTip = k.BedzOpis;

                Label lbl_BrojPoena = (Label)e.Item.FindControl("lbl_BrojPoena");
                lbl_BrojPoena.Text = Convert.ToString(p.BrojPoena);
                if (p.BrojPoena >= 3)
                {
                    lbl_BrojPoena.BackColor = Color.YellowGreen;
                    lbl_BrojPoena.ForeColor = Color.Black;
                }
                if (p.BrojPoena >= 5)
                {
                    lbl_BrojPoena.BackColor = Color.OrangeRed;
                    lbl_BrojPoena.ForeColor = Color.Black;
                    lbl_BrojPoena.BorderColor = Color.Black;
                    lbl_BrojPoena.BorderStyle = BorderStyle.Dotted;
                }
                if (p.BrojPoena >= 10)
                {
                    lbl_BrojPoena.BackColor = Color.Black;
                    lbl_BrojPoena.ForeColor = Color.White;
                    lbl_BrojPoena.BorderColor = Color.Red;
                    lbl_BrojPoena.BorderStyle = BorderStyle.Dotted;
                }
                if (p.BrojPoena >= 10)
                {
                    lbl_BrojPoena.BackColor = Color.Beige;
                    lbl_BrojPoena.BorderColor = Color.Black;
                    lbl_BrojPoena.BorderStyle = BorderStyle.Dotted;
                }


                ImageButton like = (ImageButton)e.Item.FindControl("btn_oLike");
                ImageButton unlike = (ImageButton)e.Item.FindControl("btn_oUnlike");
                if (korisnik != null)
                {
                    User_Likes ulo;
                    if (temp.getUserLikes(korisnik.id, p.id) == null)
                    {
                        ulo = new Data.EntityFramework.DAL.User_Likes();
                        ulo.PostId = p.id;
                        ulo.UserId = korisnik.id;
                        ulo.DatumLajkanja = DateTime.Now;
                        ulo.DaumRated = DateTime.Now;
                        ulo.isLiked = 0;
                        temp.SaveUser_Likes(ulo);
                    }
                    else
                    {
                        ulo = temp.getUserLikes(korisnik.id, p.id);
                    }
                    like.Enabled = false;
                    unlike.Enabled = false;

                    if (ulo.isLiked == 0)
                    {
                        like.Enabled = true;
                        unlike.Enabled = true;
                    }
                    if (ulo.isLiked == 1)
                    {
                        like.Enabled = false;
                        unlike.Enabled = true;
                    }
                    if (ulo.isLiked == 2)
                    {
                        like.Enabled = true;
                        unlike.Enabled = false;
                    }
                }
                else
                {
                    like.Enabled = false;
                    unlike.Enabled = false;
                }
            }
        }

    }
}