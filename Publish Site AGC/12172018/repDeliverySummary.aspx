<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="repDeliverySummary.aspx.cs" Inherits="AGC.repDeliverySummary" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">
    <asp:UpdatePanel runat="server" ID="upMain">
        <ContentTemplate>
                 <script type="text/javascript">
            $(function calendarInput() {    
                $('.calendarInput').datepicker();
            });

            //Search function
            $(function searchInput() {
                $('[id*=txtSearch]').on("keyup", function () {
                    var value = $(this).val().toLowerCase();
                    $('[id*=gvBranchList] tr').filter(function () {
                        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                    });
                });
            });

            //On UpdatePanel Refresh
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                       
                        $('.calendarInput').datepicker();


                        //Search function
                        $(function searchInput() {
                            $('[id*=txtSearch]').on("keyup", function () {
                                var value = $(this).val().toLowerCase();
                                $('[id*=gvBranchList] tr').filter(function () {
                                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                                });
                            });
                        });

                    }
                });
            };
    </script>

<!-- Start of Markup here -->
              <div class="card">
                <div class="card-header bg-info"><h5><span class="fas fa-print text-warning"></span> Delivery Reports</h5></div>
                <div class="card-body">
                    <div class="row">
                        <!--Selection area here -->
                        <div class="col-md-3">
                            <ul class="list-group">
                                <li class="list-group-item">
                                    <div class="input-group">
                                    <div class="input-group-prepend">
                                       <span class="input-group-text">Start Date: 
                                        </span>  
                                    </div>
                                        <asp:TextBox runat="server" ID="txtStartDate" CssClass="form-control calendarInput"></asp:TextBox>
                                        </div>
                                </li>
                                <li class="list-group-item">
                                     <div class="input-group">
                                     <div class="input-group-prepend">
                                       <span class="input-group-text">End Date: 
                                        </span>  
                                    </div>
                                         <asp:TextBox runat="server" ID="txtEndDate" CssClass="form-control calendarInput"></asp:TextBox>
                                         </div>
                                         </li>

                                <li class="list-group-item">
                                    <div class="input-group">
                                        <asp:DropDownList runat="server" ID="ddBranchList" CssClass="form-control"></asp:DropDownList>
                                     <div class="input-group-append">
                                           <asp:LinkButton runat="server" id="lnkPreview" CssClass="btn btn-outline-primary" OnClick="lnkPreview_Click"> Preview</asp:LinkButton>
                                    </div>
                                    
                                        </div>
                                </li>
                            </ul>
                        </div>

                        <!-- Crystal Report Here -->
                        <div class="col-md-9">
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server"
                                AutoDataBind="True"
                                HasCrystalLogo="False"
                                EnableDatabaseLogonPrompt="False"
                                EnableParameterPrompt="False"
                                HasToggleParameterPanelButton="False" ToolPanelView="None" HasToggleGroupTreeButton="false"
                                 />
                        </div>
                    </div>
                </div>
              </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
