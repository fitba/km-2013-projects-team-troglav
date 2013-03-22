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
    public partial class QA_Korisnici : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button btn_Korisnici = (Button)Master.FindControl("btn_Korisnici");
            //btn_Korisnici.BackColor = Color.LightGreen;
            if (!IsPostBack)
            {
                btn_PoReputaciji.BackColor = Color.LightGreen;
                btn_NoviKorisnici.BackColor = Color.LightGray;
                btn_Moderatori.BackColor = Color.LightGray;
            }
            using (TriglavBL temp = new TriglavBL())
            {
                lbl_NaslovStranice.Text = "Korisnici po reputaciji";
                dl_Korisnici.DataSource = temp.getKorisniciAll();
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

        protected void btn_PoReputaciji_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Korisnici po reputaciji";
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Korisnici.DataSource = temp.getKorisniciByReputacija();
                dl_Korisnici.DataBind();
                btn_PoReputaciji.BackColor = Color.LightGreen;
                btn_NoviKorisnici.BackColor = Color.LightGray;
                btn_Moderatori.BackColor = Color.LightGray;
            }
        }
        protected void btn_NoviKorisnici_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Korisnici nedavno registrovani";
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Korisnici.DataSource = temp.getKorisniciByDate();
                dl_Korisnici.DataBind();
                btn_PoReputaciji.BackColor = Color.LightGray;
                btn_NoviKorisnici.BackColor = Color.LightGreen;
                btn_Moderatori.BackColor = Color.LightGray;
            }
        }
        protected void btn_Moderatori_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Korisnici moderatori";
            using (TriglavBL temp = new TriglavBL())
            {
                dl_Korisnici.DataSource = temp.getKorisniciByPostovi();
                dl_Korisnici.DataBind();
                btn_PoReputaciji.BackColor = Color.LightGray;
                btn_NoviKorisnici.BackColor = Color.LightGray;
                btn_Moderatori.BackColor = Color.LightGreen;
            }
        }

      

        
    }
}