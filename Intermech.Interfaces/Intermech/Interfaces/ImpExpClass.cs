
// Type: Intermech.Interfaces.ImpExpClass
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Summary description for ImpExpClass.</summary>
    public class ImpExpClass
    {
      public static bool NewDatabaseMode = false;
      public static bool PatchDatabaseMode = false;
      public static string ConnectionString = "";
      public static string BasicOracleConnString = "";
      public static string BasicMSSQLConnString = "";
      public static string BasicLinterConnString = "";
      public static string BasicPostgreSQLConnString = "";
      public static string UserIDString = "";
      public static string UserPasswordConnString = "";
      public static string PasswordString = "";
      public static string ServerTypeConnString = "";
      public static PortalUserInfo ReplicPortalUser = new PortalUserInfo();
      public static PortalUserInfo AdminPortalUser = new PortalUserInfo();
      public static string PortalName = "";
      public static string PortalUrl = "";
      public static string SiteGuid = "";
      public static string SiteCode = "";
      public static string ProxyAddress = "";
      public static int ProxyPort = 0;

      public static string BasicConnString
      {
        get
        {
          if (Consts.IsMSSQL())
            return ImpExpClass.BasicMSSQLConnString;
          if (Consts.IsOracle())
            return ImpExpClass.BasicOracleConnString;
          if (Consts.IsLinter())
            return ImpExpClass.BasicLinterConnString;
          return Consts.IsPostgreSQL() ? ImpExpClass.BasicPostgreSQLConnString : string.Empty;
        }
        set
        {
          if (Consts.IsMSSQL())
            ImpExpClass.BasicMSSQLConnString = value;
          if (Consts.IsOracle())
            ImpExpClass.BasicOracleConnString = value;
          if (Consts.IsLinter())
            ImpExpClass.BasicLinterConnString = value;
          if (!Consts.IsPostgreSQL())
            return;
          ImpExpClass.BasicPostgreSQLConnString = value;
        }
      }
    }
}
