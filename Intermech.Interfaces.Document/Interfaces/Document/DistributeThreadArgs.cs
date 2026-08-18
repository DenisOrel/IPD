// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DistributeThreadArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Вспомогательный класс. Аргументы распределения в потоке</summary>
[Serializable]
public class DistributeThreadArgs
{
  public bool Force;
  public bool LockUndo;
  public bool UpdateUI;
  public bool IsBackgroundThread;

  public DistributeThreadArgs(bool force, bool lockUndo, bool updateUI, bool isBackgroundThread)
  {
    this.Force = force;
    this.UpdateUI = updateUI;
    this.LockUndo = lockUndo;
    this.IsBackgroundThread = isBackgroundThread;
  }
}
