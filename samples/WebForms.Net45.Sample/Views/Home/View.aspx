<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="WebForms.Net45.Sample.Views.Home.View" %>
<%@ Register TagPrefix="ctrl" TagName="SampleControlWithoutView" Src="~/Views/Home/SampleControlWithoutView.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p>Page:</p>
        <asp:TextBox ID="Element" runat="server" OnTextChanged="TextBox_TextChanged" AutoPostBack="True" />
        <p>Textbox value is <%= this.Model.Text %></p>
        <ctrl:SampleControlWithoutView runat="server" />
    </div>
</asp:Content>
