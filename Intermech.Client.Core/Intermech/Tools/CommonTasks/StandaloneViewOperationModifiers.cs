
// Type: Intermech.Tools.CommonTasks.StandaloneViewOperationModifiers
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Tools.CommonTasks;

/// <summary>
/// Модификаторы поведения для операции внедрения сведений для автономного просмотра в тело или файл документа.
/// Модификаторы не входят в настройки автономного просмотра, а формируются "на-лету" в некоторых специальных
/// режимах просмотра (например, по команде "Смотреть...").
/// </summary>
public class StandaloneViewOperationModifiers : ICloneable
{
  /// <summary>
  /// Модификатор поведения операции подготовки документа, влияющий на запись подписей документа в файл документа.
  /// Если он установлен, то в файл записывается только фамилия подписавшего, а дата подписания и сама подпись остаются пустыми.
  /// </summary>
  public bool InjectSignNamesOnly { get; set; }

  /// <summary>Создает и возвращает клон текущего объекта.</summary>
  /// <returns>Клон текущего объекта</returns>
  public StandaloneViewOperationModifiers Clone()
  {
    return new StandaloneViewOperationModifiers()
    {
      InjectSignNamesOnly = this.InjectSignNamesOnly
    };
  }

  /// <summary>Создает и возвращает клон текущего объекта.</summary>
  /// <returns>Клон текущего объекта</returns>
  object ICloneable.Clone() => (object) this.Clone();
}
