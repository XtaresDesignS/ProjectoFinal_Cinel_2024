<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MainLayout.Master" AutoEventWireup="true" CodeBehind="SignUP.aspx.cs" Inherits="ProjectoFinal_Cinel_2024.Pages.MainPages.SingUP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">


    <div class="container">
        <div class="container-signup">
            <b>
                <h3>
                    <asp:Label ID="lbl_signup" runat="server" Text="Sign up"></asp:Label></h3>
                <br />
                <br />
                <asp:Image CssClass="signup-tb" ID="img_foto" runat="server" ImageUrl="~/images/icons/camera.png" />
                &nbsp;&nbsp;&nbsp;
            <asp:FileUpload ID="fu_image" runat="server" Width="323px" />
                <br />
                <br />
            </b>
            <asp:Image CssClass="signup-tb" ID="img_userReg" runat="server" ImageUrl="~/Images/user.png" />
            &nbsp;&nbsp;&nbsp;
        <asp:TextBox CssClass="signup-tb" ID="tb_nome" runat="server" placeholder="Name" BorderStyle="None" Width="265px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_nome" runat="server" ControlToValidate="tb_nome" ErrorMessage="Campo obrigatório !!!" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Image CssClass="signup-tb" ID="img_pwLog" runat="server" ImageUrl="~/images/icons/lock.png" />
            &nbsp;&nbsp;&nbsp;
        <asp:TextBox CssClass="signup-tb" ID="tb_pw" runat="server" BorderStyle="None" placeholder="Password" Width="265px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_pw" runat="server" ControlToValidate="tb_pw" ErrorMessage="Campo obrigatório !!!" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Image CssClass="signup-tb" ID="img_confirmPW" runat="server" ImageUrl="~/images/icons/security.png" />
            &nbsp;&nbsp;&nbsp;
        <asp:TextBox CssClass="signup-tb" ID="tb_confirmPW" runat="server" BorderStyle="None" placeholder="Confirm Password" Width="265px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_confirmPW" runat="server" ControlToValidate="tb_confirmPW" ErrorMessage="Campo obrigatório !!!" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cv_passwords" runat="server" ControlToCompare="tb_pw" ControlToValidate="tb_confirmPW" ErrorMessage="As senhas não correspondem." ForeColor="Red">*</asp:CompareValidator>
            <br />
            <br />
            <asp:Image CssClass="signup-tb" ID="img_email" runat="server" ImageUrl="~/Images/mail.png" />
            &nbsp;&nbsp;&nbsp;
        <asp:TextBox CssClass="signup-tb" ID="tb_email" runat="server" BorderStyle="None" placeholder="E-mail" Width="265px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_email" runat="server" ControlToValidate="tb_email" ErrorMessage="Campo obrigatório !!!" ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="tb_email" ErrorMessage="E-mail mal composto !!!" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            <br />
            <br />
            <asp:Image CssClass="signup-tb" ID="img_userReg0" runat="server" ImageUrl="~/Images/user.png" />
        <asp:TextBox CssClass="signup-tb" ID="tb_nif" runat="server" placeholder="NIF" BorderStyle="None" Width="265px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv_nif" runat="server" ControlToValidate="tb_nif" ErrorMessage="Campo obrigatório !!!" ForeColor="Red">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Button CssClass="signup-btn" ID="btn_registarUser" runat="server" OnClick="btn_registarUser_Click" Text="Registar" Width="90px" />

            <br />

            <p>
                <asp:Label CssClass="signup-lbl" ID="lbl_mensagem" runat="server"></asp:Label>
            </p>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div>
    </div>


</asp:Content>
