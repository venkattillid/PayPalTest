<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="API.aspx.cs" Inherits="Paypaltillidtest.API" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <form id="form2" runat="server">
    <div>
    
        <br />
        Start Express Checkout Process:<br />
        <br />
        <asp:Button ID="oneTimeButton" runat="server" Text="One Time Payment" 
            Width="200px" onclick="oneTimeButton_Click"  />
    </div>
    
    </form>
    </div>
    </form>
</body>
</html>
