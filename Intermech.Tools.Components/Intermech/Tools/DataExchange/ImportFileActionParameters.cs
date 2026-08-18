// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.ImportFileActionParameters
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime;
using Intermech.UI;
using System.IO;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>Параметры для операции импорта файла.</summary>
public sealed class ImportFileActionParameters
{
  /// <summary>Возвращает или задает путь к импортируемому файлу.</summary>
  public string FullPath { get; set; }

  /// <summary>
  /// Возвращает или задает индикатор хода выполнения операции.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IPercentageProgressSink ProgressSink { get; set; }

  /// <summary>Проверяет корректность заполнения свойств объекта.</summary>
  /// <exception cref="T:InvalidOperationException">Значения свойств объекта заполнены некорректно</exception>
  public void ValidateProperties()
  {
    if (string.IsNullOrEmpty(this.FullPath))
      throw PropertyExceptions.PropertyBadValueException((object) this, "FullPath", "Не задан путь к импортируемому файлу.");
    if (!Path.IsPathRooted(this.FullPath))
      throw PropertyExceptions.PropertyBadValueException((object) this, "FullPath", $"Путь к импортируемому файлу '{this.FullPath}' задан не в абсолютной форме.");
  }
}
