// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechAcad.ITechAcadParamsService
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechAcad;

/// <summary>Интерфейс службы загрузки / сохранения настроек</summary>
public interface ITechAcadParamsService
{
  /// <summary>Загрузка настроек</summary>
  /// <param name="machineName"></param>
  /// <param name="sessionGuid"></param>
  /// <returns></returns>
  TechAcadParamsItem LoadData(string machineName, Guid sessionGuid);

  /// <summary>Сохранение настроек</summary>
  /// <param name="value">Настройки</param>
  /// <param name="machineName"></param>
  /// <param name="sessionGuid"></param>
  /// <returns></returns>
  bool SaveData(TechAcadParamsItem value, string machineName, Guid sessionGuid);
}
