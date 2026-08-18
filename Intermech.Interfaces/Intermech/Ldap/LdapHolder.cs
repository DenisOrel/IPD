
// Type: Intermech.Ldap.LdapHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Security.Principal;


namespace Intermech.Ldap
{
    public class LdapHolder : CustomHolder
    {
      private bool CheckDomainForExists(string domainName)
      {
        DomainCollection domains = Forest.GetCurrentForest().Domains;
        return this.CheckDomainForExistsEx(domainName, domains);
      }

      private bool CheckDomainForExistsEx(string domainName, DomainCollection domains)
      {
        if (domains != null)
        {
          foreach (Domain domain in (ReadOnlyCollectionBase) domains)
          {
            if (domain.Name.Equals(domainName, StringComparison.CurrentCultureIgnoreCase) || this.CheckDomainForExistsEx(domainName, domain.Children))
              return true;
          }
        }
        return false;
      }

      public override bool ReadDirectory(string domainName, bool throwException)
      {
        try
        {
          if (!this.CheckDomainForExists(domainName))
          {
            if (throwException)
              throw new CatalogNotFoundException(domainName);
            return false;
          }
          base.ReadDirectory(domainName, throwException);
          this.Clear();
          string ldap = LdapProcs.DomainNameToLdap(domainName, true);
          List<string> ouList = LdapProcs.GetOUList(domainName, SearchScope.OneLevel);
          ouList.Insert(0, ldap);
          for (int index = 0; index < ouList.Count; ++index)
          {
            using (DirectoryEntry searchRoot = new DirectoryEntry(ouList[index]))
            {
              using (DirectorySearcher search = new DirectorySearcher(searchRoot))
              {
                LdapProcs.InitSearcher(search);
                search.Filter = "(objectCategory=person)";
                foreach (SearchResult searchResult in search.FindAll())
                {
                  if (searchResult.Properties.Contains(LdapConsts.ADSAMAccountName))
                  {
                    HybridDictionary hybridDictionary = new HybridDictionary()
                    {
                      [(object) LdapConsts.ADSAMAccountName] = (object) searchResult.Properties[LdapConsts.ADSAMAccountName][0].ToString(),
                      [(object) LdapConsts._SearchResult_] = (object) searchResult
                    };
                    this.hdUsers[(object) hybridDictionary[(object) LdapConsts.ADSAMAccountName].ToString().ToUpper()] = (object) hybridDictionary;
                    if (searchResult.Properties.Contains(LdapConsts.ADObjectSID))
                      hybridDictionary[(object) LdapConsts.ADObjectSID] = (object) new SecurityIdentifier((byte[]) searchResult.Properties[LdapConsts.ADObjectSID][0], 0).Value;
                    if (searchResult.Properties.Contains(LdapConsts.ADDisplayName))
                      hybridDictionary[(object) LdapConsts.ADDisplayName] = (object) searchResult.Properties[LdapConsts.ADDisplayName][0].ToString();
                    else
                      hybridDictionary[(object) LdapConsts.ADDisplayName] = (object) string.Empty;
                    if (searchResult.Properties.Contains(LdapConsts.ADDescription))
                      hybridDictionary[(object) LdapConsts.ADDescription] = (object) searchResult.Properties[LdapConsts.ADDescription][0].ToString();
                  }
                }
              }
            }
          }
        }
        catch
        {
          if (!throwException)
            return false;
          throw;
        }
        return true;
      }
    }
}
