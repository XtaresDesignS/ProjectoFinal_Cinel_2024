<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MainLayout.Master" AutoEventWireup="true" CodeBehind="alteraPW.aspx.cs" Inherits="ProjectoFinal_Cinel_2024.Pages.alteraPW" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="card-log">
      <div class="card-log2">
          <div class="form-log">
              <p id="heading">Alteração de Password</p>
            
              <div class="field">
                  <svg
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      height="16"
                      width="16"
                      xmlns="http://www.w3.org/2000/svg"
                      class="input-icon">
                      <path
                          d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z">
                      </path>
                  </svg>
                  <asp:TextBox ID="tb_pass_Antiga" class="input-field" placeholder="Password actual" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Deve preencher o Campo da Password Atual" ControlToValidate="tb_pass_Antiga" ForeColor="#FF9900">*</asp:RequiredFieldValidator>
              </div>
              <div class="field">
                  <svg
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      height="16"
                      width="16"
                      xmlns="http://www.w3.org/2000/svg"
                      class="input-icon">
                      <path
                          d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z">
                      </path>
                  </svg>
                  <asp:TextBox ID="tb_pass_nova" class="input-field" placeholder="Password nova" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Deve preencher o Campo da Password nova" ControlToValidate="tb_pass_nova" ForeColor="#FF9900">*</asp:RequiredFieldValidator>
              </div>
              <div class="field">
                  <svg
                      viewBox="0 0 16 16"
                      fill="currentColor"
                      height="16"
                      width="16"
                      xmlns="http://www.w3.org/2000/svg"
                      class="input-icon">
                      <path
                          d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2zm3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2z">
                      </path>
                  </svg>
                  <asp:TextBox ID="tb_Confirma_Pass" class="input-field" placeholder="Confirm Password nova" runat="server"></asp:TextBox><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="As Passwords devem Ser iguais" ControlToCompare="tb_pass_nova" ControlToValidate="tb_Confirma_Pass" ForeColor="#FF9900">*</asp:CompareValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Este campo não deve ficar vazio" ControlToValidate="tb_Confirma_Pass" ForeColor="#FF9900">*</asp:RequiredFieldValidator>
              </div>
              <div class="btns">

                  <asp:Button ID="btn_altera" class="btn-form1" runat="server" Text="Confirmar alteração" OnClick="btn_altera_Click" />
                 
                  <a href="/Pages/Login.aspx" class="btn-form2">Login</a>

              </div>
      
          </div>
          <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
              <asp:Label ID="lb_Mensagem" runat="server" Text=""></asp:Label>
      </div>
  </div>
</asp:Content>
