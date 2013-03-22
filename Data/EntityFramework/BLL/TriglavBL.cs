using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Data.EntityFramework.DAL;



namespace Data.EntityFramework.BLL
{
    public class TriglavBL : IDisposable
    {
        TriglavEntities context;

        //KONSTRUKTOR BAZE/////////////////////////////////////////////////////////////////////////////////////////////
        public TriglavBL() { context = new TriglavEntities(); }
        public void Dispose() { context.Dispose(); }


        //KORISNIK ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Korisnik Autentifikacija_Usera(string uname, string pass) { return context.Korisnik.Where(x => x.Nadimak == uname && x.Lozinka == pass).SingleOrDefault(); }
        public void SaveKorisnik(Korisnik korisnik)
        {
            context.Korisnik.Add(korisnik);
            context.SaveChanges();
        }
        public void UpdateKorisnik(Korisnik korisnik)
        {
            Korisnik k = context.Korisnik.First(i => i.id == korisnik.id);
            k.BrojBronzanih = korisnik.BrojBronzanih;
            k.BrojGodina = korisnik.BrojGodina;
            k.BrojSrebrenih = korisnik.BrojSrebrenih;
            k.BrojZlatnih = korisnik.BrojZlatnih;
            k.DatumKreiranja = korisnik.DatumKreiranja;
            k.DatumZadnjegPristupa = korisnik.DatumZadnjegPristupa;
            k.Likes = korisnik.Likes;
            k.Lokacija = korisnik.Lokacija;
            k.Lozinka = korisnik.Lozinka;
            k.Nadimak = korisnik.Nadimak;
            k.OMeni = korisnik.OMeni;
            k.Pregleda = korisnik.Pregleda;
            k.Reputacija = korisnik.Reputacija;
            k.SlikaURL = korisnik.SlikaURL;
            k.temp = korisnik.temp;
            k.Unlikes = korisnik.Unlikes;
            k.BedzID = korisnik.BedzID;
            k.BedzNaziv = korisnik.BedzNaziv;
            k.BedzOpis = korisnik.BedzOpis;
            k.BedzSlika = korisnik.BedzSlika;
            context.SaveChanges();
        }
        public Korisnik getKorisnikByID(int p)
        {
            return context.Korisnik.Where(x => x.id == p).FirstOrDefault();
        }
        public Korisnik ProvjeriIme(string nadimak) { return context.Korisnik.Where(x => x.Nadimak == nadimak).SingleOrDefault(); }
        public string getSlikaByKorisnikId(int p) { return context.Korisnik.Where(x => x.id == p).SingleOrDefault().SlikaURL; }


        //KOMENTARI////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SaveKomentar(Komentari komentar)
        {
            context.Komentari.Add(komentar);
            context.SaveChanges();
        }
        public List<Komentari> getKomentariPosta(int id)
        {
            return context.Komentari.Where(x => x.PostID == id).ToList();

        }



        //POST/////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Post getPostByID(int p)
        {
            return context.Post.Where(x => x.id == p).SingleOrDefault();
        }
        public List<Post> getAllPosts()
        {
            return context.Post.Where(x => x.id != 0).OrderByDescending(y => y.DatumZadnjeIzmjene).OrderBy(z => z.BrojOdgovora).ToList();

        }
        public List<Post> getAllPosts(int vrstaId)
        {
            return context.Post.Where(x => x.PostVrsta == vrstaId).OrderByDescending(y => y.DatumZadnjeIzmjene).OrderBy(z => z.BrojOdgovora).ToList();
        }
        public List<Post> getPostByID_VrstaALL(int p)
        {
            return context.Post.Where(x => x.PostVrsta == p).ToList();
        }
        public int getCountAllPosts(int vrstaId)
        {
            return context.Post.Where(x => x.PostVrsta == vrstaId).Count();
        }
        public List<Post> getIzmjeneClanka(int RP)
        {
            return context.Post.Where(x => x.RoditeljskiPostID == RP && x.PrihvacenaIzmjena == 1).OrderByDescending(y => y.DatumKreiranja).ToList();
        }
        public Post getPosljednjaIzmjenaClanka(int RP)
        {
            return context.Post.Where(x => x.RoditeljskiPostID == RP && x.PrihvacenaIzmjena == 1).OrderByDescending(y => y.DatumKreiranja).FirstOrDefault();
        }
        public object getPrijedloziIzmjenaClanka(int RP)
        {
            return context.Post.Where(x => x.RoditeljskiPostID == RP && x.PrihvacenaIzmjena == 0).OrderByDescending(y => y.DatumKreiranja).ToList();
        }
        public void SavePost(Post post)
        {
            context.Post.Add(post);
            context.SaveChanges();
        }
        public void UpdatePost(Post post)
        {
            Post p = context.Post.First(i => i.id == post.id);
            p.Broj_Razgovora = post.Broj_Razgovora;
            p.BrojKomentara = post.BrojKomentara;
            p.BrojOdgovora = post.BrojOdgovora;
            p.BrojOmiljenih = post.BrojOmiljenih;
            p.BrojPoena = post.BrojPoena;
            p.BrojPregleda = post.BrojPregleda;
            p.BrojRangiranja = post.BrojRangiranja;
            p.DatumKreiranja = post.DatumKreiranja;
            p.DatumZadnjeAktivnosti = post.DatumZadnjeAktivnosti;
            p.DatumZadnjeIzmjene = post.DatumZadnjeIzmjene;
            p.DatumZatvaranjaPosta = post.DatumZatvaranjaPosta;
            p.Likes = post.Likes;
            p.Naslov = post.Naslov;
            p.PodKategorija = post.PodKategorija;
            p.Podnaslov = post.Podnaslov;
            p.PostVrsta = post.PostVrsta;
            p.PrihvacenaIzmjena = post.PrihvacenaIzmjena;
            p.PrihvaceniOdgovori = post.PrihvaceniOdgovori;
            p.PromijenioID = post.PromijenioID;
            p.RoditeljskiPostID = post.RoditeljskiPostID;
            p.Sadrzaj = post.Sadrzaj;
            p.Sazetak = post.Sazetak;
            p.SlikaURL = post.SlikaURL;
            p.Tagovi = post.Tagovi;
            p.temp = post.temp;
            p.Unlikes = post.Unlikes;
            p.VlasnikID = post.VlasnikID;
            p.VlasnikNadimak = post.VlasnikNadimak;
            context.SaveChanges();
        }



        //USER_LIKES////////////////////////////////////////////////////////////////////////////////////////////////////
        public int getVoteUserLikes(int UID, int PID)
        {
            User_Likes ul = context.User_Likes.Where(x => x.UserId == UID && x.PostId == PID).FirstOrDefault();
            return ul.isLiked.Value;
        }
        public User_Likes getUserLikes(int UID, int PID)
        {
            return context.User_Likes.Where(x => x.UserId == UID && x.PostId == PID).FirstOrDefault();
        }
        public int getRateUserLikes(int UID, int PID)
        {
            User_Likes ul = context.User_Likes.Where(x => x.UserId == UID && x.PostId == PID).FirstOrDefault();
            return ul.Rate.Value;
        }
        public void SaveUser_Likes(User_Likes user_likes)
        {
            context.User_Likes.Add(user_likes);
            Post post = context.Post.First(p => p.id == user_likes.PostId.Value);

            Korisnik korisnik = context.Korisnik.First(p => p.id == post.VlasnikID);
            if (user_likes.isLiked == 1)
            {
                post.Likes++;
                post.BrojPoena = post.Likes.Value - post.Unlikes.Value;
                UpdatePost(post);

                korisnik.Likes++;
                korisnik.Reputacija = korisnik.Likes.Value - korisnik.Unlikes.Value;
                UpdateKorisnik(korisnik);
            }
            KalkulirajMedaljePostaKorisnikaUP(korisnik, post);
            context.SaveChanges();
        }
        public void SaveUser_Unlikes(User_Likes user_likes)
        {
            context.User_Likes.Add(user_likes);
            Post post = context.Post.First(p => p.id == user_likes.PostId.Value);
            Korisnik korisnik = context.Korisnik.First(p => p.id == post.VlasnikID);
            if (user_likes.isLiked == 2)
            {
                post.Unlikes++;
                post.BrojPoena = post.Likes.Value - post.Unlikes.Value;
                UpdatePost(post);

                korisnik.Unlikes++;
                korisnik.Reputacija = korisnik.Likes.Value - korisnik.Unlikes.Value;
                UpdateKorisnik(korisnik);
            }
            KalkulirajMedaljePostaKorisnikaDOWN(korisnik, post);
            context.SaveChanges();
        }
        public void UpdateUser_Likes(User_Likes user_likes, int LikesBefore)
        {
            User_Likes uli = context.User_Likes.Where(x => x.id == user_likes.id && x.UserId == user_likes.UserId && x.PostId == user_likes.PostId).FirstOrDefault();
            uli.isLiked = user_likes.isLiked;
            uli.PostId = user_likes.PostId.Value;
            uli.UserId = user_likes.UserId.Value;

            if (user_likes.Rate == null)
                uli.Rate = 0;
            else
                uli.Rate = user_likes.Rate.Value;

            uli.DatumLajkanja = user_likes.DatumLajkanja.Value;
            uli.DaumRated = user_likes.DaumRated.Value;
            context.SaveChanges();

            Post post = context.Post.First(p => p.id == user_likes.PostId.Value);
            Korisnik korisnik = context.Korisnik.First(p => p.id == post.VlasnikID);

            User_Likes ul = context.User_Likes.Where(x => x.id == user_likes.id && x.UserId == user_likes.UserId && x.PostId == user_likes.PostId).FirstOrDefault();
            if (LikesBefore == 0)
            {
                if (ul.isLiked == 1)
                {
                    post.Likes++;
                    post.BrojPoena = post.Likes.Value - post.Unlikes.Value;
                    UpdatePost(post);

                    korisnik.Likes++;
                    korisnik.Reputacija = korisnik.Likes.Value - korisnik.Unlikes.Value;
                    UpdateKorisnik(korisnik);
                    KalkulirajMedaljePostaKorisnikaUP(korisnik, post);
                }
                if (ul.isLiked == 2)
                {
                    post.Unlikes++;
                    post.BrojPoena = post.Likes.Value - post.Unlikes.Value;
                    UpdatePost(post);

                    korisnik.Unlikes++;
                    korisnik.Reputacija = korisnik.Likes.Value - korisnik.Unlikes.Value;
                    UpdateKorisnik(korisnik);
                    KalkulirajMedaljePostaKorisnikaDOWN(korisnik, post);
                }
            }
            else
            {
                if (ul.isLiked == 1)
                {
                    post.Likes++;
                    post.BrojPoena = post.Likes.Value - post.Unlikes.Value;
                    UpdatePost(post);
                    KalkulirajMedaljePostaKorisnikaUP(korisnik, post);

                    post.Unlikes--;
                    post.BrojPoena = post.Likes.Value - post.Unlikes.Value;
                    UpdatePost(post);
                    KalkulirajMedaljePostaKorisnikaUP(korisnik, post);

                    korisnik.Likes++;
                    korisnik.Unlikes--;
                    korisnik.Reputacija = korisnik.Likes.Value - korisnik.Unlikes.Value;
                    UpdateKorisnik(korisnik);

                }
                if (ul.isLiked == 2)
                {
                    post.Likes--;
                    post.BrojPoena = post.Likes.Value - post.Unlikes.Value;
                    UpdatePost(post);
                    KalkulirajMedaljePostaKorisnikaDOWN(korisnik, post);

                    post.Unlikes++;
                    post.BrojPoena = post.Likes.Value - post.Unlikes.Value;
                    UpdatePost(post);
                    KalkulirajMedaljePostaKorisnikaDOWN(korisnik, post);

                    korisnik.Likes--;
                    korisnik.Unlikes++;
                    korisnik.Reputacija = korisnik.Likes.Value - korisnik.Unlikes.Value;
                    UpdateKorisnik(korisnik);

                }
            }
            context.SaveChanges();
        }
        public void SaveUser_Likes_Rates(User_Likes user_likes)
        {
            context.User_Likes.Add(user_likes);
            context.SaveChanges();

            Post post = context.Post.First(p => p.id == user_likes.PostId.Value);
            post.BrojOmiljenih = post.BrojOmiljenih.Value + user_likes.Rate.Value;
            post.BrojRangiranja++;
            UpdatePost(post);
        }
        public void UpdateUser_Likes_Rates(User_Likes user_likes, int RateBefore)
        {
            User_Likes ul = context.User_Likes.Where(x => x.id == user_likes.id && x.UserId == user_likes.UserId && x.PostId == user_likes.PostId).FirstOrDefault();
            ul.isLiked = user_likes.isLiked;
            ul.PostId = user_likes.PostId.Value;
            ul.UserId = user_likes.UserId.Value;

            if (user_likes.Rate == null)
                ul.Rate = 0;
            else
                ul.Rate = user_likes.Rate.Value;

            ul.DatumLajkanja = user_likes.DatumLajkanja.Value;
            ul.DaumRated = user_likes.DaumRated.Value;
            context.SaveChanges();

            Post post = context.Post.First(p => p.id == user_likes.PostId.Value);
            post.BrojOmiljenih = post.BrojOmiljenih.Value - RateBefore + user_likes.Rate.Value;
            ul = context.User_Likes.Where(x => x.id == user_likes.id && x.UserId == user_likes.UserId && x.PostId == user_likes.PostId).FirstOrDefault();
            if (RateBefore == 0)
                post.BrojRangiranja++;
            UpdatePost(post);
        }



        //RAZGOVORI////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Post> getRazgovoriPrihvaceniID(int p)
        {
            return context.Post.Where(x => x.RoditeljskiPostID == p).ToList();
        }

        //KATEGORIJA////////////////////////////////////////////////////////////////////////////////////////////////////        
        public List<Kategorija> getKategorijeAll()
        {
            return context.Kategorija.ToList();
        }

        //PODKATEGORIJA////////////////////////////////////////////////////////////////////////////////////////////////////        
        public List<PodKategorija> getPodkategorijaByKategorija(int KID)
        {
            return context.PodKategorija.Where(x => x.KategorijaID == KID).ToList();
        }


        //Tagovi////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Tag> getAllTags(string p)
        {
            return context.Tag.ToList();
        }
        public Tag getTagByName(string tag)
        {
            return context.Tag.Where(x => x.Naziv == tag).FirstOrDefault();
        }
        public void SaveTag(Tag tag)
        {
            context.Tag.Add(tag);
            context.SaveChanges();
        }
        public void SavePosts_Tags(Posts_Tags post_tags)
        {
            context.Posts_Tags.Add(post_tags);
            context.SaveChanges();
        }
        public List<Posts_Tags> getAllPosts_Tags(int vrstaPostaId, int TAGID)
        {
            return context.Posts_Tags.Where(x => x.Post.PostVrsta == vrstaPostaId && x.TagID == TAGID).OrderBy(z => z.Tag.Naziv).ToList();
        }
        public Tag getTagByID(int TAGID)
        {
            return context.Tag.Where(x => x.id == TAGID).FirstOrDefault();
        }
        public int getTagCount(int TID)
        {
            return context.Posts_Tags.Where(x => x.TagID == TID).Count();
        }



        //QA_TAGOVI TABOVI
        public List<Tag> getTagoviPitanjaPopularni()
        {
            var query = (
                from t in context.Tag
                join pt in context.Posts_Tags on t.id equals pt.TagID
                join p in context.Post on pt.PostID equals p.id
                where p.PostVrsta == 5
                select t).Distinct().OrderByDescending(x => x.Posts_Tags.Count).ToList();
            return (List<Tag>)query;
        }
        public List<Tag> getTagoviClanciPopularni()
        {
            var query = (
                from t in context.Tag
                join pt in context.Posts_Tags on t.id equals pt.TagID
                join p in context.Post on pt.PostID equals p.id
                where p.PostVrsta == 1
                select t).Distinct().OrderByDescending(x => x.Posts_Tags.Count).ToList();
            return (List<Tag>)query;
        }

        public List<Tag> getTagoviClanka(int PID)
        {
           var query = (
                from t in context.Tag
                join pt in context.Posts_Tags on t.id equals pt.TagID
                join p in context.Post on pt.PostID equals p.id
                where p.PostVrsta == 1 && p.id == PID
                select t).Distinct().OrderByDescending(x => x.Posts_Tags.Count).ToList();
            return (List<Tag>)query;
        }
        public List<Tag> getTagoviPitanja(int PID)
        {
            var query = (
                 from t in context.Tag
                 join pt in context.Posts_Tags on t.id equals pt.TagID
                 join p in context.Post on pt.PostID equals p.id
                 where p.PostVrsta == 5 && p.id == PID
                 select t).Distinct().OrderByDescending(x => x.Posts_Tags.Count).ToList();
            return (List<Tag>)query;
        }

        public List<Tag> getPItanjaTagoviByDate()
        {
            var query = (
                from t in context.Tag
                join pt in context.Posts_Tags on t.id equals pt.TagID
                join p in context.Post on pt.PostID equals p.id
                where p.PostVrsta == 5
                select t).Distinct().OrderBy(x => x.DatumKreiranja).ToList();
            return (List<Tag>)query;
        }
        public object getPitanjaTagoviAbeceda()
        {
            var query = (
                from t in context.Tag
                join pt in context.Posts_Tags on t.id equals pt.TagID
                join p in context.Post on pt.PostID equals p.id
                where p.PostVrsta == 5
                select t).Distinct().OrderBy(x => x.Naziv).ToList();
            return (List<Tag>)query;
        }
        public List<Posts_Tags> getPost_TagsByPostID(int PID)
        {
            return context.Posts_Tags.Where(x => x.PostID == PID).ToList();
        }
        public Posts_Tags getPosts_TagsByIDS(int p1, int p2)
        {
            return context.Posts_Tags.Where(x => x.PostID == p1 && x.TagID == p2).FirstOrDefault();
        }

        // SVI TAGOVI ZA ČLANKE
        public List<Posts_Tags> getClanciByTag()
        {
            return context.Posts_Tags.Where(x => x.Post.PostVrsta == 1).OrderBy(z => z.Tag.Naziv).ToList();
        }



        // SVI TAGOVI ZA PITANJA
        public List<Posts_Tags> getPitanjaByTaG()
        {
            return context.Posts_Tags.Where(x => x.Post.PostVrsta == 5).OrderBy(z => z.Tag.Naziv).ToList();
        }
        public List<Post> getAllClanci()
        {
            return context.Post.Where(x => x.PostVrsta == 1).ToList();
        }
        public List<Post> getAllPitanja()
        {
            return context.Post.Where(x => x.PostVrsta == 5).ToList();
        }
        public Post getClanakByID(int ID)
        {
            return context.Post.Where(x => x.id == ID && x.PostVrsta == 1).SingleOrDefault();
        }
        public Post getPitanjeByID(int ID)
        {
            return context.Post.Where(x => x.id == ID && x.PostVrsta == 5).SingleOrDefault();
        }

        //Clanci TABOVI
        public List<Post> getClanciByDate()
        {
            return context.Post.Where(x => x.PostVrsta == 1).OrderByDescending(y => y.DatumKreiranja).ToList();
        }
        public List<Post> getClanciByVotes()
        {
            return context.Post.Where(x => x.PostVrsta == 1).OrderByDescending(y => y.BrojPoena).OrderByDescending(z => z.BrojPregleda).ToList();
        }
        public List<Post> getClanciNeodgovorena()
        {
            return context.Post.Where(x => x.PostVrsta == 1 && x.BrojOdgovora < 1).OrderBy(z => z.DatumKreiranja).OrderByDescending(y => y.BrojPoena).ToList();
        }
        public List<Post> getClanciByPregledi()
        {
            return context.Post.Where(x => x.PostVrsta == 1).OrderByDescending(y => y.BrojPregleda).OrderBy(z => z.DatumZadnjeIzmjene).ToList();
        }
        public List<Post> getClanciBySedmica()
        {
            DateTime dt = DateTime.Now;
            dt.AddDays(-7);
            return context.Post.Where(x => x.PostVrsta == 1 && dt.Date.Date.CompareTo(x.DatumZadnjeIzmjene.Value) < 0).OrderByDescending(z => z.DatumZadnjeIzmjene).ToList();

        }
        public List<Post> getClanciByMjesec()
        {
            DateTime dt = DateTime.Now;
            dt.AddDays(-30);
            return context.Post.Where(x => x.PostVrsta == 1 && dt.Date.Date.CompareTo(x.DatumZadnjeIzmjene.Value) < 0).OrderBy(z => z.DatumZadnjeIzmjene).ToList();
        }

        //QA PITANJA TABOVI
        public List<Post> getPitanjaByDate()
        {
            return context.Post.Where(x => x.PostVrsta == 5).OrderByDescending(y => y.DatumKreiranja).ToList();
        }
        public List<Post> getPitanjaByVotes()
        {
            return context.Post.Where(x => x.PostVrsta == 5).OrderByDescending(y => y.BrojPoena).OrderByDescending(z => z.BrojPregleda).ToList();
        }
        public List<Post> getPitanjaNeodgovorena()
        {
            return context.Post.Where(x => x.PostVrsta == 5 && x.BrojOdgovora < 1).OrderBy(z => z.DatumKreiranja).OrderByDescending(y => y.BrojPoena).ToList();
        }
        public List<Post> getPitanjaByPregledi()
        {
            return context.Post.Where(x => x.PostVrsta == 5).OrderByDescending(y => y.BrojPregleda).OrderBy(z => z.DatumZadnjeIzmjene).ToList();
        }
        public List<Post> getPitanjaBySedmica()
        {
            DateTime dt = DateTime.Now;
            dt.AddDays(-7);
            return context.Post.Where(x => x.PostVrsta == 5 && dt.Date.Date.CompareTo(x.DatumZadnjeIzmjene.Value) < 0).OrderByDescending(z => z.DatumZadnjeIzmjene).ToList();

        }
        public List<Post> getPitanjaByMjesec()
        {
            DateTime dt = DateTime.Now;
            dt.AddDays(-30);
            return context.Post.Where(x => x.PostVrsta == 5 && dt.Date.Date.CompareTo(x.DatumZadnjeIzmjene.Value) < 0).OrderBy(z => z.DatumZadnjeIzmjene).ToList();
        }





        /// ///ODGOVORI       
        public List<Post> getOdgovoriByPitanjeID(int p)
        {
            return context.Post.Where(x => x.PostVrsta == 6 && x.RoditeljskiPostID == p).OrderBy(y => y.DatumKreiranja).ToList();
        }
        public Post getOdgovorByID(int p)
        {
            return context.Post.Where(x => x.PostVrsta == 6 && x.id == p).FirstOrDefault();
        }



        //KALKULACIJA BEDZEVA
        //public void KalkulirajBedzeveSvihKorisnika()
        //{

        //    List<Korisnik> listaSvihKorisnika = getKorisniciAll();
        //    if (listaSvihKorisnika != null)
        //    {
        //        foreach (var korisnik in listaSvihKorisnika)
        //        {
        //            KalkulirajBedzeveKorisnika(korisnik);                    
        //        }
        //    }

        //}
        //public void KalkulirajBedzeveKorisnika(Korisnik korisnik)
        //{

        //    List<Post> sviPostoviKorisnika = getSviPostoviKorisnika(korisnik);
        //    if (sviPostoviKorisnika != null)
        //    {
        //        foreach (var post in sviPostoviKorisnika)
        //        {
        //            KalkulirajMedaljePostaKorisnika(korisnik, post);
        //        }
        //    }
        //    else
        //    {
        //        KalkulirajBedzKorisnika(korisnik);
        //    }

        //}
        public void KalkulirajMedaljePostaKorisnikaUP(Korisnik korisnik, Post post)
        {
            if (post.BrojPoena == 3)
            {
                korisnik.BrojBronzanih++;
            }
            if (post.BrojPoena == 5)
            {
                korisnik.BrojSrebrenih++;
                korisnik.BrojBronzanih--;
            }
            if (post.BrojPoena == 10)
            {
                korisnik.BrojZlatnih++;
                korisnik.BrojSrebrenih--;
            }
            if (post.BrojPoena == 20 || post.BrojPoena == 30 || post.BrojPoena == 40 || post.BrojPoena == 50
                || post.BrojPoena == 60 || post.BrojPoena == 70 || post.BrojPoena == 80)
            {
                korisnik.BrojZlatnih++;
            }
            UpdateKorisnik(korisnik);
            KalkulirajBedzKorisnika(korisnik);

        }
        public void KalkulirajMedaljePostaKorisnikaDOWN(Korisnik korisnik, Post post)
        {
            if (post.BrojPoena == 2)
            {
                korisnik.BrojBronzanih--;
            }
            if (post.BrojPoena == 4)
            {
                korisnik.BrojSrebrenih--;
                korisnik.BrojBronzanih++;
            }
            if (post.BrojPoena == 9)
            {
                korisnik.BrojZlatnih--;
                korisnik.BrojSrebrenih++;
            }

            UpdateKorisnik(korisnik);
            KalkulirajBedzKorisnika(korisnik);

        }
        public void KalkulirajBedzKorisnika(Korisnik korisnik)
        {
            int bedzID = 10;

            if (korisnik.BrojZlatnih.Value >= 25)
            {
                bedzID = 1;
            }
            else if (korisnik.BrojZlatnih.Value >= 10)
            {
                bedzID = 2;
            }
            else if (korisnik.BrojZlatnih.Value >= 5)
            {
                bedzID = 3;
            }
            else if (korisnik.BrojZlatnih.Value >= 3)
            {
                bedzID = 4;
            }
            else if (korisnik.BrojZlatnih.Value >= 1 && korisnik.BrojSrebrenih.Value >= 5)
            {
                bedzID = 5;
            }
            else if (korisnik.BrojZlatnih.Value >= 1 && korisnik.BrojSrebrenih.Value >= 3)
            {
                bedzID = 6;
            }
            else if (korisnik.BrojZlatnih.Value >= 1)
            {
                bedzID = 7;
            }
            else if (korisnik.BrojZlatnih.Value < 1 && korisnik.BrojSrebrenih.Value >= 2)
            {
                bedzID = 8;
            }
            else if (korisnik.BrojZlatnih.Value < 1 && korisnik.BrojSrebrenih.Value >= 1)
            {
                bedzID = 9;
            }
            else if (korisnik.BrojZlatnih.Value < 1 && korisnik.BrojSrebrenih.Value < 1 && korisnik.BrojBronzanih.Value <= 1)
            {
                bedzID = 10;
            }


            Bedz bedz = getBedzByID(bedzID);
            korisnik.BedzID = bedz.id;
            korisnik.BedzNaziv = bedz.Naziv;
            korisnik.BedzOpis = bedz.Opis;
            korisnik.BedzSlika = bedz.SlikaURL;
            UpdateKorisnik(korisnik);

        }
        public List<Bedz> getSviBedzevi()
        {
            return context.Bedz.ToList();
        }
        public Bedz getBedzByID(int bedzID)
        {
            return context.Bedz.Where(x => x.id == bedzID).FirstOrDefault();
        }
        public List<Post> getSviPostoviKorisnika(Korisnik korisnik)
        {
            return context.Post.Where(x => x.VlasnikID == korisnik.id).ToList();
        }
        public List<Korisnik> getKorisniciAll()
        {
            return context.Korisnik.ToList();
        }
        public List<Post> getPitanjaByVlasnikID(int id)
        {
            return context.Post.Where(x => x.VlasnikID == id && x.PostVrsta == 5).OrderByDescending(y => y.DatumZadnjeAktivnosti).ToList();
        }
        public List<Post> getClanciByVlasnikID(int id)
        {
            return context.Post.Where(x => x.VlasnikID == id && x.PostVrsta == 1).OrderByDescending(y => y.DatumZadnjeAktivnosti).ToList();
        }
        public List<Post> getOdgovoriByVlasnikID(int id)
        {
            return context.Post.Where(x => x.VlasnikID == id && x.PostVrsta == 6).OrderByDescending(y => y.DatumZadnjeAktivnosti).ToList();
        }

        public List<Korisnik> NositeljiBedzevaByBedzID(int BID)
        {
            return context.Korisnik.Where(k => k.BedzID == BID).ToList();
        }

        public object getSviBedzeviBybrojKorisnika()
        {
            return context.Bedz.OrderBy(y => y.Korisnik.GroupBy(z => z.BedzID).Count()).ToList();
        }


        public object getKorisniciByReputacija()
        {
            return context.Korisnik.OrderByDescending(x => x.Reputacija).ToList();
        }
        public object getKorisniciByDate()
        {
            return context.Korisnik.OrderByDescending(x => x.DatumKreiranja).ToList();
        }
        public List<Korisnik> getKorisniciByPostovi()
        {
            var query = (
                from p in context.Post
                join k in context.Korisnik on p.VlasnikID equals k.id
                where p.PostVrsta == 5
                select k).Distinct().OrderBy(x => x.Post.OrderByDescending(y => y.VlasnikID.Value).Count()).ToList();
            return (List<Korisnik>)query;
            //return context.Korisnik.OrderByDescending(x => x.id == context.Post.GroupBy(p => p.VlasnikID.Value).Count()).ToList();
        }
        public string getPitanjaByKorisnikTags(Korisnik korisnik)
        {
            string recenica = "";

            List<Post> listKorisnikovihPitanja = getPitanjaByVlasnikID(korisnik.id);
            if (listKorisnikovihPitanja != null)
            {
                foreach (var pitanje in listKorisnikovihPitanja)
                {
                    recenica += ", " + pitanje.Tagovi;
                }
            }

            List<Post> listKorisnikovihOdgovora = getOdgovoriByVlasnikId(korisnik.id);
            if (listKorisnikovihOdgovora != null)
            {
                foreach (var odgovor in listKorisnikovihOdgovora)
                {
                    recenica += odgovor.Tagovi;
                }
            }

            List<Post> listKorisnikovihClanaka = getClanciByVlasnikID(korisnik.id);
            if (listKorisnikovihClanaka != null)
            {
                foreach (var pitanje in listKorisnikovihClanaka)
                {
                    recenica += ", " + pitanje.Tagovi;
                }
            }

            List<Post> listKorisnikovihLajkanihPostova = getPostoviLajkaniByKorisnikID(korisnik.id);
            if (listKorisnikovihOdgovora != null)
            {
                foreach (var odgovor in listKorisnikovihOdgovora)
                {
                    recenica += odgovor.Tagovi;
                }
            }

            string finalnarecenica = "";
            string[] listaTagova = recenica.Split(',');
            foreach (var item in listaTagova)
            {
                if (getTagByName(item.Trim()) != null)
                    finalnarecenica += getTagByName(item.Trim()).Naziv + " ";
            }

            return finalnarecenica;
        }

        private List<Post> getPostoviLajkaniByKorisnikID(int UID)
        {
            Post p = new Post();
            List<User_Likes> lul = (List<User_Likes>)context.User_Likes.Where(x => x.UserId == UID).Distinct().ToList();
            List<Post> listaLajkanihPostova = new List<Post>();
            foreach (var ul in lul)
            {
                p = getPitanjeByID(ul.PostId.Value);

                listaLajkanihPostova.Add(p);
            }
            return listaLajkanihPostova;
        }
        public List<Post> getOdgovoriByVlasnikId(int VID)
        {
            return context.Post.Where(x => x.PostVrsta == 6 && x.VlasnikID == VID).ToList();
        }


        //KATEGORIJE////////////////////////////////////////////////////////////

        public Kategorija getKategorijaByID(int p)
        {
            return context.Kategorija.Where(x => x.id == p).FirstOrDefault();
        }
        public PodKategorija getPodKategorijaByID(int p)
        {
            return context.PodKategorija.Where(x => x.id == p).FirstOrDefault();
        }
        public List<Post> GetPitanjaByPodKategorija(int p)
        {
            return context.Post.Where(x => x.PostVrsta == 5 && x.PodKategorija == p).OrderByDescending(y => y.DatumKreiranja).ToList();
        }
        public List<Post> GetPitanjaByKategorija(int KID)
        {
            var query = (
                from p in context.Post
                join pk in context.PodKategorija on p.PodKategorija equals pk.id
                where p.PostVrsta == 5 && pk.KategorijaID == KID
                select p).Distinct().OrderByDescending(x => x.DatumZadnjeAktivnosti).ToList();
            return (List<Post>)query;
        }
        public List<Post> GetClanciByPodKategorija(int p)
        {
            return context.Post.Where(x => x.PostVrsta == 1 && x.PodKategorija == p).OrderByDescending(y => y.DatumKreiranja).Distinct().ToList();
        }
        public List<Post> GetClanciByKategorija(int KID)
        {
            var query = (
               from p in context.Post
               join pk in context.PodKategorija on p.PodKategorija equals pk.id
               where p.PostVrsta == 1 && pk.KategorijaID == KID
               select p).Distinct().OrderByDescending(x => x.DatumZadnjeAktivnosti).ToList();
            return (List<Post>)query;

        }
        public List<PodKategorija> GetPodKategorijeByKategorijaID(int p)
        {
            return context.PodKategorija.Where(x => x.KategorijaID == p).ToList();
        }

        public List<Kategorija> getKategorijeAllPopular()
        {
            return context.Kategorija.OrderBy(x => x.Naslov).ToList();
        }
        public List<PodKategorija> getPodKategorijaAllPopular()
        {
            return context.PodKategorija.OrderByDescending(x => x.Post.Count).Distinct().ToList();
        }

        public int getClanciCountByPodkategorija(int PKID)
        {
            return context.Post.Where(x => x.PodKategorija == PKID && x.PostVrsta == 1).Count();
            // return context.Posts_Tags.Where(x => x.TagID == TID).Count();
        }
        public int getClanciCountBykategorija(int KID)
        {
            int countKategorija = 0;
            List<PodKategorija> lpk = getPodkategorijaByKategorija(KID);
            foreach (var pk in lpk)
            {
                countKategorija += getClanciCountByPodkategorija(pk.id);
            }
            return countKategorija;
        }

        public int getPitanjaCountByPodkategorija(int PKID)
        {
            return context.Post.Where(x => x.PodKategorija == PKID && x.PostVrsta == 5).Count();
            // return context.Posts_Tags.Where(x => x.TagID == TID).Count();
        }
        public int getPitanjaCountBykategorija(int KID)
        {
            int countKategorija = 0;
            List<PodKategorija> lpk = getPodkategorijaByKategorija(KID);
            foreach (var pk in lpk)
            {
                countKategorija += getPitanjaCountByPodkategorija(pk.id);
            }
            return countKategorija;
        }

        public Post getNajpopularnijiClanakSaTagom(int TID, int KID)
        {
            var query = (
               from t in context.Tag
               join pt in context.Posts_Tags on t.id equals pt.TagID
               join p in context.Post on pt.PostID equals p.id
               where p.VlasnikID != KID && t.id == TID && p.PostVrsta == 1
               select p).OrderByDescending(y => y.Posts_Tags.Count).OrderByDescending(x => x.BrojPoena).Distinct().FirstOrDefault();

            return (Post)query;

        }

        public Post getNajpopularnijePitanjeSaTagom(int TID, int KID)
        {
            var query = (
               from t in context.Tag
               join pt in context.Posts_Tags on t.id equals pt.TagID
               join p in context.Post on pt.PostID equals p.id
               where p.VlasnikID != KID && t.id == TID && p.PostVrsta == 5
               select p).OrderByDescending(y => y.Posts_Tags.Count).OrderByDescending(x => x.BrojPoena).Distinct().FirstOrDefault();

            return (Post)query;

        }


        public List<Post> GetPreporukaClanaka(int KID)
        {
            var query = (
               
               from ul in context.User_Likes 
               join p in context.Post on ul.PostId equals p.id
               where (p.VlasnikID != KID || p.PromijenioID == KID || ul.UserId == KID) && (p.PostVrsta == 1 )
               select p).Distinct().OrderByDescending(y => y.DatumZadnjeAktivnosti.Value).OrderBy(z => z.BrojPoena).OrderBy(p => p.BrojPoena).
               OrderBy(e => e.BrojPregleda).OrderBy(m => m.BrojOdgovora).OrderBy(u => u.BrojOmiljenih).ToList();

            return (List<Post>)query;

            //return context.Post.Where(x => (x.PromijenioID == KID || x.VlasnikID == KID ) && x.PostVrsta == 1).
            //    OrderByDescending(y => y.DatumZadnjeAktivnosti.Value).OrderBy(z => z.BrojPoena).OrderBy(p => p.BrojPoena).
            //    OrderBy(e => e.BrojPregleda).OrderBy(m => m.BrojOdgovora).OrderBy(u => u.BrojOmiljenih).ToList();
        }

        public List<Post> GetPreporukaPitanja(int KID)
        {
            var query = (

              from ul in context.User_Likes
              join p in context.Post on ul.PostId equals p.id
              where (p.VlasnikID != KID || p.PromijenioID == KID || ul.UserId == KID) && (p.PostVrsta == 5 )
              select p).Distinct().OrderByDescending(y => y.DatumZadnjeAktivnosti.Value).OrderBy(z => z.BrojPoena).OrderBy(p => p.BrojPoena).
              OrderBy(e => e.BrojPregleda).OrderBy(m => m.BrojOdgovora).OrderBy(u => u.BrojOmiljenih).ToList();

            return (List<Post>)query;
        }

        public PodKategorija getPodkategorijaByNaziv(string p)
        {
            return context.PodKategorija.Where(x => x.Naslov == p).FirstOrDefault();
        }



        public List<Korisnik> GetKorisniciRelatedToClanakTags(Tag TAG)
        {
            var query = (
              from k in context.Korisnik
              join p in context.Post on k.id equals p.VlasnikID
              join pt in context.Posts_Tags on p.id equals pt.PostID
              join t in context.Tag on pt.TagID equals t.id
              where p.PostVrsta == 1 && t.id == TAG.id                  
              select k).OrderByDescending(y => y.Reputacija).OrderBy(x => x.BedzID).Distinct().ToList();
              return (List<Korisnik>)query;
        }

        public List<Korisnik> GetKorisniciRelatedToPitanjeTags(Tag TAG)
        {
            var query = (
              from k in context.Korisnik
              join p in context.Post on k.id equals p.VlasnikID
              join pt in context.Posts_Tags on p.id equals pt.PostID
              join t in context.Tag on pt.TagID equals t.id
              where p.PostVrsta == 5 && p.Tagovi.Contains(TAG.Naziv)
              select k).OrderByDescending(y => y.Reputacija).OrderBy(x => x.BedzID).Distinct().ToList();
            return (List<Korisnik>)query;
        }
    }
}
