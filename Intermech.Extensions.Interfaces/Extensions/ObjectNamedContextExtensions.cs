// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ObjectNamedContextExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Common;
using Intermech.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ObjectNamedContextExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetObjectContextName([NotNull] this object obj, char delimiter = '/', bool includeTypeInfo = false)
  {
    string str = obj is INamedContext namedContext ? namedContext.GetFullContextName(delimiter) : (string) null;
    if (includeTypeInfo)
    {
      string name = obj.GetType().Name;
      str = !string.IsNullOrEmpty(str) ? str + delimiter.ToString() + name : name;
    }
    return str ?? obj.GetType().Name;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetObjectContextName([NotNull] this object obj, bool includeTypeInfo)
  {
    return obj.GetObjectContextName('/', includeTypeInfo);
  }
}
