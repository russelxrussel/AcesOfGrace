<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="BranchDeliveryPosting.aspx.cs" Inherits="AGC.BranchDeliveryPosting" %>

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
                    $('[id*=gvDeliveryForPostingList] tr').filter(function () {
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
                                $('[id*=gvDeliveryForPostingList] tr').filter(function () {
                                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                                });
                            });
                        });

                    }
                });
            };
    </script>


            <div class="card">
                <div class="card-header bg-primary"><h5><span class="fas fa-trash text-warning"></span> Branch Delivery Posting</h5></div>
                <div class="card-body">
                  
                    <div class="row">
                        <div class="col-md-12">
                           
                            <ul class="list-group">
                             
                                <li class="list-group-item">
                                    <div class="row">
                                        <div class="col-md-9">
                                     <div class="input-group mb-3">
                                 <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Branch"></asp:TextBox>
                                 <div class="input-group-append">
                                     <asp:LinkButton runat="server" ID="U_Search" CssClass="btn btn-outline-primary btn-sm"
                                         data-toggle="tooltip" data-placement="bottom" title="Find Branch"><span class="fas fa-search"></span> FIND</asp:LinkButton>
                                 </div>
                             </div>
                                        </div>
                                        <div class="col-md-3 text-right">
                                            <asp:LinkButton runat="server" ID="lnkPostAll" CssClass="btn btn-success" OnClick="lnkPostAll_Click"><span class="fas fa-upload" data-toggle="tooltip" data-placement="top" title="Post All Delivery"></span> POST ALL</asp:LinkButton>
                                        </div>
                                    </div>
                                     <asp:Panel runat="server" ID="panelBranch" Height="600px" ScrollBars="Vertical">
                                    <asp:GridView runat="server" ID="gvDeliveryForPostingList" ShowHeader="true" CssClass="table table-sm table-responsive-md table-hover" GridLines="Horizontal" AutoGenerateColumns="false" OnRowCommand="gvScheduleBranch_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="DeliveryNum" HeaderText="Reference Num"/>
                                            <asp:BoundField DataField="PartnerName" HeaderText="Partner"/>
                                            <asp:BoundField DataField="BranchCode" HeaderText="Code" />
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch" />
                                            <asp:BoundField DataField="TOTALDELIVERYCOST" HeaderText="Total Cost" DataFormatString="{0:n2}"/>
                                            <asp:BoundField DataField="DeliveryDate" DataFormatString="{0:d}" HeaderText="Delivery Date"/>


                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                 
                                                 <asp:LinkButton runat="server" ID="lnkView" CssClass="btn btn-sm btn-outline-warning" CommandName="View"><span class="fas fa-file-alt" data-toggle="tooltip" data-placement="top" title="View Delivery Details"></span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkPost" CssClass="btn btn-sm btn-outline-success" CommandName="Post"><span class="fas fa-upload" data-toggle="tooltip" data-placement="top" title="Post Delivery"></span></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField>
                                                <ItemTemplate>
                                                 
                                                 <asp:LinkButton runat="server" ID="lnkCancel" CssClass="btn btn-sm btn-outline-danger" CommandName="Cancel"><span class="fas fa-minus-circle" data-toggle="tooltip" data-placement="top" title="Cancel Delivery"></span></asp:LinkButton>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           
                                        </Columns>
                                    </asp:GridView>
                                        </asp:Panel>
                                    </li>
                                 
                                 </ul>
                        </div>




                </div>


                  

                <div class="modal fade bd-example-modal-lg" id="modalView" tabindex="-1" role="dialog" aria-labelledby="modalSuccessLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                  <div class="modal-header bg-info">
                    <h5 class="modal-title" id="modalViewLabel"><span class="fas fa-envelope text-warning"></span> Aces of Grace Corporation</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body">
                    
                        <hr />
                        <h4><b><asp:Label runat="server" ID="lblDRNUM" CssClass="text-danger"></asp:Label></b></h4>
                        <asp:GridView runat="server" ID="gvDRList" CssClass="table table-hover table-sm table-responsive-md" GridLines="Horizontal" AutoGenerateColumns="false">
                                        <Columns>

                                            <asp:BoundField DataField="itemCode" />
                                            <asp:BoundField DataField="itemName" HeaderText="Item" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                            <asp:BoundField DataField="UnitCost" HeaderText="Unit Cost" DataFormatString="{0:n2}" />
                                            <asp:BoundField DataField="totalCost" HeaderText="Total" DataFormatString="{0:n2}" />

                                        </Columns>
                                    </asp:GridView>

                  </div>
                  <div class="modal-footer">
       
                        <asp:LinkButton runat="server" ID="LinkButton2" CssClass="btn btn-dark" Text="Close"  data-dismiss="modal"></asp:LinkButton>
        
                  </div>
                    </div>
                    </div>
                     </div><!-- End of Modal -->


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
