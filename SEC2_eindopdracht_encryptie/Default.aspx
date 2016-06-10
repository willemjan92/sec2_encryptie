<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SEC2_eindopdracht_encryptie.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Security 2 - Encryptie</title>
    <style>
        td
        {
            vertical-align: top
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblTitle" runat="server" Text="Security 2 - Encryptie" Font-Size="XX-Large"></asp:Label>
        <table>
            <tr>
               <td><asp:Label ID="lbl_Naam" runat="server" Text="Naam: "></asp:Label></td>
                <td><asp:TextBox ID="txt_Naam" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
               <td><asp:Label ID="lbl_Tekst" runat="server" Text="Geheime Tekst:"></asp:Label></td>
                <td><asp:TextBox ID="txt_Tekst" runat="server" Height="150px" Width="350px" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
               <td><asp:Label ID="lbl_Wachtwoord" runat="server" Text="Wachtwoord:"></asp:Label></td>
                <td><asp:TextBox ID="txt_Wachtwoord" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_Verstuur" runat="server" Text="Verstuur!"  OnClick="btn_Verstuur_Click" /></td>
                <td>
                    <asp:Label ID="lbl_Encrypted" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:DropDownList ID="drp_EncryptedMessages" runat="server" Width="150px"></asp:DropDownList></td>
                <td><asp:Button ID="btn_Lees" runat="server" Text="Lees Bericht" OnClick="btn_Lees_Click" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lbl_Decrypted" runat="server" Text=""></asp:Label></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
