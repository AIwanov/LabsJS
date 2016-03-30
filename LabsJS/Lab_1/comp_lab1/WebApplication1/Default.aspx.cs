using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        }
        public class Parameter
        {
            [XmlElement("Name")]
            public string name;
            [XmlElement("Description")]
            public string description;
            [XmlElement("Value")]
            public string value;

            public Parameter()
            {
                name = "";
                description = "";
                value = "";
            }

            public Parameter(string _name, string _description, string _value)
            {
                name = _name;
                description = _description;
                value = _value;
            }
            public string parName
            {
                get { return name; }
                set { name = value; }
            }
            public string parDescription
            {
                get { return description; }
                set { description = value; }
            }
            public string parValue
            {
                get { return this.value; }
                set { this.value = value; }
            }
        }

        public Parameters data;
        protected void Page_Load(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Parameters));

            FileStream fs = new FileStream("Input.xml", FileMode.Open);

            XmlReader reader = XmlReader.Create(fs);

            data = (Parameters)serializer.Deserialize(reader);
            fs.Close();

            this.DataBind();
        }
    }
}
