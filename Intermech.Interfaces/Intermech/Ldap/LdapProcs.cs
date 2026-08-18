
// Type: Intermech.Ldap.LdapProcs
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;


namespace Intermech.Ldap
{
    public static class LdapProcs
    {
      public static void InitSearcher(DirectorySearcher search)
      {
        search.PageSize = 1000;
        search.PropertiesToLoad.Clear();
        search.PropertiesToLoad.Add(LdapConsts.ADDisplayName.ToLower());
        search.PropertiesToLoad.Add(LdapConsts.ADDescription.ToLower());
        search.PropertiesToLoad.Add(LdapConsts.ADSAMAccountName.ToLower());
        search.PropertiesToLoad.Add(LdapConsts.ADObjectSID.ToLower());
        search.PropertiesToLoad.Add(LdapConsts.SearchResultGivenName);
        search.PropertiesToLoad.Add(LdapConsts.SearchResultSN);
        search.PropertiesToLoad.Add(LdapConsts.SearchResultMail);
        search.PropertiesToLoad.Add(LdapConsts.SearchResultHomePostalAddress);
        search.PropertiesToLoad.Add(LdapConsts.SearchResultTelephoneNumber);
        search.PropertiesToLoad.Add(LdapConsts.SearchResultPostalAddress);
        search.PropertiesToLoad.Add(LdapConsts.SearchResultHomePhone);
        search.PropertiesToLoad.Add(LdapConsts.SearchResultMobilePhone);
        search.PropertiesToLoad.Add(LdapConsts.SearchResultPhysicalDeliveryOfficeName);
      }

      public static string DomainNameToWinnt(string domainName) => "WinNT://" + domainName;

      public static string DomainNameToLdap(string domainName, bool users)
      {
        string[] strArray = domainName.Split('.');
        string ldap = "LDAP://" + (users ? "cn=Users," : string.Empty);
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (index > 0)
            ldap += ",";
          ldap = $"{ldap}DC={strArray[index]}";
        }
        return ldap;
      }

      /// <summary>Вернуть список подразделений в домене</summary>
      /// <param name="domainName"></param>
      /// <returns></returns>
      public static List<string> GetOUList(string domainName, SearchScope scope)
      {
        List<string> ouList = new List<string>();
        using (DirectorySearcher directorySearcher = new DirectorySearcher(LdapProcs.DomainNameToLdap(domainName, false)))
        {
          directorySearcher.SearchScope = scope;
          directorySearcher.PropertiesToLoad.Add("ou");
          directorySearcher.Filter = "(objectCategory=organizationalUnit)";
          foreach (SearchResult searchResult in directorySearcher.FindAll())
            ouList.Add(searchResult.Path);
        }
        return ouList;
      }

      /// <summary>Возвращает атрибуты пользователя AD</summary>
      /// <param name="personName"></param>
      /// <param name="path"></param>
      /// <returns></returns>
      public static SearchResult GetPersonADAttributes(string personName, DirectorySearcher search)
      {
        search.Filter = $"(&(objectCategory=person)(objectClass=user)(anr={personName}))";
        return search.FindOne();
      }

      /// <summary>Вернуть список компов текущего домена</summary>
      /// <returns>Список пар (Guid объекта AD)-(Имя компа)</returns>
      public static Dictionary<Guid, string> GetNetworkHostsForCurrentDomain()
      {
        return LdapProcs.GetNetworkHosts(Domain.GetComputerDomain().Name);
      }

      /// <summary>Вернуть список компов домена</summary>
      /// <param name="dname">имя домена</param>
      /// <returns>Список пар (Guid объекта AD)-(Имя компа); Внимание! Guid сейчас случайный, т.к. при зачитывании guid из AD выбивает exception</returns>
      public static Dictionary<Guid, string> GetNetworkHosts(string dname)
      {
        Dictionary<Guid, string> networkHosts = new Dictionary<Guid, string>();
        using (DirectoryEntry directoryEntry = new DirectoryEntry("WinNT://" + dname))
        {
          directoryEntry.Children.SchemaFilter.Add("Computer");
          foreach (DirectoryEntry child in directoryEntry.Children)
          {
            networkHosts.Add(Guid.NewGuid(), child.Name);
            child.Close();
            child.Dispose();
          }
        }
        return networkHosts;
      }
    }
}
