// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.IExpertFormulable
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Expert;

/// <summary>Common interface for Formula and Cond</summary>
public interface IExpertFormulable : 
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  /// <summary>Get TempFormula (for editing)</summary>
  /// <returns></returns>
  TempFormula GetTempFormula();

  /// <summary>Set TempFormula (after editing)</summary>
  /// <param name="tf">TempFormula</param>
  void UpdateObject(TempFormula tf);

  /// <summary>Сбрасывать ли единицы измерения при расчете?</summary>
  bool DropMeasure { get; set; }

  bool AutoConvert { get; set; }
}
