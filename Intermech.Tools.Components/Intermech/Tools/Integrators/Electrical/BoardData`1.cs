// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.BoardData`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public class BoardData<TProxy>
{
  /// <summary>Прокси на схему/плату</summary>
  public TProxy Proxy { get; private set; }

  /// <summary>Компонент описывающий головное изделие (штамп)</summary>
  public IValueBagContainer AsmComponent { get; private set; }

  /// <summary>Признак главной схемы проекта</summary>
  public bool MainSchema { get; set; }

  /// <summary>Обозначение схемы</summary>
  public string Designation { get; private set; }

  /// <summary>Наименование схемы</summary>
  public string Name { get; private set; }

  /// <summary>Уникальный ключ изделия</summary>
  public string ArticleKey { get; private set; }

  public BoardData(
    TProxy proxy,
    IValueBagContainer asmComponent,
    bool mainSchema,
    string designation,
    string name,
    string articleKey)
  {
    this.Proxy = proxy;
    this.AsmComponent = asmComponent;
    this.MainSchema = mainSchema;
    this.Designation = designation;
    this.Name = name;
    this.ArticleKey = articleKey;
  }
}
