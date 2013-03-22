using Data.EntityFramework.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Public
{
    public partial class ClanakStareIzmjene : System.Web.UI.Page
    {
        public Data.EntityFramework.DAL.Post post { get; set; }
        public Data.EntityFramework.DAL.Post post_odobreni { get; set; }
        public Data.EntityFramework.DAL.Post post_izmjena { get; set; }
        public Data.EntityFramework.DAL.Korisnik korisnik { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            korisnik = (Data.EntityFramework.DAL.Korisnik)Session["LogiraniKorisnik"];

            using (TriglavBL temp = new TriglavBL())
            {

                if (Request.QueryString != null)
                {
                    if (Request.QueryString["PostID"] != null)
                    {
                        int postId = Int32.Parse(Request.QueryString["PostID"]);
                        post = temp.getPostByID(postId);
                    }
                    else
                    {
                        Response.Redirect("/Public/Home.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/Public/Home.aspx");
                }


                if (korisnik != null)
                {
                    dl_PrihvaceneIzmjene.DataSource = temp.getIzmjeneClanka(post.id); //roditeljski post
                    dl_PrihvaceneIzmjene.DataBind();

                    dl_PrijedloziIzmjena.Visible = true;
                    dl_PrijedloziIzmjena.DataSource = temp.getPrijedloziIzmjenaClanka(post.id);
                    dl_PrijedloziIzmjena.DataBind();

                    if (korisnik.id == post.VlasnikID)
                    {
                        Btn_Odobri.Visible = true;
                    }

                    if (!IsPostBack)
                    {
                        Btn_Odobri.Visible = false;
                        txt_odobreni.Visible = false;
                        txt_izmjena.Visible = false;
                    }
                }
                else
                {
                    Btn_Odobri.Visible = false;

                    dl_PrihvaceneIzmjene.DataSource = temp.getIzmjeneClanka(post.id); //roditeljski post
                    dl_PrihvaceneIzmjene.DataBind();

                    dl_PrijedloziIzmjena.Visible = false;
                    dl_PrijedloziIzmjena.DataSource = temp.getPrijedloziIzmjenaClanka(post.id);
                    dl_PrijedloziIzmjena.DataBind();
                    Btn_Odobri.Visible = false;
                    txt_odobreni.Visible = false;
                    txt_izmjena.Visible = false;
                    Btn_Uporedi.Visible = false;
                }

                btn_GlavnaStranica.BackColor = System.Drawing.Color.LightBlue;
                btn_Citaj.BackColor = System.Drawing.Color.White;
                btn_Razgovor.BackColor = System.Drawing.Color.White;
                btn_VidiIzvornik.BackColor = System.Drawing.Color.White;
                btn_VidiIzmjene.BackColor = System.Drawing.Color.LightBlue;
            }
        }

        protected void btn_GlavnaStranica_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }
        protected void btn_Razgovor_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakRazgovor.aspx?id=" + post.id);
        }
        protected void btn_Citaj_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
        }
        protected void btn_VidiIzvornik_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakUredi.aspx?PostID=" + post.id);
        }
        protected void btn_VidiIzmjene_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Public/ClanakStareIzmjene.aspx?PostID=" + post.id);
        }


        protected void dl_PrijedloziIzmjena_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            using (TriglavBL temp = new TriglavBL())
            {
                int id = (int)DataBinder.Eval(e.Item.DataItem, "id");
                post_odobreni = temp.getPostByID(id);
                // Response.Write("<script>alert('"+id+"');</script>");
            }

            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;


            RadioButton cur = (RadioButton)e.Item.FindControl("rdbP");
            string script = "SetSingleRadioButton('" + cur.ClientID + "',this)";
            cur.Attributes.Add("onclick", script);

        }

        protected void Btn_Uporedi_Click(object sender, EventArgs e)
        {
            if (post_odobreni.Sadrzaj != "")
                txt_odobreni.Text = post_odobreni.Sadrzaj;
            else
                txt_odobreni.Text = post.Sadrzaj;

            txt_izmjena.Text = post_odobreni.Sadrzaj;
            Btn_Odobri.Visible = true;
            txt_odobreni.Visible = true;
            txt_izmjena.Visible = true;

        }
        protected void Btn_Odobri_Click(object sender, EventArgs e)
        {

            using (TriglavBL temp = new TriglavBL())
            {
                post_odobreni.PrihvacenaIzmjena = 1;
                temp.UpdatePost(post_odobreni);

                post.PrihvaceniOdgovori++;
                temp.UpdatePost(post);
                Response.Redirect("/Public/Clanak.aspx?PostID=" + post.id);
            }

        }
        protected void txt_Naslov_Click(object sender, EventArgs e)
        {
            LinkButton _sender = (LinkButton)sender;
            HiddenField hid = (HiddenField)_sender.FindControl("hf");
            using (TriglavBL temp = new TriglavBL())
            {
                txt_odobreni.Visible = true;
                txt_odobreni.Text = temp.getPostByID(Convert.ToInt32(hid.Value)).Sadrzaj;
                _sender.BackColor = Color.AliceBlue;
            }
        }
    }
}

