<%@ Page Title="" Language="C#" MasterPageFile="~/AGC.Master" AutoEventWireup="true" CodeBehind="setupSchedule.aspx.cs" Inherits="AGC.setupSchedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentPage" runat="server">
<div class="container container_content">


<asp:UpdatePanel runat="server" ID="uplMain">
    <ContentTemplate>
 <hr />
  <div class="panel panel-warning">
    <div class="panel-heading">
       <div class="row">
           <div class="col-md-8">
                Maintenance of Delivery Schedule
           </div>

           <div class="col-md-4 text-right">
               <div class="input-group input-group-sm">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control text-uppercase"
                    placeholder="Search">
                </asp:TextBox>
                <div class="input-group-btn">
                    <asp:LinkButton runat="server" ID="U_Search" CssClass="btn btn-primary" 
                        onclick="U_Search_Click"><span class="glyphicon glyphicon-search"></span> FIND</asp:LinkButton>
                </div>
            </div>
           </div>
       </div>
     
    </div>
    <div class="panel-body">
    <asp:Panel runat="server" ID="panelSchedule" ScrollBars="Vertical" Height="600px">
    <!-- Display Gridview List of Items -->
    <asp:GridView runat="server" ID="gvScheduleSetup" 
            CssClass="table table-responsive table-hover table-condensed table-bordered" AutoGenerateColumns="False">
    
        <Columns>
           
            <asp:BoundField DataField="SchedID" HeaderText="ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
           <asp:BoundField DataField="BranchName" HeaderText="Branch" />
        
            <asp:TemplateField>
                <HeaderTemplate>
                    <span class="badge">Monday</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox runat="server" ID="chkM" Checked='<%# Eval("M") == DBNull.Value ? false :  Eval("M") %>' />
                            <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>

                    </div>
                    
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <span class="badge">Tuesday</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox runat="server" ID="chkT" Checked='<%# Eval("T") == DBNull.Value ? false :  Eval("T") %>' />
                            <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>

                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <span class="badge">Wednesday</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox runat="server" ID="chkW" Checked='<%# Eval("W") == DBNull.Value ? false :  Eval("W") %>' />
                            <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>

                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <span class="badge">Thursday</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox runat="server" ID="chkTh" Checked='<%# Eval("Th") == DBNull.Value ? false :  Eval("Th") %>' />
                            <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>

                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <span class="badge">Friday</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox runat="server" ID="chkF" Checked='<%# Eval("F") == DBNull.Value ? false :  Eval("F") %>' />
                            <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>

                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <span class="badge">Saturday</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox runat="server" ID="chkSa" Checked='<%# Eval("Sa") == DBNull.Value ? false :  Eval("Sa") %>' />
                            <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>

                    </div>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <HeaderTemplate>
                    <span class="badge">Sunday</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="checkbox">
                        <label>
                            <asp:CheckBox runat="server" ID="chkS" Checked='<%# Eval("S") == DBNull.Value ? false :  Eval("S") %>' />
                            <span class="cr"><i class="cr-icon glyphicon glyphicon-ok"></i></span>
                        </label>

                    </div>
                </ItemTemplate>
            </asp:TemplateField>
           
             <asp:TemplateField ItemStyle-CssClass="text-center">
            <ItemTemplate>
            <asp:Button ID="btnEditItem" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnEditItem_Click" />
            </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    
    </asp:GridView>
        </asp:Panel>
 
    </div>
      <div class="panel-footer">

      </div>
   </div>


        <!--MESSAGE MODAL SECTION-->

     
     

                    <!--Message Save SUCCESS-->
                    <div class="modal fade" id="msgSuccessModal">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header bg-success">
                                    <button class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">
                                        DGMU Enterprises</h4>
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

                      <!--Message Error -->
                    <div class="modal fade" id="msgErrorModal">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header bg-danger">
                                    <button class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">
                                        DGMU Enterprises</h4>
                                </div>
                                <div class="modal-body">
                                    <h4>
                                        <span class="glyphicon glyphicon-alert"></span>&nbsp;
                                        <asp:Label runat="server" ID="lblErrorPrompt"></asp:Label></h4>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>

    </ContentTemplate>
</asp:UpdatePanel>
</div><!-- END OF DIV CONTAINER-->


</asp:Content>
