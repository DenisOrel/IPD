// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMSystemTrace
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.PDMTree;

internal static class PDMSystemTrace
{
  internal static readonly TraceSwitch General = new TraceSwitch("Tools.PDMSystem", string.Empty, "0");
}
