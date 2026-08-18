// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBStoredObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;


namespace Intermech.Kernel;

public class DBStoredObject : MarshalByRefObject
{
  private ParamsDictionary _paramsTable;

  protected ParamsDictionary paramsTable
  {
    get
    {
      if (this._paramsTable == null)
        this._paramsTable = new ParamsDictionary();
      return this._paramsTable;
    }
  }

  internal void SetParamsTableValue(int colID, object val) => this.paramsTable[colID] = val;
}
