
// Type: Intermech.Ldap.CustomHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Specialized;


namespace Intermech.Ldap
{
    public class CustomHolder
    {
      protected string domainName = string.Empty;
      public HybridDictionary hdUsers = new HybridDictionary();

      public string DomainName => this.domainName;

      public void Clear() => this.hdUsers.Clear();

      public virtual bool ReadDirectory(string domainName, bool throwException)
      {
        this.domainName = domainName;
        return true;
      }
    }
}
