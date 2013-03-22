<%@ Page Title="" Language="C#" MasterPageFile="~/Public/WikiTriglav.Master" AutoEventWireup="true" CodeBehind="ClanakUredi.aspx.cs" Inherits="Web.Public.ClanakUredi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/tinymce/tiny_mce.js"></script>
    <script src="/Scripts/tinymce/tiny_mce_init.js"></script>
    <script src="/Scripts/tinymce/plugins/save/editor_plugin.js"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div style="float: left;">
        <asp:Button ID="btn_GlavnaStranica" runat="server" Text="Članak" BorderStyle="Dotted" BorderWidth="2px" Style="margin-left: 40px" Width="120px" OnClick="btn_GlavnaStranica_Click" />
        <asp:Button ID="btn_Razgovor" runat="server" Text="Razgovor" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_Razgovor_Click" />
    </div>
    <div style="text-align: right; margin-right: 40px;">
        <asp:Button ID="btn_Citaj" runat="server" Text="Čitaj" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_Citaj_Click" />
        <asp:Button ID="btn_VidiIzvornik" runat="server" Text="Uredi" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_VidiIzvornik_Click" />
        <asp:Button ID="btn_VidiIzmjene" runat="server" Text="Vidi izmjene" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_VidiIzmjene_Click" style="height: 26px" />
    </div>

        Uređujete 3glav Članak:  <asp:Label ID="lbl_ClanakIzvorni" runat="server" Font-Size="Medium"></asp:Label><br />

        
        <asp:TextBox ID="txt_Sadrzaj" runat="server" TextMode="MultiLine" CssClass="mceEditor" EnableViewState="True" AutoPostBack="True"></asp:TextBox>
        <asp:Button ID="btn_SacuvajIzmjeneClanka" runat="server" Text="Sačuvaj Izmjene" OnClick="btn_SacuvajIzmjeneClanka_Click" />
        <asp:Button ID="btn_Odustani" runat="server" Text="Odustani" OnClick="btn_Odustani_Click" />
        <asp:Button ID="btn_Preview" runat="server" Text="Prikaži izgled" OnClick="btn_Preview_Click" />
        <asp:Label ID="lbl_Promjenjeni" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbl_Obavjestenje" runat="server" ForeColor="Maroon"></asp:Label>


  



</asp:Content>
