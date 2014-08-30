using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Core.Cmn.Extensions
{
    public class SerializationExtensions
    {

        public static XmlSerializerNamespaces GetNamespaces()
        {

            XmlSerializerNamespaces ns;
            ns = new XmlSerializerNamespaces();
            ns.Add("xs", "http://www.w3.org/2001/XMLSchema");
            ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            return ns;

        }

        public static string TargetNamespace
        {
            get
            {
                return "http://www.w3.org/2001/XMLSchema";
            }
        }

        public static string SerializeObjectIntoXML<T>(object tobeConvertToXMLObj)
        {
            var ser = new XmlSerializer(typeof(T), SerializationExtensions.TargetNamespace);
            var memStream = new MemoryStream();
            var xmlWriter = new XmlTextWriter(memStream, Encoding.UTF8);
            xmlWriter.Namespaces = true;
            ser.Serialize(xmlWriter, tobeConvertToXMLObj, GetNamespaces());
            xmlWriter.Close();
            memStream.Close();
            string xml;
            xml = Encoding.UTF8.GetString(memStream.GetBuffer());
            xml = xml.Substring(xml.IndexOf(Convert.ToChar(60)));
            xml = xml.Substring(0, (xml.LastIndexOf(Convert.ToChar(62)) + 1));
            return xml;
        }

        public static object DeSerializeXMLIntoObject<T>(string xmlConvertedString)
        {
            var ser = new XmlSerializer(typeof(T));
            var stringReader = new StringReader(xmlConvertedString);
            var xmlReader = new XmlTextReader(stringReader);
            object obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();

            return obj;
        }

        public static string StoreViewElementIntoXML(object viewElementComplexTypeObj)
        {
            var xs = new XmlSerializer(viewElementComplexTypeObj.GetType());

            var sw = new StringWriter();
            xs.Serialize(sw, viewElementComplexTypeObj);
            var serializedString = sw.ToString();
            using (var db = new SqlConnection(@"Data Source=.\SQL2012ENT;Initial Catalog=DB1;User Id=sa; Password=123456"))
            {
                db.Open();
                try
                {
                    using (var cmd = new SqlCommand("Update  ViewElements set XMLViewData=@Xml where Id=3 ", db))
                    {
                        cmd.Parameters.Add("@Xml", serializedString);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception excep)
                        {

                        }
                    }
                }
                finally { db.Close(); }
            }
            return serializedString;
        }


    }
}
