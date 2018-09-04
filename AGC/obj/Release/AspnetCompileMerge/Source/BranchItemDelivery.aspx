<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="BranchItemDelivery.aspx.cs" Inherits="AGC.BranchItemDelivery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">


    <asp:UpdatePanel runat="server" ID="upMain" UpdateMode="Conditional">
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
                <div class="card-header"><h5><span class="fas fa-truck text-primary"></span> Branch Item Delivery</h5></div>
                <div class="card-body">
                     <div id="alertErrorMessage" class="alert alert-danger alert-dismissible">
                            <button type="button" class="close" data-dismiss="alert">&times;</button>
                            <strong><span class="fas fa-exclamation"></span></strong> 
                            <asp:Label runat="server" ID="lblErrorMessage"></asp:Label>
                            </div> 


                    <div class="row">
                        <div class="col-md-5">
                           
                            <ul class="list-group">
                                <li class="list-group-item active">Branch Details</li>
                                <li class="list-group-item">
                                    <asp:TextBox runat="server" ID="txtDeliveryDate" CssClass="form-control is-invalid calendarInput" placeholder="Delivery Date"></asp:TextBox></li>
                                <li class="list-group-item"><asp:DropDownList runat="server" ID="ddBranches" CssClass="dropdown form-control is-invalid"></asp:DropDownList></li>
                               <li class="list-group-item"><asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Type Remarks"></asp:TextBox></li>
                                 
                                 </ul>
                        </div>



                        <div class="col-md-7">
                            <div class="card">
                                <div class="card-header">
                                    <div class="row">
                                        <div class="col-md-6">
                                             Delivery Item Quantity
                                        </div>
                                        <div class="col-md-6 text-right">
                                            <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-outline-primary btn-sm" OnClick="lnkSave_Click"><span class="fas fa-save"></span> SAVE</asp:LinkButton>
                                        </div>
                                    </div>
                                   </div>
                                <div class="card-body">
                                    
                                    <asp:GridView runat="server" ID="gvItems" CssClass="table table-sm table-responsive-md" GridLines="Horizontal" AutoGenerateColumns="false">
                                        <Columns>

                                            <asp:BoundField DataField="ItemCode" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />

                                            <asp:TemplateField ControlStyle-Width="50%" HeaderText="Quantity Delivered">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtItemQuantity" CssClass="form-control text-center"></asp:TextBox>
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
