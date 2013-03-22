<%@ Page Title="" Language="C#" MasterPageFile="~/Public/QA.Master" AutoEventWireup="true" CodeBehind="QA_PitanjeNovo.aspx.cs" Inherits="Web.Public.QA_PitanjeNovo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <%--UKLJUČIVANJE tinyMCE--%>
    <script src="/Scripts/tinymce/tiny_mce.js"></script>
    <script src="/Scripts/tinymce/tiny_mce_init.js"></script>
    <script src="/Scripts/tinymce/plugins/save/editor_plugin.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--KATEGORIJA--%>
      <asp:Label ID="lbl_Kategorija" runat="server" Text="Kategorija"></asp:Label>
        <asp:DropDownList ID="ddl_Kategorija" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    <%--PODKATEGORIJA--%>
        <asp:Label ID="lbl_podkategorija" runat="server" Text="Podkategorija"></asp:Label>
        <asp:DropDownList ID="ddl_Podkategorija" runat="server">
        </asp:DropDownList>

     <asp:Label ID="lbl_Naslov" runat="server" Text="Naslov"></asp:Label>
        <asp:TextBox ID="txt_Naslov" runat="server"></asp:TextBox><br />
        <asp:Label ID="lbl_Sadrzaj" runat="server" Text="Sadržaj"></asp:Label><br />
        <asp:TextBox ID="txt_Sadrzaj" runat="server" TextMode="MultiLine" CssClass="mceEditor" EnableViewState="True" AutoPostBack="True"></asp:TextBox><br />
        <asp:Label ID="lbl_Tagovi" runat="server" Text="Tagovi, između tagova obavezno staviti zarez !"></asp:Label>
        <asp:TextBox ID="txt_Tagovi" runat="server"></asp:TextBox><br />
    <asp:Button ID="btn_SavePitanje" runat="server" OnClick="btn_SaveClanak_Click" Text="Postavi pitanje" />
       
</asp:Content>

