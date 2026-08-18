
// Type: Intermech.Scripting.CSharp.DesignTime.CSharpScriptProjectOptions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Scripting.CSharp.DesignTime;

public sealed class CSharpScriptProjectOptions
{
  private const string RunAtClientSideOption = "RunAtClientSide";

  public CSharpScriptProjectOptions() => this.RunAtClientSide = true;

  public bool RunAtClientSide { get; set; }

  public static Dictionary<string, string> ToDictionary(CSharpScriptProjectOptions options)
  {
    return options != null ? new Dictionary<string, string>()
    {
      {
        "RunAtClientSide",
        options.RunAtClientSide ? "true" : "false"
      }
    } : throw new ArgumentNullException(nameof (options));
  }

  public static CSharpScriptProjectOptions FromDictionary(Dictionary<string, string> options)
  {
    if (options == null)
      throw new ArgumentNullException(nameof (options));
    CSharpScriptProjectOptions scriptProjectOptions = new CSharpScriptProjectOptions();
    string str;
    if (options.TryGetValue("RunAtClientSide", out str))
      scriptProjectOptions.RunAtClientSide = str == "true";
    return scriptProjectOptions;
  }
}
