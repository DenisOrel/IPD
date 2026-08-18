// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.ResolutionProgressReportRecord
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Office.Interfaces;

/// <summary>Информация о выпущенном отчёте о выполнении поручения</summary>
[Serializable]
public readonly struct ResolutionProgressReportRecord(long authorID, DateTime releaseDate)
{
  public readonly long AuthorID = authorID;
  public readonly DateTime ReleaseDate = releaseDate;
}
