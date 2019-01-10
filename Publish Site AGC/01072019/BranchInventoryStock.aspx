<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="BranchInventoryStock.aspx.cs" Inherits="AGC.BranchInventoryStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <style type="text/css">
        .disabledLink {
                color: #800000 !important;
              
            }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">


    <asp:UpdatePanel runat="server" ID="upMain" UpdateMode="Conditional">
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


            <div class="card">
                <div class="card-header">
                                             <h5><span class="fas fa-clipboard-list text-warning"></span> Branch Stock</h5>
                                        
                     </div>
                <div class="card-body">
                  
                    <div class="row">
                     
                        <div class="col-md-12">
                            <div class="card">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-md-4"> 
                                            <%--<asp:Label runat="server" ID="lblBranchNameStock"></asp:Label> --%>
                                         <asp:DropDownList runat="server" ID="ddBranch" CssClass="dropdown form-control" OnSelectedIndexChanged="ddBranch_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList> 
                                        </div>
                                    </div>
                                         
                                    </div>
                                   </div>
                                <div class="card-body">
                                    
                                    <asp:GridView runat="server" ID="gvItems" CssClass="table table-hover table-sm table-responsive-md" GridLines="Horizontal" AutoGenerateColumns="false">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item" />
                                            <asp:BoundField DataField="Floating" HeaderText="On Delivery" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-primary text-center" ItemStyle-Font-Bold="true" />

                                            <asp:BoundField DataField="tStockIN" HeaderText="Delivered" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Font-Bold="true" />
                                            <asp:BoundField DataField="tStockReturn" HeaderText="Return" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />

                                            <asp:BoundField DataField="tStockAdjustmentIn" HeaderText="Adj(IN)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="tStockAdjustmentOut" HeaderText="Adj(Out)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="tStockTransferIn" HeaderText="Trans(IN)"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"/>
                                            <asp:BoundField DataField="tStockTransferOut" HeaderText="Trans(Out)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"  />
                                            <asp:BoundField DataField="tStockOUT" HeaderText="Sales" ItemStyle-CssClass="text-center text-success" HeaderStyle-CssClass="text-center" ItemStyle-Font-Bold="true" />
                                            <asp:BoundField DataField="tStockLevelWarning" HeaderText="Min Stock" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Font-Bold="true" />
                                            <asp:BoundField DataField="tStockBalance" HeaderText="SOH" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center text-danger" ItemStyle-Font-Bold="true" />
                                          

                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                   <asp:LinkButton runat="server" ID="lnkView" CssClass="btn btn-sm btn-outline-warning text-center" CommandName="View"><span class="fas fa-file-alt" data-toggle="tooltip" data-placement="top" title="View Item Transaction History"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>


                    </div>


                  


                </div>
            </div>

             <div class="modal fade bd-example-modal-lg" id="modalError" tabindex="-1" role="dialog" aria-labelledby="modalSuccessLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                  <div class="modal-header bg-danger">
                    <h5 class="modal-title" id="modalErrorLabel"><span class="fas fa-envelope text-warning"></span> Aces of Grace Corporation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body">
                   <h5><b><span class="fas fa-exclamation-circle text-danger"></span></b> <asp:Label runat="server" ID="lblErrorMessage"></asp:Label></h5>
       
                  </div><!-- End of Modal -->
                  <div class="modal-footer">
       
                        <asp:LinkButton runat="server" ID="lnkCancel" CssClass="btn btn-dark" Text="Close"  data-dismiss="modal"></asp:LinkButton>
        
                  </div>

              
                                   <!-- Modal to print -->
                    <div class="modal fade bd-example-modal-sm" id="modalPrint2" tabindex="-1" role="dialog" aria-labelledby="modalPrintLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-header bg-success">
                                    <h5 class="modal-title"><span class="fas fa-envelope text-warning"></span>Aces of Grace Corporation</h5>

                                </div>
                                <div class="modal-body">

                                    <h5>Branch Delivery successfully saved.</h5>
                                    <h6><b><span class="fas fa-print text-success"></span></b>Do you want to print Branch Delivery Receipt Now?</h6>
                                </div>
                                <!-- End of Modal -->
                                <div class="modal-footer">
                                    <asp:LinkButton runat="server" ID="lnkPrint" CssClass="btn btn-danger" Text="Print" OnClick="lnkPrint_Click"></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkClose" CssClass="btn btn-dark" Text="Close" data-dismiss="modal"></asp:LinkButton>

                                </div>
    </div>
  </div>
</div>
          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
