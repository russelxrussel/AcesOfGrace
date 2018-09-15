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
                <div class="card-header"><h5><span class="fas fa-clipboard-list text-warning"></span> Branch Stock</h5></div>
                <div class="card-body">
                  
                    <div class="row">
                        <div class="col-md-5">
                           
                            <ul class="list-group">
                                    
                                <li class="list-group-item">
                                     <div class="input-group mb-3">
                                 <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Branch"></asp:TextBox>
                                 <div class="input-group-append">
                                     <asp:LinkButton runat="server" ID="U_Search" CssClass="btn btn-outline-primary btn-sm"
                                         data-toggle="tooltip" data-placement="bottom" title="Find Branch"><span class="fas fa-search"></span> FIND</asp:LinkButton>
                                 </div>
                             </div>
                                    <asp:Panel runat="server" ID="panelBranch" Height="600px" ScrollBars="Vertical">
                                    <asp:GridView runat="server" ID="gvBranchList" ShowHeader="false" CssClass="table table-sm table-responsive-md table-hover" GridLines="Horizontal" AutoGenerateColumns="false" OnRowCommand="gvScheduleBranch_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="BranchCode"/>
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch" />

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                 <asp:LinkButton runat="server" ID="lnkNewDelivery" CssClass="btn btn-sm btn-outline-primary" CommandName="Select"><span class="fas fa-arrow-alt-circle-right" data-toggle="tooltip" data-placement="top" title="Insert Delivery"></span></asp:LinkButton>
                                              
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                        </Columns>
                                    </asp:GridView>
                                        </asp:Panel>
                                    </li>
                                 
                                 </ul>
                        </div>



                        <div class="col-md-7">
                            <div class="card">
                                <div class="card-header">
                                          <span class="fas fa-store text-warning"></span> <asp:Label runat="server" ID="lblBranchNameStock"></asp:Label>
                                     
                                    </div>
                                   </div>
                                <div class="card-body">
                                    
                                    <asp:GridView runat="server" ID="gvItems" CssClass="table table-hover table-sm table-responsive-md" GridLines="Horizontal" AutoGenerateColumns="false">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item" />
                                            <asp:BoundField DataField="Floating" HeaderText="Pending" ItemStyle-CssClass="text-danger text-center" ItemStyle-Font-Bold="true" />

                                             <asp:BoundField DataField="tStockBalance" HeaderText="On Hand" ItemStyle-CssClass="text-center" ItemStyle-Font-Bold="true" />
                                            <%-- <asp:TemplateField ControlStyle-Width="50%" ControlStyle-CssClass="text-center" HeaderText="Pending">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblPendingStock" Text='<%# Eval("Floating") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%-- <asp:TemplateField ControlStyle-Width="50%" ControlStyle-CssClass="text-center" HeaderText="On Hand">
                                                <ItemTemplate>
                                                    <b><asp:Label runat="server" ID="lblItemStock" Text='<%# Eval("tStockBalance") %>'></asp:Label></b>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField ControlStyle-Width="50%" HeaderText="">
                                                <ItemTemplate>
                                                   <asp:LinkButton runat="server" ID="lnkView" CssClass="btn btn-sm btn-outline-warning" CommandName="View"><span class="fas fa-file-alt" data-toggle="tooltip" data-placement="top" title="View Branch Stock Transaction"></span></asp:LinkButton>
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
