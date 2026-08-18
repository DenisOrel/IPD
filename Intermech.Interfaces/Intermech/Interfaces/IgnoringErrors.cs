
// Type: Intermech.Interfaces.IgnoringErrors
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces
{
    /// <summary>Ошибки, которые будут проигнорированны при импорте</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_303")]
    [Category("Misc")]
    [Flags]
    public enum IgnoringErrors
    {
      None = 0,
      [CustomDescription("Attribute.Interfaces_304")] IgnoreFormulaErrors = 1,
    }
}
