<%@ Page Title="" Language="C#" MasterPageFile="~/Public/QA.Master" AutoEventWireup="true" CodeBehind="QA_Tagovi.aspx.cs" Inherits="Web.Public.QA_Tagovi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="glavna"> 
    <div class="omotac">
    <div style="text-align: left; margin-right: 40px;">
        <asp:Button ID="btn_Popularni" runat="server" Text="Popularni" BorderStyle="Dotted" BorderWidth="2px" Style="margin-left: 40px" Width="120px" OnClick="btn_Popularni_Click" />
        <asp:Button ID="btn_PoAbecedi" runat="server" Text="Po Abecedi" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_PoAbecedi_Click" />
        <asp:Button ID="btn_Najnoviji" runat="server" Text="Najnoviji" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_Najnoviji_Click" />
        <br />
        <hr />
        <br />
         <asp:Label ID="lbl_NaslovStranice" runat="server" Font-Bold="True" ></asp:Label>
        <br />
        <%--DATALIST PITANJA--%>

        <div style="padding: 10px 10px 10px 10px;">
            <asp:TextBox ID="txt_TagoviPretraga" runat="server"></asp:TextBox>
            <asp:Button ID="btn_TagoviPretraga" runat="server" Text="Potražite tag" OnClick="btn_TagoviPretraga_Click" />
            <br />


            <asp:DataList ID="rpt_Tagovi" runat="server" OnItemDataBound="rpt_Tagovi_ItemDataBound" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" GridLines="Both">
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"></FooterStyle>
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White"></HeaderStyle>

                <ItemStyle BackColor="#FFF7E7" ForeColor="#8C4510"></ItemStyle>
                <ItemTemplate>
                    <asp:LinkButton ID="lb_TagNaziv" runat="server" PostBackUrl='<%# "~/Public/QA_Pitanja.aspx?TagID=" + Eval("id") %>'>  <%#Eval("Naziv") %> </asp:LinkButton>
                    <asp:Label ID="lbl_BrojTagovanihPostova" runat="server"></asp:Label>
                    <abbr class="timeago" title="<%# DataBinder.Eval(Container.DataItem, "DatumKreiranja", "{0:yyyy-M-dThh:mm:ss}") %>">nije uspjelo</abbr><br />
                    <asp:Literal ID="lit_TagOpis" runat="server" Text='<%#Eval("Opis") %>'>    </asp:Literal><br />
                </ItemTemplate>
                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White"></SelectedItemStyle>
            </asp:DataList>

        </div>

    </div>
</div>
    </div>
</asp:Content>
