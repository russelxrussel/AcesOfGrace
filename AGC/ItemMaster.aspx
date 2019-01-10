<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="ItemMaster.aspx.cs" Inherits="AGC.ItemMaster" %>

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
                $('[id*=txtSearchItem]').on("keyup", function () {
                    var value = $(this).val().toLowerCase();
                    $('[id*=gvItemList] tr').filter(function () {
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
                            $('[id*=txtSearchItem]').on("keyup", function () {
                                var value = $(this).val().toLowerCase();
                                $('[id*=gvItemList] tr').filter(function () {
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
                    <h5><asp:LinkButton runat="server" ID="lnkCancelNewItem" CssClass="btn btn-outline-danger btn-sm" OnClick="lnkCancelNewBranch_Click"><span class="fas fa-arrow-left"></span></asp:LinkButton> Item Data</h5>
               </div>
               <div class="col-md-6 text-right">
                 <asp:Panel runat="server" ID="panItemEditName" Visible="false">
                     <h5><asp:Label runat="server" ID="lblItemEditName" CssClass="text-success"></asp:Label></h5>
                 </asp:Panel>
                  
               </div>
           </div>
         <div class="form-inline">
         
              
               
  
          </div>
        </div>

     
        
          
        <div class="card-body">
                
           
             <asp:Panel ID="panelItemList" runat="server" Visible="true">
                    <div class="row">
                    <div class="col align-self-end small">
                         <div class="input-group mb-1">
               
                     <div class="input-group-prepend">
                    <asp:LinkButton runat="server" ID="lnkNew" CssClass="btn btn-outline-primary" OnClick="lnkNew_Click" data-toggle="tooltip" data-placement="bottom" title="Create New Branch"><span class="fas fa-plus"></span> New</asp:LinkButton>
                    </div>
                    <asp:TextBox runat="server" ID="txtSearchItem" placeholder="Search Item" CssClass="form-control text-uppercase"></asp:TextBox>   
               
                         </div>
                    </div>
                    <div class="col-md-6">
                        

                    </div>
                  
                   
                    </div>
                  <hr />
             
             <asp:Panel ID="panelBranchListContent" runat="server" Height="550px" ScrollBars="Vertical">
             <asp:GridView runat="server" ID="gvItemList" CssClass="table table-hover" AutoGenerateColumns="false" ShowHeader="true" GridLines="Horizontal" OnRowCommand="gvBranchList_RowCommand">
                 <Columns>

                     <asp:TemplateField>
                        <ItemTemplate>
                                  <asp:LinkButton runat="server" ID="lnkView" CssClass="btn btn-sm btn-outline-warning" data-toggle="tooltip" data-placement="top" title="View Basic Info"><span class="fas fa-file-pdf"></span></asp:LinkButton>    
                        </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="ItemCode" HeaderText="Code" />
                     <asp:BoundField DataField="ItemName" HeaderText="Item" />
                     <asp:BoundField DataField="ItemCategoryName"  HeaderText="Category"/>
                     <asp:BoundField DataField="payAccName" HeaderText="Payable Account" />
                     <asp:BoundField DataField="UnitCost"  HeaderText="Cost" ItemStyle-CssClass="text-danger"/>
                     <asp:BoundField DataField="UnitPrice" HeaderText="Price" ItemStyle-CssClass="text-success" ItemStyle-Font-Bold="true" />
                   
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
            <asp:Panel runat="server" ID="panelItemInput" Visible="true">
           
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
                  
                    
                    <div class="form-row">

                        <div class="form-group col-md-4">
                            <label for="txtItemCode">Item Code:</label>
                            <asp:TextBox runat="server" ID="txtItemCode" MaxLength="10" CssClass="form-control text-uppercase is-invalid" placeholder="Item Code"></asp:TextBox>

                        </div>

                        <div class="form-group col-md-6">
                            <label for="txtBranchName">Item Name:</label>
                            <asp:TextBox runat="server" ID="txtItemName" MaxLength="75" CssClass="form-control text-uppercase is-invalid" placeholder="Item Name"></asp:TextBox>
                        </div>
                         <div class="form-group col-md-2">
                             <label for="ddStatus">Status:</label>
                             <asp:DropDownList runat="server" ID="ddStatus" CssClass="dropdown form-control is-invalid"></asp:DropDownList>
                        </div>

                    </div>

                    <div class="form-row">
                       

                        <div class="form-group col-md-4">
                            <label for="ddPersonIncharge">Unit of Measure:</label>
                            <asp:DropDownList runat="server" ID="ddUOM" CssClass="dropdown form-control is-invalid"></asp:DropDownList>

                        </div>

                        <div class="form-group col-md-4">
                            <label for="ddSupervisor">Item Category:</label>
                            <asp:DropDownList runat="server" ID="ddItemCategory" CssClass="dropdown form-control is-invalid"></asp:DropDownList>
                        </div>

                          <div class="form-group col-md-4">
                            <label for="ddBranchArea">Payable Account:</label>
                            <asp:DropDownList runat="server" ID="ddPayableAccount" CssClass="dropdown form-control is-invalid"></asp:DropDownList>
                        </div>
                       

                    </div>

                    <div class="form-row">

                         <div class="form-group col-md-4">
                            <label for="txtOpeningDate">Unit Cost</label>
                            <asp:TextBox runat="server" ID="txtUnitCost" Enabled="false" CssClass="form-control is-invalid" placeholder="Unit Cost" onkeypress="return(event.charCode == 8 || event.charCode == 0) ? 0: event.charCode >= 46 && event.charCode <=57"></asp:TextBox>
                        </div>
                       

                         <div class="form-group col-md-4">
                        <label for="txtRemarks">Unit Price:</label>
                        <asp:TextBox runat="server" ID="txtUnitPrice" Enabled="false" CssClass="form-control is-invalid" placeholder="Unit Price" onkeypress="return(event.charCode == 8 || event.charCode == 0) ? 0: event.charCode >= 46 && event.charCode <=57"></asp:TextBox>
                    </div>
                    </div>

                  
                    
                </div>
            </div>

      
                                   


                </asp:Panel>                     

      
                                
                 

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
         
                       
                      </ContentTemplate>
                    </asp:UpdatePanel>


</asp:Content>
