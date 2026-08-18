// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.LazyCADDocumentProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Runtime.ComInterop.Proxies;
using Interop.CADInterface;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class LazyCADDocumentProvider : CADInterfaceObjectProxy, ICADDocumentProvider
{
  private string documentFullPath;
  private CADSystemProxy cadSystem;
  private ICADDocument cachedRawDocument;

  public LazyCADDocumentProvider(string fullPath, CADSystemProxy cadSystem)
  {
    if (string.IsNullOrEmpty(fullPath))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (fullPath));
    if (!Path.IsPathRooted(fullPath))
      throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_283"), (object) fullPath), nameof (fullPath));
    if (cadSystem == null)
      throw new ArgumentNullException(nameof (cadSystem));
    this.documentFullPath = fullPath;
    this.cadSystem = cadSystem;
  }

  /// <summary>
  /// Возвращает абсолютный путь к файлу документа, если он известен провайдеру. Если путь не известен, то метод вернет null.
  /// </summary>
  /// <returns>Абсолютный путь к файлу документа или null</returns>
  public string TryGetFullPath() => this.documentFullPath;

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
    return (ICADDocument) (this.cadSystem.InternalOpenDocument(this.documentFullPath, false, false) ?? throw new ApplicationProxyException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_284"), (object) this.documentFullPath)));
  }
}
