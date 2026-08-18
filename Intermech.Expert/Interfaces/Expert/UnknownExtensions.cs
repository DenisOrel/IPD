// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.UnknownExtensions
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// Методы для сравнения ЛЮБЫХ объектов с null, DBNull и Unknown
/// </summary>
public static class UnknownExtensions
{
  public static bool IsNull(this object obj) => obj == null;

  public static bool NotNull(this object obj) => obj != null;

  public static bool IsDBNull(this object obj) => obj is DBNull || obj is Unknown;

  public static bool NotDBNull(this object obj) => !(obj is DBNull) && !(obj is Unknown);

  public static bool IsNullOrDBNull(this object obj) => obj.IsNull() || obj.IsDBNull();

  public static bool NotNullOrDBNull(this object obj) => !obj.IsNull() && !obj.IsDBNull();
}
