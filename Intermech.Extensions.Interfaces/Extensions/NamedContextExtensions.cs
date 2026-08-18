// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.NamedContextExtensions
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Common;
using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Extensions;

public static class NamedContextExtensions
{
  [CanBeNull]
  public static string GetFullContextName([NotNull] this INamedContext namedContext, char delimiter = '/')
  {
    string fullContextName = (string) null;
    INamedContext ownerNamedContext = namedContext.OwnerNamedContext;
    if (ownerNamedContext != null)
      fullContextName = ownerNamedContext.GetFullContextName(delimiter);
    string contextName = namedContext.ContextName;
    if (!string.IsNullOrEmpty(contextName))
      fullContextName = !string.IsNullOrEmpty(fullContextName) ? fullContextName + delimiter.ToString() + contextName : contextName;
    return fullContextName;
  }
}
