namespace University_System.Helpers
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public static class XmlExtension
    {
        public static string Serialize<T>(this T value)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(
                    stringWriter, 
                    new XmlWriterSettings
                {
                    //Encoding = Encoding.UTF8,
                    OmitXmlDeclaration = true,
                    Indent = true,
                    NewLineOnAttributes = false
                })) 
                {

                    xmlSerializer.Serialize(xmlWriter, value);    
                    return stringWriter.ToString();
                }
            }
        }

        public static T Deserialize<T>(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T deserializer;

            using (XmlReader reader = XmlReader.Create( new StreamReader(path, Encoding.UTF8)))
            {
                return deserializer = (T)xmlSerializer.Deserialize(reader);
            }
        }
    } 
}
