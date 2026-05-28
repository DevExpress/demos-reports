<%@ Control Language="C#" CodeBehind="~/UserControls/EventMonitor.ascx.cs" Inherits="DevExpress.Web.Demos.UserControls_EventMonitor" AutoEventWireup="True" %>
<script type="text/javascript">
        
    function ClearButton_Click() {
        DXEventMonitor.Clear();
    }
</script>
<div class="EventLogPanel">
    <div id="EventLog" <%= GetStyleAttribute() %>></div>
    <dx:ASPxButton runat="server" ID="ClearButton" AutoPostBack="false" Text="Clear">
        <ClientSideEvents Click="ClearButton_Click" />
    </dx:ASPxButton>
</div>
<% if(ShowEventListPanel) { %>
<div id="EventListContainer" class="EventListPanel" <%= GetScrollHeight() %> >
    <div>Trace Events:</div>
    <asp:Repeater runat="server" ID="Repeater">
        <ItemTemplate>            
            <input type="checkbox" id="EventCheck<%# Container.DataItem %>" checked="checked" />
            <label for="EventCheck<%# Container.DataItem %>"><%# Container.DataItem.ToString().Replace("_"," ") %></label>
            <br />
        </ItemTemplate>
    </asp:Repeater>
</div>     
<% } %>       
