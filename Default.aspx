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
    function onKeyDownNum(event, field) {
        var cursor = event.target.selectionStart
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
                        field.value = old_value;
                        field.selectionStart = cursor
                        field.selectionEnd = cursor
                        return
                    }
                }
                field.value = str_value;
                if (str_value != "-")
                    field.setAttribute("data-oldValue", str_value)
            }
        }
        setTimeout(handle, 0);
    }

    function onKeyDownString(field) {
        var cursor = event.target.selectionStart
        var old_value = field.value
        function handle() {
            var str_value = field.value;
            if (str_value.length > 10) {
                field.value = old_value;
                field.selectionStart = cursor
                field.selectionEnd = cursor
            }
        }
        setTimeout(handle, 0);
    }

    function onBlurEvent(event, field) {
        function handle() {
            if (field.value.length == 1 && field.value == "-") {
                field.value = field.getAttribute("data-oldValue");
            }
        }
        setTimeout(handle, 0);
    }
</script>

