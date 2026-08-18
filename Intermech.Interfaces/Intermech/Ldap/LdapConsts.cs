
// Type: Intermech.Ldap.LdapConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Ldap
{
    public static class LdapConsts
    {
      public const string ModuleLdap = "CLIENT";
      public const string SectionLdap = "LDAP";
      /// <summary>Режим разработчика для LDAP</summary>
      public const string ParamLdapDeveloperMode = "LDAP_DEVELOPER_MODE";
      public const bool ParamLdapDeveloperModeDefault = false;
      /// <summary>Имя файла в настройках</summary>
      public static readonly string ConfigName = "Ldap.Settings";
      public static readonly string DBID = "ID";
      public static readonly string ADUser = "USER";
      public static readonly string ADGroup = "GROUP";
      public static readonly string ADDisplayName = "DISPLAYNAME";
      public static readonly string ADDescription = "DESCRIPTION";
      public static readonly string ADSAMAccountName = "SAMACCOUNTNAME";
      public static readonly string ADObjectSID = "OBJECTSID";
      public static readonly string _SearchResult_ = "_SEARCHRESULT_";
      public static readonly int IpsStatusSubitemIndex = 3;
      public static readonly string xmlConfiguration = "configuration";
      public static readonly string xmlCatalog = "catalog";
      public static readonly string xmlName = "name";
      public static readonly string xmlExclusions = "exclusions";
      public static readonly string xmlUser = "user";
      public static readonly string xmlSID = "sid";
      public static readonly string xmlDefaultCatalog = "defaultcatalog";
      /// <summary>first name</summary>
      public static readonly string SearchResultGivenName = "givenName";
      /// <summary>last name</summary>
      public static readonly string SearchResultSN = "sn";
      /// <summary>smtp mail address</summary>
      public static readonly string SearchResultMail = "mail";
      /// <summary>Address-Home attribute домашний адрес -&gt;  02dc</summary>
      public static readonly string SearchResultHomePostalAddress = "homePostalAddress";
      /// <summary>
      /// Telephone-Number attribute телефонный номер - в IPS в "служебный телефон"
      /// </summary>
      public static readonly string SearchResultTelephoneNumber = "telephoneNumber";
      /// <summary>Address attribute почтовый адрес. -&gt; 015dd</summary>
      public static readonly string SearchResultPostalAddress = "streetAddress";
      /// <summary>
      /// Phone-Home-Primary attribute домашний телефонный номер -&gt; 02dd
      /// </summary>
      public static readonly string SearchResultHomePhone = "homePhone";
      /// <summary>
      /// Phone-Mobile-Primary attribute мобильный телефонный номер -&gt; 015df
      /// </summary>
      public static readonly string SearchResultMobilePhone = "mobile";
      /// <summary>Physical-Delivery-Office-Name attribute</summary>
      public static readonly string SearchResultPhysicalDeliveryOfficeName = "physicalDeliveryOfficeName";
    }
}
