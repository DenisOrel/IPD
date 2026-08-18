// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Configurator.InheritArchiveRightsFromTechProcessOption
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Client.Configurator;

/// <summary>
/// Реализация настройки "Наследовать права доступа архива техпроцесса" для объектов техпроцесса
/// </summary>
internal class InheritArchiveRightsFromTechProcessOption : ObjTypeOptionProp
{
  /// <summary>Конструктор</summary>
  public InheritArchiveRightsFromTechProcessOption()
    : base(LocalizationHolder.rm.GetString("TechCard.Client_513"))
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pdh"></param>
  /// <param name="category"></param>
  /// <param name="id"></param>
  /// <returns></returns>
  protected override PropDescriptor[] OnGetDescriptors(
    PropDescriptorHolder pdh,
    int category,
    object id)
  {
    if (pdh == null)
      return (PropDescriptor[]) null;
    if (category != 4)
      return (PropDescriptor[]) null;
    int int32 = Convert.ToInt32(id);
    if (int32 <= 0)
      return (PropDescriptor[]) null;
    if (!MetaDataHelper.IsObjectTypeChildOf(TechCardConsts.ObjectTypes.TechProcBaseID, TechCardConsts.ObjectTypes.DocumentBaseID))
      return (PropDescriptor[]) null;
    if (MetaDataHelper.IsObjectTypeChildOf(int32, TechCardConsts.ObjectTypes.TechProcBaseID))
      return (PropDescriptor[]) null;
    ObjectTypeFolder objectTypeFolder = ((CustomFolder) pdh).NodeParent.Tag as ObjectTypeFolder;
    tag = ((CustomFolder) pdh).NodeParent.Tag as ObjectTypeFolder;
    while (tag != null)
    {
      if (tag.NodeParent.Tag is ObjectTypeFolder tag)
        objectTypeFolder = tag;
    }
    if ((objectTypeFolder != null ? Convert.ToInt32(objectTypeFolder.Id) : -1) != TechCardConsts.ObjectTypes.TechBaseObjectID)
      return (PropDescriptor[]) null;
    this.propertyDescriptor = (PropDescriptor) null;
    foreach (PropDescriptor propDescriptor in pdh.PropDescriptorCollection)
    {
      if (propDescriptor.DisplayName.Equals(this.subscriberID))
      {
        this.propertyDescriptor = propDescriptor;
        break;
      }
    }
    bool aBoolean = false;
    if (Convert.ToInt32(id) > 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObjectType(Convert.ToInt32(id), false) is IDBMetadataExtensions objectType)
        {
          int[] mdValuesInt = objectType.GetMDValuesInt(TechCardConsts.Params.MdeInheritArchiveRightsFromTechProc);
          aBoolean = mdValuesInt.Length != 0 && Convert.ToBoolean(mdValuesInt[0]);
        }
      }
    }
    this.attributeValue = (object) aBoolean;
    if (this.propertyDescriptor != null)
      this.propertyDescriptor.SetValue((object) this, (object) new BoolPropertyClass(aBoolean));
    else
      this.propertyDescriptor = new PropDescriptor(0, (object) null, this.SubscriberID, (object) new BoolPropertyClass(aBoolean), typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, this.subscriberID, false, true, false);
    if (this.propertyDescriptor == null)
      return (PropDescriptor[]) null;
    return new PropDescriptor[1]{ this.propertyDescriptor };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pdh"></param>
  /// <param name="category"></param>
  /// <param name="id"></param>
  /// <param name="idOld"></param>
  /// <returns></returns>
  protected override bool OnApply(PropDescriptorHolder pdh, int category, object id, object idOld)
  {
    bool boolean = Convert.ToBoolean(this.attributeValue);
    foreach (PropDescriptor propDescriptor in pdh.PropDescriptorCollection)
    {
      if (propDescriptor.DisplayName.Equals(this.subscriberID))
      {
        if (propDescriptor.GetValue(id) is BoolPropertyClass boolPropertyClass)
        {
          boolean = boolPropertyClass.Boolean;
          break;
        }
        break;
      }
    }
    if (boolean != Convert.ToBoolean(this.attributeValue))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetObjectType(Convert.ToInt32(id)) is IDBMetadataExtensions objectType)
        {
          string rightsFromTechProc = TechCardConsts.Params.MdeInheritArchiveRightsFromTechProc;
          int[] valuesList = new int[1]
          {
            Convert.ToInt32(boolean)
          };
          objectType.SetMDValues(rightsFromTechProc, 4, valuesList);
        }
      }
      this.attributeValue = (object) boolean;
    }
    return false;
  }

  /// <summary>Регистрация настройки в конфигураторе</summary>
  public static void RegisterCategoryProp(IServiceProvider serviceProvider)
  {
    ServiceUtils.GetService<IDatabaseConfiguratorService>((object) serviceProvider, false)?.RegisterCategoryProps(4, (ICategoryProps) new InheritArchiveRightsFromTechProcessOption());
  }
}
