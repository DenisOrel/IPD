// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IFileNameGenerator
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Интерфейс для получения уникального имени файла</summary>
public interface IFileNameGenerator
{
  /// <summary>Генерирует уникальное имя файла для новых документов.</summary>
  /// <param name="session"></param>
  /// <param name="Prefix">префикс</param>
  /// <param name="Extention">расширение</param>
  /// <returns></returns>
  string GenerateFileName(object session, string Prefix, string Extention);
}
