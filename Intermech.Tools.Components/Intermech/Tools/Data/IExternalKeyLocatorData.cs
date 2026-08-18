// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.IExternalKeyLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Позволяет реализовать декодер исходных данных для алгоритма поиска изделия в применяемости
/// документа по внешнему ключу изделия, хранящемуся в файле документа.
/// </summary>
public interface IExternalKeyLocatorData
{
  /// <summary>
  /// Возвращает внешний ключ изделия, хранящийся в файле документа.
  /// </summary>
  /// <returns>Значение внешнего ключа изделия, может быть равно null или пустой строке</returns>
  string GetExternalKey();

  /// <summary>Возвращает версию документа.</summary>
  /// <returns>Значение идентификатора версии документа, может быть неопределено</returns>
  long GetDocumentId();
}
