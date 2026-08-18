
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeProcessorControlsContext
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// Контекст для propertygrig используемый в AttributeProcessor
/// </summary>
internal class AttributeProcessorControlsContext : ITypeDescriptorContext, IServiceProvider
{
  private PropDescriptor _descriptor;
  private AttributeValues _instance;
  private IElementInfo _parentInfo;

  /// <summary>Конструктор.</summary>
  /// <param name="av">AttributeValues</param>
  /// <param name="describer">Описатель</param>
  /// <param name="parentInfo">Информация о родителе</param>
  internal AttributeProcessorControlsContext(
    AttributeValues av,
    PropDescriptor descriptor,
    IElementInfo parentInfo)
  {
    this._instance = av;
    this._descriptor = descriptor;
    this._parentInfo = parentInfo;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <remarks>Пока null - далее, если понадобится, можно че нить вернуть</remarks>
  public IContainer Container { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public object Instance => (object) this._instance;

  /// <summary>Компонент изменился.</summary>
  public void OnComponentChanged()
  {
    if (ServicesManager.ServiceContainer == null || !(ServicesManager.ServiceContainer.GetService(typeof (IComponentChangeService)) is IComponentChangeService service))
      return;
    service.OnComponentChanged((object) this._instance, (MemberDescriptor) this.PropertyDescriptor, (object) null, (object) null);
  }

  /// <summary>Компонент изменяется.</summary>
  /// <returns>Если компонент может изменяться - True, иначе - False</returns>
  public bool OnComponentChanging() => true;

  /// <summary>
  /// 
  /// </summary>
  public PropertyDescriptor PropertyDescriptor => (PropertyDescriptor) this._descriptor;

  /// <summary>Получить сервис указанного типа.</summary>
  /// <param name="serviceType">Тип сервиса</param>
  /// <returns>Объект типа serviceType или null</returns>
  public object GetService(Type serviceType)
  {
    return ServicesManager.ServiceContainer == null ? (object) null : ServicesManager.ServiceContainer.GetService(serviceType);
  }
}
