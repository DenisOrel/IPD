// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.Unknown
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// Класс, отмечающий "неизвестное значение". null - это отсутствие значения,
/// а Unknown.Instance означает, что значение нужно дочитать из базы
/// </summary>
[Serializable]
public sealed class Unknown
{
  private static readonly Unknown instance = new Unknown();

  private Unknown()
  {
  }

  public static Unknown Value => Unknown.instance;

  public override string ToString() => "<UNKNOWN>";

  public override bool Equals(object other)
  {
    return (object) (other as Unknown) != null || other is DBNull;
  }

  public override int GetHashCode() => 0;

  public static bool operator ==(Unknown o1, Unknown o2) => true;

  public static bool operator !=(Unknown o1, Unknown o2) => false;

  public static bool operator ==(Unknown o1, DBNull o2) => true;

  public static bool operator !=(Unknown o1, DBNull o2) => false;

  public static bool operator ==(DBNull o1, Unknown o2) => true;

  public static bool operator !=(DBNull o1, Unknown o2) => false;
}
