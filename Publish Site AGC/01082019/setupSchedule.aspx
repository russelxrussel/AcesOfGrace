<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="setupSchedule.aspx.cs" Inherits="AGC.setupSchedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">
<%--<div class="container container_content">--%>


<asp:UpdatePanel runat="server" ID="uplMain">
    <ContentTemplate>
         <script type="text/javascript">
            //$(function calendarInput() {    
            //    $('.calendarInput').datepicker();
            //});

           

            //Search function
            $(function searchInput()
            {
                $('[id*=txtSearch]').on("keyup", function () {
                    var value = $(this).val().toLowerCase();
                    $('[id*=gvScheduleSetup] tr').filter(function () {
                        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                    });
                });
            });

          
      
   

            //On UpdatePanel Refresh
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                       
                       // $('.calendarInput').datepicker();

                        //Search function
                        $(function searchInput() {
                            $('[id*=txtSearch]').on("keyup", function () {
                                var value = $(this).val().toLowerCase();
                                $('[id*=gvScheduleSetup] tr').filter(function () {
                                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                                });
                            });
                        });


                    }
                });
            };
    </script>

<%-- <hr />--%>
  
  
    <!-- Display Gridview List of Items -->

          <div class="card">
                <div class="card-header">
                     <div class="row">
                 <div class="col-md-6">
             <h5><span class="fas fa-calendar-alt text-primary"></span> Delivery Schedule Setup</h5>
           </div>

                 <div class="col-6 text-right">

                             <div class="input-group mb-3">
                                 <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search Branch"></asp:TextBox>
                                 <div class="input-group-append">
                                     <asp:LinkButton runat="server" ID="U_Search" CssClass="btn btn-outline-primary btn-sm"
                                         OnClick="U_Search_Click" data-toggle="tooltip" data-placement="bottom" title="Find Branch"><span class="fas fa-search"></span> FIND</asp:LinkButton>
                                 </div>
                             </div>
                         </div>
                 
                </div>
       </div>
      
                <div class="card-body">
                    <asp:Panel runat="server" ID="panel1" ScrollBars="Vertical" Height="700px">
                    <asp:GridView runat="server" ID="gvScheduleSetup" CssClass="table table-hover" ShowHeader="true" GridLines="Horizontal" 
                        AlternatingRowStyle-CssClass="bg-light" HeaderStyle-CssClass="bg-info" AutoGenerateColumns="False">

                        <Columns>

                            <asp:BoundField DataField="SchedID" ItemStyle-CssClass="text-hide" HeaderStyle-CssClass="hidden" />
                            <asp:BoundField DataField="BranchName" HeaderText="Branch" />

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span class="badge">Monday</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="checkbox text-center">
                                    
                                        <asp:CheckBox runat="server" ID="chkM" Checked='<%# Eval("M") == DBNull.Value ? false :  Eval("M") %>' />
                                  
                                    </div>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span class="badge">Tuesday</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="checkbox text-center">
                                     
                                        <asp:CheckBox runat="server" ID="chkT" Checked='<%# Eval("T") == DBNull.Value ? false :  Eval("T") %>' />
                               
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span class="badge">Wednesday</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="checkbox text-center">
                                     
                                        <asp:CheckBox runat="server" ID="chkW" Checked='<%# Eval("W") == DBNull.Value ? false :  Eval("W") %>' />
                                       
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span class="badge">Thursday</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="checkbox text-center">
                                     

                                        <asp:CheckBox runat="server" ID="chkTh" Checked='<%# Eval("Th") == DBNull.Value ? false :  Eval("Th") %>' />

                                     
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span class="badge">Friday</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="checkbox text-center">
                                        <%--<label>--%>
                                        <asp:CheckBox runat="server" ID="chkF" Checked='<%# Eval("F") == DBNull.Value ? false :  Eval("F") %>' />
                                        <%--  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span class="badge">Saturday</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="checkbox text-center">
                                        <%-- <label>--%>
                                        <asp:CheckBox runat="server" ID="chkSa" Checked='<%# Eval("Sa") == DBNull.Value ? false :  Eval("Sa") %>' />
                                        <%--  <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span class="badge">Sunday</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="checkbox text-center">
                                        <%--<label>--%>
                                        <asp:CheckBox runat="server" ID="chkS" Checked='<%# Eval("S") == DBNull.Value ? false :  Eval("S") %>' />
                                        <%--   <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>--%>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Button ID="btnEditItem" runat="server" CssClass="btn btn-outline-primary btn-sm" Text="Update" OnClick="btnEditItem_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>
                    </asp:Panel>
                </div>

         </div>
        
          
       
   
 
   


        <!--MESSAGE MODAL SECTION-->

     
     

                    <!--Message Save SUCCESS-->
                    <div class="modal fade" id="msgSuccessModal">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header bg-success">
                                    
                                    
                                    <h4 class="modal-title">
                                        Aces of Grace Corporation</h4>
                                    <button class="close" data-dismiss="modal">
                                        &times;</button>
                                </div>
                                <div class="modal-body">
                                    <h4>
                                        <span class="glyphicon glyphicon-success"></span>&nbsp;
                                        <asp:Label runat="server" ID="lblMessageSuccess"></asp:Label></h4>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
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
<!-- </div>END OF DIV CONTAINER-->


</asp:Content>
