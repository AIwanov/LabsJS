using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace comp_lab1
{
    public partial class _Default : System.Web.UI.Page
    {
        [XmlRoot("Parameters")]
        public class Parameters
        {
            [XmlElement("Parameter")]
            public List<Parameter> parameters;

            public Parameters()
            {
                parameters = new List<Parameter>();
            }
        }
        public class Parameter
        {
            [XmlElement("Name")]
            private string name;
            [XmlElement("Description")]
            private string description;
            [XmlElement("Type")]
            private string type;
            [XmlElement("Value")]
            private string value;

            public Parameter()
            {
                name = "";
                description = "";
                type = "String";
                value = "";
            }

            public Parameter(string _name, string _description, string _type, string _value)
            {
                name = _name;
                description = _description;
                type = _type;
                value = _value;
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string Description
            {
                get { return description; }
                set { description = value; }
            }
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            public string Value
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        public static Parameters data;
        protected void load_parameters_from_xml()
        {
            if (data == null)
                data = new Parameters();
            if (data.parameters.Count > 0)
                data.parameters.Clear();

            XmlSerializer serializer = new XmlSerializer(typeof(Parameters));

            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/Input.xml", FileMode.Open);

            XmlReader reader = XmlReader.Create(fs);

            data = (Parameters)serializer.Deserialize(reader);

            fs.Close();
        }

        protected void add_buttons()
        {
            Button add = new Button();
            add.Text = "Добавить";
            add.ID = "button_add";

            add.Click += button_add_Click;
            add.Click += load_controls_by_form;

            Button save = new Button();
            save.Text = "Сохранить";
            save.ID = "button_save";

            save.Click += button_save_Click;

            panel.Controls.Add(add);
            panel.Controls.Add(save);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                load_parameters_from_xml();
            load_controls_by_form(sender, e);
        }

        private void load_controls_by_form(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            Table table = new Table();
            for (int i = 0; i < data.parameters.Count; i++)
            {
                TableRow row = new TableRow();

                TextBox parName = new TextBox();
                parName.ID = string.Format("parName{0}", i);
                parName.Text = data.parameters[i].Name;

                TextBox parDescription = new TextBox();
                parDescription.ID = string.Format("parDescription{0}", i);
                parDescription.Text = data.parameters[i].Description;

                TableCell new_cell_name = new TableCell();
                TableCell new_cell_description = new TableCell();

                new_cell_name.Controls.Add(parName);
                new_cell_description.Controls.Add(parDescription);

                row.Cells.Add(new_cell_name);
                row.Cells.Add(new_cell_description);

                DropDownList list = new DropDownList();
                list.ID = string.Format("type{0}", i);
                list.Items.Add("String");
                list.Items.Add("Boolean");
                list.Items.Add("Int32");

                TableCell new_cell_1 = new TableCell();
                TableCell new_cell_2 = new TableCell();
                Control parValue = new Control();
                switch (data.parameters[i].Type)
                {
                    case "String":
                        {
                            list.SelectedIndex = 0;
                            TextBox text_box = new TextBox();
                            text_box.ID = string.Format("parValue{0}", i);
                            text_box.Text = data.parameters[i].Value;
                            parValue = text_box;
                            break;
                        }
                    case "Boolean":
                        {
                            list.SelectedIndex = 1;
                            CheckBox check_box = new CheckBox();
                            check_box.ID = string.Format("parValue{0}", i);
                            bool flag_checked = false;
                            Boolean.TryParse(data.parameters[i].Value, out flag_checked);
                            check_box.Checked = flag_checked;
                            parValue = check_box;
                            break;
                        }
                    case "Int32":
                        {
                            list.SelectedIndex = 2;
                            TextBox text_box = new TextBox();
                            text_box.ID = string.Format("parValue{0}", i);
                            int value;
                            text_box.Text = int.TryParse(data.parameters[i].Value, out value) ? data.parameters[i].Value : "";
                            text_box.Attributes.Add("onkeydown", "onKeyUp(this)");
                            text_box.Attributes.Add("runat", "server");

                            parValue = text_box;
                            break;
                        }
                    default:
                        break;
                }
                list.SelectedIndexChanged += new EventHandler(list_TextChanged);
                list.AutoPostBack = true;

                new_cell_1.Controls.Add(list);
                row.Cells.Add(new_cell_1);
                new_cell_2.Controls.Add(parValue);
                row.Cells.Add(new_cell_2);

                Button delete = new Button();
                delete.Text = "Удалить";
                delete.ID = string.Format("button_delete{0}", i);

                delete.Click += new EventHandler(delete_Click);
                TableCell new_cell = new TableCell();
                new_cell.Controls.Add(delete);
                row.Cells.Add(new_cell);

                table.Rows.Add(row);

                panel.Controls.Add(table);
            }
            add_buttons();
        }

        void text_box_TextChanged(object sender, EventArgs e)
        {
            read_from_form();
            TextBox text_box;
            text_box = (TextBox)sender;
            int value;
            if (!int.TryParse(text_box.Text, out value))
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "Symbol is not a number");
        }

        private void list_TextChanged(object sender, EventArgs e)
        {
            read_from_form();
            DropDownList list;
            list = (DropDownList)sender;
            data.parameters[int.Parse(list.ID.Replace("type", ""))].Type = list.SelectedValue;
            base.OnLoad(e);
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            read_from_form();
            Parameter new_parameter = new Parameter();
            data.parameters.Add(new_parameter);
            base.OnLoad(e);
        }

        private void read_from_form()
        {
            data.parameters.Clear();
            Table table = (Table)panel.Controls[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Parameter parametr = new Parameter();
                for (int j = 0; j < table.Rows[i].Cells.Count; j++)
                {
                    Type type_ = table.Rows[i].Cells[j].Controls[0].GetType();
                    if (type_ == typeof(TextBox))
                    {
                        TextBox t = (TextBox)(table.Rows[i].Cells[j].Controls[0]);
                        if (t.ID.Contains("parName"))
                        {
                            parametr.Name = t.Text;
                        }
                        if (t.ID.Contains("parValue"))
                        {
                            parametr.Value = t.Text;
                        }
                        if (t.ID.Contains("parDescription"))
                        {
                            parametr.Description = t.Text;
                        }
                        if (t.ID.Contains("type"))
                        {
                            parametr.Type = t.Text;
                        }
                    }
                    if (type_ == typeof(DropDownList))
                    {
                        DropDownList t = (DropDownList)(table.Rows[i].Cells[j].Controls[0]);
                        parametr.Type = t.SelectedValue;
                    }
                    if (type_ == typeof(CheckBox))
                    {
                        CheckBox t = (CheckBox)(table.Rows[i].Cells[j].Controls[0]);
                        parametr.Value = t.Checked.ToString();
                    }
                }
                data.parameters.Add(parametr);
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Parameters));
            TextWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Output.xml");
            read_from_form();
            serializer.Serialize(writer, data);
            writer.Close();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            read_from_form();
            Button b;
            b = (Button)sender;
            data.parameters.RemoveAt(int.Parse(b.ID.Replace("button_delete", "")));
            base.OnLoad(e);
        }
    }
}