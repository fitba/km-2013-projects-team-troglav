<%@ Page Title="" Language="C#" MasterPageFile="~/Public/WikiTriglav.Master" AutoEventWireup="true" CodeBehind="ClanakStareIzmjene.aspx.cs" Inherits="Web.Public.ClanakStareIzmjene" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">         
        function SetSingleRadioButton(nameregex, current) {
            //alert(nameregex);
            var br = 0, index = 0;
            re = new RegExp(nameregex);
           
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    elm = document.forms[0].elements[i];
                    if (elm.type == 'radio' ) {
                        if (elm != current) {
                            elm.checked = false;
                        }
                    }
                }
                current.checked = true;
            } 
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" Visible="False">
    
    <div style="float: left;">
        <asp:Button ID="btn_GlavnaStranica" runat="server" Text="Članak" BorderStyle="Dotted" BorderWidth="2px" Style="margin-left: 40px" Width="120px" OnClick="btn_GlavnaStranica_Click" />
        <asp:Button ID="btn_Razgovor" runat="server" Text="Razgovor" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_Razgovor_Click" />
    </div>
    <div style="text-align: right; margin-right: 40px;">
        <asp:Button ID="btn_Citaj" runat="server" Text="Čitaj" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_Citaj_Click" />
        <asp:Button ID="btn_VidiIzvornik" runat="server" Text="Uredi" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_VidiIzvornik_Click" />
        <asp:Button ID="btn_VidiIzmjene" runat="server" Text="Vidi izmjene" BorderStyle="Dotted" BorderWidth="2px" Width="120px" OnClick="btn_VidiIzmjene_Click" />
    </div>

    <br />
    <br />
     <asp:DataList ID="dl_PrihvaceneIzmjene" runat="server" CellPadding="4" ForeColor="#333333">
           <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
           <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
           <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
           <ItemTemplate>
               <div style="border-bottom-style: solid; border-bottom-width: thin">  
                      <asp:HiddenField ID="hf" runat="server" Value = '<%#Eval("id")%>' /> 
              <asp:LinkButton ID="txt_Naslov" runat="server" Font-Underline="False" Value='<%#Eval("Naslov") %>' OnClick ="txt_Naslov_Click"> <%#Eval("Naslov") %></asp:LinkButton>&nbsp;<asp:Label ID="txt_Datum" runat="server" > <%#Eval("DatumKreiranja") %> </asp:Label>                 
           </ItemTemplate>
           <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:DataList>
    <br />

    Prijedlozi izmjena
    <br />
     <asp:DataList ID="dl_PrijedloziIzmjena" runat="server" OnItemDataBound="dl_PrijedloziIzmjena_ItemDataBound" CellPadding="4" ForeColor="#333333">
           <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
           <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
           <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
           <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
           <ItemTemplate>
               <div style="border-bottom-style: solid; border-bottom-width: thin">                   
              <asp:RadioButton ID="rdbP" Checked="false" GroupName="g2" Text="Select" runat="server" onKeyPress="return suppress(event);" />
              <asp:HiddenField ID="hf" runat="server" Value = '<%#Eval("id")%>' /> 
               <asp:LinkButton ID="txt_Naslov" runat="server" Font-Underline="False"> <%#Eval("Naslov") %></asp:LinkButton>&nbsp;<asp:Label ID="txt_Datum" runat="server" > <%#Eval("DatumKreiranja") %> </asp:Label>                 
           </ItemTemplate>
           <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:DataList>
    <br />

    <asp:Button ID="Btn_Uporedi" runat="server" Text="Uporedi članke" OnClick="Btn_Uporedi_Click" />
    <asp:Button ID="Btn_Odobri" runat="server" Text="OdobriIzmjenu" OnClick="Btn_Odobri_Click" Visible="False" />
    <br />
    zadnji
    <asp:TextBox ID="txt_odobreni" runat="server" TextMode="MultiLine" Height="110px" Width="386px" Visible="False"></asp:TextBox>
    izmjena
    <asp:TextBox ID="txt_izmjena" runat="server" TextMode="MultiLine" Height="107px" Width="434px" Visible="False"></asp:TextBox>



</asp:Content>
