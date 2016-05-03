<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="comp_lab1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Panel id="panel" runat="server"></asp:Panel>
        </div>
    </form>
</body>
</html>
<script type='text/javascript'>
   function onKeyUp(field) {
   function handle(){
       var str_value = "";
       if (field.value.length == 1 && field.value == "-")
           str_value = field.value;
       if (field.value.length && !str_value.length) {
           try {
               var int_value = parseInt(field.value);
               if (isNaN(int_value)) {
                   throw Error("Error");
               }
               str_value = int_value.toString();
           }
           catch (e) {
               str_value = field.value.substring(0, field.value.length - 1);
               onKeyUp(field)
           }
       }
       field.value = str_value;
       }
       setTimeout(handle, 0);
   }
</script>
