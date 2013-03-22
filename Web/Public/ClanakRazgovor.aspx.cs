using Data.EntityFramework.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Public
{
    public partial class ClanakRazgovor : System.Web.UI.Page
    {
        private int postId;
        public Data.EntityFramework.DAL.Post post { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
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
                        Response.Redirect("/Public/Home.aspx");
                    }
                }
            }
            btn_Clanak.BackColor = System.Drawing.Color.LightGray;
            btn_Razgovor.BackColor = System.Drawing.Color.White;
            btn_Citaj.BackColor = System.Drawing.Color.LightGray;
            btn_VidiIzvornik.BackColor = System.Drawing.Color.LightGray;
            btn_VidiIzmjene.BackColor = System.Drawing.Color.LightGray;  

        }

        protected void btn_Clanak_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }  
        protected void btn_Razgovor_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakRazgovor.aspx");
        }
        protected void btn_Citaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }
        protected void btn_VidiIzvornik_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakUredi.aspx");
        }
        protected void btn_VidiIzmjene_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakStareIzmjene.aspx");
        }              
    }
}