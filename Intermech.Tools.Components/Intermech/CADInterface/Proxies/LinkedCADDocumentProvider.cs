// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.LinkedCADDocumentProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Позволяет получить объект CAD-документа из имеющегося объекта CAD-конфигурации документа.
/// </summary>
public sealed class LinkedCADDocumentProvider : CADInterfaceObjectProxy, ICADDocumentProvider
{
  private IModelConfigurationProvider configurationProvider;
  private ICADDocument cachedRawDocument;

  /// <summary>Создает объект.</summary>
  /// <param name="configurationProvider">Поставщик объекта CAD-конфигурации документа</param>
  public LinkedCADDocumentProvider(IModelConfigurationProvider configurationProvider)
  {
    this.configurationProvider = configurationProvider != null ? configurationProvider : throw new ArgumentNullException(nameof (configurationProvider));
  }

  /// <summary>
  /// Возвращает абсолютный путь к файлу документа, если он известен провайдеру. Если путь не известен, то метод вернет null.
  /// </summary>
  /// <returns>Абсолютный путь к файлу документа или null</returns>
  public string TryGetFullPath() => (string) null;

  /// <summary>
  /// Находит и возвращает COM-объект документа CAD-системы. Поиск выполняется при первом обращении к свойству, результат поиска кэшируется.
  /// </summary>
  public ICADDocument Document
  {
    [DebuggerStepThrough] get
    {
      if (this.cachedRawDocument == null)
        this.cachedRawDocument = this.FindDocument();
      return this.cachedRawDocument;
    }
  }

  private ICADDocument FindDocument()
  {
    return this.RawGetCADDocument(this.configurationProvider.RawConfiguration);
  }

  private ICADDocument RawGetCADDocument(IModelConfiguration rawConfiguration)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.GetCADDocument()");
    try
    {
      return rawConfiguration.GetCADDocument();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IModelConfiguration.GetCADDocument()");
    }
  }
}
