// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.ICutCopy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Интерфейс для передачи данных об типе операции (Вырезать, Копировать) при этих операциях через буфер обмена IPS
/// </summary>
public interface ICutCopy
{
  /// <summary>
  /// Флаг операции: true - объект вырезан, false - объект скопирован
  /// </summary>
  bool IsCut { get; set; }

  /// <summary>
  /// Индекс иконки в глобальной коллекции именованных значков NamedImageList
  /// </summary>
  int ImageIndex { get; }
}
