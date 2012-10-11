<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebTest.Default" %>

<%@ Register assembly="Zyrenth.Web" namespace="Zyrenth.Web" tagprefix="zyrenth" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/custom-theme/jquery-ui-1.8.23.custom.css" rel="stylesheet" type="text/css" />
	<link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.23.custom.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <zyrenth:ColorSwatch ID="ColorSwatch1" runat="server" Color="#FF66FF" Text="Blue">
        </zyrenth:ColorSwatch>
        <zyrenth:ColorSwatch ID="ColorSwatch2" runat="server" Color="#99FF66" Text="Green">
        </zyrenth:ColorSwatch>
        <zyrenth:ColorSwatch ID="ColorSwatch3" runat="server" Color="#FF33CC" Text="Waffle"
            SecondaryColors-Capacity="4">
        </zyrenth:ColorSwatch>
        <zyrenth:ColorSwatch ID="ColorSwatch4" runat="server" Color="#00CCFF" Text="Red">
        </zyrenth:ColorSwatch>
        <zyrenth:ModalPopup ID="ModalPopup1" runat="server" Title="Modal Test" 
			onbuttonclicked="ModalPopup1_ButtonClicked" Resizable="true" ShowCloseButton="false">
            <Content>
                <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
            </Content>
			<Buttons>
				<zyrenth:ModalPopupButton Text="Delete" CommandName="Delete" CssClass="modalButtonLeft" Icon="trash" />
				<zyrenth:ModalPopupButton Text="Cancel" CommandName="Cancel"  />
				<zyrenth:ModalPopupButton Text="Hidden" CommandName="hdn" Icon="comment" IconOnly="true" />
				<zyrenth:ModalPopupButton Text="OK" CommandName="OK"  />
			</Buttons>
        </zyrenth:ModalPopup>
    </div>
    </form>
</body>
</html>
