// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.NameOf`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Linq.Expressions;

#nullable disable
namespace Experimental.Data.Entities;

public static class NameOf<TObject>
{
  public static string PropertyName<TProperty>(
    Expression<Func<TObject, TProperty>> propertySelector)
  {
    if (propertySelector == null)
      throw new ArgumentNullException(nameof (propertySelector));
    return ((MemberExpression) propertySelector.Body).Member.Name;
  }
}
