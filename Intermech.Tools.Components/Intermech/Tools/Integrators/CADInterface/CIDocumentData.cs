// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIDocumentData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Секция содержит специфические данные документа, относящиеся к CAD-интерфейсу. В частности,
/// эта секция используется для кэширования ссылок на COM-объект документа.
/// </summary>
public sealed class CIDocumentData
{
  private CADDocumentProxy document;
  private ICollection<ModelConfigurationProxy> allConfigurations;

  /// <summary>
  /// Возвращает или задает ссылку на COM-объект документа CAD-интерфейса.
  /// </summary>
  public CADDocumentProxy Document
  {
    get => this.document;
    set => this.document = value;
  }

  /// <summary>Возвращает коллекцию всех конфигураций документа.</summary>
  public ICollection<ModelConfigurationProxy> AllConfigurations
  {
    get
    {
      if (this.allConfigurations == null)
        this.allConfigurations = this.document.GetAllConfigurations();
      return this.allConfigurations;
    }
  }
}
