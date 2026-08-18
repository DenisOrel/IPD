
// Type: Intermech.Interfaces.Briefcase.BriefcaseExportProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Параметры экспорта</summary>
    [Serializable]
    public class BriefcaseExportProperties
    {
      public List<ExportAttribute> ExportAttributes;
      /// <summary>
      /// экспорт по следующим типам связей
      /// array of int // если null, то экспорт по всем типам связей.
      /// </summary>
      public ArrayList ExportRelationTypes;
      public bool ExportSecurity = true;
      public string Comment = string.Empty;
      public bool IncludeLocalization;
      public bool ExpandedLog;
      public string ExportRuleID = string.Empty;
      /// <summary>оставлять портфель на сервере в папке ServerFolder</summary>
      public bool ServerPlacement;
      /// <summary>
      /// папка, в которую сохранять портфель - если пусто, то в temp папку
      /// </summary>
      public string ServerFolder = string.Empty;

      public BriefcaseExportProperties(
        List<ExportAttribute> aExportAttributes,
        ArrayList aExportRelationTypes,
        bool aExportSecurity,
        string aComment,
        bool aServerPlacement,
        string aServerFolder,
        bool aIncludeLocalization,
        bool aExpandedLog,
        string aExportRuleID)
      {
        this.ExportAttributes = aExportAttributes;
        this.ExportRelationTypes = aExportRelationTypes;
        this.ExportSecurity = aExportSecurity;
        this.Comment = aComment;
        this.ServerPlacement = aServerPlacement;
        this.ServerFolder = aServerFolder;
        this.IncludeLocalization = aIncludeLocalization;
        this.ExpandedLog = aExpandedLog;
        this.ExportRuleID = aExportRuleID;
      }
    }
}
