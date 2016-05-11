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
    function onKeyDownNum(field) {
        function handle() {
            var old_value = field.getAttribute("data-oldValue")
            if (old_value != field.value) {
                var str_value = "";
                if (field.value.length == 1 && field.value == "-")
                    str_value = field.value;
                if (field.value.length && !str_value.length) {
                    try {
                        var int_value = parseInt(field.value);
                        if (isNaN(int_value) || field.value != int_value.toString() || int_value < -2147483648 || int_value > 2147483647) {
                            throw Error("Error");
                        }
                        str_value = int_value.toString();
                    }
                    catch (e) {
                        str_value = old_value
                        field.selectionStart = field.getAttribute("data-cursor")
                    }
                }
                field.setAttribute("data-oldValue", str_value)
                field.value = str_value;
            }
        }
        setTimeout(handle, 0);
    }

    function onKeyDownString(field) {
        function handle() {
            var str_value = field.value;
            if (str_value.length > 10) {
                field.value = field.value.substring(0, 10);
                onKeyDownString(field)
            }
        }
        setTimeout(handle, 0);
    }
    function onPast(field) {
        field.focus()
        field.setAttribute("data-oldValue", field.value)
        field.setAttribute("data-cursor", field.getSelection().toString())
    }
</script>

