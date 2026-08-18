
// Type: Intermech.Ldap.CatalogNotFoundException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Ldap
{
    public class CatalogNotFoundException : Exception
    {
      private string catalogName = string.Empty;

      public CatalogNotFoundException(string catalogName) => this.catalogName = catalogName;

      public override string Message => $"Каталог {this.catalogName} недоступен или не существует";
    }
}
