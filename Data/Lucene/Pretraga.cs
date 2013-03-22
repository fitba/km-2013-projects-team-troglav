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

namespace Data.Lucene
{
    public class Pretraga
    {
        //KONSTRUKTOR
        public Pretraga() { }

        public static DataTable searchClanci(string pretraga)
        {
            DataTable ResultsClanci = new DataTable();
            // create the searcher
            // index is placed in "index" subdirectory
            string indexDirectory = "J:/Triglav_Web_App/Triglav/Web/Lucene/Clanci";
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            IndexSearcher searcher = new IndexSearcher(FSDirectory.Open(indexDirectory));

            // parse the query, "text" is the default field to search
            var parser = new MultiFieldQueryParser(Version.LUCENE_30, new[] { "Naslov", "Sazetak", "Sadrzaj", "Tagovi" }, analyzer);
            //var parser = new QueryParser(Version.LUCENE_30, "Sazetak" , analyzer);
            Query query = parser.Parse(pretraga);


            //// create the result DataTable
            ResultsClanci.Columns.Add("id", typeof(Int32));
            ResultsClanci.Columns.Add("Naslov", typeof(string));
            ResultsClanci.Columns.Add("Sadrzaj", typeof(string));
            ResultsClanci.Columns.Add("Tagovi", typeof(string));
            ResultsClanci.Columns.Add("DatumKreiranja", typeof(DateTime));
            ResultsClanci.Columns.Add("DatumZadnjeIzmjene", typeof(DateTime));
            ResultsClanci.Columns.Add("DatumZadnjeAktivnosti", typeof(DateTime));
            ResultsClanci.Columns.Add("DatumZatvaranjaPosta", typeof(DateTime));
            ResultsClanci.Columns.Add("PrihvaceniOdgovori", typeof(Int32));
            ResultsClanci.Columns.Add("BrojOdgovora", typeof(Int32));
            ResultsClanci.Columns.Add("BrojKomentara", typeof(Int32));
            ResultsClanci.Columns.Add("BrojOmiljenih", typeof(Int32));
            ResultsClanci.Columns.Add("BrojPregleda", typeof(Int32));
            ResultsClanci.Columns.Add("BrojPoena", typeof(Int32));
            ResultsClanci.Columns.Add("VlasnikID", typeof(Int32));
            ResultsClanci.Columns.Add("VlasnikNadimak", typeof(string));
            ResultsClanci.Columns.Add("PromijenioID", typeof(Int32));
            ResultsClanci.Columns.Add("RoditeljskiPostID", typeof(Int32));
            //Results.Columns.Add("PodKategorija", typeof(Int32));
            ResultsClanci.Columns.Add("PostVrsta", typeof(Int32));
            ResultsClanci.Columns.Add("SlikaURL", typeof(string));
            ResultsClanci.Columns.Add("temp", typeof(string));
            ResultsClanci.Columns.Add("Likes", typeof(Int32));
            ResultsClanci.Columns.Add("Unlikes", typeof(Int32));
            ResultsClanci.Columns.Add("Sazetak", typeof(string));
            ResultsClanci.Columns.Add("BrojRangiranja", typeof(Int32));
            ResultsClanci.Columns.Add("PrihvacenaIzmjena", typeof(Int32));
            ResultsClanci.Columns.Add("Podnaslov", typeof(string));
            ResultsClanci.Columns.Add("Broj.Razgovora", typeof(Int32));
            ResultsClanci.Columns.Add("sample", typeof(string));
            ResultsClanci.Columns.Add("sampleNaslov", typeof(string));

            // search
            TopDocs hits = searcher.Search(query, 5); // 5 rezultata

            //E this.total = hits.TotalHits;

            // create highlighter
            IFormatter formatter = new SimpleHTMLFormatter("<span style=\"font-weight:bold; background-color: #e5ecf9; \">", "</span>"); // ovdje radi hl svoje 
            SimpleFragmenter fragmenter = new SimpleFragmenter(80);
            QueryScorer scorer = new QueryScorer(query);
            Highlighter highlighter = new Highlighter(formatter, scorer);
            highlighter.TextFragmenter = fragmenter;


            for (int i = 0; i < hits.ScoreDocs.Count(); i++)
            {
                // get the document from index
                Document doc = searcher.Doc(hits.ScoreDocs[i].Doc);

                TokenStream stream = analyzer.TokenStream("", new StringReader(doc.Get("Sazetak")));
                String sample = highlighter.GetBestFragments(stream, doc.Get("Sazetak"), 3, "..."); // uzimamo najbolje fragmente texta 



                //String path = doc.Get("path");

                // create a new row with the result data
                DataRow row = ResultsClanci.NewRow();

                row["id"] = doc.Get("id");
                row["Naslov"] = doc.Get("Naslov"); //doc.Get("Naslov");
                row["Sadrzaj"] = doc.Get("Sadrzaj");
                row["Tagovi"] = doc.Get("Tagovi");
                row["DatumKreiranja"] = doc.Get("DatumKreiranja");
                row["DatumZadnjeIzmjene"] = doc.Get("DatumZadnjeIzmjene");
                row["DatumZadnjeAktivnosti"] = doc.Get("DatumZadnjeAktivnosti");
                //row["DatumZatvaranjaPosta"] = doc.Get("DatumZatvaranjaPosta");
                row["PrihvaceniOdgovori"] = doc.Get("PrihvaceniOdgovori");
                row["BrojOdgovora"] = doc.Get("BrojOdgovora");
                row["BrojKomentara"] = doc.Get("BrojKomentara");
                row["BrojOmiljenih"] = doc.Get("BrojOmiljenih");
                row["BrojPregleda"] = doc.Get("BrojPregleda");
                row["BrojPoena"] = doc.Get("BrojPoena");
                //row["VlasnikID"] = doc.Get("VlasnikID");
                row["VlasnikNadimak"] = doc.Get("VlasnikNadimak");
                //row["PromijenioID"] = doc.Get("PromijenioID");
                //row["RoditeljskiPostID"] = doc.Get("RoditeljskiPostID");
                //row["PodKategorija"] = doc.Get("PodKategorija");
                row["PostVrsta"] = doc.Get("PostVrsta");
                row["SlikaURL"] = doc.Get("SlikaURL");
                //row["temp"] = doc.Get("temp");
                row["Likes"] = doc.Get("Likes");
                row["Unlikes"] = doc.Get("Unlikes");
                row["Sazetak"] = sample; //doc.Get("Sazetak");
                row["BrojRangiranja"] = doc.Get("BrojRangiranja");
                row["PrihvacenaIzmjena"] = doc.Get("PrihvacenaIzmjena");
                row["Podnaslov"] = doc.Get("Podnaslov");
                //row["Broj.Razgovora"] = doc.Get("Broj.Razgovora");
                //row["sample"] = sample;
                //row["sampleNaslov"] = sampleNaslov;


                ResultsClanci.Rows.Add(row);
            }
            searcher.Dispose();

            return ResultsClanci; // vracamo datatable i dodajemo u datasource 
        }
        public static DataTable searchPitanja(string pretraga)
        {
            DataTable ResultsPitanja = new DataTable();
            // create the searcher
            // index is placed in "index" subdirectory
            string indexDirectory = "J:/Triglav_Web_App/Triglav/Web/Lucene/Pitanja";
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            IndexSearcher searcher = new IndexSearcher(FSDirectory.Open(indexDirectory));

            // parse the query, "text" is the default field to search
            var parser = new MultiFieldQueryParser(Version.LUCENE_30, new[] { "Naslov", "Sadrzaj", "Tagovi" }, analyzer);
            //var parser = new QueryParser(Version.LUCENE_30, "Sadrzaj", analyzer);
            Query query = parser.Parse(pretraga);


            //// create the result DataTable
            ResultsPitanja.Columns.Add("id", typeof(Int32));
            ResultsPitanja.Columns.Add("Naslov", typeof(string));
            ResultsPitanja.Columns.Add("Sadrzaj", typeof(string));
            ResultsPitanja.Columns.Add("Tagovi", typeof(string));
            ResultsPitanja.Columns.Add("DatumKreiranja", typeof(DateTime));
            ResultsPitanja.Columns.Add("DatumZadnjeIzmjene", typeof(DateTime));
            ResultsPitanja.Columns.Add("DatumZadnjeAktivnosti", typeof(DateTime));
            ResultsPitanja.Columns.Add("DatumZatvaranjaPosta", typeof(DateTime));
            ResultsPitanja.Columns.Add("PrihvaceniOdgovori", typeof(Int32));
            ResultsPitanja.Columns.Add("BrojOdgovora", typeof(Int32));
            ResultsPitanja.Columns.Add("BrojKomentara", typeof(Int32));
            ResultsPitanja.Columns.Add("BrojOmiljenih", typeof(Int32));
            ResultsPitanja.Columns.Add("BrojPregleda", typeof(Int32));
            ResultsPitanja.Columns.Add("BrojPoena", typeof(Int32));
            ResultsPitanja.Columns.Add("VlasnikID", typeof(Int32));
            ResultsPitanja.Columns.Add("VlasnikNadimak", typeof(string));
            ResultsPitanja.Columns.Add("PromijenioID", typeof(Int32));
            ResultsPitanja.Columns.Add("RoditeljskiPostID", typeof(Int32));
            //Results.Columns.Add("PodKategorija", typeof(Int32));
            ResultsPitanja.Columns.Add("PostVrsta", typeof(Int32));
            // ResultsPitanja.Columns.Add("SlikaURL", typeof(string));
            ResultsPitanja.Columns.Add("temp", typeof(string));
            ResultsPitanja.Columns.Add("Likes", typeof(Int32));
            ResultsPitanja.Columns.Add("Unlikes", typeof(Int32));
            ResultsPitanja.Columns.Add("Sazetak", typeof(string));
            ResultsPitanja.Columns.Add("BrojRangiranja", typeof(Int32));
            ResultsPitanja.Columns.Add("PrihvacenaIzmjena", typeof(Int32));
            ResultsPitanja.Columns.Add("Podnaslov", typeof(string));
            ResultsPitanja.Columns.Add("Broj.Razgovora", typeof(Int32));
            ResultsPitanja.Columns.Add("sample", typeof(string));

            // search
            TopDocs hits = searcher.Search(query, 5);

            //E this.total = hits.TotalHits;

            // create highlighter
            IFormatter formatter = new SimpleHTMLFormatter("<span style=\"font-weight:bold; background-color: #e5ecf9; \">", "</span>");
            SimpleFragmenter fragmenter = new SimpleFragmenter(80);
            QueryScorer scorer = new QueryScorer(query);
            Highlighter highlighter = new Highlighter(formatter, scorer);
            highlighter.TextFragmenter = fragmenter;


            for (int i = 0; i < hits.ScoreDocs.Count(); i++)
            {
                // get the document from index
                Document doc = searcher.Doc(hits.ScoreDocs[i].Doc);

                TokenStream stream = analyzer.TokenStream("", new StringReader(doc.Get("Sadrzaj")));
                String sample = highlighter.GetBestFragments(stream, doc.Get("Sadrzaj"), 3, "...");


                //String path = doc.Get("path");

                // create a new row with the result data
                DataRow rowPitanja = ResultsPitanja.NewRow();

                rowPitanja["id"] = doc.Get("id");
                rowPitanja["Naslov"] = doc.Get("Naslov");
                rowPitanja["Sadrzaj"] = sample; //doc.Get("Sadrzaj");
                rowPitanja["Tagovi"] = doc.Get("Tagovi");
                rowPitanja["DatumKreiranja"] = doc.Get("DatumKreiranja");
                rowPitanja["DatumZadnjeIzmjene"] = doc.Get("DatumZadnjeIzmjene");
                rowPitanja["DatumZadnjeAktivnosti"] = doc.Get("DatumZadnjeAktivnosti");
                //row["DatumZatvaranjaPosta"] = doc.Get("DatumZatvaranjaPosta");
                rowPitanja["PrihvaceniOdgovori"] = doc.Get("PrihvaceniOdgovori");
                rowPitanja["BrojOdgovora"] = doc.Get("BrojOdgovora");
                rowPitanja["BrojKomentara"] = doc.Get("BrojKomentara");
                rowPitanja["BrojOmiljenih"] = doc.Get("BrojOmiljenih");
                rowPitanja["BrojPregleda"] = doc.Get("BrojPregleda");
                rowPitanja["BrojPoena"] = doc.Get("BrojPoena");
                //row["VlasnikID"] = doc.Get("VlasnikID");
                rowPitanja["VlasnikNadimak"] = doc.Get("VlasnikNadimak");
                //row["PromijenioID"] = doc.Get("PromijenioID");
                //row["RoditeljskiPostID"] = doc.Get("RoditeljskiPostID");
                //row["PodKategorija"] = doc.Get("PodKategorija");
                rowPitanja["PostVrsta"] = doc.Get("PostVrsta");
                //rowPitanja["SlikaURL"] = doc.Get("SlikaURL");
                //row["temp"] = doc.Get("temp");
                rowPitanja["Likes"] = doc.Get("Likes");
                rowPitanja["Unlikes"] = doc.Get("Unlikes");
                rowPitanja["Sazetak"] = doc.Get("Sazetak");
                rowPitanja["BrojRangiranja"] = doc.Get("BrojRangiranja");
                rowPitanja["PrihvacenaIzmjena"] = doc.Get("PrihvacenaIzmjena");
                rowPitanja["Podnaslov"] = doc.Get("Podnaslov");
                //row["Broj.Razgovora"] = doc.Get("Broj.Razgovora");
                //rowPitanja["sample"] = sample;


                ResultsPitanja.Rows.Add(rowPitanja);
            }
            searcher.Dispose();
            return ResultsPitanja;
        }

        public static List<Post> getClanciPretrage(string recenica)
        {
            if (recenica != "")
            {

                Directory directoryPronadjeniClanci = Data.Lucene.Indexing.GetDirectoryClanci();
                Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
                IndexReader indexReader = IndexReader.Open(directoryPronadjeniClanci, true);
                Searcher searcher = new IndexSearcher(indexReader);

                //var queryParser = new QueryParser(Version.LUCENE_30, "Naslov", analyzer);
                var queryParser = new MultiFieldQueryParser(Version.LUCENE_30, new[] { "Naslov", "Sazetak", "Sadrzaj", "Tagovi" }, analyzer);
                var query = queryParser.Parse(recenica.Trim()); // Rastavljanje rečenice na rijeci


                TopDocs pronadjeno = searcher.Search(query, indexReader.MaxDoc);
                List<Post> postovi = new List<Post>();
                var hits = pronadjeno.ScoreDocs;
                foreach (var hit in hits)
                {
                    var documentFromSearcher = searcher.Doc(hit.Doc);
                    using (TriglavBL temp = new TriglavBL())
                    {
                        postovi.Add(temp.getClanakByID(Convert.ToInt32(documentFromSearcher.Get("id"))));
                    }
                }


                searcher.Dispose();
                directoryPronadjeniClanci.Dispose();
                return postovi;
            }
            else return null;
        }
        public static List<Post> getClanciByTag(int TAGID)
        {
            Directory directoryPronadjeniClanciTagovi = Data.Lucene.Indexing.GetDirectoryClanciTagovi();
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
            IndexReader indexReader = IndexReader.Open(directoryPronadjeniClanciTagovi, true);
            Searcher searcher = new IndexSearcher(indexReader);

            //var queryParser = new QueryParser(Version.LUCENE_30, "Naslov", analyzer);
            var queryParser = new MultiFieldQueryParser(Version.LUCENE_30, new[] { "PostID", "TagID", "DatumKreiranja" }, analyzer);
            var query = queryParser.Parse(Convert.ToString(TAGID)); // Rastavljanje rečenice na rijeci

            TopDocs pronadjeno = searcher.Search(query, indexReader.MaxDoc);
            List<Post> postovi = new List<Post>();

            if (pronadjeno != null)
            {
                var hits = pronadjeno.ScoreDocs;
                foreach (var hit in hits)
                {
                    var documentFromSearcher = searcher.Doc(hit.Doc);
                    using (TriglavBL temp = new TriglavBL())
                    {
                        postovi.Add(temp.getPostByID(Convert.ToInt32(documentFromSearcher.Get("PostID"))));
                    }
                }
                searcher.Dispose();
                directoryPronadjeniClanciTagovi.Dispose();
                return postovi;
            }
            else
            {
                searcher.Dispose();
                directoryPronadjeniClanciTagovi.Dispose();
                return postovi;
            }
        }

        public static List<Post> getPitanjaPretrage(string recenica)
        {
            if (recenica != "")
            {
                Directory directoryPronadjenaPitanja = Data.Lucene.Indexing.GetDirectoryPitanja();
                Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
                IndexReader indexReader = IndexReader.Open(directoryPronadjenaPitanja, true);
                Searcher searcher = new IndexSearcher(indexReader);

                //var queryParser = new QueryParser(Version.LUCENE_30, "Naslov", analyzer);
                var queryParser = new MultiFieldQueryParser(Version.LUCENE_30, new[] { "Naslov", "Sadrzaj", "Tagovi" }, analyzer);
                var query = queryParser.Parse(recenica.Trim()); // Rastavljanje rečenice na rijeci

                TopDocs pronadjeno = searcher.Search(query, indexReader.MaxDoc);
                List<Post> pitanja = new List<Post>();
                

                var hits = pronadjeno.ScoreDocs;
                foreach (var hit in hits)
                {
                    var documentFromSearcher = searcher.Doc(hit.Doc);
                    using (TriglavBL temp = new TriglavBL())
                    {
                        pitanja.Add(temp.getPitanjeByID(Convert.ToInt32(documentFromSearcher.Get("id"))));
                    }
                }


                searcher.Dispose();
                directoryPronadjenaPitanja.Dispose();
                return pitanja;
            }
            else return null;


        }
        public static List<Post> getPitanjaByTag(int TAGID)
        {

            Directory directoryPronadjenaPitanjaTagovi = Data.Lucene.Indexing.GetDirectoryPitanjaTagovi();
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
            IndexReader indexReader = IndexReader.Open(directoryPronadjenaPitanjaTagovi, true);
            Searcher searcher = new IndexSearcher(indexReader);

            //var queryParser = new QueryParser(Version.LUCENE_30, "Naslov", analyzer);
            var queryParser = new MultiFieldQueryParser(Version.LUCENE_30, new[] { "PostID", "TagID", "DatumKreiranja" }, analyzer);
            var query = queryParser.Parse(Convert.ToString(TAGID)); // Rastavljanje rečenice na rijeci

            TopDocs pronadjeno = searcher.Search(query, indexReader.MaxDoc);
            List<Post> PitanjaTagovi = new List<Post>();

            if (pronadjeno != null)
            {
                var hits = pronadjeno.ScoreDocs;
                foreach (var hit in hits)
                {
                    var documentFromSearcher = searcher.Doc(hit.Doc);
                    using (TriglavBL temp = new TriglavBL())
                    {
                        PitanjaTagovi.Add(temp.getPitanjeByID(Convert.ToInt32(documentFromSearcher.Get("PostID"))));
                    }
                }
                searcher.Dispose();
                directoryPronadjenaPitanjaTagovi.Dispose();
                return PitanjaTagovi;
            }
            else
            {
                searcher.Dispose();
                directoryPronadjenaPitanjaTagovi.Dispose();
                return PitanjaTagovi;
            }
           

        }
    }

}