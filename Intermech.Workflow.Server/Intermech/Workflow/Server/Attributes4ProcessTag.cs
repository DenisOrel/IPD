// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Attributes4ProcessTag
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces.Server.WebPortal;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

[Serializable]
internal class Attributes4ProcessTag : Attributes4Tag
{
  public string Data;

  public Attributes4ProcessTag(string data) => this.Data = data;
}
