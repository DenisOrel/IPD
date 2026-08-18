// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.ISchComponent
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using Intermech.Data;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Компонент электрической схемы</summary>
public interface ISchComponent : IParametrable, IValueBagContainer, IIdentification
{
  /// <summary>Позиционное обозначение</summary>
  string DesignatorText { get; }
}
