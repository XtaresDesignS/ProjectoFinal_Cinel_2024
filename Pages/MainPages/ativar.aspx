<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MainLayout.Master" AutoEventWireup="true" CodeBehind="ativar.aspx.cs" Inherits="ProjectoFinal_Cinel_2024.Pages.MainPages.ativar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <asp:Label ID="lbl_mensagemAtivar" runat="server" Text=""></asp:Label>

    <br />
    <br />
    <asp:Button ID="btn_PedirAtivacao" runat="server" OnClick="btn_PedirAtivacao_Click" Text="Pedir Nova Ativação" Width="157px"/>

</asp:Content>
