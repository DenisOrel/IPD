// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.SeriesDates.IRange
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Search.Pdm.SeriesDates;

public interface IRange : INotifyPropertyChanged
{
  object Start { get; set; }

  bool HasStart { get; set; }

  object End { get; set; }

  bool HasEnd { get; set; }

  bool IsEmpty { get; }

  SeriesDatesGroup Group { get; set; }
}
