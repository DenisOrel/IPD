
// Type: Intermech.Interfaces.Briefcase.OperationType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.Briefcase
{
    /// <summary>Типы операций при импорте портфеля</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_321")]
    [Category("Misc")]
    public enum OperationType
    {
      [CustomDescription("Attribute.Interfaces_322")] Unpacking,
      [CustomDescription("Attribute.Interfaces_323")] Importing,
      [CustomDescription("Attribute.Interfaces_324")] ImportingMetaData,
      [CustomDescription("Attribute.Interfaces_325")] RestoreMetaData,
      [CustomDescription("Attribute.Interfaces_326")] CheckingData,
      [CustomDescription("Attribute.Interfaces_327")] Finished,
      [CustomDescription("Attribute.Interfaces_328")] Error,
      [CustomDescription("Attribute.Interfaces_329")] CheckError,
      [CustomDescription("Attribute.Interfaces_330")] TerminateCurrent,
      [CustomDescription("Attribute.Interfaces_331")] ImportingSecurity,
      [CustomDescription("Attribute.Interfaces_332")] CheckingMetaData,
      [CustomDescription("Attribute.Interfaces_333")] CheckingTerminate,
    }
}
