using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.ComponentModel;

namespace comp_lab1
{
    public partial class _Default : System.Web.UI.Page
    {
        [XmlRoot("Parameters")]
        public class Parameters
        {
            [XmlElement("Parameter")]
            public List<Parameter> parameters;
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
                type = "";
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

        public static List<Panel> list_panel;
        protected void load_parameters_from_xml()
        {
            if (list_panel == null)
                list_panel = new List<Panel>();
            if (list_panel.Count > 0)
                list_panel.Clear();

            XmlSerializer serializer = new XmlSerializer(typeof(Parameters));

            FileStream fs = new FileStream("Input.xml", FileMode.Open);

            XmlReader reader = XmlReader.Create(fs);

            Parameters data;
            data = (Parameters)serializer.Deserialize(reader);

            for (int i = 0; i < data.parameters.Count; i++)
            {
                Panel text_panel = new Panel();
                text_panel.ID = string.Format("panel{0}", i);

                TextBox parName = new TextBox();
                parName.ID = string.Format("parName{0}", i);
                parName.Text = data.parameters[i].Name;

                TextBox parDescription = new TextBox();
                parDescription.ID = string.Format("parDescription{0}", i);
                parDescription.Text = data.parameters[i].Description;

                text_panel.Controls.Add(parName);
                text_panel.Controls.Add(parDescription);

                DropDownList list = new DropDownList();
                list.ID = string.Format("type{0}", i);
                list.Items.Add("String");
                list.Items.Add("Boolean");
                list.Items.Add("Int32");

                switch (data.parameters[i].Type)
                {
                    case "String":
                        {
                            list.SelectedIndex = 0;
                            text_panel.Controls.Add(list);
                            TextBox parValue = new TextBox();
                            parValue.ID = string.Format("parValue{0}", i);
                            parValue.Text = data.parameters[i].Value;
                            text_panel.Controls.Add(parValue);
                            break;
                        }
                    case "Boolean":
                        {
                            list.SelectedIndex = 1;
                            text_panel.Controls.Add(list);
                            CheckBox parValue = new CheckBox();
                            parValue.ID = string.Format("parValue{0}", i);
                            parValue.Checked = data.parameters[i].Value == "True";
                            text_panel.Controls.Add(parValue);
                            break;
                        }
                    case "Int32":
                        {
                            list.SelectedIndex = 2;
                            text_panel.Controls.Add(list);
                            TextBox parValue = new TextBox();
                            parValue.ID = string.Format("parValue{0}", i);
                            parValue.Text = data.parameters[i].Value;

                            RegularExpressionValidator validator = new RegularExpressionValidator();
                            validator.ID = string.Format("validator{0}", i);
                            validator.ControlToValidate = parValue.ID;
                            validator.ValidationExpression = "[0-9]";

                            text_panel.Controls.Add(parValue);
                            text_panel.Controls.Add(validator);
                            break;
                        }
                    default:
                        break;
                }
                Button delete = new Button();
                delete.Text = "Удалить";
                delete.ID = string.Format("button_delete{0}", i);

                delete.Click += delete_Click;
                text_panel.Controls.Add(delete);

                list_panel.Add(text_panel);
            }
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
            if (list_panel == null || list_panel.Count == 0)
                load_parameters_from_xml();
            load_controls_by_form(sender, e);
            base.OnInit(e);
        }

        private void load_controls_by_form(object sender, EventArgs e)
        {
            panel.Controls.Clear();
            for (int i = 0; i < list_panel.Count; i++)
            {
                panel.Controls.Add(list_panel[i]);
            }
            add_buttons();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int index = list_panel.Count + 1;

            Panel new_parameter = new Panel();
            new_parameter.ID = string.Format("panel{0}", index);

            TextBox parName = new TextBox();
            parName.ID = string.Format("parName{0}", index);

            TextBox parDescription = new TextBox();
            parDescription.ID = string.Format("parDescription{0}", index);

            DropDownList list = new DropDownList();
            list.ID = string.Format("type{0}", index);
            list.Items.Add("String");
            list.Items.Add("Boolean");
            list.Items.Add("Int32");

            TextBox parValue = new TextBox();
            parValue.ID = string.Format("parValue{0}", index);

            Button delete = new Button();
            delete.Text = "Удалить";
            delete.ID = string.Format("button_delete{0}", index);

            delete.Click += delete_Click;

            new_parameter.Controls.Add(parName);
            new_parameter.Controls.Add(parDescription);
            new_parameter.Controls.Add(list);
            new_parameter.Controls.Add(parValue);

            new_parameter.Controls.Add(delete);

            list_panel.Add(new_parameter);
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Parameters));
            TextWriter writer = new StreamWriter("Input.xml");

            Parameters data = new Parameters();
            data.parameters = new List<Parameter>();
            for (int i = 0; i < list_panel.Count; i++)
            {
                Parameter parametr = new Parameter();
                for (int j = 0; j < list_panel[i].Controls.Count; j++)
                {
                    Type type_ = list_panel[i].Controls[j].GetType();
                    if (type_ == typeof(TextBox))
                    {
                        TextBox t = (TextBox)(list_panel[i].Controls[j]);
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
                        DropDownList t = (DropDownList)(list_panel[i].Controls[j]);
                        parametr.Type = t.SelectedValue;
                    }
                    if (type_ == typeof(CheckBox))
                    {
                        CheckBox t = (CheckBox)(list_panel[i].Controls[j]);
                        parametr.Value = t.Checked.ToString();
                    }
                }
                data.parameters.Add(parametr);
            }
            serializer.Serialize(writer, data);
            writer.Close();
        }

        private void delete_Click(object sender, EventArgs e)
        {
        }

        //protected void Page_Closed(object sender, EventArgs e)
        //{
        //    list_panel.Clear();
        //}
    }
}