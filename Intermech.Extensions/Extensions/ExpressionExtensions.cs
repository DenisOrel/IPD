// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ExpressionExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Intermech.Extensions;

public static class ExpressionExtensions
{
  [NotNull]
  public static PropertyInfo GetProperty<TValue>([NotNull] this Expression<Func<TValue>> expression)
  {
    return Intermech.Diagnostics.Check.Is<PropertyInfo>((object) Intermech.Diagnostics.Check.Is<MemberExpression>((object) expression.Body).Member, "Function must map an object property access");
  }

  public static (object Object, PropertyInfo PropertyInfo) GetObjectProperty<TValue>(
    [NotNull] this Expression<Func<TValue>> expression)
  {
    MemberExpression memberExpression = Intermech.Diagnostics.Check.Is<MemberExpression>((object) expression.Body);
    PropertyInfo propertyInfo = Intermech.Diagnostics.Check.Is<PropertyInfo>((object) memberExpression.Member, "Function must map an object property access");
    return (Intermech.Diagnostics.Check.NotNull<object>(Intermech.Diagnostics.Check.Is<ConstantExpression>((object) memberExpression.Expression, "Function must map an object property access").Value, "Object with property is invoked"), propertyInfo);
  }

  [NotNull]
  public static FieldInfo GetField<TValue>([NotNull] this Expression<Func<TValue>> expression)
  {
    return Intermech.Diagnostics.Check.Is<FieldInfo>((object) Intermech.Diagnostics.Check.Is<MemberExpression>((object) expression.Body).Member, "Function must map an object field access");
  }

  public static (object Object, FieldInfo FieldInfo) GetObjectField<TValue>(
    [NotNull] this Expression<Func<TValue>> expression)
  {
    MemberExpression memberExpression = Intermech.Diagnostics.Check.Is<MemberExpression>((object) expression.Body);
    FieldInfo fieldInfo = Intermech.Diagnostics.Check.Is<FieldInfo>((object) memberExpression.Member, "Function must map an object field access");
    return (Intermech.Diagnostics.Check.NotNull<object>(Intermech.Diagnostics.Check.Is<ConstantExpression>((object) memberExpression.Expression, "Function must map an object field access").Value, message: "Object with field is invoked"), fieldInfo);
  }

  public static (object Object, MemberInfo MemberInfo) GetMember<TValue>(
    [NotNull] this Expression<Func<TValue>> expression)
  {
    MemberExpression memberExpression = Intermech.Diagnostics.Check.Is<MemberExpression>((object) expression.Body);
    MemberInfo memberInfo = Intermech.Diagnostics.Check.Is<MemberInfo>((object) memberExpression.Member, "Function must map an object field access");
    return (Intermech.Diagnostics.Check.NotNull<object>(Intermech.Diagnostics.Check.Is<ConstantExpression>((object) memberExpression.Expression, "Function must map an object field access").Value, message: "Object with field is invoked"), memberInfo);
  }
}
