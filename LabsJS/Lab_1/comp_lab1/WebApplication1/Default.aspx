<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="comp_lab1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--<script runat="server">
protected void get_list()
{
    for (int i = 0; i < data.parameters.Count; i++)
    {
        dataGrid1.Items[i].Cells[0].Text = data.parameters[i].name;
        dataGrid1.Items[i].Cells[1].Text = data.parameters[i].description;
        dataGrid1.Items[i].Cells[2].Text = data.parameters[i].value;
    }
}
</script>  -->
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <!--<asp:DataGrid id="dataGrid1" runat="server"></asp:DataGrid>-->
        <div>
            <asp:TextBox ID="name" runat="server" Text="<%# data.parameters[0].parName %>" />
            <asp:TextBox ID="description" runat="server" Text="<%# data.parameters[0].parDescription %>" />
            <asp:TextBox ID="value" runat="server" Text="<%# data.parameters[0].parValue %>" />
        </div>
        <div>
            <asp:TextBox ID="name1" runat="server" Text="<%# data.parameters[1].parName %>" />
            <asp:TextBox ID="description1" runat="server" Text="<%# data.parameters[1].parDescription %>" />
            <asp:TextBox ID="value1" runat="server" Text="<%# data.parameters[1].parValue %>" />
        </div>
    </form>
</body>
</html>
