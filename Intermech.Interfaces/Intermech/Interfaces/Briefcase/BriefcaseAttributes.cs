
// Type: Intermech.Interfaces.Briefcase.BriefcaseAttributes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Briefcase
{
    public struct BriefcaseAttributes(
      string aName,
      string aComment,
      int aVersion,
      DateTime aExportDate,
      DateTime aLastSystemUpdate,
      bool aClosed,
      bool includeLocalization,
      Guid siteGuid)
    {
      public string Name = aName;
      public string Comment = aComment;
      public DateTime ExportDate = aExportDate;
      public int Version = aVersion;
      public DateTime LastSystemUpdate = aLastSystemUpdate;
      public bool Closed = aClosed;
      /// <summary>Содержит информацию об локализации</summary>
      public bool IncludeLocalization = includeLocalization;
      /// <summary>
      /// Глобальный идентификатор узла информационной системы на котором был сформирован этот портфель
      /// </summary>
      public Guid SiteGuid = siteGuid;
    }
}
