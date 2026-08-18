// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IDBArticle
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Интерфейс для установки флага в DBArticle</summary>
public interface IDBArticle
{
  /// <summary>Флаг для блокировки удаления лишней связи между исполнением и спецификацией</summary>
  bool KeepRelationWithSpecification { get; set; }
}
