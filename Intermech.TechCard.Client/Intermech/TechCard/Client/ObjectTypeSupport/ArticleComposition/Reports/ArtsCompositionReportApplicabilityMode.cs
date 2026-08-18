// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Reports.ArtsCompositionReportApplicabilityMode
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Reports;

/// <summary>Режимы отображения применяемости для состава</summary>
[Flags]
internal enum ArtsCompositionReportApplicabilityMode
{
  /// <summary>Отображение применяемости не требуется</summary>
  None = 0,
  /// <summary>Входимость по конструкторскому составу</summary>
  Design = 1,
  /// <summary>Входимость по технологическому составу (техпроцессу)</summary>
  TechProc = 2,
}
