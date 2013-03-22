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


namespace Web.Lucene
{
    public partial class LuceneTestSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                Directory directory = FSDirectory.Open(new DirectoryInfo("J:/Triglav_Web_App/Triglav/Web/Lucene/LuceneIndex"));
                Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);

                var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);


                
                List<Post> sviPostovi = temp.getAllPosts(1);
                foreach (var post in sviPostovi)
                {
                    var document = new Document();
                    document.Add(new Field("id", Convert.ToString(post.id), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("Naslov", post.Naslov, Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Sadrzaj", post.Sadrzaj, Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Sazetak", post.Sazetak, Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Tagovi", post.Tagovi, Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("DatumKreiranja", Convert.ToString(post.DatumKreiranja), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("DatumZadnjeIzmjene", Convert.ToString(post.DatumZadnjeIzmjene), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("DatumZadnjeAktivnosti", Convert.ToString(post.DatumZadnjeAktivnosti), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("BrojOdgovora", Convert.ToString(post.BrojOdgovora), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Likes", Convert.ToString(post.Likes), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Unlikes", Convert.ToString(post.Unlikes), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("VlasnikId", Convert.ToString(post.VlasnikID), Field.Store.YES, Field.Index.ANALYZED));
                    document.Add(new Field("Unlikes", Convert.ToString(post.Unlikes), Field.Store.YES, Field.Index.ANALYZED));

                    writer.AddDocument(document);
                }

                writer.Optimize();
                writer.Close();
            }


        }

        //static void Main(string sta, string gdje)
        //{
        //    using (TriglavBL temp = new TriglavBL())
        //    {
        //        Directory directory = FSDirectory.Open(new DirectoryInfo("J:/Triglav_Web_App/Triglav/Web/Lucene/LuceneIndex"));
        //        Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);

        //        var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);


        //        var document = new Document();
        //        List<Post> sviPostovi = temp.getAllPosts(1);
        //        foreach (var post in sviPostovi)
        //        {
        //            document.Add(new Field("id", Convert.ToString(post.id), Field.Store.YES, Field.Index.NOT_ANALYZED));
        //            document.Add(new Field("Naslov", post.Naslov, Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("Sadrzaj", post.Sadrzaj, Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("Sazetak", post.Sazetak, Field.Store.YES, Field.Index.ANALYZED));
        //            // document.Add(new Field("Tagovi", post.Tagovi, Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("DatumKreiranja", Convert.ToString(post.DatumKreiranja), Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("DatumZadnjeIzmjene", Convert.ToString(post.DatumZadnjeIzmjene), Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("DatumZadnjeAktivnosti", Convert.ToString(post.DatumZadnjeAktivnosti), Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("BrojOdgovora", Convert.ToString(post.BrojOdgovora), Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("Likes", Convert.ToString(post.Likes), Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("Unlikes", Convert.ToString(post.Unlikes), Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("VlasnikId", Convert.ToString(post.VlasnikID), Field.Store.YES, Field.Index.ANALYZED));
        //            document.Add(new Field("Unlikes", Convert.ToString(post.Unlikes), Field.Store.YES, Field.Index.ANALYZED));

        //            writer.AddDocument(document);
        //        }

        //        writer.Optimize();
        //        writer.Close();

                
        //    }

        //}

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            GetResultsFromPost(txt_search.Text);

        }

        private void GetResultsFromPost(string TrazenaRijec)
        {
            Directory directory = FSDirectory.Open("J:/Triglav_Web_App/Triglav/Web/Lucene/LuceneIndex");
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);

            IndexReader indexReader = IndexReader.Open(directory, true);
            Searcher indexSearch = new IndexSearcher(indexReader);

            var queryParser = new QueryParser(Version.LUCENE_29, "Naslov", analyzer);
            var query = queryParser.Parse(TrazenaRijec);

            Response.Write("Searching for: " + query.ToString());
            TopDocs resultDocs = indexSearch.Search(query, indexReader.MaxDoc);
            Response.Write("Results Found: " + resultDocs.TotalHits);

            List<Post> postovi = new List<Post>();

            var hits = resultDocs.ScoreDocs;
            foreach (var hit in hits)
            {

                var documentFromSearcher = indexSearch.Doc(hit.Doc);
                Console.WriteLine(documentFromSearcher.Get("Naslov") + " \n" );

                using (TriglavBL temp = new TriglavBL())
                {
                   postovi.Add(temp.getPostByID(Convert.ToInt32(documentFromSearcher.Get("id"))));
                }
            }
            dl_Clanci.DataSource = postovi;
            dl_Clanci.DataBind();

            indexSearch.Dispose();
            directory.Dispose();
        }
    }
}

