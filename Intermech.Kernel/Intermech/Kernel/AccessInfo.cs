// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.AccessInfo
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

public class AccessInfo
{
  public bool Result;
  public bool DenyMode;
  public bool GrantAlwaysMode;
  public bool DefaultAccess;
  public DateTime AddTime;
  public List<string> CheckLogString;
  public int CheckAccessHashCode;

  public AccessInfo(
    bool result,
    bool denyMode,
    bool defaultAccess,
    bool grantAlwaysMode,
    List<string> checkLogString,
    int accessHashCode)
  {
    this.Result = result;
    this.DenyMode = denyMode;
    this.GrantAlwaysMode = grantAlwaysMode;
    this.DefaultAccess = defaultAccess;
    this.AddTime = DateTime.Now;
    this.CheckAccessHashCode = accessHashCode;
    this.CheckLogString = new List<string>(10);
    int num = 5;
    for (int index = checkLogString.Count - 1; index >= 0 && ((int) checkLogString[index][0] != (int) "-"[0] || --num >= 1); --index)
      this.CheckLogString.Insert(0, checkLogString[index]);
  }
}
