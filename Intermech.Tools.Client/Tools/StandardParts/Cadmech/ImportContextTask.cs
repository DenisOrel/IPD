// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Cadmech.ImportContextTask
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.StandardParts.Cadmech;

internal class ImportContextTask
{
  private ImportContext importContext;

  public ImportContext ImportContext
  {
    [DebuggerStepThrough] get => this.importContext;
    set
    {
      this.importContext = value;
      if (this.importContext != null)
        this.SafelyInitialize();
      else
        this.DoCleanupContextData();
    }
  }

  private void SafelyInitialize()
  {
    try
    {
      this.DoInitializeContextData();
    }
    catch
    {
      this.DoCleanupContextData();
      throw;
    }
  }

  protected virtual void DoInitializeContextData()
  {
  }

  protected virtual void DoCleanupContextData()
  {
  }

  protected void RequireImportContext()
  {
    if (this.ImportContext == null)
      throw new InvalidOperationException($"Property 'ImportContext' of object '{this}' must be assigned first.");
  }
}
