// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.ILCObject
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;

#nullable disable
namespace Intermech.PropertyEditors;

public interface ILCObject
{
  void LoadProps();

  void SaveProps();

  bool IsNode { get; }

  bool IsLink { get; }

  void ChangeEvent(EventArgs e);

  bool Apply(object oldId);

  void Cancel();
}
