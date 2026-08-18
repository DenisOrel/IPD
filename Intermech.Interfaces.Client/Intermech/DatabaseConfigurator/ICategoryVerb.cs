// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.ICategoryVerb
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.DatabaseConfigurator;

/// <summary>
/// Интерфейс для Verb-ов в PropertyGrid, предоставляемых службе IDatabaseConfiguratorService
/// </summary>
public interface ICategoryVerb
{
  /// <summary>заголовок Verbа</summary>
  string Caption { get; }

  /// <summary>вызов Verba</summary>
  /// <param name="category">категория</param>
  /// <param name="id">идентификатор в рамках категории</param>
  /// <returns>true если произошли изменения</returns>
  bool Execute(int category, object id);

  /// <summary>применить</summary>
  /// <returns></returns>
  bool Apply();

  /// <summary>отменить</summary>
  void Cancel();
}
