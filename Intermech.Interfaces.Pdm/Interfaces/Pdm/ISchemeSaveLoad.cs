// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ISchemeSaveLoad
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Интерфейс сохранения данных объекта</summary>
public interface ISchemeSaveLoad
{
  /// <summary>Обновить данные объекта</summary>
  /// <param name="scheme">Схема данных визуализатора</param>
  void SaveScheme(VisSchemeParms scheme);

  /// <summary>Загрузить схему из объекта</summary>
  /// <returns></returns>
  VisSchemeParms LoadScheme();
}
