// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMEditContext
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Interop.CADInterface;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Tools.PDMTree;

[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IEditContext))]
public sealed class PDMEditContext : PDMSystemComponent, IEditContext
{
  private readonly long contextId;

  internal PDMEditContext(long contextId, PDMSystem pdmSystem)
    : base(pdmSystem)
  {
    this.contextId = contextId != 0L ? contextId : throw new ArgumentException("Идентификатор контекста редактирования не задан.", nameof (contextId));
  }

  public string GetName()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMEditContext.GetName");
    this.PDMSystem.PrepareCall();
    try
    {
      return DBHelper.GetObjectCaption(this.contextId);
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }
}
