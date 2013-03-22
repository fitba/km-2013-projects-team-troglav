<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationForm.aspx.cs" Inherits="Web.RegistrationForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     
    <form id="form1" runat="server">
      

        <a href="Login.aspx" style="font-size: small">< Povratak na logiranje</a>
        <br />
        <hr />


        <h3>Registracija korisnika</h3>

        <div style="margin: 20px 20px 20px 20px; text-align: left">


            <asp:Label runat="server" Text="Korisnicko ime" ID="lbl_KorisnickoIme" Width="120px"></asp:Label>
            <asp:TextBox ID="txt_KorisnickoIme" runat="server" Width="200px" ToolTip="Upišite vaše korisničko ime"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_KorisnickoIme" runat="server" ControlToValidate="txt_KorisnickoIme" ErrorMessage="Molimo Vas unesite korisničko ime" ForeColor="#0099CC" Display="None"></asp:RequiredFieldValidator>
            <br />
            <br />

            <asp:Label runat="server" Text="Lozinka" ID="lbl_Lozinka" Width="120px"></asp:Label>
            <asp:TextBox ID="txt_Lozinka" runat="server" Width="200px" TextMode="Password" ToolTip="Upišite vasu lozinku"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_Lozinka" runat="server" ControlToValidate="txt_Lozinka" ErrorMessage="Molimo Vas unesite lozinku" ForeColor="#0099CC" Display="None"></asp:RequiredFieldValidator>
            <br />
            <br />

            <asp:Label runat="server" Text="Broj godina" ID="lbl_BrojGodina" Width="120px"></asp:Label>
            <asp:TextBox ID="txt_BrojGodina" runat="server" Width="200px" ToolTip="Upišite koliko imate godina"></asp:TextBox>
            <asp:RangeValidator ID="rv_BrojGodina" runat="server" ControlToValidate="txt_BrojGodina" ErrorMessage="Da li ste punoljetni" ForeColor="#0099CC" MaximumValue="99" MinimumValue="14" Display="None"></asp:RangeValidator>
            <br />
            <br />

            <asp:Image ID="img_Foto" runat="server" Height="126px" ImageUrl="~/Content/Users_Photo/DefaultUser.jpg" Width="125px" />
            <br />
            <br />
            <asp:Label runat="server" Text="Fotografija" ID="lbl_Foto" Width="120px"></asp:Label>
            <br />
            <asp:FileUpload ID="FuFoto" runat="server" />
            <asp:Button ID="btn_uploadFoto" runat="server" Text="Upload" Style="margin-left: 48px" OnClick="btn_uploadFoto_Click" CausesValidation ="false"/>

            <br />
            <br />
            <asp:Label runat="server" Text="O meni" ID="lbl_OMeni" Width="120px"></asp:Label>
            <asp:RequiredFieldValidator ID="rfv_OMeni" runat="server" ControlToValidate="txt_OMeni" ErrorMessage="Molimo vas napišite kratku biografiju" ForeColor="#0099CC" Display="None"></asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txt_OMeni" runat="server" TextMode="MultiLine" Height="250px" Width="500px" ToolTip="Napišite nešto o sebi"></asp:TextBox>
            <br />
            <br />
            <br />            
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidateRequestMode="Inherit" />
             

            <asp:Button ID="btn_SaveKorisnik" runat="server" Text="Registriraj korisnika" Style="margin-left: 174px" OnClick="btn_SaveKorisnik_Click" />


        </div>
          
    </form>
</body>
</html>
