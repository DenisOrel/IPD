// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ExplicitCADDocumentProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Interop.CADInterface;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class ExplicitCADDocumentProvider : CADInterfaceObjectProxy, ICADDocumentProvider
{
  private ICADDocument rawDocument;
  private string fullPath;

  public ExplicitCADDocumentProvider(ICADDocument rawDocument, string documentFullPath = null)
  {
    this.rawDocument = rawDocument != null ? rawDocument : throw new ArgumentNullException(nameof (rawDocument), LocalizationHolder.rm.GetString("Tools.Components_281"));
    this.fullPath = documentFullPath;
  }

  /// <summary>
  /// Возвращает абсолютный путь к файлу документа, если он известен провайдеру. Если путь не известен, то метод вернет null.
  /// </summary>
  /// <returns>Абсолютный путь к файлу документа или null</returns>
  public string TryGetFullPath() => this.fullPath;

  /// <summary>
  /// Находит и возвращает COM-объект документа CAD-системы. Поиск выполняется при первом обращении к свойству, результат поиска кэшируется.
  /// </summary>
  public ICADDocument Document
  {
    [DebuggerStepThrough] get => this.rawDocument;
  }
}
