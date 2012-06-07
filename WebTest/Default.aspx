<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebTest.Default" %>

<%@ Register Assembly="Zyrenth Web" Namespace="Zyrenth.Web" TagPrefix="zyrenth" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<zyrenth:ColorSwatch ID="ColorSwatch1" runat="server" Color="#FF66FF" Text="Blue">
		</zyrenth:ColorSwatch>
		<zyrenth:ColorSwatch ID="ColorSwatch2" runat="server" Color="#99FF66" Text="Green">
		</zyrenth:ColorSwatch>
		<zyrenth:ColorSwatch ID="ColorSwatch3" runat="server" Color="#FF33CC" 
			Text="Waffle" SecondaryColors-Capacity="4">
		</zyrenth:ColorSwatch>
		<zyrenth:ColorSwatch ID="ColorSwatch4" runat="server" Color="#00CCFF" Text="Red">
		</zyrenth:ColorSwatch>
    </div>
    </form>
</body>
</html>
