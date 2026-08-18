// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADSystemProxyBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;
using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Класс для создания прокси-обёрток документов и конфигураций
/// </summary>
public class CADSystemProxyBuilder
{
  private readonly IModelConfigurationNameMangler cfgNameMangler;

  /// <summary>Создает объект.</summary>
  public CADSystemProxyBuilder() => this.cfgNameMangler = this.DoCreateConfigurationNameMangler();

  /// <summary>Создать обёртку для документа CAD-системы</summary>
  /// <param name="provider">Провайдер</param>
  /// <param name="appProxy">CAD-система</param>
  /// <returns>Обёртка для документа CAD-системы</returns>
  /// <exception cref="T:ArgumentNullException">provider || appProxy</exception>
  public CADDocumentProxy CreateDocument(ICADDocumentProvider provider, CADSystemProxy appProxy)
  {
    if (provider == null)
      throw new ArgumentNullException(nameof (provider));
    CADDocumentProxy document = appProxy != null ? this.DoCreateDocument(provider, appProxy) : throw new ArgumentNullException(nameof (appProxy));
    if (this.AfterCreateDocument != null)
      this.AfterCreateDocument(this, document);
    return document;
  }

  /// <summary>Создать обёртку для документа CAD-системы</summary>
  /// <param name="provider">Провайдер</param>
  /// <param name="appProxy">CAD-система</param>
  /// <returns>Обёртка для документа CAD-системы</returns>
  protected virtual CADDocumentProxy DoCreateDocument(
    ICADDocumentProvider provider,
    CADSystemProxy appProxy)
  {
    return new CADDocumentProxy(provider, appProxy);
  }

  /// <summary>Создать обёртку для элемента модели CAD-системы</summary>
  /// <param name="component">Элемент модели</param>
  /// <param name="appProxy">CAD-система</param>
  /// <returns>Обёртка для элемента модели CAD-системы</returns>
  /// <exception cref="T:ArgumentNullException">component || appProxy</exception>
  public ModelComponentProxy CreateModelComponent(
    IModelComponent component,
    CADSystemProxy appProxy)
  {
    if (component == null)
      throw new ArgumentNullException(nameof (component));
    ModelComponentProxy modelComponent = appProxy != null ? this.DoCreateModelComponent(component, appProxy) : throw new ArgumentNullException(nameof (appProxy));
    if (this.AfterCreateModelComponent != null)
      this.AfterCreateModelComponent(this, modelComponent);
    return modelComponent;
  }

  /// <summary>Создать обёртку для элемента модели CAD-системы</summary>
  /// <param name="component">Элемент модели</param>
  /// <param name="appProxy">CAD-система</param>
  /// <returns>Обёртка для элемента модели CAD-системы</returns>
  protected virtual ModelComponentProxy DoCreateModelComponent(
    IModelComponent component,
    CADSystemProxy appProxy)
  {
    return new ModelComponentProxy(component, appProxy);
  }

  /// <summary>
  /// Создать обёртку для конфигурации документа CAD-системы
  /// </summary>
  /// <param name="configurationProvider">Провайдер конфигурации документа</param>
  /// <param name="document">Документ, которому принадлежит конфигурация</param>
  /// <param name="appProxy">CAD-система</param>
  /// <param name="creationContext">Контекст получения конфигурации документа</param>
  /// <returns>Обёртка для конфигурации документа</returns>
  /// <exception cref="T:ArgumentNullException">configurationProvider || document || appProxy || creationContext</exception>
  public ModelConfigurationProxy CreateModelConfiguration(
    IModelConfigurationProvider configurationProvider,
    CADDocumentProxy document,
    CADSystemProxy appProxy,
    IModelConfigurationCreationContext creationContext)
  {
    if (configurationProvider == null)
      throw new ArgumentNullException(nameof (configurationProvider));
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (appProxy == null)
      throw new ArgumentNullException(nameof (appProxy));
    if (creationContext == null)
      throw new ArgumentNullException(nameof (creationContext));
    ModelConfigurationProxy modelConfiguration = this.DoCreateModelConfiguration(configurationProvider, document, appProxy, creationContext);
    if (this.AfterCreateModelConfiguration != null)
      this.AfterCreateModelConfiguration(this, modelConfiguration);
    return modelConfiguration;
  }

  /// <summary>
  /// Создать обёртку для конфигурации документа CAD-системы
  /// </summary>
  /// <param name="configurationProvider">Провайдер конфигурации документа</param>
  /// <param name="document">Документ, которому принадлежит конфигурация</param>
  /// <param name="appProxy">CAD-система</param>
  /// <param name="creationContext">Контекст получения конфигурации документа</param>
  /// <returns>Обёртка для конфигурации документа</returns>
  protected virtual ModelConfigurationProxy DoCreateModelConfiguration(
    IModelConfigurationProvider configurationProvider,
    CADDocumentProxy document,
    CADSystemProxy appProxy,
    IModelConfigurationCreationContext creationContext)
  {
    return new ModelConfigurationProxy(configurationProvider, document, appProxy, creationContext);
  }

  /// <summary>
  /// Создает объект для корректировки и преобразования имен конфигураций.
  /// </summary>
  /// <returns>Созданный объект</returns>
  protected virtual IModelConfigurationNameMangler DoCreateConfigurationNameMangler()
  {
    return (IModelConfigurationNameMangler) new EmptyModelConfigurationNameMangler();
  }

  /// <summary>
  /// Созвращает объект для корректировки и преобразования имен конфигураций.
  /// </summary>
  public IModelConfigurationNameMangler ConfigurationNameMangler => this.cfgNameMangler;

  public event Action<CADSystemProxyBuilder, CADDocumentProxy> AfterCreateDocument;

  public event Action<CADSystemProxyBuilder, ModelConfigurationProxy> AfterCreateModelConfiguration;

  public event Action<CADSystemProxyBuilder, ModelComponentProxy> AfterCreateModelComponent;
}
