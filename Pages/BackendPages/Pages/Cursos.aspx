<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/BackendPages/BackendLayout.Master" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="ProjectoFinal_Cinel_2024.Pages.BackendPages.Pages.Cursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBackEnd" runat="server">
     <div class="main-content col">
         <asp:TextBox ID="tb_nomeCurso" runat="server" Width="350px" placeholder="Nome do Curso"></asp:TextBox>
     &nbsp;&nbsp;&nbsp;
         <asp:DropDownList ID="ddl_tipo" runat="server" DataSourceID="SqlDataSource2" DataTextField="nome_Tipo" DataValueField="id_Tipo" Width="350px">
         </asp:DropDownList>
         <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:GestCinel2_DBConnectionString %>" SelectCommand="SELECT * FROM [Tipos]"></asp:SqlDataSource>
         <br />
         <br />
         <asp:DropDownList ID="ddl_area" runat="server" DataSourceID="SqlDataSource1" DataTextField="Nome_Area" DataValueField="id_Area" Height="16px" Width="350px">
         </asp:DropDownList>
         &nbsp;&nbsp;&nbsp;
         <asp:DropDownList ID="ddl_local" runat="server" DataSourceID="SqlDataSource3" DataTextField="nome_Local" DataValueField="id_Local" Width="350px">
         </asp:DropDownList>
         <br />
         <br />
         <asp:TextBox ID="tb_dataInicio" runat="server" TextMode="Date" Width="350px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
         <asp:TextBox ID="tb_dataFim" runat="server" TextMode="Date" Width="350px"></asp:TextBox>
         <br />
         <br />
         <asp:Button ID="btn_geraRef" runat="server" OnClick="btn_geraRef_Click" Text="Gerar Referência" Width="100px" />
&nbsp;&nbsp;&nbsp;
         <asp:Label ID="lbl_ref" runat="server"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:TextBox ID="tb_horario" runat="server" Width="350px" placeholder="Horário do Curso (texto)"></asp:TextBox>
         <br />
         <br />
         <asp:TextBox ID="tb_valor" runat="server" Width="350px" placeholder="Valor do Curso (texto)"></asp:TextBox>
         &nbsp;&nbsp;&nbsp;
         <asp:DropDownList ID="ddl_estado" runat="server" DataSourceID="SqlDataSource4" DataTextField="Nome_Estado" DataValueField="id_Estado" Width="350px">
         </asp:DropDownList>
         <br />
         <br />
         <asp:Button ID="btn_gravar" runat="server" OnClick="btn_gravar_Click" Text="Gravar" Width="100px" />
         <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GestCinel2_DBConnectionString %>" SelectCommand="SELECT * FROM [Estados]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GestCinel2_DBConnectionString %>" SelectCommand="SELECT * FROM [Locais]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GestCinel2_DBConnectionString %>" SelectCommand="SELECT * FROM [Areas]"></asp:SqlDataSource>
     </div>
   
</asp:Content>
