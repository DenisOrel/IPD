// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripting.CSharp.ScriptCheckResult
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripting.CSharp;

internal sealed class ScriptCheckResult
{
  public ScriptCheckResult(ScriptInfo scriptInfo, bool isValid, string requiredAction)
  {
    if (scriptInfo == null)
      throw new ArgumentNullException(nameof (scriptInfo));
    if (requiredAction == null)
      throw new ArgumentNullException(nameof (requiredAction));
    this.ScriptInfo = scriptInfo;
    this.IsValid = isValid;
    this.RequiredAction = requiredAction;
  }

  public ScriptInfo ScriptInfo { get; private set; }

  public bool IsValid { get; private set; }

  public string RequiredAction { get; private set; }
}
