
// Type: Intermech.Client.Core.FormDesigner.Controls.ControlsContext
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// Контекст, создан для наследников UITypeEditor в функцию GetEditStyle(ITypeDescriptorContext context)
/// Если Editor используется PropertyGrid'ом то context формируется автоматически,
/// если Editor в иных случаях, например в контролах, то context нужно формировать самому.
/// </summary>
internal class ControlsContext : ITypeDescriptorContext, IServiceProvider, IConditionHolder
{
  private IAttributePropertyDescriber _describer;
  private AttributeValues _instance;
  private IElementInfo _parentInfo;
  private ConditionStructure[] _conditions;

  public ConditionStructure[] Conditions => this._conditions;

  /// <summary>Конструктор.</summary>
  /// <param name="av">AttributeValues</param>
  /// <param name="describer">Описатель</param>
  /// <param name="parentInfo">Информация о родителе</param>
  internal ControlsContext(
    AttributeValues av,
    IAttributePropertyDescriber describer,
    IElementInfo parentInfo)
    : this(av, describer, parentInfo, (ConditionStructure[]) null)
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="av">AttributeValues</param>
  /// <param name="describer">Описатель</param>
  /// <param name="parentInfo">Информация о родителе</param>
  /// <param name="conditions">условия для выбора, напр по контекстной выборке</param>
  internal ControlsContext(
    AttributeValues av,
    IAttributePropertyDescriber describer,
    IElementInfo parentInfo,
    ConditionStructure[] conditions)
  {
    this._instance = av;
    this._describer = describer;
    this._parentInfo = parentInfo;
    this._conditions = conditions;
    this.CreateDescriptor();
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
  public PropertyDescriptor PropertyDescriptor { get; private set; }

  /// <summary>Получить сервис указанного типа.</summary>
  /// <param name="serviceType">Тип сервиса</param>
  /// <returns>Объект типа serviceType или null</returns>
  public object GetService(Type serviceType)
  {
    return ServicesManager.ServiceContainer == null ? (object) null : ServicesManager.ServiceContainer.GetService(serviceType);
  }

  /// <summary>Создание Descriptor'а.</summary>
  private void CreateDescriptor()
  {
    if (this._instance == null)
      return;
    if (this._describer != null)
      this.PropertyDescriptor = (PropertyDescriptor) new PropDescriptor(this._instance.AttributeID, (object) this._parentInfo, this._instance.AttributeName, this._instance.Values[0], this._describer.GetPropDescriptorType(this._instance.AttributeID, this._instance.AttributeType), this._describer.GetPropDescriptorConverter(this._instance.AttributeID), (object) null, string.Empty, string.Empty, false, false, true, this._describer.GetPropDescriptorMask(this._instance.AttributeID, string.Empty));
    else
      this.PropertyDescriptor = (PropertyDescriptor) new PropDescriptor(this._instance.AttributeID, (object) this._parentInfo, this._instance.AttributeName, this._instance.Values[0], (Type) null, (TypeConverter) null, (object) null, string.Empty, string.Empty, false, false, true, string.Empty);
  }
}
