<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="BranchStockTransfer.aspx.cs" Inherits="AGC.BranchStockTransfer" %>

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
                <div class="card-header"><h5><span class="fas fa-sync text-warning"></span> Branch Stock Transfer / Pull IN - OUT</h5></div>
                <div class="card-body">
                     <div class="card">
                                <div class="card-header">
                                          <div class="row">
                                              <div class="col-md-4">
                                             <div class="input-group mb-3">
                                        <asp:TextBox runat="server" ID="txtTransferDate" CssClass="form-control is-invalid calendarInput" placeholder="Transfer Date"></asp:TextBox>    
                                   
                                    </div>     
                                                  
                                              </div>
                                              
                                             <div class="col-md-8 text-right">
                                                <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-outline-primary btn-sm" OnClick="lnkSave_Click"><span class="fas fa-save"></span> SAVE</asp:LinkButton>
                                             
                                          </div>
                                          </div>
                                          
                                        
                                   
                                   </div>
                                
                            </div>
              
                    <div class="row">
                        <div class="col-md-6">
                            <ul class="list-group">
                                <li class="list-group-item">Branch Source:<asp:DropDownList runat="server" ID="ddSource" CssClass="dropdown form-control" AutoPostBack="true" OnSelectedIndexChanged="ddSource_SelectedIndexChanged"></asp:DropDownList>
                                </li>
                                <li class="list-group-item">
                                    <asp:GridView runat="server" ID="gvItemsSource" CssClass="table table-hover table-sm table-responsive-sm" GridLines="Horizontal" AutoGenerateColumns="false" OnRowDataBound="gvItemsSource_RowDataBound">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" ItemStyle-Width="20%" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item" ItemStyle-Width="30%" />
                                           
                                             <asp:BoundField DataField="tStockBalance" HeaderText="On Hand" ItemStyle-Width="25%" ItemStyle-CssClass="text-center" ItemStyle-Font-Bold="true" />

                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                   <asp:TextBox runat="server" ID="txtTransferQty" MaxLength="4" CssClass="form-control text-center"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            
                                        </Columns>
                                    </asp:GridView>
                                </li>
                            </ul>

                        </div>

                        <div class="col-md-6">
                            <ul class="list-group">

                                <li class="list-group-item">Branch Destination:<asp:DropDownList runat="server" ID="ddDestination" CssClass="dropdown form-control" AutoPostBack="true" OnSelectedIndexChanged="ddDestination_SelectedIndexChanged"></asp:DropDownList>
                                </li>
                                <li class="list-group-item">
                                    <asp:GridView runat="server" ID="gvItemDestination" CssClass="table table-hover table-sm" GridLines="Horizontal" AutoGenerateColumns="false">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item" />

                                            <asp:BoundField DataField="tStockBalance" HeaderText="On Hand" ItemStyle-CssClass="text-center" ItemStyle-Font-Bold="true" />


                                        </Columns>
                                    </asp:GridView>
                                </li>
                            </ul>
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
                   <h5><b><span class="fas fa-check-circle text-success"></span></b> <asp:Label runat="server" ID="lblSuccessMessage"></asp:Label></h5>
       
                  </div><!-- End of Modal -->
                  <div class="modal-footer">
       
                        <asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-dark" Text="Close"  data-dismiss="modal"></asp:LinkButton>
        
                  </div>
                    </div>
                    </div>
                     </div>

            <div class="modal fade bd-example-modal-lg" id="modalError" tabindex="-1" role="dialog" aria-labelledby="modalErrorLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header bg-danger">
                            <h5 class="modal-title" id="modalErrorLabel"><span class="fas fa-envelope text-warning"></span>Aces of Grace Corporation</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <h5><b><span class="fas fa-exclamation-circle text-danger"></span></b>
                                <asp:Label runat="server" ID="lblErrorMessage"></asp:Label></h5>

                        </div>
                        <!-- End of Modal -->
                        <div class="modal-footer">

                            <asp:LinkButton runat="server" ID="lnkCancel" CssClass="btn btn-dark" Text="Close" data-dismiss="modal"></asp:LinkButton>

                        </div>


                    </div>
                </div>
            </div>

     
          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
