// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ClosedCADDocumentProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Runtime.ComInterop.Proxies;
using Interop.CADInterface;

#nullable disable
namespace Intermech.CADInterface.Proxies;

internal sealed class ClosedCADDocumentProvider : CADInterfaceObjectProxy, ICADDocumentProvider
{
  private static readonly ClosedCADDocumentProvider defaultInstance = new ClosedCADDocumentProvider();

  /// <summary>
  /// Возвращает абсолютный путь к файлу документа, если он известен провайдеру. Если путь не известен, то метод вернет null.
  /// </summary>
  /// <returns>Абсолютный путь к файлу документа или null</returns>
  public string TryGetFullPath() => throw this.CreateAccessException();

  /// <summary>
  /// Находит и возвращает COM-объект документа CAD-системы. Поиск выполняется при первом обращении к свойству, результат поиска кэшируется.
  /// </summary>
  public ICADDocument Document => throw this.CreateAccessException();

  private ApplicationProxyException CreateAccessException()
  {
    return new ApplicationProxyException(LocalizationHolder.rm.GetString("Tools.Components_280"));
  }

  /// <summary>
  /// Возвращает экземпляр объекта, используемый по умолчанию.
  /// </summary>
  public static ClosedCADDocumentProvider Default => ClosedCADDocumentProvider.defaultInstance;
}
