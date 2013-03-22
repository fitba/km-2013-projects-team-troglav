<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Komentari.ascx.cs" Inherits="Web.Controls.Komentari.Komentari" %>
<div class="dodajkoment">
    <%--<asp:Image Style="float: left" ID="img_LogiraniKorisnik" runat="server" Height="48px" Width="48px" BorderColor="Gray" BorderStyle="Solid" />--%>
    <asp:TextBox ID="txt_Sadrzaj" runat="server" TextMode="MultiLine" Width="99%" Height="100px"   Wrap="true"></asp:TextBox>
     
    <div class="dodajkomentdugmeokvir">
    <asp:Button ID="btn_SaveKomentar" CssClass="dodajkomentdugme" runat="server" Text="Komentiraj" Style="margin-top: 5px" Width="154px" Height="27px" OnClick="btn_SaveKomentar_Click" />
    </div>
 
</div>

<div>

    <asp:Repeater ID="rpt_KomentariNaPost" runat="server" DataSourceID="SqlKomKor">
        <ItemTemplate>
            <div class="komentari">
                             <div class="komentarislika">
                                <asp:Image ID="img_LogiraniKorisnik" ImageUrl='<%# Eval("SlikaURL") %>' runat="server" Height="70px" Width="70px"/>
                            </div>

                <div class="komentariinfo">
                    <p class="komentarimargin">


                          <asp:Label ID="lbl_OKorisniku" runat="server"  >
                         <%# Eval("Nadimak") %>  | Reputacija<%# Eval("Reputacija") %> | Sviđanja<%# Eval("Likes") %> | Nesviđanja<%# Eval("Unlikes") %>
                            </asp:Label>
                        </p>


                    </div>
                          <div class="komentarisadrzaj">
                            
                            <%# Eval("Sadrzaj") %>
                             

                        </div>
                </div>

            </div>

        </ItemTemplate>
    </asp:Repeater>
 </div>
<asp:SqlDataSource runat="server" ID="SqlKomKor" ConnectionString='<%$ ConnectionStrings:TriglavConnectionString %>' SelectCommand="SELECT Komentari.id, Komentari.Sadrzaj, Komentari.DatumKreiranja, Komentari.Likes, Komentari.Unlikes, Komentari.PostID, Komentari.KorisnikID, Post.id AS postId, Korisnik.Nadimak, Korisnik.Reputacija, Korisnik.Pregleda, Korisnik.Likes AS Expr1, Korisnik.Unlikes AS Expr2, Korisnik.SlikaURL, Komentari.isRazgovor FROM Komentari INNER JOIN Post ON Komentari.PostID = Post.id INNER JOIN Korisnik ON Komentari.KorisnikID = Korisnik.id WHERE (Komentari.PostID = @PostId) AND (Komentari.isRazgovor = 0) ORDER BY Komentari.DatumKreiranja DESC">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="" Name="PostId" QueryStringField="PostId" />
    </SelectParameters>
</asp:SqlDataSource>

