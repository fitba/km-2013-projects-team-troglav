<%@ Page Title="" Language="C#" MasterPageFile="~/Public/QA.Master" AutoEventWireup="true" CodeBehind="QA_Bedzevi.aspx.cs" EnableEventValidation="true" Inherits="Web.Public.QA_Bedzevi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="glavna">
    <div class="omotac">
    <asp:DataList ID="dl_bedzevi" runat="server" OnItemDataBound="dl_bedzevi_ItemDataBound" CellPadding="4" ForeColor="#333333" CellSpacing="15" RepeatColumns="3" RepeatDirection="Horizontal">
        <AlternatingItemStyle BackColor="White" ForeColor="#284775"></AlternatingItemStyle>

        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333"></ItemStyle>
        <ItemTemplate>

            <asp:ImageButton ID="img_Bedz" runat="server" PostBackUrl='<%#"/Public/QA_Bedz.aspx?BedzID="+ Eval("id") %>'  ImageUrl='<%# Eval("SlikaURL") %>' ImageAlign="Left" Height="120px" Width="120px" />
            <asp:Label ID="lbl_Naziv" runat="server" Text='<%# Eval("Naziv") %>'></asp:Label>
            <asp:Label ID="lbl_Opis" runat="server" Text='<%# Eval("Opis") %>'></asp:Label><br />
            <abbr class="timeago" title="<%#  DataBinder.Eval(Container.DataItem, "DatumKreiranja", "{0:yyyy-M-dThh:mm:ss}") %>">nije uspjelo</abbr>
            <asp:HiddenField ID="hid" runat="server" Value='<%# Eval("id") %>' />
            <asp:Label ID="lbl_BrojKorisnika" runat="server"></asp:Label>
        </ItemTemplate>
        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedItemStyle>
    </asp:DataList>

    </div>
    </div>
</asp:Content>
