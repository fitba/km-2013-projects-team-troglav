<%@ Page Title="" Language="C#" MasterPageFile="~/Public/WikiTriglav.Master" AutoEventWireup="true" CodeBehind="ClanakNovi.aspx.cs" Inherits="Web.Public.ClanakNovi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--UKLJUČIVANJE tinyMCE--%>
    <script src="/Scripts/tinymce/tiny_mce.js"></script>
    <script src="/Scripts/tinymce/tiny_mce_init.js"></script>
    <script src="/Scripts/tinymce/plugins/save/editor_plugin.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-color: white; padding: 10px 10px 10px 10px;">
        <asp:Label ID="lbl_Kategorija" runat="server" Text="Kategorija"></asp:Label>
        <asp:DropDownList ID="ddl_Kategorija" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Label ID="lbl_podkategorija" runat="server" Text="Podkategorija"></asp:Label>
        <asp:DropDownList ID="ddl_Podkategorija" runat="server">
        </asp:DropDownList>
        <br />        
        <br />
        <%--FOTO--%>
        <asp:Image ID="img_Foto" runat="server" Height="126px" ImageUrl="~/Content/Clanci_Photo/Triglav.png" Width="125px" />
            <br />
            <br />
            <asp:Label runat="server" Text="Fotografija" ID="lbl_Foto" Width="120px"></asp:Label>
            <br />
            <asp:FileUpload ID="FuFoto" runat="server" />
            <asp:Button ID="btn_uploadFoto" runat="server" Text="Upload" Style="margin-left: 48px" OnClick="btn_uploadFoto_Click" CausesValidation ="false"/>


        <asp:Label ID="lbl_Naslov" runat="server" Text="Naslov"></asp:Label>
        <asp:TextBox ID="txt_Naslov" runat="server"></asp:TextBox><br />
        <asp:Label ID="lbl_Sadrzaj" runat="server" Text="Sadržaj"></asp:Label><br />
        <asp:TextBox ID="txt_Sadrzaj" runat="server" TextMode="MultiLine" CssClass="mceEditor" EnableViewState="True" AutoPostBack="True"></asp:TextBox><br />
        <asp:Label ID="lbl_Sazetak" runat="server" Text="Sažetak"></asp:Label><br />
        <asp:TextBox ID="txt_Sazetak" runat="server" TextMode="MultiLine" CssClass="mceEditor" EnableViewState="True" AutoPostBack="True"></asp:TextBox>
        <asp:Label ID="lbl_Tagovi" runat="server" Text="Tagovi, između tagova obavezno staviti zarez !"></asp:Label>
        <asp:TextBox ID="txt_Tagovi" runat="server"></asp:TextBox><br />
        <br />
        <asp:Button ID="btn_SaveClanak" runat="server" OnClick="btn_SaveClanak_Click" Text="Sačuvaj članak" />
        <br />
        <br />
        <br />
        <asp:Button ID="btn_Dodaj_Poglavlje" runat="server" Text="Dodaj Poglavlje" OnClick="btn_Dodaj_Poglavlje_Click" Visible="False" /><br />


        <asp:Panel ID="pnl_NovoPoglavlje" runat="server" Visible="False">
            Novo poglavlje
        <br />
            <asp:Label ID="lbl_NaslovPoglavlja" runat="server" Text="Naslov poglavlja"></asp:Label>
            <asp:TextBox ID="txt_NaslovPoglavlja" runat="server"></asp:TextBox><br />
            <asp:Label ID="lbl_SadrzajPoglavlja" runat="server" Text="Sadržaj poglavlja"></asp:Label><br />
            <asp:TextBox ID="txt_SadrzajPoglavlja" runat="server" TextMode="MultiLine" CssClass="mceEditor" EnableViewState="True" AutoPostBack="True"></asp:TextBox>
             <br />
            <asp:Button ID="btn_SacuvajPoglavlje" runat="server" OnClick="btn_SacuvajPoglavlje_Click" Text="Sacuvaj poglavlje" />
            <br />
        </asp:Panel>
    </div>



</asp:Content>
