// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.IImbaseKeyLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Позволяет реализовать декодер исходных данных для алгоритма поиска изделия по ключу Imbase.
/// </summary>
public interface IImbaseKeyLocatorData
{
  /// <summary>Возвращает ключ Imbase.</summary>
  /// <returns>Значение ключа Imbase, может быть равно null или пустой строке</returns>
  string GetImbaseKey();
}
