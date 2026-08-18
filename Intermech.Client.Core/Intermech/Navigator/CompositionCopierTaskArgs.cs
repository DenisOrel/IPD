
// Type: Intermech.Navigator.CompositionCopierTaskArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator;

/// <summary>
/// Аргументы для потока копирования состава от объекта прототипа
/// </summary>
internal sealed class CompositionCopierTaskArgs
{
  /// <summary>
  /// Идентификатор версии объекта в составе которого создается копия структуры
  /// </summary>
  public long ObjectID { get; }

  /// <summary>
  /// Идентификатор версии объекта-прототипа копируемой структуры
  /// </summary>
  public long TemplateObjectID { get; }

  public CompositionCopierTaskArgs(long newObjectID, long templateObjectID)
  {
    this.ObjectID = newObjectID;
    this.TemplateObjectID = templateObjectID;
  }
}
