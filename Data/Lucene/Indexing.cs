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
using Lucene.Net.QueryParsers;

namespace Data.Lucene 
{
    public class Indexing 
    {
       
       public Indexing() { }

        /// <summary>
        /// Directory Članci
        /// </summary>
        /// <param name="postVrstaId"></param>
        /// <returns></returns>            
       public static Directory GetDirectoryClanci()
        {

            using (TriglavBL temp = new TriglavBL())
            {                

                Directory directoryClanci = FSDirectory.Open(new DirectoryInfo("J:/Triglav_Web_App/Triglav/Web/Lucene/Clanci"));
                Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);

                var writer = new IndexWriter(directoryClanci, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);

                List<Post> sviPostovi = temp.getAllClanci();
                foreach (var post in sviPostovi)
                {
                    //Add & boost
                    var clanak = new Document();
                    clanak.Add(new Field("id", Convert.ToString(post.id), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO));
                    clanak.Add(new Field("PostVrsta", Convert.ToString(post.PostVrsta), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("Naslov", post.Naslov, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    clanak.GetField("Naslov").Boost = (2.0F);
                    clanak.Add(new Field("Sadrzaj", post.Sadrzaj, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    clanak.GetField("Sadrzaj").Boost = (1.2F);
                    if (post.Sazetak != null)
                    {
                        clanak.Add(new Field("Sazetak", post.Sazetak, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                        clanak.GetField("Sazetak").Boost = (1.5F);
                    }
                    clanak.Add(new Field("Tagovi", post.Tagovi, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    clanak.GetField("Tagovi").Boost = (2.5F);
                    clanak.Add(new Field("VlasnikId", Convert.ToString(post.VlasnikID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("VlasnikNadimak", Convert.ToString(post.VlasnikNadimak), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                    clanak.Add(new Field("DatumKreiranja", Convert.ToString(post.DatumKreiranja), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("DatumZadnjeIzmjene", Convert.ToString(post.DatumZadnjeIzmjene), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("DatumZadnjeAktivnosti", Convert.ToString(post.DatumZadnjeAktivnosti), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("PrihvaceniOdgovori", Convert.ToString(post.PrihvaceniOdgovori), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("BrojOdgovora", Convert.ToString(post.BrojOdgovora), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("BrojKomentara", Convert.ToString(post.BrojKomentara), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("BrojOmiljenih", Convert.ToString(post.BrojOmiljenih), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("BrojPregleda", Convert.ToString(post.BrojPregleda), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("BrojPoena", Convert.ToString(post.BrojPoena), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("Likes", Convert.ToString(post.Likes), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("Unlikes", Convert.ToString(post.Unlikes), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("PromijenioID", Convert.ToString(post.PromijenioID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("RoditeljskiPostID", Convert.ToString(post.RoditeljskiPostID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("BrojRangiranja", Convert.ToString(post.BrojRangiranja), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("PrihvacenaIzmjena", Convert.ToString(post.PrihvacenaIzmjena), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                    clanak.Add(new Field("Broj_Razgovora", Convert.ToString(post.Broj_Razgovora), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                    writer.AddDocument(clanak);
                }

                writer.Optimize();
                writer.Dispose();
                return directoryClanci;
            }
           
        }
       public static Directory GetDirectoryClanciTagovi()
        {
            using (TriglavBL temp = new TriglavBL())
            {
                Directory directoryClanciTagovi = FSDirectory.Open(new DirectoryInfo("J:/Triglav_Web_App/Triglav/Web/Lucene/ClanciTagovi"));
                Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);

                var writer = new IndexWriter(directoryClanciTagovi, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);

                List<Posts_Tags> sviPostoviTagovi = temp.getClanciByTag(); 
                foreach (var post_tags in sviPostoviTagovi)
                {
                    //Add & boost
                    var Posts_Tags = new Document();
                    Posts_Tags.Add(new Field("id", Convert.ToString(post_tags.id), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO));
                    Posts_Tags.Add(new Field("PostID", Convert.ToString(post_tags.PostID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO));
                    Posts_Tags.Add(new Field("TagID", Convert.ToString(post_tags.TagID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO));
                   
                    writer.AddDocument(Posts_Tags);
                }

                writer.Optimize();
                writer.Dispose();
                return directoryClanciTagovi;
            }
        }

       /// <summary>
       /// Directory Pitanja
       /// </summary>
       /// <param name="postVrstaId"></param>
       /// <returns></returns>
       public static Directory GetDirectoryPitanja()
       {

           using (TriglavBL temp = new TriglavBL())
           {

               Directory directoryPitanja = FSDirectory.Open(new DirectoryInfo("J:/Triglav_Web_App/Triglav/Web/Lucene/Pitanja"));
               Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);

               var writer = new IndexWriter(directoryPitanja, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);

               List<Post> SvaPitanja = temp.getAllPitanja();
               foreach (var post in SvaPitanja)
               {
                   //Add & boost
                   var clanak = new Document();
                   clanak.Add(new Field("id", Convert.ToString(post.id), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO));
                   clanak.Add(new Field("PostVrsta", Convert.ToString(post.PostVrsta), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("Naslov", post.Naslov, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                   clanak.GetField("Naslov").Boost = (2.0F);
                   clanak.Add(new Field("Sadrzaj", post.Sadrzaj, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                   clanak.GetField("Sadrzaj").Boost = (1.2F);
                   clanak.Add(new Field("Sazetak", post.Sazetak, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                   clanak.GetField("Sazetak").Boost = (1.5F);                   
                   clanak.Add(new Field("Tagovi", post.Tagovi, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                   clanak.GetField("Tagovi").Boost = (2.5F);
                   clanak.Add(new Field("VlasnikId", Convert.ToString(post.VlasnikID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("VlasnikNadimak", Convert.ToString(post.VlasnikNadimak), Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS));
                   clanak.Add(new Field("DatumKreiranja", Convert.ToString(post.DatumKreiranja), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("DatumZadnjeIzmjene", Convert.ToString(post.DatumZadnjeIzmjene), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("DatumZadnjeAktivnosti", Convert.ToString(post.DatumZadnjeAktivnosti), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("PrihvaceniOdgovori", Convert.ToString(post.PrihvaceniOdgovori), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("BrojOdgovora", Convert.ToString(post.BrojOdgovora), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("BrojKomentara", Convert.ToString(post.BrojKomentara), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("BrojOmiljenih", Convert.ToString(post.BrojOmiljenih), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("BrojPregleda", Convert.ToString(post.BrojPregleda), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("BrojPoena", Convert.ToString(post.BrojPoena), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("Likes", Convert.ToString(post.Likes), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("Unlikes", Convert.ToString(post.Unlikes), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("PromijenioID", Convert.ToString(post.PromijenioID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("RoditeljskiPostID", Convert.ToString(post.RoditeljskiPostID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("BrojRangiranja", Convert.ToString(post.BrojRangiranja), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("PrihvacenaIzmjena", Convert.ToString(post.PrihvacenaIzmjena), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
                   clanak.Add(new Field("Broj_Razgovora", Convert.ToString(post.Broj_Razgovora), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

                   writer.AddDocument(clanak);
               }

               writer.Optimize();
               writer.Dispose();
               return directoryPitanja;
           }

       }
       public static Directory GetDirectoryPitanjaTagovi()
       {
           using (TriglavBL temp = new TriglavBL())
           {
               Directory directoryPitanjaTagovi = FSDirectory.Open(new DirectoryInfo("J:/Triglav_Web_App/Triglav/Web/Lucene/PitanjaTagovi"));
               Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);

               var writer = new IndexWriter(directoryPitanjaTagovi, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);

               List<Posts_Tags> svaPitanjaTagovi = temp.getPitanjaByTaG();
               foreach (var post_tags in svaPitanjaTagovi)
               {
                   //Add & boost
                   var Posts_Tags = new Document();
                   Posts_Tags.Add(new Field("id", Convert.ToString(post_tags.id), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO));
                   Posts_Tags.Add(new Field("PostID", Convert.ToString(post_tags.PostID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO));
                   Posts_Tags.Add(new Field("TagID", Convert.ToString(post_tags.TagID), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS, Field.TermVector.NO));

                   writer.AddDocument(Posts_Tags);
               }

               writer.Optimize();
               writer.Dispose();
               return directoryPitanjaTagovi;
           }
       }
    }
}