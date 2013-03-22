<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LuceneTestSearch.aspx.cs" Inherits="Web.Lucene.LuceneTestSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txt_search" runat="server"></asp:TextBox>
        <asp:Button ID="btn_Search" runat="server" Text="Button" OnClick="btn_Search_Click" />

        <asp:DataList ID="dl_Clanci" runat="server">
            <ItemTemplate>
                <asp:LinkButton ID="txt_Naslov" runat="server" Font-Underline="False" PostBackUrl='<%# "~/Public/Clanak.aspx?PostID=" + Eval("id") %>' > <%#Eval("Naslov") %></asp:LinkButton><br />
                <asp:Label ID="txt_Sazetak" runat="server" ><%#Eval("Sazetak") %></asp:Label>
            </ItemTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
