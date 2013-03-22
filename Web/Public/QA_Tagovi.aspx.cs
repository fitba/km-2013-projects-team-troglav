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
    public partial class QA_Tagovi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            using (TriglavBL temp = new TriglavBL())
            {
                //ovdje ide sistem preporuke      
                Button btn_Tagovi = (Button)Master.FindControl("btn_Tagovi");
                //btn_Tagovi.BackColor = Color.LightGreen;

                if (!IsPostBack)
                {
                    rpt_Tagovi.DataSource = temp.getTagoviPitanjaPopularni();
                    rpt_Tagovi.DataBind();
                    lbl_NaslovStranice.Text = "Popularni tagovi";
                    btn_Popularni.BackColor = Color.LightGreen;
                    btn_PoAbecedi.BackColor = Color.LightGray;
                    btn_Najnoviji.BackColor = Color.LightGray;
                }
            }


        }
        protected void rpt_Tagovi_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                int tagCount = temp.getTagCount(id);
                Label lbl_brojtagova = (Label)e.Item.FindControl("lbl_BrojTagovanihPostova");
                lbl_brojtagova.Text = Convert.ToString(tagCount);
            }
        }


        protected void btn_Popularni_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Popularni tagovi";
            using (TriglavBL temp = new TriglavBL())
            {
                rpt_Tagovi.DataSource = temp.getTagoviPitanjaPopularni();
                rpt_Tagovi.DataBind();
            }
            
            btn_Popularni.BackColor = Color.LightGreen;
            btn_PoAbecedi.BackColor = Color.LightGray;
            btn_Najnoviji.BackColor = Color.LightGray;
        }
        protected void btn_PoAbecedi_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Tagovi po abecednom redu";
            using (TriglavBL temp = new TriglavBL())
            {
                rpt_Tagovi.DataSource = temp.getPitanjaTagoviAbeceda();
                rpt_Tagovi.DataBind();
            }
            btn_Popularni.BackColor = Color.LightGray;
            btn_PoAbecedi.BackColor = Color.LightGreen;
            btn_Najnoviji.BackColor = Color.LightGray;
        }
        protected void btn_Najnoviji_Click(object sender, EventArgs e)
        {
            lbl_NaslovStranice.Text = "Najnoviji tagovi";
            using (TriglavBL temp = new TriglavBL())
            {
                rpt_Tagovi.DataSource = temp.getPItanjaTagoviByDate();
                rpt_Tagovi.DataBind();
            }
            btn_Popularni.BackColor = Color.LightGray; ;
            btn_PoAbecedi.BackColor = Color.LightGray;
            btn_Najnoviji.BackColor = Color.LightGreen;
        }


        protected void btn_TagoviPretraga_Click(object sender, EventArgs e)
        {

            if (txt_TagoviPretraga.Text != "")
            {
                using (TriglavBL temp = new TriglavBL())
                {
                    List<Tag> listaTagova = new List<Tag>();
                    string[] tags = txt_TagoviPretraga.Text.Split(',');
                    Tag TAG;
                    foreach (var tagname in tags)
                    {
                        TAG = temp.getTagByName(tagname);
                        if (TAG != null)
                        {
                            listaTagova.Add(TAG);
                        }
                    }
                    if (listaTagova != null)
                    {
                        lbl_NaslovStranice.Text = "Rezultat pretrage tagova: " + txt_TagoviPretraga.Text;
                        rpt_Tagovi.DataSource = listaTagova;
                        rpt_Tagovi.DataBind();
                    }
                }
            }
        }
    }
}