// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageDistribute_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события PageDistribute</summary>
public class PageDistribute_EventArgs : EventArgs
{
  /// <summary>Страница</summary>
  public PageData Page;

  /// <summary>Конструктор</summary>
  public PageDistribute_EventArgs(PageData page) => this.Page = page;
}
