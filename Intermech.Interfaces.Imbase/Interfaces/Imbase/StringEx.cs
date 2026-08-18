// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.StringEx
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
public static class StringEx
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static string MaxStringLength(this string str)
  {
    str = str.Trim();
    return str.Length <= Consts.MaxStringSize ? str : str.Substring(0, Consts.MaxStringSize).TrimEnd();
  }
}
