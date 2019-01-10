<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rep_BranchDeliveryReceiptSingle.aspx.cs" Inherits="AGC.rep_BranchDeliveryReceiptSingle" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Aces of Grace Corporation System</title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
      <hr />
      <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" ReportSourceID="CrystalReportSource1" 
            EnableParameterPrompt="False" 
            ToolPanelView="None" HasCrystalLogo="False" HasGotoPageButton="False" HasPageNavigationButtons="False" HasSearchButton="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" />
      
    </div>
    </form>
</body>
</html>