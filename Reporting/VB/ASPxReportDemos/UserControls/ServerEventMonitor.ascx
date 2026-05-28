<%@ Control Language="vb" CodeBehind="~/UserControls/ServerEventMonitor.ascx.vb" Inherits="DevExpress.Web.Demos.UserControls_ServerEventMonitor" AutoEventWireup="True" %>
<script runat="server">



</script>

<div class="EventLogPanel">
    <div runat="server" id="EventLog" class="EventLog"></div>
    <dx:ASPxButton runat="server" ID="ClearButton" Text="Clear" OnClick="ClearButton_Click"/>    
</div>
<div class="EventListPanel">
    <div>Trace Events:</div>
    <asp:Repeater runat="server" ID="Repeater">
        <ItemTemplate>          
            <dx:ASPxCheckBox AutoPostBack="true" ID="CheckBox" runat="server" Text="<%#Container.DataItem%>" Checked="true"/>
        </ItemTemplate>
    </asp:Repeater>
</div>