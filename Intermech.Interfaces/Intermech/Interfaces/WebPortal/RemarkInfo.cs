
// Type: Intermech.Interfaces.WebPortal.RemarkInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.WebPortal
{
    [Serializable]
    public class RemarkInfo : AttributeInfo
    {
      public char PublishSite;
      public string EnableSites;
      public DateTime PublishTime;
      public List<ValueInfo> Values;

      public RemarkInfo(
        string guid,
        string name,
        string shortName,
        string alias,
        FieldTypes type,
        char publishSite,
        DateTime publishTime,
        string enableSites)
        : base(guid, name, shortName, alias, type)
      {
        this.PublishSite = publishSite;
        this.PublishTime = publishTime;
        this.EnableSites = enableSites;
        this.Values = new List<ValueInfo>(1);
      }
    }
}
