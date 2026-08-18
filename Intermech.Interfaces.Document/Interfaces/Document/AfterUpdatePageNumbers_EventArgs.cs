// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.AfterUpdatePageNumbers_EventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс. Аргументы распределения в потоке</summary>
[Serializable]
public class AfterUpdatePageNumbers_EventArgs
{
  public bool UpdateUI;
  public bool UpdateLayout;

  public AfterUpdatePageNumbers_EventArgs(bool updateUI, bool updateLayout)
  {
    this.UpdateUI = updateUI;
    this.UpdateLayout = updateLayout;
  }
}
