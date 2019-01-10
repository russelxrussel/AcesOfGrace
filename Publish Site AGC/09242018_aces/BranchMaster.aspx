<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="BranchMaster.aspx.cs" Inherits="AGC.BranchMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">
   
    <asp:UpdatePanel runat="server" ID="upMain" UpdateMode="Conditional">
        <ContentTemplate>

     <script type="text/javascript">
            $(function calendarInput() {    
                $('.calendarInput').datepicker();
            });

           

            //Search function
            $(function searchInput()
            {
                $('[id*=txtSearchBranch]').on("keyup", function () {
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
                            $('[id*=txtSearchBranch]').on("keyup", function () {
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
           <div class="row">
               <div class="col-md-6">
                    <h5><asp:LinkButton runat="server" ID="lnkCancelNewBranch" CssClass="btn btn-outline-danger btn-sm" OnClick="lnkCancelNewBranch_Click"><span class="fas fa-arrow-left"></span></asp:LinkButton> Branch Data</h5>
               </div>
               <div class="col-md-6 text-right">
                 <asp:Panel runat="server" ID="panBranchEditName" Visible="false">
                     <h5><asp:Label runat="server" ID="lblBranchEditName" CssClass="text-success"></asp:Label></h5>
                 </asp:Panel>
                  
               </div>
           </div>
         <div class="form-inline">
         
              
               
  
          </div>
        </div>

     
        
          
        <div class="card-body">
                
           
             <asp:Panel ID="panelBranchList" runat="server" Visible="true">
                    <div class="row">
                    <div class="col align-self-end small">
                         <div class="input-group mb-1">
               
                     <div class="input-group-prepend">
                    <asp:LinkButton runat="server" ID="lnkNew" CssClass="btn btn-outline-primary" OnClick="lnkNew_Click" data-toggle="tooltip" data-placement="bottom" title="Create New Branch"><span class="fas fa-plus"></span> New</asp:LinkButton>
                    </div>
                    <asp:TextBox runat="server" ID="txtSearchBranch" placeholder="Search Branch" CssClass="form-control text-uppercase"></asp:TextBox>   
               
                         </div>
                    </div>
                    <div class="col-md-6">
                        

                    </div>
                  
                   
                    </div>
                  <hr />
             
             <asp:Panel ID="panelBranchListContent" runat="server" Height="550px" ScrollBars="Vertical">
             <asp:GridView runat="server" ID="gvBranchList" CssClass="table table-hover" AutoGenerateColumns="false" ShowHeader="true" GridLines="Horizontal" OnRowCommand="gvBranchList_RowCommand">
                 <Columns>

                     <asp:TemplateField>
                        <ItemTemplate>
                                  <asp:LinkButton runat="server" ID="lnkView" CssClass="btn btn-sm btn-outline-warning" data-toggle="tooltip" data-placement="top" title="View Basic Info"><span class="fas fa-file-pdf"></span></asp:LinkButton>    
                        </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="BranchCode" HeaderText="Code" />
                     <asp:BoundField DataField="BranchName" HeaderText="Branch" />
                     <asp:BoundField DataField="AreaName"  HeaderText="Area"/>
                     <asp:BoundField DataField="SupervisorName" HeaderText="Supervisor" />
                   
                       <asp:TemplateField>
                         <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="btn btn-sm btn-outline-primary" CommandName="Select"><span class="fas fa-pencil-alt" data-toggle="tooltip" data-placement="top" title="Edit Branch Information"></span></asp:LinkButton>    
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>
             </asp:Panel> 
            </asp:Panel>
           

           
            <!-- INPUT FORM -->
            <asp:Panel runat="server" ID="panelBranchInput" Visible="true">
            
                <nav>
               <div class="nav nav-tabs" id="nav-tab" role="tablist">
                 <a class="nav-item nav-link active" id="nav-info-tab" data-toggle="tab" href="#nav-info" role="tab" aria-controls="nav-info" aria-selected="false"><span class="fas fa-clipboard-list text-warning"></span> Basic Information</a>
                 <a class="nav-item nav-link" id="nav-machine-tab" data-toggle="tab" href="#nav-machine" role="tab" aria-controls="nav-machine" aria-selected="false"><span class="fas fa-archive text-warning"></span> Machine and Equipment</a>
                 <a class="nav-item nav-link" id="nav-repair-tab" data-toggle="tab" href="#nav-repair" role="tab" aria-controls="nav-repair" aria-selected="false"><span class="fas fa-wrench text-warning"></span> Repair/Maintenance Record</a>
               </div>
             </nav>
                <!--TAB PANEL -->
                <br />
                      
                         
                 <div class="tab-content" id="nav-tabContent">
                <!-- Start of Basic -->
                <div class="tab-pane fade show active" id="nav-info" role="tabpanel" aria-labelledby="nav-info-tab">
                     <div class="card">
                    <div class="card-header">
                    <div class="row">
                        <div class="col-md-6"><asp:Label runat="server" ID="lblContentText"></asp:Label></div>
                        
                        <div class="col-md-6 text-right">
                              <asp:LinkButton runat="server" ID="lnkSaveBranch" CssClass="btn btn-outline-primary btn-sm" Text="Save" OnClick="lnkSaveBranch_Click"></asp:LinkButton>
                              
                        </div>
                        
                    </div>
                    </div>
             
                            <div class="card-body">
                    <div id="alertMessage" class="alert alert-danger alert-dismissible">
                            <button type="button" class="close" data-dismiss="alert">&times;</button>
                            <strong><span class="fas fa-exclamation"></span></strong> Please complete required inputs.
                            </div> 
                    
                    <div class="form-row">

                        <div class="form-group col-md-4">
                            <label for="txtBranchCode">Branch Code:</label>
                            <asp:TextBox runat="server" ID="txtBranchCode" MaxLength="10" CssClass="form-control text-uppercase is-invalid" placeholder="Branch Code"></asp:TextBox>

                        </div>

                        <div class="form-group col-md-6">
                            <label for="txtBranchName">Branch Name:</label>
                            <asp:TextBox runat="server" ID="txtBranchName" MaxLength="75" CssClass="form-control text-uppercase is-invalid" placeholder="Branch Name"></asp:TextBox>
                        </div>
                         <div class="form-group col-md-2">
                             <label for="ddStatus">Status:</label>
                             <asp:DropDownList runat="server" ID="ddStatus" CssClass="dropdown form-control is-invalid"></asp:DropDownList>
                        </div>

                        <div class="form-group col-md-4">
                            <label for="txtContactNumber">Contact #:</label>
                            <asp:TextBox runat="server" ID="txtContactNumber" MaxLength="50" CssClass="form-control" placeholder="Contact Numbers"></asp:TextBox>
                        </div>

                        <div class="form-group col-md-8">
                            <label for="txtAddress">Address:</label>
                            <asp:TextBox runat="server" ID="txtAddress" MaxLength="150" CssClass="form-control" placeholder="Address"></asp:TextBox>

                        </div>

                    </div>

                    <div class="form-row">
                       

                        <div class="form-group col-md-4">
                            <label for="ddPersonIncharge">Person Incharge:</label>
                            <asp:DropDownList runat="server" ID="ddPersonIncharge" CssClass="dropdown form-control"></asp:DropDownList>

                        </div>

                        <div class="form-group col-md-4">
                            <label for="ddSupervisor">Supervisor:</label>
                            <asp:DropDownList runat="server" ID="ddSupervisor" CssClass="dropdown form-control is-invalid"></asp:DropDownList>
                        </div>

                          <div class="form-group col-md-4">
                            <label for="ddBranchArea">Branch Area:</label>
                            <asp:DropDownList runat="server" ID="ddBranchArea" CssClass="dropdown form-control is-invalid"></asp:DropDownList>
                        </div>
                       

                    </div>

                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <label for="ddPartner">Partner:</label>
                            <asp:DropDownList runat="server" ID="ddPartner" CssClass="dropdown form-control is-invalid"></asp:DropDownList>
                        </div>

                         <div class="form-group col-md-4">
                            <label for="txtOpeningDate">Opening Date</label>
                            <asp:TextBox runat="server" ID="txtOpeningDate" CssClass="form-control is-invalid calendarInput" placeholder="Opening Date"></asp:TextBox>
                        </div>
                       

                         <div class="form-group col-md-4">
                        <label for="txtRemarks">Remarks:</label>
                        <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" placeholder="Remarks"></asp:TextBox>
                    </div>
                    </div>

                    <div class="form-row">
                         
                         <div class="form-group col-md-6">
                            <label for="txtLessorName">Lessor Name:</label>
                            <asp:TextBox runat="server" ID="txtLessorName" MaxLength="75" CssClass="form-control text-uppercase" placeholder="Lessor Name"></asp:TextBox>

                        </div>
                           <div class="form-group col-md-4">
                            <label for="ddModePayment">Mode of Payment</label>
                          <asp:DropDownList runat="server" ID="ddModePayment" CssClass="dropdown form-control"></asp:DropDownList>
                        </div>
                       

                         <div class="form-group col-md-2">
                        <label for="txtPaymentDate">Day of Payment:</label>
                       <%-- <asp:TextBox runat="server" ID="txtPaymentDate" CssClass="form-control" placeholder="Date of Payment" ToolTip="Example (Every 8th of the month)"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" ID="ddPaymentDay" CssClass="form-control"></asp:DropDownList>
                    </div>
                        
                    </div>

                    
                </div>
            </div>

                </div>
                <!-- End of Tab Basic Info -->
              
                <!-- Start of Tab Machine and Equipment -->
                <div class="tab-pane fade" id="nav-machine" role="tabpanel" aria-labelledby="nav-machine-tab">
                   <asp:Panel runat="server" ID="panMachineEquipment" Enabled="false">

                    <asp:UpdatePanel runat="server" ID="upMachineEquipment" UpdateMode="Conditional">
                        <ContentTemplate>
                            
                          
                                    <div class="row">
                                        <div class="col">
                                            <!-- Machine Equipment List -->
                                        <div class="card">
                                            <div class="card-header">
                                                        <span class="fas fa-compass"></span> List of Machine Equipment Issue
                                                </div>
                                            <div class="card-body">
                                                <div id="alertMessageME" class="alert alert-danger alert-dismissible">
                                                <button type="button" class="close" data-dismiss="alert">&times;</button>
                                                <strong><span class="fas fa-exclamation"></span></strong> <asp:Label runat="server" ID="lblAlertMessageText"></asp:Label>
                                                </div>
                                                <div id="successMessageME" class="alert alert-success alert-dismissible">
                                                <button type="button" class="close" data-dismiss="alert">&times;</button>
                                                <strong><span class="fas fa-exclamation"></span></strong> <asp:Label runat="server" ID="lblSuccessMessageText"></asp:Label>
                                                </div>
                                                <asp:GridView runat="server" ID="gvMachineEquipment" CssClass="table" GridLines="Horizontal" AutoGenerateColumns="false" 
                                                   ShowHeaderWhenEmpty="true"  OnRowCommand="gvMachineEquipment_RowCommand" OnRowDataBound="gvMachineEquipment_RowDataBound"
                                                    HeaderStyle-CssClass="bg-warning" >
                                            <Columns>
                                                     
                                                <asp:BoundField DataField="ID" Visible="false"/>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:DropDownList runat="server" ID="ddMachineEquipment" CssClass="dropdown form-control is-invalid"></asp:DropDownList>
                                                    </HeaderTemplate>
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblMachineEquipName" Text='<%# Eval("machEquipName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                    <EditItemTemplate>
                                                        <asp:Label runat="server" ID="lblMachEquipCode" Text='<%# Eval("machEquipCode") %>' Visible="false"></asp:Label>
                                                        <asp:DropDownList runat="server" ID="ddMachineEquipName" CssClass="form-control is-invalid"></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:TextBox runat="server" ID="txtMEDescription" MaxLength="75" CssClass="form-control text-uppercase" placeholder="Additional Description"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDescription" Text='<%# Eval("addtDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" Text='<%# Bind("addtDescription") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:TextBox runat="server" ID="txtSerial" MaxLength="75" CssClass="form-control text-uppercase" placeholder="Serial"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                         <asp:Label runat="server" ID="lblSerial" Text='<%# Eval("serial") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtSerial" CssClass="form-control" Text='<%# Bind("serial") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:TextBox runat="server" ID="txtDateIssue" CssClass="form-control is-invalid calendarInput" placeholder="Date Issue"></asp:TextBox>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                           <asp:Label runat="server" ID="lblDateIssue" Text='<%# Eval("dateissue","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                         <asp:TextBox runat="server" ID="txtDateIssue" CssClass="calendarInput form-control is-invalid" Text='<%# Bind("dateissue","{0:d}") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkCreate" CommandArgument='<%# Eval("ID") %>' CommandName="Create" CssClass="btn btn-success btn-sm" ToolTip="Add Machine/Equipment to Branch"><span class="fas fa-plus-circle"></span></asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkEdit" CommandArgument='<%# Eval("ID") %>' CommandName="Select" CssClass="btn btn-outline-primary btn-sm" ToolTip="Edit Machine/Equipment of this Branch"><span class="fas fa-pencil-alt"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkUpdate" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-outline-success btn-sm" CommandName="UpdateRow" ToolTip="Update Machine/Equipment of this Branch"><span class="fas fa-check-circle"></span></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkDelete" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-outline-danger btn-sm" CommandName="DeleteRow" ToolTip="Remove Machine/Equipment on this Branch"><span class="fas fa-times-circle"></span></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkCancel" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-outline-warning btn-sm" CommandName="CancelEdit" ToolTip="Cancel modification"><span class="fas fa-minus-circle"></span></asp:LinkButton>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                            </div>
                                        </div>
                                        </div>
                                       
                                    </div>
                                   


                                     

                              <!-- Modal Confirmation of Delete Machine and Equipment -->
        <div class="modal fade bd-example-modal-sm" id="modalConfirmDelete"  tabindex="-1" role="dialog" aria-labelledby="modalSuccessLabel" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
        <div class="modal-header bg-danger">
        <h5 class="modal-title"><span class="fas fa-envelope text-warning"></span> Aces of Grace Corporation</h5>
        
      </div>
      <div class="modal-body">
       <h5><b><span class="fas fa-times-circle text-danger"></span></b> Are you sure you want to delete this Machine / Equipment?</h5>
       <br />
       <asp:TextBox runat="server" ID="txtDeletedRemarks" Rows="3" TextMode="MultiLine" CssClass="form-control is-invalid" placeholder="Deletion Remarks required"></asp:TextBox>
      </div><!-- End of Modal -->
      <div class="modal-footer">
            <asp:LinkButton runat="server" ID="lnkConfirmDelete" CssClass="btn btn-danger" Text="Delete" OnClick="lnkConfirmDelete_Click"></asp:LinkButton>
            <asp:LinkButton runat="server" ID="lnkCloseDeleteMachineEquipment" CssClass="btn btn-dark" Text="Close"  OnClick="lnkCloseDeleteMachineEquipment_Click"></asp:LinkButton>
        
      </div>
    </div>
  </div>
</div>

                                
                            
                      </ContentTemplate>
                    </asp:UpdatePanel>

                  </asp:Panel>

                </div><!-- End of Tab Machine and Equipment -->

                
               <div class="tab-pane fade" id="nav-repair" role="tabpanel" aria-labelledby="nav-repair-tab">Repair and Maintenance Record</div>
            </div>
          


           </asp:Panel>
        </div>

         

         <!-- Modal -->
        <div class="modal fade bd-example-modal-lg" id="modalSuccess" tabindex="-1" role="dialog" aria-labelledby="modalSuccessLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-header bg-success">
        <h5 class="modal-title" id="modalErrorLabel"><span class="fas fa-envelope text-warning"></span> Aces of Grace Corporation</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
       <h5><b><span class="fas fa-check-circle text-success"></span></b> <asp:Label runat="server" ID="lblSuccessMessage"></asp:Label></h5>
       
      </div><!-- End of Modal -->
      <div class="modal-footer">
       
            <asp:LinkButton runat="server" ID="lnkCancel" CssClass="btn btn-dark" Text="Close"  data-dismiss="modal"></asp:LinkButton>
        
      </div>
    </div>
  </div>
</div>



    </div>

                    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
