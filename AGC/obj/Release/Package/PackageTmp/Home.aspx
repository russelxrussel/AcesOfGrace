<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AGC.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">
<h2><b>WELCOME TO ACES OF GRACE CORPORATION COMPUTERIZED SYSTEM!!!</b></h2>
<asp:UpdatePanel runat="server" ID="upMain">
    <ContentTemplate>
     <script type="text/javascript">
            $(function calendarInput() {    
                $('.calendarInput').datepicker();
            });

         
            //On UpdatePanel Refresh
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                       
                        $('.calendarInput').datepicker();

                    }
                });
            };
    </script>
        <div class="card">
            <div class="card-header bg-dark"></div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                        <!--Top Sales inquiry -->
                        <div class="card">
                            <div class="card-header bg-warning"><span class="fas fa-bolt"></span> Top Performing Branch Sales</div>
                            <div class="card-body">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Start Date:</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtStartDate" CssClass="form-control calendarInput"></asp:TextBox>
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">End Date:</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtEndDate" CssClass="form-control calendarInput"></asp:TextBox>
                                     
                                </div>
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">TOP #:</span>
                                    </div>
                                    <asp:TextBox runat="server" ID="txtTopNumber" CssClass="form-control"></asp:TextBox> 
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">Item:</span>
                                    </div>
                                    <asp:DropDownList runat="server" ID="ddItemList" CssClass="form-control"></asp:DropDownList>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" id="lnkPreview" CssClass="btn btn-outline-primary" OnClick="lnkPreview_Click"> Preview</asp:LinkButton>
                                    </div>
                                </div>
                                <hr />
                                <asp:Panel runat="server" ID="panelTopBranch" Height="300px" ScrollBars="Vertical">
                                    <asp:GridView runat="server" ID="gvTopSaleBranch" ShowHeader="true" CssClass="table table-sm table-responsive-md table-hover" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch" />
                                            <asp:BoundField DataField="Qty" HeaderText="Heads" />
                                            <asp:BoundField DataField="TotalPrice" HeaderText="Total Sales" ItemStyle-CssClass="text-right" DataFormatString="{0:N}" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
