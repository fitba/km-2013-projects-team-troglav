<%@ Page Title="" Language="C#" MasterPageFile="~/Public/QA.Master" AutoEventWireup="true" CodeBehind="QA_Home.aspx.cs" Inherits="Web.Public.QA_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="glavna">
        <div class="desno">                        

            <%--DATALIST KATEGORIJE--%>
            <div class="kategorije2">
                <div class="naslov">
                    <h1>Oblasti</h1>
                </div>

            <div class="oblastiteme">
               
                <ul>
                    <asp:DataList ID="dl_Kategorije" runat="server" OnItemDataBound="dl_Kategorije_ItemDataBound">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="lb_NazivKategorije" runat="server"  ><%# Eval("Naslov") %></asp:LinkButton>
                                <asp:LinkButton ID="lb_BrojClanaka" runat="server" Font-Size="XX-Small" ForeColor="#33CCCC"></asp:LinkButton>
                                <asp:LinkButton ID="lb_BrojPitanja" runat="server" Font-Size="XX-Small" ForeColor="#669900"></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:DataList>
                </ul>
                </div>
                
                <div class="naslov">
                    <h1>Teme</h1>
                </div>
                <%--DATALIST PODKATEGORIJE--%>
                 <div class="oblastiteme">
                 
                <ul>
                <asp:DataList ID="dl_Podkategorije" runat="server" OnItemDataBound="dl_Podkategorije_ItemDataBound">
                    <ItemTemplate>
                      <li>
                        <asp:LinkButton ID="lb_NazivPodKategorije" runat="server" ><%# Eval("Naslov") %></asp:LinkButton>
                        <asp:LinkButton ID="lb_BrojClanakaP" runat="server" Font-Size="XX-Small" ForeColor="#33CCCC"></asp:LinkButton>
                        <asp:LinkButton ID="lb_BrojPitanjaP" runat="server" Font-Size="XX-Small" ForeColor="#669900"></asp:LinkButton>
                        </li>
                    </ItemTemplate>
                     
                </asp:DataList>
                </ul>
                </div> 
                
                <div class="naslovqahome">
                    <h1>Tagovi Q&A</h1>
                </div>


                <div class="tagoviqahome">
                    <ul>
                        <asp:DataList ID="rpt_TagoviQA" runat="server" OnItemDataBound="rpt_TagoviQA_ItemDataBound">

                            <ItemTemplate>
                                <li>
                                    <div class="tagobruc">
                                        <asp:LinkButton ID="lb_TagNaziv" runat="server" PostBackUrl='<%# "~/Public/QA_Pitanja.aspx?TagID=" + Eval("id") %>'>  <%#Eval("Naziv") %> </asp:LinkButton>
                                    </div>
                                    <div class="brojtagovani">
                                        <asp:Label ID="lbl_BrojTagovanihPostova" runat="server"></asp:Label>
                                    </div>

                                    <%--<asp:Literal ID="lit_TagOpis" runat="server" Text='<%#Eval("Opis") %>'>    </asp:Literal> --%>
                                </li>
                            </ItemTemplate>

                        </asp:DataList>
                    </ul>
                </div>

                <div class="naslov">
                    <h1>Tagovi Wiki</h1>
                </div>

                <div class="tagoviwiki">
                    <ul>
                        <asp:DataList ID="rpt_Tagovi" runat="server" OnItemDataBound="rpt_Tagovi_ItemDataBound">
                            <ItemTemplate>
                                <li>
                                    <div class="tagobruc">
                                        <asp:LinkButton ID="lb_TagNaziv" runat="server" PostBackUrl='<%# "~/Public/Home.aspx?TagID=" + Eval("id") %>'>  <%#Eval("Naziv") %> </asp:LinkButton>
                                    </div>
                                    <div class="brojtagovani">
                                        <asp:Label ID="lbl_BrojTagovanihPostova" runat="server"></asp:Label>
                                    </div>
                                    <%-- <asp:Literal ID="lit_TagOpis" runat="server" Text='<%#Eval("Opis") %>'>    </asp:Literal> --%>
                                </li>
                            </ItemTemplate>
                        </asp:DataList>
                    </ul>

                </div>               




            </div>




        </div>

        <div class="searchinfo">
            <p>
                <asp:Label ID="lbl_NaslovStranice" runat="server" Font-Bold="True"></asp:Label>
            </p>
        </div>

        <div class="lijevo">


            <div class="searchinfo2">
                <div class="flag">
                </div>

                <ul>
                    <li class="prva">
                        <asp:Button ID="btn_Posljednje" runat="server" Text="Posljednje" OnClick="btn_Posljednje_Click" CssClass="nav2dugmadi" /></li>
                    <li>
                        <asp:Button ID="btn_Istaknuti" runat="server" Text="Istaknuto" OnClick="btn_Istaknuti_Click" CssClass="nav2dugmadi" /></li>
                    <li>
                        <asp:Button ID="btn_Hot" runat="server" Text="Hot" OnClick="btn_Hot_Click" CssClass="nav2dugmadi" /></li>
                    <li>
                        <asp:Button ID="btn_OveSedmice" runat="server" Text="Ove sedmice" OnClick="btn_OveSedmice_Click" CssClass="nav2dugmadi" /></li>
                    <li class="zadnja">
                        <asp:Button ID="btn_OvogMjeseca" runat="server" Text="Ovog mjeseca" OnClick="btn_OvogMjeseca_Click" CssClass="nav2dugmadi" /></li>
                </ul>


            </div>

            <div class="kontenjer">

                <%--DATALIST PITANJA--%>
                <%--PREPORUKA PITANJA--%>
                <%--<asp:DataList ID="dl_Pitanja" runat="server" OnItemDataBound="dl_Pitanja_ItemDataBound" width="100%" GridLines="Vertical">--%>
                <asp:DataList ID="dl_Pitanja" runat="server" OnItemDataBound="dl_Clanci_ItemDataBound" Width="100%" GridLines="Vertical">


                    <ItemTemplate>
                        <div class="questions">
                            <div class="statistikaobruc">


                                <div class="qstat">
                                    <h2>
                                        <asp:Label ID="lbl_BrojGlasova" runat="server" Text='<%#Eval("BrojPoena") %>'></asp:Label></h2>
                                    <p>Glasovi</p>
                                </div>

                                <div class="qstat">
                                    <h2>
                                        <asp:Label ID="lbl_BrojOdgovora" runat="server" Text='<%#Eval("BrojOdgovora") %>'></asp:Label></h2>
                                    <p>Odgovora</p>
                                </div>

                                <div class="qstat">
                                    <h2>
                                        <asp:Label ID="lbl_BrojPregleda" runat="server" Text='<%#Eval("BrojPregleda") %>'></asp:Label></h2>
                                    <p>Pregleda</p>
                                </div>


                            </div>

                            <div class="pitanjeobruc">

                                <div class="naslovpitanja">

                                    <p>
                                        <asp:LinkButton ID="txt_Naslov" runat="server" PostBackUrl='<%# "~/Public/QA_Pitanje.aspx?PostID=" + Eval("id") %>'> <%#Eval("Naslov") %></asp:LinkButton>
                                    </p>
                                </div>

                                <div class="pitanjainfo">
                                    <div class="oblast">
                                        <asp:LinkButton ID="lb_oKategorija" runat="server" CssClass="pitanjadugmadi">LinkButton</asp:LinkButton>
                                        <asp:Label ID="Label1" runat="server" ForeColor="gray" Text="Label">></asp:Label>
                                        <asp:LinkButton ID="lb_oPodKategorija" runat="server" CssClass="pitanjadugmadi">LinkButton</asp:LinkButton>
                                    </div>
                                    <div class="rep">

                                        <abbr class="timeago" title="<%# DataBinder.Eval(Container.DataItem, "DatumZadnjeIzmjene", "{0:yyyy-M-dThh:mm:ss}") %>">nije uspjelo</abbr>
                                        <asp:Label ID="Label2" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                                        <asp:Image ID="img_User" runat="server" Height="16px" Width="16px" CssClass="pitanjadugmadi" />
                                        <asp:Label ID="lbl_User" runat="server" Text='<%#Eval("VlasnikNadimak") %>' CssClass="pitanjadugmadi"></asp:Label>
                                        <asp:Label ID="Label3" runat="server" ForeColor="gray" Text="Label">|</asp:Label>
                                        <asp:Label ID="lbl_Reputacija" runat="server" CssClass="pitanjadugmadi" />

                                    </div>
                                </div>

                                <%--  HIGHLIGHTER PITANJA--%>
                                <div class="sadrzajpitanja">
                                    <span class="sample"><%# DataBinder.Eval(Container.DataItem, "Sadrzaj") %></span>
                                </div>

                                <%--DATALIST TAGOVI CLANKA--%>

                                <asp:DataList ID="dl_Tagovi" runat="server" CaptionAlign="NotSet" AlternatingItemStyle-HorizontalAlign="Center" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <div class="tagqahome">
                                            <asp:LinkButton ID="txt_tag" runat="server" Font-Underline="False" PostBackUrl='<%# "~/Public/QA_Home.aspx?TagID=" + Eval("id") %>' Font-Bold="True"><%#Eval("Naziv") %></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>



                            </div>
                    </ItemTemplate>

                </asp:DataList>


            </div>


        </div>


    </div>
    <%--GLAVNA KRAJ--%>
</asp:Content>
