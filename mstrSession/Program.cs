//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Xml;
using System.Net;
using System.Xml.XPath;

namespace mstrSession
{
    class Program
    {
        static void Main(string[] args)
        {
            string strFilePath = "http://localhost/MicroStrategy/asp/TaskProc.aspx?taskId=login&taskEnv=xml&taskContentType=xml&server=localhost&project=MicroStrategy+Tutorial&userid=Administrator&password=Administrator";
            XmlDocument loginXmlDocument = new XmlDocument();
            XmlUrlResolver loginXmlUrlResolver = new XmlUrlResolver();
            NetworkCredential loginNetworkCredential = new NetworkCredential("Davide", "vhjz342", "WIN-17MYOLPIZ1F");
            loginXmlUrlResolver.Credentials = loginNetworkCredential;
            loginXmlDocument.XmlResolver = loginXmlUrlResolver;
            loginXmlDocument.Load(strFilePath);

            string sessionStateParameter = loginXmlDocument.SelectNodes("/taskResponse/root/sessionState")[0].InnerText;
            //string xmlReportExecutionEvent = "evt=4001&src=mstrWeb.customXMLReport.4001";
            string reportID = "reportID=EB4EFA7E4C7DB41CC017B1908AEB0021";
            //
            // http://localhost/MicroStrategy/asp/TaskAdmin.aspx?taskId=reportDataService&taskEnv=html&taskContentType=html&server=localhost&project=MicroStrategy+Tutorial&userid=Administrator&password=Administrator&styleName=MobileHTMLGridStyle&reportID=743A98F045B5EF766121BEA692E9F6C7
            //
            string xmlReportExecution = "http://localhost/MicroStrategy/asp/TaskProc.aspx?taskId=reportExecute&taskEnv=xml&taskContentType=xml&sessionState=" + sessionStateParameter + "&" + reportID;
            XmlDocument executeXmlDocument = new XmlDocument();
            executeXmlDocument.XmlResolver = loginXmlUrlResolver;
            executeXmlDocument.Load(xmlReportExecution);
            string messageIDParameter = executeXmlDocument.SelectNodes("/taskResponse/msg/id")[0].InnerText;
        }
    }
}
