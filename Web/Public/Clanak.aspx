<%@ Page Title="" Language="C#" MasterPageFile="~/Public/WikiTriglav.Master" AutoEventWireup="true" CodeBehind="Clanak.aspx.cs" Inherits="Web.Public.Clanak" %>

<%@ Register Src="~/Controls/Komentari/Komentari.ascx" TagName="KomentariBox" TagPrefix="kc" %>

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

        <div class="lijevo">
            <div class="searchinfo2">
                <div class="flag">
                </div>

                <ul>
                    <li class="prva">
                        <asp:Button ID="btn_GlavnaStranica" runat="server" Text="Članak" CssClass="nav2dugmadi" OnClick="btn_GlavnaStranica_Click" /></li>
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

            <div class="kontenjer">

                <div class="clanakfull">


                    <div class="clanakfullinfo">
                        <asp:Label ID="lbl_KorisnikNadimak" runat="server" CssClass="clanakfulldugmadi"></asp:Label>

                        <asp:Image ID="img_Korisnik" runat="server" CssClass="clanakfulldugmadi" />
                        <asp:Label ID="Label2" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                        <asp:Literal ID="lbl_Reputacija" runat="server" ></asp:Literal>
                        <asp:Label ID="Label1" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                        <asp:Label ID="lbl_Kreirano" runat="server" CssClass="clanakfulldugmadi"></asp:Label>
                        <asp:Label ID="Label3" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                        <asp:Label ID="lbl_BrojPregleda" runat="server" CssClass="clanakfulldugmadi"></asp:Label>
                        <asp:Label ID="Label4" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                        <asp:Label ID="lbl_BrojPrihvacenihodgovora" runat="server" CssClass="clanakfulldugmadi"></asp:Label>
                        <asp:Label ID="Label5" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                        <asp:Label ID="lbl_BrojPoena" runat="server" CssClass="clanakfulldugmadi"></asp:Label>
                        <asp:Label ID="Label6" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                        <asp:Label ID="lbl_BrojKomentara" runat="server" CssClass="clanakfulldugmadi"></asp:Label>
                         

                    </div>
                    <div class="clanakfullnaslov">

                        <asp:Label ID="lbl_Naslov" runat="server"></asp:Label>
                    </div>


                    <div class="clanakfullsadrzaj">
                        <div class="clanakfullslika">
                            <asp:ImageButton ID="img_Clanak_Photo" runat="server" Width="300px" PostBackUrl="~/Public/Clanak.aspx?PostID=" />

                        </div>




                        <asp:Literal ID="lit_Sazetak" runat="server"></asp:Literal>

                        <asp:Literal ID="lit_Sadrzaj" runat="server"></asp:Literal>
                    </div>







                    <div>


<%--                        <br />
                        <asp:Literal ID="lbl_biografija" runat="server"></asp:Literal>
                        <br />--%>

                    </div>



                    
 





                </div>

            </div>



        </div>
        <div class="lijevo2">
             
                    <div class="ocjene2">


                    <div class="likes">
                        <div class="likes1">
                    <asp:ImageButton ID="btn_Like" CssClass="dodajkomentdugme" runat="server" Text="Up" Width="50px" OnClick="btn_Like_Click"  Height="28px" ImageUrl="~/Content/Buttons/Vote_Up_Arrow.png" /> 
                    <asp:Label ID="lbl_Likes" runat="server"></asp:Label>
                    <br />
                     
                     
                     
                    <asp:ImageButton ID="btn_Unlike" CssClass="dodajkomentdugme" runat="server" Text="Down" OnClick="btn_Unlike_Click" Width="50px" Height="24px" ImageUrl="~/Content/Buttons/Vote_Down_Arrow.png" /> 
                    <asp:Label ID="lbl_Unlikes" runat="server"></asp:Label>
                            </div>
     
                    

                     <div class="likesbodovi">
                        <p><asp:Label ID="lbl_VotesScore" runat="server"></asp:Label></p>
                          
                        <h3><asp:Label ID="lbl_bodovi" runat="server" Text="Label">Bodova</asp:Label></h3>
                    </div>
                        </div>
                    
                    <div class="stars"> 
                        <div class="zvezdice">                
                    <asp:Button ID="btn_RateThis_01" CssClass="dodajkomentdugme" runat="server" OnClick="btn_RateThis_01_Click" Text="1" Width="40px" Height="40px" />
                    <asp:Button ID="btn_RateThis_02" CssClass="dodajkomentdugme" runat="server" OnClick="btn_RateThis_02_Click" Text="2" Width="40px" Height="40px" />
                    <asp:Button ID="btn_RateThis_03" CssClass="dodajkomentdugme" runat="server" OnClick="btn_RateThis_03_Click" Text="3" Width="40px" Height="40px" />
                    <asp:Button ID="btn_RateThis_04" CssClass="dodajkomentdugme" runat="server" OnClick="btn_RateThis_04_Click" Text="4" Width="40px" Height="40px" />
                    <asp:Button ID="btn_RateThis_05" CssClass="dodajkomentdugme" runat="server" OnClick="btn_RateThis_05_Click" Text="5" Width="40px" Height="40px" />
                     
                       </div>
                           
                        <p><asp:Label ID="lbl_RateThis" runat="server">Rate </asp:Label> </p>
                     
                    </div>
                    </div>
                 
        </div>

        <div class="lijevo2">


            <div class="komentaribox" >
                <kc:KomentariBox runat="server" ID="KomentariBox" />
            </div>

        </div>
        </div>
</asp:Content>
