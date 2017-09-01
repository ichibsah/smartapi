<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="fullPublish._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Full publish - (All Master pages)</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Select Project</h2>
            <p>
                All master pages in the selected project will be published.
            </p>
            <p>

                <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" ReadOnly="True"></asp:TextBox>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Select Language</h2>
            <p>
                This choice applies to all pages.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Select Variant</h2>
            <p>
                This choice applies to all pages.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">

        </div>
    </div>
</asp:Content>
