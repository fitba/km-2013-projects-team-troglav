<%@ Page Title="" Language="C#" MasterPageFile="~/Public/WikiTriglav.Master" AutoEventWireup="true" CodeBehind="ClanakRazgovor.aspx.cs" Inherits="Web.Public.ClanakRazgovor" %>

<%@ Register Src="~/Controls/Razgovori/Razgovori.ascx" TagName="RazgovoriBox" TagPrefix="rb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="glavna">
<div class="desno">

            <div class="kategorije2">
                <div class="naslov">
                    <h1>WIKI preporuke</h1>
                </div>

                <div class="oblastitemeclanakfull">

                    <ul>
                        <asp:DataList ID="dl_WikiPoveznice" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton ID="lb_ClanakNaziv" runat="server" PostBackUrl='<%# "~/Public/Clanak.aspx?PostID=" + Eval("id") %>'><%#Eval("Naslov") %></asp:LinkButton>
                                </li>
                            </ItemTemplate>
                        </asp:DataList>

                    </ul>
                </div>

            </div>

            <div class="kategorije2">
                <div class="naslov">
                    <h1>Q&A preporuke</h1>
                </div>

                <div class="oblastitemeclanakfull">
                    <ul>

                        <asp:DataList ID="dl_QAPoveznice" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton ID="lb_PitanjeNaziv" runat="server" PostBackUrl='<%# "~/Public/QA_Pitanje.aspx?PostID=" + Eval("id") %>'><%#Eval("Naslov") %></asp:LinkButton>
                                </li>
                            </ItemTemplate>
                        </asp:DataList>

                    </ul>
                </div>
            </div>
             <%--PREPORUKA KORISNICI ELVIS DODAO--%>
              <div class="naslov">
                    <h1>Korisnici</h1>
                </div>

                <div class="tagoviwiki">
                    <ul>
                        <asp:DataList ID="dl_Korisnici" runat="server" >
                            <ItemTemplate>
                                <li>
                                    <asp:ImageButton ID="img_User" runat="server" PostBackUrl='<%#"/Public/QA_Pitanja.aspx?KorisnikID="+ Eval("id") %>' ImageUrl='<%#Eval("SlikaURL") %>' Height="16px" Width="16px" CssClass="pitanjadugmadi" />
                                   <%-- <asp:Image ID="img_User" runat="server" ImageUrl='<%#Eval("SlikaURL") %>' Height="16px" Width="16px" CssClass="pitanjadugmadi" />--%>
                                     <asp:Label ID="lbl_User" runat="server" Text='<%#Eval("Nadimak") %>' CssClass="pitanjadugmadi"></asp:Label>
                                     <asp:Label ID="Label3" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                                     <asp:Label ID="lbl_Reputacija" runat="server"  Text='<%#Eval("Reputacija") %>' CssClass="pitanjadugmadi" />  
                                </li>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>

                </div>
        </div>

        <div class="lijevo2">

       <div class="searchinfo2">
                <div class="flag">
                </div>

                <ul>
                    <li class="prva">
                        <asp:Button ID="btn_Clanak" runat="server" Text="Članak" CssClass="nav2dugmadi" OnClick="btn_Clanak_Click" /></li>
                    <li>
                        <asp:Button ID="btn_Razgovor" runat="server" Text="Razgovor" CssClass="nav2dugmadi" OnClick="btn_Razgovor_Click" /></li>
                    <li>
                        <asp:Button ID="btn_Citaj" runat="server" Text="Čitaj" CssClass="nav2dugmadi" OnClick="btn_Citaj_Click" /></li>
                    <li>
                        <asp:Button ID="btn_VidiIzvornik" runat="server" Text="Uredi" CssClass="nav2dugmadi" OnClick="btn_VidiIzvornik_Click" /></li>
                    <li class="zadnja">
                        <asp:Button ID="btn_VidiIzmjene" runat="server" Text="Vidi izmjene" CssClass="nav2dugmadi" OnClick="btn_VidiIzmjene_Click" /></li>
                </ul>


            </div>

 

    <div >

        <%--KONTROLA RAZGOVORI--%>
        <rb:RazgovoriBox runat="server" ID="RazgovoriBox1" />
 
    </div>


        </div>

    </div>








































        
   
</asp:Content>
