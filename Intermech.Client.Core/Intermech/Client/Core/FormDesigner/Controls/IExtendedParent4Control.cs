
// Type: Intermech.Client.Core.FormDesigner.Controls.IExtendedParent4Control
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Расширенный интерфейс для установки родителя для контрола.
/// </summary>
public interface IExtendedParent4Control : IParent4Control
{
  /// <summary>Идентификатор типа объекта/связи.</summary>
  int ParentTypeID { get; set; }
}
