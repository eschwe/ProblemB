using System;
using System.IO;
using System.Xml;

namespace SportsBallService
{
    class SportsBallXmlProvider : ISportsBallService
    {
        private XmlDocument[] XmlDocs;
        private string DataSource;

        public SportsBallXmlProvider(string dataSource)
        {
            DataSource = dataSource;
        }

        public string GetMascotNameFromTeamName(string teamName)
        {
            string mascot = "unknown";

            if (XmlDocs == null)
            {
                try
                {
                    string[] files = Directory.GetFiles(DataSource);
                    XmlDocs = new XmlDocument[files.Length];
                    for (int i = 0; i < files.Length; i++)
                    {
                        XmlDocs[i] = new XmlDocument();
                        XmlDocs[i].Load(files[i]);
                    }
                }
                catch (Exception e)
                {
                    return "ERROR: Xml data could not be loaded: " + e.Message;
                }
            }

            try
            {
                foreach (XmlDocument xml in XmlDocs)
                {
                    XmlNode node = xml.DocumentElement.FirstChild;

                    do
                    {
                        if (node.Attributes[0].Value.ToLower() == teamName.ToLower())
                        {
                            mascot = node.Attributes[1].Value;
                        }
                        node = node.NextSibling;
                    }
                    while (node != null);
                }
            }
            catch (Exception e)
            {
                return "ERROR: Unknown error in XML data: " + e.Message;
            }

            return mascot;
        }
    }
}
