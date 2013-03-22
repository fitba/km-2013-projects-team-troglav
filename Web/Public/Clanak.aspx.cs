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
    public partial class Clanak : System.Web.UI.Page
    {
        private int postId;
        public Data.EntityFramework.DAL.Post post { get; set; }
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }
        public Data.EntityFramework.DAL.User_Likes user_likes { get; set; }

        /// <summary>
        /// PAGE LOAD /////////////////////////////////////////////////////////////////////////////
        /// </summary>
        ///
        protected void Page_Load(object sender, EventArgs e)
        {
            korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];

            using (TriglavBL temp = new TriglavBL())
            {
                if (Request.QueryString != null)
                {
                    if (Request.QueryString["PostID"] != null)
                    {
                        postId = Int32.Parse(Request.QueryString["PostID"]);
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

            btn_GlavnaStranica.BackColor = System.Drawing.Color.LightGray;
            btn_Razgovor.BackColor = System.Drawing.Color.LightGray;
            btn_Citaj.BackColor = System.Drawing.Color.LightGray;
            btn_VidiIzvornik.BackColor = System.Drawing.Color.LightGray;
            btn_VidiIzmjene.BackColor = System.Drawing.Color.LightGray;
        }
        private void LoadClanak(int postId)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                post = temp.getPostByID(postId);
                lbl_Naslov.Text = post.Naslov;
                lit_Sazetak.Text = post.Sazetak;
                img_Clanak_Photo.ImageUrl = post.SlikaURL;

                if (post.PrihvaceniOdgovori > 0)
                {
                    lit_Sazetak.Text = temp.getPosljednjaIzmjenaClanka(post.id).Sadrzaj;
                }
                lit_Sadrzaj.Text = post.Sadrzaj;

                lbl_BrojKomentara.Text = "Komentara: " + Convert.ToString(post.BrojKomentara.Value); ;
                lbl_BrojPregleda.Text = "Pregleda: " + Convert.ToString(post.BrojPregleda.Value);
                lbl_BrojPrihvacenihodgovora.Text = "Odgovora: " + Convert.ToString(post.PrihvaceniOdgovori.Value);
                lbl_KorisnikNadimak.Text = "Korisnik: " + Convert.ToString(post.VlasnikNadimak);
                lbl_KorisnikNadimak.GetRouteUrl("/Public/Korisnik.aspx?id=" + post.VlasnikID.Value);
                lbl_Kreirano.Text = "Članak kreiran: " + Convert.ToString(post.DatumKreiranja.Value);
                

                lbl_Reputacija.Text = "Reputacija: " + Convert.ToString(post.Korisnik.Reputacija.Value);
                //lbl_biografija.Text = "Biografija:\n " + Convert.ToString(post.Korisnik.OMeni);

                GetVotesInfo(); //Votes info
                lbl_VotesScore.Text = Convert.ToString(post.BrojPoena);
                GetRatesInfo(); //Rates info
                KomentariBox.PostId = postId;

                dl_WikiPoveznice.DataSource = Data.Lucene.Pretraga.getClanciPretrage(post.Tagovi).Take(5);                
                dl_WikiPoveznice.DataBind();

                dl_QAPoveznice.DataSource = Data.Lucene.Pretraga.getPitanjaPretrage(post.Tagovi).Take(5);
                dl_QAPoveznice.DataBind();

                //Preporuka korisnici

                List<Tag> lt = temp.getTagoviClanka(post.id);
                List<Data.EntityFramework.DAL.Korisnik> lk = new List<Data.EntityFramework.DAL.Korisnik>();
                foreach (var t in lt)
                {
                    lk.AddRange(temp.GetKorisniciRelatedToClanakTags(t));
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
                        if (temp.getUserLikes(korisnik.id, post.id) == null)
                        {
                            lbl_Likes.Text = Convert.ToString(post.Likes.Value) + " osoba je glasovalo za! ";
                            lbl_Unlikes.Text = Convert.ToString(post.Unlikes.Value) + " osoba je glasovalo protiv! ";
                            btn_Like.Enabled = true; // btn vote up
                            btn_Unlike.Enabled = true; // btn vote down  
                        }
                        else
                        {
                            user_likes = temp.getUserLikes(korisnik.id, post.id);
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
                    }
                    else
                    {
                        lbl_Likes.Text = Convert.ToString(post.Likes.Value) + " osoba je glasovalo za! ";
                        lbl_Unlikes.Text = Convert.ToString(post.Unlikes.Value) + " osoba je glasovalo protiv! ";
                        btn_Like.Enabled = true;
                        btn_Unlike.Enabled = true;

                    }
                }

                else
                {
                    lbl_Likes.Text = Convert.ToString(post.Likes.Value) + " osoba je glasovalo za! ";
                    lbl_Unlikes.Text = Convert.ToString(post.Unlikes.Value) + " osoba je glasovalo protiv! ";
                    btn_Like.Enabled = false;
                    btn_Unlike.Enabled = false;
                }
            }
        }
        protected void btn_Like_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                user_likes = temp.getUserLikes(korisnik.id, post.id);

                if (user_likes == null)
                {
                    user_likes = new Data.EntityFramework.DAL.User_Likes();
                    user_likes.PostId = post.id;
                    user_likes.UserId = korisnik.id;
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
        protected void btn_Unlike_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                user_likes = temp.getUserLikes(korisnik.id, post.id);
                if (user_likes == null)
                {
                    user_likes = new Data.EntityFramework.DAL.User_Likes();
                    user_likes.PostId = post.id;
                    user_likes.UserId = korisnik.id;
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
                lbl_BrojPoena.Text = "Rejting: " + Convert.ToString(post.BrojOmiljenih.Value / post.BrojRangiranja.Value);
            else
                lbl_BrojPoena.Text = "Rejting: " + 0;
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
        private void EnableAllRateButtons()
        {
            btn_RateThis_01.Enabled = true;
            btn_RateThis_02.Enabled = true;
            btn_RateThis_03.Enabled = true;
            btn_RateThis_04.Enabled = true;
            btn_RateThis_05.Enabled = true;
            lbl_RateThis.Text = "Ocjeni članak";
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
                    user_likes.DaumRated = DateTime.Now;
                    user_likes.DatumLajkanja = DateTime.Now;
                    user_likes.isLiked = 0;
                    user_likes.Rate = rate; // setujemo rate
                    temp.SaveUser_Likes_Rates(user_likes);
                }
                else
                {
                    Int32 RateBefore = temp.getRateUserLikes(user_likes.UserId.Value, user_likes.PostId.Value);
                    user_likes.DaumRated = DateTime.Now;
                    user_likes.Rate = rate;
                    temp.UpdateUser_Likes_Rates(user_likes, RateBefore);
                }
            }
            LoadClanak(post.id);
        }


        /// <summary>
        /// LINK BUTTONS////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 
        protected void btn_GlavnaStranica_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }
        protected void btn_Razgovor_Click(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                List<Post> listaRazgovora = temp.getRazgovoriPrihvaceniID(post.id);
                if (listaRazgovora != null)
                {
                    Response.Redirect("/Public/ClanakRazgovor.aspx?PostID=" + post.id);
                }

            }
        }
        protected void btn_Citaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }
        protected void btn_VidiIzvornik_Click(object sender, EventArgs e)
        {
            if (korisnik != null)
            {
                Response.Redirect("/Public/ClanakUredi.aspx?PostID=" + post.id);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "Login()", true);
            }
        }
        protected void btn_VidiIzmjene_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakStareIzmjene.aspx?PostID=" + post.id);
        }

    }
}