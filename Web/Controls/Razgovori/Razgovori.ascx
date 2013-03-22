<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Razgovori.ascx.cs" Inherits="Web.Controls.Razgovori.Razgovori" %>

<script src="/Scripts/tinymce/tiny_mce.js"></script>
    <script src="/Scripts/tinymce/tiny_mce_init.js"></script>
    <script src="/Scripts/tinymce/plugins/save/editor_plugin.js"></script>

<div class="dodajkoment">
    <%--<asp:Image style="float:left" ID="img_LogiraniKorisnik" runat="server" Height="48px" Width="48px" BorderColor="Gray" BorderStyle="Solid" />--%>
    <asp:TextBox ID="txt_Sadrzaj" runat="server" TextMode="MultiLine" Width="100%" Height="180px"  CssClass="mceEditor" EnableViewState="True" AutoPostBack="True"></asp:TextBox>
     
    <div class="dodajkomentdugmeokvir">
    <asp:Button ID="btn_SaveKomentar" CssClass="dodajkomentdugme" runat="server" Text="Spremi razgovor"  Width="154px" Height="27px" OnClick="btn_SaveKomentar_Click" />
    </div>

</div>
<div>
   
    <asp:Repeater ID="rpt_KomentariNaPost" runat="server" DataSourceID="SqlRazKor">
        <ItemTemplate>
             <div class ="komentari">
                 <div class="komentarislika">
                <asp:Image ID="img_LogiraniKorisnik" ImageUrl='<%# Eval("SlikaURL") %>' runat="server" Height="70px" Width="70px" />
                 </div>


                <div class="komentariinfo">
                    <p class="komentarimargin">
                    <asp:Label ID="lbl_OKorisniku" runat="server" >
                         <%# Eval("Nadimak") %>  | Reputacija<%# Eval("Reputacija") %> | Sviđanja<%# Eval("Likes") %> | Nesviđanja<%# Eval("Unlikes") %>
                    </asp:Label>

                    </p>
                </div>

                <div class="komentarisadrzaj">
                    <%# Eval("Sadrzaj") %>
                    < 
                    
                </div>
            


</div>
        </ItemTemplate>
    </asp:Repeater>
</div>
 
<asp:SqlDataSource runat="server" ID="SqlRazKor" ConnectionString='<%$ ConnectionStrings:TriglavConnectionString %>' SelectCommand="SELECT Komentari.id, Komentari.Sadrzaj, Komentari.DatumKreiranja, Komentari.Likes, Komentari.Unlikes, Komentari.PostID, Komentari.KorisnikID, Post.id AS postId, Korisnik.Nadimak, Korisnik.Reputacija, Korisnik.Pregleda, Korisnik.Likes AS Expr1, Korisnik.Unlikes AS Expr2, Korisnik.SlikaURL, Komentari.isRazgovor FROM Komentari INNER JOIN Post ON Komentari.PostID = Post.id INNER JOIN Korisnik ON Komentari.KorisnikID = Korisnik.id WHERE (Komentari.PostID = @PostId) AND (Komentari.isRazgovor = 1) ORDER BY Komentari.DatumKreiranja DESC">
    <SelectParameters>
        <asp:QueryStringParameter DefaultValue="" Name="PostId" QueryStringField="PostId" />
    </SelectParameters>
</asp:SqlDataSource>



