<%@ Page Title="" Language="C#" MasterPageFile="~/Public/QA.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="QA_Pitanje.aspx.cs" Inherits="Web.Public.QA_Pitanje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--UKLJUČIVANJE tinyMCE--%>
    <script src="/Scripts/tinymce/tiny_mce.js"></script>
    <script src="/Scripts/tinymce/tiny_mce_init.js"></script>
    <script src="/Scripts/tinymce/plugins/save/editor_plugin.js"></script>

    <style type="text/css">
        .mceEditor {
        }
    </style>

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
             <div class="kategorije2">
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
                                      <asp:Label ID="lbl_User" runat="server" Text='<%#Eval("Nadimak") %>' CssClass="pitanjadugmadi"></asp:Label>
                                     <asp:Label ID="Label3" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                                     <asp:Label ID="lbl_Reputacija" runat="server"  Text='<%#Eval("Reputacija") %>' CssClass="pitanjadugmadi" />  
                                </li>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>

                </div>
             </div>

        </div>


        <div class="lijevo">

            <div class="kontenjer">

                <div class="clanakfull">
                    <div class="clanakfullinfo">
                     <asp:LinkButton ID="lb_oKategorija" runat="server" CssClass="clanakfulldugmadi">LinkButton</asp:LinkButton>
                         <asp:Label ID="Label6" runat="server" ForeColor="gray" Text="Label">></asp:Label>
                    <asp:LinkButton ID="lb_oPodKategorija" runat="server" CssClass="clanakfulldugmadi">LinkButton</asp:LinkButton>  
                         <asp:Label ID="Label1" runat="server" ForeColor="gray" Text="Label">|</asp:Label>                     
                        <asp:Label ID="lbl_BrojPrihvacenihodgovoratext" runat="server" Text="odgovori" CssClass="clanakfulldugmadi"  ></asp:Label>
                        <asp:Label ID="lbl_BrojPrihvacenihodgovora" runat="server" CssClass="clanakfulldugmadi"  ></asp:Label> 
                         <asp:Label ID="Label2" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                        <asp:Label ID="lbl_BrojPregledaText" runat="server" Text="Pregledi" CssClass="clanakfulldugmadi" ></asp:Label>
                        <asp:Label ID="lbl_BrojPregleda" runat="server" CssClass="clanakfulldugmadi" ></asp:Label> 
                         <asp:Label ID="Label3" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                         <asp:Label ID="lbl_RejtingText" runat="server" Text="rejting" CssClass="clanakfulldugmadi"  ></asp:Label>
                        <asp:Label ID="lbl_Rejting" runat="server" CssClass="clanakfulldugmadi" ></asp:Label> 
                         <asp:Label ID="Label4" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                       
                        <asp:Label ID="lbl_Kreirano" runat="server" CssClass="clanakfulldugmadi" ></asp:Label>

                    </div>

                </div>

                <div class="clanakfullnaslov">
                    <asp:Label ID="lbl_Naslov" runat="server" ></asp:Label>

                </div>

                <div class="clanakfullsadrzaj">
                    <asp:Literal ID="lit_Sadrzaj" runat="server" ></asp:Literal>
                </div>

                

            </div>
       







    </div>

        <div class="lijevo2">
            <div class="ocjene2">
                <div class="likes">
                     
                        <div class="likes1">
                    <asp:ImageButton ID="btn_Like" CssClass="dodajkomentdugme" runat="server" Text="Up" Width="50px" OnClick="btn_Like_Click"  Height="25px" ImageUrl="~/Content/Buttons/Vote_Up_Arrow.png" /> 
                    <asp:Label ID="lbl_Likes"  runat="server"></asp:Label> 
                    <br />
                     
                                    
                    <asp:ImageButton ID="btn_Unlike" CssClass="dodajkomentdugme" runat="server" Text="Down" OnClick="btn_Unlike_Click" Width="50px" Height="23px" ImageUrl="~/Content/Buttons/Vote_Down_Arrow.png" /> 
                    <asp:Label ID="lbl_Unlikes"  runat="server"></asp:Label>
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
                        <p><asp:Label ID="lbl_RateThis" runat="server">Rate </asp:Label></p>
                        <br />
                    <div class="pitanjeautor" >
                        <asp:Image ID="img_Korisnik" runat="server" />
                        <asp:Label ID="lbl_KorisnikNadimak" runat="server" Font-Size="12px"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text="Label">|</asp:Label>
                        <asp:Literal ID="lbl_Reputacija" runat="server"></asp:Literal>
                        <asp:Label ID="Label9" runat="server" Text="Label">|</asp:Label>
                        <asp:Image ID="img_BedzVlsanika" runat="server" Height="25px" Width="25px" />
                        <asp:Label ID="lbl_NazivBedzaVlasnika" runat="server"></asp:Label>
                    </div>
                    </div>


<%--                    <div>
                        <asp:ImageButton ID="btn_Like" runat="server" OnClick="btn_Like_Click" ImageUrl="~/Content/Buttons/Vote_Up_Arrow.png" Height="15px" Width="30px" /><br />
                        <asp:Label ID="lbl_VotesScore" runat="server" Font-Size="X-Large"></asp:Label><br />
                        <asp:ImageButton ID="btn_Unlike" runat="server" OnClick="btn_Unlike_Click" ImageUrl="~/Content/Buttons/Vote_Down_Arrow.png" Height="15px" Width="30px" /><br />
                        <asp:Label ID="lbl_Likes" runat="server" Font-Size="XX-Small"></asp:Label><br />
                        <asp:Label ID="lbl_Unlikes" runat="server" Font-Size="XX-Small"></asp:Label>
                    </div>--%>

                </div>

        </div>

        <div class="lijevo2">
         <div class="unosodg">

         
        <%--NOVI ODGOVOR--%>
        <asp:TextBox ID="txt_oSadrzaj" runat="server" TextMode="MultiLine" CssClass="mceEditor" EnableViewState="True" AutoPostBack="True" Width="100%"></asp:TextBox>
             <div class="dodajkomentdugmeokvir">
        <asp:Button ID="btn_SaveOdgovor" CssClass="dodajkomentdugme" runat="server" Width="154px" Height="27px" OnClick="btn_SaveOdgovor_Click" Text="Objavite odgovor" />
                 </div>
            </div>
        </div>

        <div class="lijevo2">
        <%-- LISTA ODGOVORA--%>
        <asp:DataList ID="dl_odgovori" runat="server" CellPadding="3" GridLines="Vertical" Width="100%" OnItemDataBound="dl_odgovori_ItemDataBound">

            <ItemTemplate>
                <div class="odgovor">
                    <div class="odgovorslika">
                        <asp:Image ID="img_User" runat="server" Height="70px" Width="70px" />
                  <div class="odgovorvotes">
                    <asp:ImageButton ID="btn_oLike" runat="server" OnClick="btn_oLike_Click" ImageUrl="~/Content/Buttons/Vote_Up_Arrow.png" Height="15px" Width="30px" /><br />
                    <asp:Label ID="lbl_BrojPoena" runat="server" Font-Size="X-Large"><%#Eval("BrojPoena")%></asp:Label><br />
                    <asp:ImageButton ID="btn_oUnlike" runat="server" OnClick="btn_oUnlike_Click" ImageUrl="~/Content/Buttons/Vote_Down_Arrow.png" Height="15px" Width="30px" />
                </div>
                    </div>

                    <div class="odgovorinfo">
                    <p class="odgovormargin">
                    <asp:Label ID="lbl_oKorisnikNadimak" runat="server" ><%# Eval("VlasnikNadimak") %></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text="Label">|</asp:Label>
                    <asp:Image ID="img_Bedz" runat="server" Height="20px" Width="20px" />
                    <asp:Label ID="lbl_NazivBedza" runat="server"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Text="Label">|</asp:Label>
                    <asp:Label ID="lbl_Reputacija" runat="server"></asp:Label>
                        <asp:Label ID="Label8" runat="server" Text="Label">|</asp:Label>
                       <abbr class="timeago" title='<%# DataBinder.Eval(Container.DataItem, "DatumZadnjeIzmjene", "{0:yyyy-M-dThh:mm:ss+01:00}")  %>'>nije uspjelo</abbr> 
                        </p>
                    </div>
                


                <div class="odgovorsadrzaj">
                    <asp:HiddenField ID="hf" runat="server" Value='<%#Eval("id")%>' />                    
                    <asp:Literal ID="lit_oSadrzaj" runat="server"  Text='<%# Eval("Sadrzaj") %>'></asp:Literal>                       
                    
                </div>

 
                

                </div>
            </ItemTemplate>
 
        </asp:DataList>

      </div>


         </div>


</asp:Content>
