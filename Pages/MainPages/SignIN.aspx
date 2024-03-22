<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/MainLayout.Master" AutoEventWireup="true" CodeBehind="SignIN.aspx.cs" Inherits="ProjectoFinal_Cinel_2024.Pages.MainPages.SignIN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">


    <div class="card-log">
        <div class="card-log2">
            <div class="form-log">
                <p id="heading">Login</p>
                <div class="field">
                    <svg
                        viewBox="0 0 16 16"
                        fill="currentColor"
                        height="16"
                        width="16"
                        xmlns="http://www.w3.org/2000/svg"
                        class="input-icon">
                        <path
                            d="M13.106 7.222c0-2.967-2.249-5.032-5.482-5.032-3.35 0-5.646 2.318-5.646 5.702 0 3.493 2.235 5.708 5.762 5.708.862 0 1.689-.123 2.304-.335v-.862c-.43.199-1.354.328-2.29.328-2.926 0-4.813-1.88-4.813-4.798 0-2.844 1.921-4.881 4.594-4.881 2.735 0 4.608 1.688 4.608 4.156 0 1.682-.554 2.769-1.416 2.769-.492 0-.772-.28-.772-.76V5.206H8.923v.834h-.11c-.266-.595-.881-.964-1.6-.964-1.4 0-2.378 1.162-2.378 2.823 0 1.737.957 2.906 2.379 2.906.8 0 1.415-.39 1.709-1.087h.11c.081.67.703 1.148 1.503 1.148 1.572 0 2.57-1.415 2.57-3.643zm-7.177.704c0-1.197.54-1.907 1.456-1.907.93 0 1.524.738 1.524 1.907S8.308 9.84 7.371 9.84c-.895 0-1.442-.725-1.442-1.914z">
                        </path>
                    </svg>

                    <asp:TextBox ID="tb_email_utilizador" class="input-field" placeholder="@Email" autocomplete="off" runat="server" TextMode="Email"></asp:TextBox>
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
                    <asp:TextBox ID="tb_pass_utilizador" class="input-field" placeholder="Password" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="btns">

                    <asp:Button ID="btn_Login" class="btn-form1" runat="server" Text="Login" OnClick="btn_Login_Click" />

                    <a href="/Pages/MainPages/GooglePage.aspx" id="Gmail_Login" class="btn-form4">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            x="0px"
                            y="0px"
                            width="100"
                            height="100"
                            viewBox="0 0 48 48"
                            class="input-icon">
                            <path
                                fill="#fbc02d"
                                d="M43.611,20.083H42V20H24v8h11.303c-1.649,4.657-6.08,8-11.303,8c-6.627,0-12-5.373-12-12	s5.373-12,12-12c3.059,0,5.842,1.154,7.961,3.039l5.657-5.657C34.046,6.053,29.268,4,24,4C12.955,4,4,12.955,4,24s8.955,20,20,20	s20-8.955,20-20C44,22.659,43.862,21.35,43.611,20.083z">
                            </path>
                            <path
                                fill="#e53935"
                                d="M6.306,14.691l6.571,4.819C14.655,15.108,18.961,12,24,12c3.059,0,5.842,1.154,7.961,3.039	l5.657-5.657C34.046,6.053,29.268,4,24,4C16.318,4,9.656,8.337,6.306,14.691z">
                            </path>
                            <path
                                fill="#4caf50"
                                d="M24,44c5.166,0,9.86-1.977,13.409-5.192l-6.19-5.238C29.211,35.091,26.715,36,24,36	c-5.202,0-9.619-3.317-11.283-7.946l-6.522,5.025C9.505,39.556,16.227,44,24,44z">
                            </path><path fill="#1565c0" d="M43.611,20.083L43.595,20L42,20H24v8h11.303c-0.792,2.237-2.231,4.166-4.087,5.571	c0.001-0.001,0.002-0.001,0.003-0.002l6.19,5.238C36.971,39.205,44,34,44,24C44,22.659,43.862,21.35,43.611,20.083z"></path>
                        </svg>
                        Gmail </a>
                    <a href="\Pages\MainPages\SignUP.aspx" class="btn-form2">Sign Up</a>

                </div>
                <asp:Button ID="btn_Recupera" class="btn-form3" runat="server" Text="Recuperar Password" />

            </div>
        </div>
    </div>
    <hr />
    <asp:Label ID="lb_Mensagem" runat="server" Text=""></asp:Label>




</asp:Content>
