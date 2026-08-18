// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Секция содержит специфические данные изделия, относящиеся к CAD-интерфейсу.
/// В частности, эта секция используется для кэширования ссылок на COM-объект конфигурации
/// документа, соответствующей изделию.
/// </summary>
public sealed class CIArticleData
{
  private ModelConfigurationProxy modelConfiguration;

  /// <summary>
  /// Возвращает или задает конфигурацию документа, на основе которой формируется изделие IPS.
  /// </summary>
  public ModelConfigurationProxy Configuration
  {
    get => this.modelConfiguration;
    set => this.modelConfiguration = value;
  }
}
