<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="hermes.Views.Home.Index" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <p>
    <form method="post" action='<%= Url.Action("AddRssFeed") %>'>
   <input type="text" name="feedUri" />
   <input type="submit" name="addTask" value="Add Feed" />
</form>
    </p>
</asp:Content>
