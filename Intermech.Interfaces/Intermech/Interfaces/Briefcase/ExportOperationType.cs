
// Type: Intermech.Interfaces.Briefcase.ExportOperationType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Типы операций при импорте портфеля</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_ExportOperationTypes")]
    [Category("Misc")]
    public enum ExportOperationType
    {
      [CustomDescription("String.Empty")] Idle,
      [CustomDescription("Attribute.Interfaces_Exporting")] Exporting,
      [CustomDescription("Attribute.Interfaces_ExportingMetaData")] ExportingMetaData,
      [CustomDescription("Attribute.Interfaces_CheckingData")] CheckingData,
      [CustomDescription("Attribute.Interfaces_ExportTerminate")] Finished,
      [CustomDescription("Attribute.Interfaces_ExportError")] Error,
      [CustomDescription("Attribute.Interfaces_ExportSecurity")] ExportingSecurity,
      [CustomDescription("Attribute.Interfaces_CheckingMetadata")] CheckingMetadata,
    }
}
