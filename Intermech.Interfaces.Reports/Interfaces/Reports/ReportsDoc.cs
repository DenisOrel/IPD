// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportsDoc
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Документ</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Родительский элемент</param>
public class ReportsDoc(ReportsBaseDoc owner) : ReportsBaseDoc(owner)
{
  /// <summary>Сжатое (архивированное) XML содержимое документа</summary>
  public byte[] Data { get; set; }
}
