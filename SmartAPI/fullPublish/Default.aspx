<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="fullPublish._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Full publish - (All Master pages)</h1>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Project</h2>
            <p>
                All master pages in the current project will be published.
            </p>
            <p>

                <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Select Language</h2>
            <p>
                This choice applies to all pages.
            </p>
            <p>
                 <asp:CheckBoxList ID="CheckBoxLangVariant" runat="server" RepeatDirection="Vertical">
                </asp:CheckBoxList>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Select Variant</h2>
            <p>
                This choice applies to all pages.
            </p>
            <p>
                <asp:CheckBoxList ID="CheckBoxPrjVariant" runat="server" RepeatDirection="Vertical">
                </asp:CheckBoxList>
            </p>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <asp:TextBox ID="oConsole" runat="server" TextMode="MultiLine" Rows="5" ReadOnly="True" OnTextChanged="oConsole_TextChanged"></asp:TextBox>
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" />
</asp:Content>
