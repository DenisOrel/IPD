// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.PageUnlockedArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы события PageUnlocked</summary>
public class PageUnlockedArgs
{
  public PageData Page;
  /// <summary>Страница разбита</summary>
  public bool IsDistributed;
  public XmlReadArgs ReadArgs;

  public PageUnlockedArgs(PageData page, bool isDistributed, XmlReadArgs readArgs)
  {
    this.Page = page;
    this.IsDistributed = isDistributed;
    this.ReadArgs = readArgs;
  }
}
