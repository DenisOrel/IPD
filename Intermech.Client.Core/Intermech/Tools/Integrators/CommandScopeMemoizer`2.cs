
// Type: Intermech.Tools.Integrators.CommandScopeMemoizer`2
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.UI;
using System;
using System.Collections.Generic;


namespace Intermech.Tools.Integrators;

public sealed class CommandScopeMemoizer<T, TResult>
{
  private readonly object tag = new object();
  private readonly Func<T, TResult> function;

  public CommandScopeMemoizer(Func<T, TResult> function)
  {
    this.function = function != null ? function : throw new ArgumentNullException(nameof (function));
  }

  public TResult Invoke(T arg)
  {
    UICommandInfo uiCommandInfo = UIVars.UICommand.Value;
    if (uiCommandInfo == null)
      return this.function(arg);
    object obj;
    if (!uiCommandInfo.Tags.TryGetValue(this.tag, out obj))
    {
      obj = (object) new Dictionary<T, TResult>();
      uiCommandInfo.Tags.Add(this.tag, obj);
    }
    Dictionary<T, TResult> dictionary = (Dictionary<T, TResult>) obj;
    TResult result;
    if (!dictionary.TryGetValue(arg, out result))
    {
      result = this.function(arg);
      dictionary.Add(arg, result);
    }
    return result;
  }
}
