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
    public partial class QA_Bedzevi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)       
        {
            Button btn_Bedzevi = (Button)Master.FindControl("btn_Bedzevi");
            //btn_Bedzevi.BackColor = Color.LightGreen;
            using (TriglavBL temp = new TriglavBL())
            {
                dl_bedzevi.DataSource = temp.getSviBedzeviBybrojKorisnika();
                dl_bedzevi.DataBind();
            }
        }

        protected void dl_bedzevi_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            int id = (int)DataBinder.Eval(e.Item.DataItem, "id");

            using (TriglavBL temp = new TriglavBL())
            {
                Bedz b = temp.getBedzByID(id);
                List<Data.EntityFramework.DAL.Korisnik> listaNositelja = temp.NositeljiBedzevaByBedzID(id);

                Label lbl_BrojKorisnika = (Label)e.Item.FindControl("lbl_BrojKorisnika");
                lbl_BrojKorisnika.Text = "Bedz posjeduje: " + listaNositelja.Count() +" korisnika";
            }

        }

       
    }
}