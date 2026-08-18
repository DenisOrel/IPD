// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.ISchSheetSymbol
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using Intermech.Data;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Функциональная группа</summary>
public interface ISchSheetSymbol : ISchComponent, IParametrable, IValueBagContainer, IIdentification
{
  /// <summary>Имя файла со схемой на данную функциональную группу</summary>
  string FileName { get; }
}
