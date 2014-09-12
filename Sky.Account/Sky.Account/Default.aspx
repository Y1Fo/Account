<%@ Page Language="C#" Inherits="Sky.Account.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
<head runat="server">
	<title>Default</title>
</head>
<body>
	<form id="form1" runat="server">
		<asp:TextBox ID="txtUserName" runat="server" Width="200px" Text="Hello---"></asp:TextBox><br/>
        <asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox><br/>

        <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click"/>
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click"/>
        <asp:Button ID="btnList" runat="server" Text="List" OnClick="btnList_Click"/>
        <!---<asp:Button ID="btnAddCoin" runat="server" Text="AddCoin" OnClick="btnAddCoin_Click"/>
        <asp:Button ID="btnReduceCoin" runat="server" Text="ReduceCoin" OnClick="btnReduceCoin_Click"/>-->
	</form>
</body>
</html>
