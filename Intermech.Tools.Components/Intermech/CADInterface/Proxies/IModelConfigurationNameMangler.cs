// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.IModelConfigurationNameMangler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Позволяет реализовать преобразователь для имен конфигураций 3D-моделей.
/// Он используется в случаях, когда CAD-система допускает пустое имя для мастер-конфигурации.
/// </summary>
public interface IModelConfigurationNameMangler
{
  /// <summary>
  /// Обработать имя конфигурации документа, полученное от CAD-интерфейса.
  /// </summary>
  /// <param name="documentFile">Имя файла документа, которому принадлежит конфигурация</param>
  /// <param name="rawName">Имя конфигурации документа, полученное от CAD-интерфейса</param>
  /// <returns>Обработанное имя</returns>
  string ToSafeName(string documentFile, string rawName);

  /// <summary>
  /// Преобразовать имя конфигурации документа в оригинальное (до его обработки методом GetSafeConfigurationName)
  /// </summary>
  /// <param name="documentFile">Имя файла документа, которому принадлежит конфигурация</param>
  /// <param name="safeName">Имя конфигурации документа, полученное методом GetSafeConfigurationName</param>
  /// <returns>Имя конфигурации документа, полученное от CAD-интерфейса</returns>
  string ToRawName(string documentFile, string safeName);
}
