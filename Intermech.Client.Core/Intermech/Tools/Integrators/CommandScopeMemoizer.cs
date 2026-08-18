
// Type: Intermech.Tools.Integrators.CommandScopeMemoizer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Tools.Integrators;

public static class CommandScopeMemoizer
{
  public static Func<T1, T2, TResult> Wrap<T1, T2, TResult>(Func<T1, T2, TResult> function)
  {
    if (function == null)
      throw new ArgumentNullException(nameof (function));
    Func<Tuple<T1, T2>, TResult> helper = CommandScopeMemoizer.Wrap<Tuple<T1, T2>, TResult>((Func<Tuple<T1, T2>, TResult>) (pair => function(pair.Item1, pair.Item2)));
    return (Func<T1, T2, TResult>) ((arg1, arg2) => helper(Tuple.Create<T1, T2>(arg1, arg2)));
  }

  public static Func<T, TResult> Wrap<T, TResult>(Func<T, TResult> function)
  {
    return new Func<T, TResult>(new CommandScopeMemoizer<T, TResult>(function).Invoke);
  }
}
