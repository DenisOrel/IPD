// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.ScriptInfo
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class ScriptInfo
{
  public ScriptInfo(long objectId, string caption)
  {
    if (objectId == 0L)
      throw new ArgumentException("Идентификатор версии объекта не задан.", nameof (objectId));
    if (caption == null)
      throw new ArgumentNullException(nameof (caption));
    this.ObjectId = objectId;
    this.Caption = caption;
  }

  public long ObjectId { get; private set; }

  public string Caption { get; private set; }
}
