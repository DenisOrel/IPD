
// Type: Intermech.Interfaces.XmlUtil
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Interfaces
{
    public class XmlUtil
    {
      private XmlDocument doc = new XmlDocument();

      private XmlElement GetElement(string akey, XmlNode axmlNodeAppSettings, bool createIfNull)
      {
        XmlElement newChild = (XmlElement) axmlNodeAppSettings.SelectSingleNode($"add[@key='{akey}']");
        if (newChild == null & createIfNull)
        {
          newChild = this.doc.CreateElement("add");
          newChild.SetAttribute("key", akey);
          axmlNodeAppSettings.AppendChild((XmlNode) newChild);
        }
        return newChild;
      }

      private bool WriteXml(string xmlFileName)
      {
        try
        {
          this.doc.Load(xmlFileName);
        }
        catch
        {
          return true;
        }
        XmlNode axmlNodeAppSettings = this.doc.SelectSingleNode("//configuration//appSettings");
        if (axmlNodeAppSettings == null)
          return false;
        this.GetElement("ConnectionName", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.ServerTypeConnString);
        if (Consts.IsMSSQL())
          this.GetElement("ConnectionString.Server.SQL", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.BasicConnString);
        if (Consts.IsOracle())
          this.GetElement("ConnectionString.Server.Oracle", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.BasicConnString);
        if (Consts.IsLinter())
          this.GetElement("ConnectionString.Server.Linter", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.BasicConnString);
        if (Consts.IsPostgreSQL())
          this.GetElement("ConnectionString.Server.PostgreSQL", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.BasicConnString);
        this.GetElement("UsePassword", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.UserPasswordConnString);
        this.GetElement("User ID", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.UserIDString);
        this.GetElement("Password", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.PasswordString);
        this.GetElement("PortalName", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.PortalName);
        this.GetElement("PortalUrl", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.PortalUrl);
        this.GetElement("SiteGuid", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.SiteGuid);
        this.GetElement("SiteCode", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.SiteCode);
        this.GetElement("ProxyAddress", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.ProxyAddress);
        this.GetElement("ProxyPort", axmlNodeAppSettings, true).SetAttribute("value", Convert.ToString(ImpExpClass.ProxyPort));
        this.GetElement("PortalReplicUserName", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.ReplicPortalUser.Name);
        this.GetElement("PortalReplicLogin", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.ReplicPortalUser.Login);
        this.GetElement("PortalReplicPassword", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.ReplicPortalUser.Password);
        this.GetElement("PortalAdminUserName", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.AdminPortalUser.Name);
        this.GetElement("PortalAdminLogin", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.AdminPortalUser.Login);
        this.GetElement("PortalAdminPassword", axmlNodeAppSettings, true).SetAttribute("value", ImpExpClass.AdminPortalUser.Password);
        try
        {
          this.doc.Save((XmlWriter) new XmlTextWriter(xmlFileName, (Encoding) null)
          {
            Formatting = Formatting.Indented
          });
        }
        catch
        {
          return false;
        }
        return true;
      }

      private bool ReadXml(string xmlFileName)
      {
        try
        {
          this.doc.Load(xmlFileName);
        }
        catch
        {
          return false;
        }
        XmlNode axmlNodeAppSettings = this.doc.SelectSingleNode("//configuration//appSettings");
        if (axmlNodeAppSettings == null)
          return false;
        XmlElement element1 = this.GetElement("ConnectionName", axmlNodeAppSettings, false);
        if (element1 == null)
          return false;
        ImpExpClass.ServerTypeConnString = element1.GetAttribute("value");
        if (ImpExpClass.ServerTypeConnString.ToUpper() == "Server.Oracle".ToUpper())
          Consts.RDBMS = RDBMSList.Oracle;
        else if (ImpExpClass.ServerTypeConnString.ToUpper() == "Server.SQL".ToUpper())
          Consts.RDBMS = RDBMSList.MSSQL;
        else if (ImpExpClass.ServerTypeConnString.ToUpper() == "Server.Linter".ToUpper())
        {
          Consts.RDBMS = RDBMSList.Linter;
        }
        else
        {
          if (!(ImpExpClass.ServerTypeConnString.ToUpper() == "Server.PostgreSQL".ToUpper()))
            return false;
          Consts.RDBMS = RDBMSList.PostgreSQL;
        }
        XmlElement element2 = this.GetElement("ConnectionString.Server.SQL", axmlNodeAppSettings, false);
        if (element2 != null)
          ImpExpClass.BasicMSSQLConnString = element2.GetAttribute("value");
        XmlElement element3 = this.GetElement("ConnectionString.Server.Oracle", axmlNodeAppSettings, false);
        if (element3 != null)
          ImpExpClass.BasicOracleConnString = element3.GetAttribute("value");
        XmlElement element4 = this.GetElement("ConnectionString.Server.Linter", axmlNodeAppSettings, false);
        if (element4 != null)
          ImpExpClass.BasicLinterConnString = element4.GetAttribute("value");
        XmlElement element5 = this.GetElement("ConnectionString.Server.PostgreSQL", axmlNodeAppSettings, false);
        if (element5 != null)
          ImpExpClass.BasicPostgreSQLConnString = element5.GetAttribute("value");
        XmlElement element6 = this.GetElement("UsePassword", axmlNodeAppSettings, false);
        if (element6 == null)
          return false;
        ImpExpClass.UserPasswordConnString = element6.GetAttribute("value");
        XmlElement element7 = this.GetElement("User ID", axmlNodeAppSettings, false);
        if (element7 == null)
          return false;
        ImpExpClass.UserIDString = element7.GetAttribute("value");
        Consts.SystemAdmin = element7.GetAttribute("value");
        XmlElement element8 = this.GetElement("Password", axmlNodeAppSettings, false);
        if (element8 == null)
          return false;
        ImpExpClass.PasswordString = element8.GetAttribute("value");
        XmlElement element9 = this.GetElement("PortalName", axmlNodeAppSettings, false);
        if (element9 != null)
          ImpExpClass.PortalName = element9.GetAttribute("value");
        XmlElement element10 = this.GetElement("PortalUrl", axmlNodeAppSettings, false);
        if (element10 != null)
          ImpExpClass.PortalUrl = element10.GetAttribute("value");
        XmlElement element11 = this.GetElement("SiteGuid", axmlNodeAppSettings, false);
        if (element11 != null)
          ImpExpClass.SiteGuid = element11.GetAttribute("value");
        XmlElement element12 = this.GetElement("SiteCode", axmlNodeAppSettings, false);
        if (element12 != null)
          ImpExpClass.SiteCode = element12.GetAttribute("value");
        XmlElement element13 = this.GetElement("ProxyAddress", axmlNodeAppSettings, false);
        if (element13 != null)
          ImpExpClass.ProxyAddress = element13.GetAttribute("value");
        XmlElement element14 = this.GetElement("ProxyPort", axmlNodeAppSettings, false);
        if (element14 != null)
        {
          int result;
          ImpExpClass.ProxyPort = int.TryParse(element14.GetAttribute("value"), out result) ? result : 0;
        }
        XmlElement element15 = this.GetElement("PortalReplicUserName", axmlNodeAppSettings, false);
        if (element15 != null)
          ImpExpClass.ReplicPortalUser.Name = element15.GetAttribute("value");
        XmlElement element16 = this.GetElement("PortalReplicLogin", axmlNodeAppSettings, false);
        if (element16 != null)
          ImpExpClass.ReplicPortalUser.Login = element16.GetAttribute("value");
        XmlElement element17 = this.GetElement("PortalReplicPassword", axmlNodeAppSettings, false);
        if (element17 != null)
          ImpExpClass.ReplicPortalUser.Password = element17.GetAttribute("value");
        XmlElement element18 = this.GetElement("PortalAdminUserName", axmlNodeAppSettings, false);
        if (element18 != null)
          ImpExpClass.AdminPortalUser.Name = element18.GetAttribute("value");
        XmlElement element19 = this.GetElement("PortalAdminLogin", axmlNodeAppSettings, false);
        if (element19 != null)
          ImpExpClass.AdminPortalUser.Login = element19.GetAttribute("value");
        XmlElement element20 = this.GetElement("PortalAdminPassword", axmlNodeAppSettings, false);
        if (element20 != null)
          ImpExpClass.AdminPortalUser.Password = element20.GetAttribute("value");
        return true;
      }

      public bool ReadConfigXmlFiles()
      {
        string directoryName = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        return this.ReadXml(directoryName + "\\ConsoleServer.exe.config") || this.ReadXml(directoryName + "\\Intermech.Server.Service.exe.config");
      }

      public bool WriteConfigXmlFiles()
      {
        string directoryName = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        return this.WriteXml(directoryName + "\\ConsoleServer.exe.config") & this.WriteXml(directoryName + "\\Intermech.Server.Service.exe.config");
      }
    }
}
