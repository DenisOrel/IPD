// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.ExcludeAbnormalValuesSettings
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>
/// Настройки для исключения аномальных значений из графика
/// </summary>
[Serializable]
public class ExcludeAbnormalValuesSettings
{
  /// <summary>Следует исключить аномальные значения из графика</summary>
  [XmlElement(ElementName = "NeedExcludeAbnormalValues")]
  public bool NeedExcludeAbnormalValues;
  /// <summary>Процент отклонения от среднеквадратичного значения</summary>
  [XmlElement(ElementName = "Percentage")]
  public uint Percentage;

  public ExcludeAbnormalValuesSettings()
  {
    this.NeedExcludeAbnormalValues = true;
    this.Percentage = StatisticsConst.DefaultDeviationPercentage;
  }

  public ExcludeAbnormalValuesSettings(bool needExcludeAbnormalValues, uint percentage)
  {
    this.NeedExcludeAbnormalValues = needExcludeAbnormalValues;
    this.Percentage = percentage;
  }
}
