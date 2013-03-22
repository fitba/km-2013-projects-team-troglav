<%@ Page Title="" Language="C#" MasterPageFile="~/Public/QA.Master" AutoEventWireup="true" CodeBehind="QA_Neodgovoreni.aspx.cs" Inherits="Web.Public.QA_Neodgovoreni" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="glavna">
    <div class="omotac">
    <div style="text-align: left; margin-right: 40px;">
        <asp:Button ID="btn_MojiTagovi" runat="server" Text="Moji Tagovi" BorderStyle="Dotted" BorderWidth="2px" Style="margin-left: 40px" Width="120px" OnClick="btn_MojiTagovi_Click" />
        <asp:Button ID="btn_Najnovija" runat="server" Text="Najnovija" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_Najnovija_Click" />
        <asp:Button ID="btn_PoGlasovima" runat="server" Text="Po Glasovima" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_PoGlasovima_Click" />
        <br />
        <hr />
        <br />
        Top Pitanja
        <br />

        <%--DATALIST PITANJA--%>
        
        <div style="padding: 10px 10px 10px 10px;">

            <asp:DataList ID="dl_Pitanja" runat="server" OnItemDataBound="dl_Pitanja_ItemDataBound" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <AlternatingItemStyle BackColor="#DCDCDC"></AlternatingItemStyle>

            <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>

            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"></HeaderStyle>

            <ItemStyle BackColor="#EEEEEE" ForeColor="Black"></ItemStyle>
            <ItemTemplate>
                <asp:LinkButton ID="txt_Naslov" runat="server" Font-Underline="False" PostBackUrl='<%# "~/Public/QA_Pitanje.aspx?PostID=" + Eval("id") %>'> <%#Eval("Naslov") %></asp:LinkButton>
                <abbr class="timeago" title="<%# DataBinder.Eval(Container.DataItem, "DatumZadnjeIzmjene", "{0:yyyy-M-dThh:mm:ss}") %>">nije uspjelo</abbr><br />
                <br />
                <asp:Label ID="txt_Sazetak" runat="server"> <%#Eval("Sadrzaj") %> </asp:Label>

                <%--DATALIST TAGOVI CLANKA--%>
                <asp:DataList ID="dl_Tagovi" runat="server" CaptionAlign="NotSet" AlternatingItemStyle-HorizontalAlign="Center" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <div style="margin: 2px; padding: 2px 10px 2px 10px; float: left; background-color: #999999; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: x-small;">
                            <asp:LinkButton ID="txt_tag" runat="server" Font-Underline="False" PostBackUrl='<%# "~/Public/QA_Home.aspx?TagID=" + Eval("id") %>' ForeColor="White" Font-Bold="True"><%#Eval("Naziv") %></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:DataList>

                <asp:Image ID="img_User" runat="server" Height="25px" Width="25px" />
                <asp:Label ID="lbl_User" runat="server" Text='<%#Eval("VlasnikNadimak") %>'></asp:Label><br />
                <asp:Label ID="lbl_Reputacija" runat="server" />
                <asp:Image ID="img_BedzVlsanika" runat="server" Height="25px" Width="25px"/> 
                <asp:Label ID="lbl_NazivBedzaVlasnika" runat="server" ></asp:Label>
                <asp:Label ID="lbl_Gold" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lbl_Silver" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lbl_Bronze" runat="server" Text="Label"></asp:Label>

            </ItemTemplate>
            <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedItemStyle>
        </asp:DataList>         
        </div>
        </div>

                </div>
       </div>  
</asp:Content>
