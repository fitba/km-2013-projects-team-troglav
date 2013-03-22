<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Public.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

       <div id="traka">
            <div class="omotactraka">
                 <div class="logiranje1">

                  <a href="RegistrationForm.aspx">Registriracija novog korisnika</a>

                </div>
            </div>
           
       </div>

    <div id="login">
    
        <asp:Label ID="Label1" runat="server" Text="Korisničko ime:"></asp:Label>
        <asp:TextBox ID="txt_KorisnickoIme" runat="server">blocky</asp:TextBox>
         
       
        <asp:Label ID="Label2" runat="server" Text="Lozinka:"></asp:Label>
        <asp:TextBox ID="txt_Lozinka" runat="server" TextMode="Password">test</asp:TextBox>
         
        
        <asp:Button ID="btn_Logiranje" runat="server" Text="Login"  OnClick="btn_Logiranje_Click" />
    
    </div>
    </form>
</body>
</html>
