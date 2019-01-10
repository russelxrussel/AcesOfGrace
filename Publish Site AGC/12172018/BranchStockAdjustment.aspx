<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="BranchStockAdjustment.aspx.cs" Inherits="AGC.BranchStockAdjustment" %>

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
                <div class="card-header"><h5><span class="fas fa-expand-arrows-alt text-warning"></span> Stock Adjustment</h5></div>
                <div class="card-body">
                  
                    <div class="row">
                        <div class="col-md-5">
                           
                            <ul class="list-group">
                                <li class="list-group-item">
                                     <div class="input-group">
                                        <asp:TextBox runat="server" ID="txtAdjustmentDate" CssClass="form-control is-invalid calendarInput" placeholder="Adjustment Date"></asp:TextBox>    
                                      
                                              </div>
                                </li>
                                  <li class="list-group-item">
                                     <asp:DropDownList runat="server" ID="ddAdjustmentType" CssClass="form-control is-invalid"></asp:DropDownList>
                                 </li>
                                <li class="list-group-item">
                                    <div class="input-group mb-3">
                                 <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Branch"></asp:TextBox>
                             </div>
                                    <asp:Panel runat="server" ID="panelBranch" Height="300px" ScrollBars="Vertical">
                                    <asp:GridView runat="server" ID="gvBranchList" ShowHeader="false" CssClass="table table-sm table-responsive-md table-hover" GridLines="Horizontal" AutoGenerateColumns="false" OnRowCommand="gvScheduleBranch_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="BranchCode"/>
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch" />

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                 <asp:LinkButton runat="server" ID="lnkInsertQuantity" CssClass="btn btn-sm btn-outline-primary" CommandName="Select"><span class="fas fa-arrow-alt-circle-right" data-toggle="tooltip" data-placement="top" title="Insert Quantity"></span></asp:LinkButton>
                                                 
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                        </Columns>
                                    </asp:GridView>
                                        </asp:Panel>
                                    </li>
                                 <li class="list-group-item">
                                     <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" placeholder="Adjustment Remarks" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                 </li>
                                 </ul>
                        </div>



                        <div class="col-md-7">
                            <div class="card">
                                <div class="card-header">
                                          <div class="row">
                                              <div class="col-md-8">
                                                   <span class="fas fa-store text-warning"></span> <asp:Label runat="server" ID="lblBranchNameStock"></asp:Label>
                                                   <asp:LinkButton runat="server" ID="lnkViewPendingAdjustment" CssClass="btn btn-sm btn-outline-danger" OnClick="lnkViewPendingAdjustment_Click" Visible="false"><span class="fas fa-exclamation-circle" data-toggle="tooltip" data-placement="top" title="View Adjustment Details"></span> PENDING ADJUSTMENT</asp:LinkButton>
                                              </div>
                                              
                                                  <div class="col-md-4 text-right">
                                               
                                                <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-outline-primary btn-sm" OnClick="lnkSave_Click"><span class="fas fa-save"></span> SAVE</asp:LinkButton>
                                             
                                          </div>
                                          </div>
                                          
                                        
                                   
                                   </div>
                                <div class="card-body">
                                    
                                    <asp:GridView runat="server" ID="gvItems" CssClass="table table-hover table-sm table-responsive-md" GridLines="Horizontal" AutoGenerateColumns="false">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item" />

                                            <asp:TemplateField ControlStyle-Width="50%" HeaderText="Adjustment Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtAdjustmentQty" MaxLength="6" CssClass="form-control text-center" TextMode="Number"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:BoundField DataField="tStockBalance" ItemStyle-Font-Bold="true" ItemStyle-CssClass="text-center" HeaderText="Item" />

                                        </Columns>
                                    </asp:GridView>


                                     <hr />
                                  
                                </div>
                            </div>

                        </div>


                        <!-- PENDING ADJUSTMENT -->
                        <div class="modal fade bd-example-modal-lg" id="modalStockAdjustmentForPosting" tabindex="-1" role="dialog" aria-labelledby="modalSuccessLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header bg-info">
                                        <h5 class="modal-title" id="modalViewLabel"><span class="fas fa-envelope text-warning"></span>  Aces of Grace Corporation</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">

                                        <hr />
                                        <h4><b>
                                            <asp:Label runat="server" ID="lblBranchNameAdjustment" CssClass="text-danger"></asp:Label></b></h4>
                                        <asp:GridView runat="server" ID="gvStockAdjustmentForPosting" CssClass="table table-hover table-sm table-responsive-md" GridLines="Horizontal" AutoGenerateColumns="false">
                                            <Columns>

                                                <asp:BoundField DataField="stockAdjustmentNum" />
                                                <asp:BoundField DataField="adjustmentName" HeaderText ="Adjustment Type" />
                                                <asp:BoundField DataField="itemName" HeaderText="Item" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                <asp:BoundField DataField="AdjustmentDate" DataFormatString="{0:d}" />
                                                
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <div class="modal-footer">

                                        <asp:LinkButton runat="server" ID="LinkButton2" CssClass="btn btn-dark" Text="Close" data-dismiss="modal"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End of Modal -->

                </div>


                  


                </div>
            </div>

             <div class="modal fade bd-example-modal-lg" id="modalError" tabindex="-1" role="dialog" aria-labelledby="modalErrorLabel" aria-hidden="true">
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

                    </div>
                    </div>
                 </div>

                 <div class="modal fade bd-example-modal-lg" id="modalSuccess" tabindex="-1" role="dialog" aria-labelledby="modalSuccessLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                  <div class="modal-header bg-success">
                    <h5 class="modal-title" id="modalSuccessLabel"><span class="fas fa-envelope text-warning"></span> Aces of Grace Corporation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body">
                   <h5><b><span class="fas fa- text-danger"></span></b> <asp:Label runat="server" ID="lblSuccessMessage"></asp:Label></h5>
       
                  </div><!-- End of Modal -->
                  <div class="modal-footer">
       
                        <asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-dark" Text="Close"  data-dismiss="modal"></asp:LinkButton>
        
                  </div>


                    </div>
                    </div>
                 </div>

     
          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
