// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.IPDMStandardLibrary
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal interface IPDMStandardLibrary
{
  IEventLogWriter Log { get; set; }

  string BeginUpdatePart(string partName, string modelFileName);

  void EndUpdatePart(string partName, string modelFileName);
}
