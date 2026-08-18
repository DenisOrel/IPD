
// Type: Intermech.Client.Core.FormDesigner.External.Classes.ReceiveMethod
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Client.Core.FormDesigner.External.Classes;

/// <summary>Методы возврата данных.</summary>
[TypeConverter(typeof (EnumDescConverter))]
public enum ReceiveMethod
{
  [CustomDescription("Attribute.Client.Core_27")] NotReturn,
  [CustomDescription("Attribute.Client.Core_28")] File,
  [Description("Clipboard")] Clipboard,
}
