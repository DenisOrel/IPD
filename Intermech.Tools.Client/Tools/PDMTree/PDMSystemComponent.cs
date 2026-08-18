// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMSystemComponent
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Runtime.ComInterop.LocalServer;
using System;

#nullable disable
namespace Intermech.Tools.PDMTree;

public abstract class PDMSystemComponent : SingleThreadedObject, IPDMSystemProvider
{
  private readonly PDMSystem pdmSystem;
  private string traceTypeName;

  protected PDMSystemComponent(PDMSystem pdmSystem)
  {
    this.pdmSystem = pdmSystem != null ? pdmSystem : throw new ArgumentNullException(nameof (pdmSystem));
  }

  PDMSystem IPDMSystemProvider.PDMSystem => this.pdmSystem;

  internal string TraceTypeName
  {
    get
    {
      if (this.traceTypeName == null)
        this.traceTypeName = this.GetType().Name;
      return this.traceTypeName;
    }
  }

  internal PDMSystem PDMSystem => this.pdmSystem;
}
