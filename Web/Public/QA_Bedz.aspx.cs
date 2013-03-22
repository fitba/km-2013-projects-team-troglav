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
    public partial class QA_Bedz : System.Web.UI.Page
    {
        public Data.EntityFramework.DAL.Bedz bedz { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            Button btn_Bedzevi = (Button)Master.FindControl("btn_Bedzevi");
            btn_Bedzevi.BackColor = Color.LightGreen;

            using (TriglavBL temp = new TriglavBL())
            {
                if (Request.QueryString["BedzID"] != null)
                {
                    bedz = temp.getBedzByID(Convert.ToInt32(Request.QueryString["BedzID"]));
                    
                    Napunipolja();
                }

                else
                {
                    Response.Redirect("/Public/Bedzevi.aspx");
                }
            }

        }

        private void Napunipolja()
        {
            using (TriglavBL temp = new TriglavBL())
            {
                img_Bedz.ImageUrl = bedz.SlikaURL;
                lbl_Naziv.Text = bedz.Naziv;
                lbl_Opis.Text = bedz.Opis;
                List<Data.EntityFramework.DAL.Korisnik> listaNositelja = temp.NositeljiBedzevaByBedzID(bedz.id);
                lbl_BrojKorisnika.Text = "Bedz posjeduje: " + listaNositelja.Count() + " korisnika";
                dl_Korisnici.DataSource = listaNositelja;
                dl_Korisnici.DataBind();


            }
        }

        protected void dl_Korisnici_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                List<Post> listaPostova = temp.getPitanjaByVlasnikID(id);
                List<Post> listaOdgovora = temp.getOdgovoriByVlasnikID(id);

                Label lbl_BrojPostova = (Label)e.Item.FindControl("lbl_BrojPostova");
                Label lbl_BrojOdgovora = (Label)e.Item.FindControl("lbl_BrojOdgovora");

                lbl_BrojPostova.Text = "Postovi" + listaPostova.Count();
                lbl_BrojOdgovora.Text = "Odgovori" + listaOdgovora.Count();
            }
        }
    }
}