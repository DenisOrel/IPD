// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.ICompositionFiltrationCommand
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System.Collections.Specialized;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Интерфейс команды дополнительной фильтрации состава</summary>
public interface ICompositionFiltrationCommand
{
  /// <summary>Текущее состояние</summary>
  object Value { get; }

  void CreateCommand(INamedImageList namedImageList);

  void OnPutPluginData(HybridDictionary tag);

  void OnGetPluginData(HybridDictionary tag);
}
