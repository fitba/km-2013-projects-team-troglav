<%@ Page Title="" Language="C#" MasterPageFile="~/Public/QA.Master" AutoEventWireup="true" CodeBehind="QA_Bedz.aspx.cs" Inherits="Web.Public.QA_Bedz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <asp:ImageButton ID="img_Bedz" runat="server"  ImageAlign="Left" Height="120px" Width="120px" />
            <asp:Label ID="lbl_Naziv" runat="server" ></asp:Label>
            <asp:Label ID="lbl_Opis" runat="server" ></asp:Label><br />
            <abbr id="tago" class="timeago" title="DatumKreiranja">nije uspjelo</abbr>            
            <asp:Label ID="lbl_BrojKorisnika" runat="server"></asp:Label><br /><br />


    <%-- DATALIST KORISNICI--%>
    <asp:DataList ID="dl_Korisnici" runat="server" RepeatColumns="3" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" CellSpacing="15" OnItemDataBound="dl_Korisnici_ItemDataBound" RepeatDirection="Horizontal">
        <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>

        <FooterStyle BackColor="#CCCCCC" ForeColor="Black"></FooterStyle>

        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"></HeaderStyle>

        <ItemStyle BackColor="#EEEEEE" ForeColor="Black"></ItemStyle>
        <ItemTemplate>

            <asp:ImageButton ID="img_Korisnik" runat="server" Height="120px" Width="120px" ImageAlign="left" ImageUrl='<%#Eval("SlikaURL") %>' PostBackUrl='<%#"/Public/QA_Pitanja.aspx?KorisnikID="+ Eval("id") %>' />
            <asp:Label ID="lbl_Nadimak" runat="server"><%#Eval("Nadimak") %></asp:Label>
            <asp:Label ID="lbl_BrojGodina" runat="server"><%# "BrojGodina: "+Eval("BrojGodina") %></asp:Label><br />
            <asp:Label ID="OMeni" runat="server"><%#Eval("OMeni") %></asp:Label>
            <abbr class="timeago" title='<%# "Registriran"+ DataBinder.Eval(Container.DataItem, "DatumKreiranja", "{0:yyyy-M-dThh:mm:ss+01:00}") %>'>nije uspjelo</abbr><br />
            <abbr class="timeago" title='<%# "Zadnja aktivnost" + DataBinder.Eval(Container.DataItem, "DatumZadnjegPristupa", "{0:yyyy-M-dThh:mm:ss+01:00}") %>'>nije uspjelo</abbr><br />

            <asp:Label ID="lbl_Reputacija" runat="server" Text='<%# "Rep: " + Eval("Reputacija") %>'><%# "Rep" + Eval("Reputacija") %></asp:Label>
            <asp:Label ID="lbl_Likes" runat="server" Text='<%#"Sviđanja: " + Eval("Likes") %>'></asp:Label>
            <asp:Label ID="lbl_Unlikes" runat="server" Text='<%# "Nesviđanja: " +Eval("Unlikes") %>'></asp:Label>
            <br />
            <asp:Image ID="img_Bedz" runat="server" Height="50px" Width="50px" ImageUrl='<%#Eval("BedzSlika") %>' />
            <asp:Label ID="lbl_BedzNaziv" runat="server"><%#Eval("BedzNaziv") %></asp:Label>
            <br />
            <asp:Label ID="lbl_BrojZlatnih" runat="server" Text='<%#"Zlatnika: " + Eval("BrojZlatnih") %>'></asp:Label>
            <asp:Label ID="lbl_BrojSrebrenih" runat="server" Text='<%#"Dukata: "+ Eval("BrojSrebrenih") %>'></asp:Label>
            <asp:Label ID="lbl_Bronze" runat="server" ToolTip='<%#Eval("BedzOpis") %>' Text='<%#"Groševa: " + Eval("BrojBronzanih") %>'></asp:Label>
            <br />
            <asp:Label ID="lbl_BrojPostova" runat="server"></asp:Label>
            <asp:Label ID="lbl_BrojOdgovora" runat="server"></asp:Label>

        </ItemTemplate>
        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White"></SelectedItemStyle>
    </asp:DataList>
</asp:Content>
