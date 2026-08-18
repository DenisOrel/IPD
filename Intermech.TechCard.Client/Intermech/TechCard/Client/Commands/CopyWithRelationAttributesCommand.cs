// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Commands.CopyWithRelationAttributesCommand
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Commands;

/// <summary>
/// 
/// </summary>
internal class CopyWithRelationAttributesCommand : TechCardSelectedItemsCommand
{
  /// <summary>Информация о связях (контексте) копируемых объектов</summary>
  private readonly List<IDBRelationID> _relItems = new List<IDBRelationID>();

  /// <summary>Конструктор</summary>
  public CopyWithRelationAttributesCommand()
    : base("CopyWithRelAttrs")
  {
  }

  /// <summary>Проверка параметров команды</summary>
  /// <returns></returns>
  protected override bool ValidateCommandArgs()
  {
    if (!base.ValidateCommandArgs())
      return false;
    this._relItems.AddRange((IEnumerable<IDBRelationID>) this.Items.AsItemsList<IDBRelationID>());
    if (this._relItems.Count == 0)
      throw new Exception(LocalizationHolder.rm.GetString("TechCard.Client_527"));
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override bool ExecuteCommand()
  {
    List<int> list1 = this._relItems.Select<IDBRelationID, int>((Func<IDBRelationID, int>) (item => item.RelationType)).ToList<int>();
    GenericListHelper.MakeUnique<int>(list1);
    List<int> intList = new List<int>();
    string name = "TechCopyWithRelAttr_" + string.Join<int>(",", (IEnumerable<int>) list1);
    IConfigurationManager service1 = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service1 != null)
    {
      IConfiguration configuration = service1.Open(name);
      if (configuration != null)
      {
        string property = configuration.GetProperty("attributes");
        if (!string.IsNullOrEmpty(property))
        {
          string str = property;
          char[] chArray = new char[1]{ ',' };
          foreach (string s in str.Split(chArray))
          {
            int result;
            if (int.TryParse(s, out result))
              intList.Add(result);
          }
        }
      }
    }
    List<int> list2 = new List<int>();
    foreach (int relTypeID in list1)
      list2.AddRange(MetaDataHelper.GetAttribute4RelationTypeList(relTypeID).Select<IMSAttribute4RelationType, int>((Func<IMSAttribute4RelationType, int>) (item => item.AttributeID)));
    GenericListHelper.MakeUnique<int>(list2);
    bool filterRecords = CoreConsts.FilterRecords;
    try
    {
      CoreConsts.FilterRecords = false;
      SelectorForm selectorForm = new SelectorForm(typeof (AttributesFolder), LocalizationHolder.rm.GetString("TechCard.Client_528"), new System.Type[4]
      {
        typeof (AttributeFolder),
        typeof (AttributeGroupFolder),
        typeof (AttributeTypeAssignedGroupFolder),
        typeof (AttributesFolder)
      }, true);
      selectorForm.SelectFocusedWhenNothingMultiselected = false;
      selectorForm.ClearSelection();
      selectorForm.OnCheckActions = SelectorForm.CheckActions.UncheckParents | SelectorForm.CheckActions.UncheckChildren | SelectorForm.CheckActions.CheckChildren;
      selectorForm.OnUncheckActions = SelectorForm.CheckActions.UncheckParents | SelectorForm.CheckActions.UncheckChildren;
      selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(list2.ToArray(), true, true);
      if (intList.Count != 0)
      {
        selectorForm.ExpandLevelsOnLoad = -1;
        selectorForm.InitSelectionAsType(new ArrayList((ICollection) intList), new ArrayList((ICollection) new System.Type[1]
        {
          typeof (AttributeFolder)
        }));
      }
      else
        selectorForm.ExpandLevelsOnLoad = 1;
      if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
        return false;
      intList.Clear();
      foreach (object id in selectorForm.IDList)
        intList.Add(Convert.ToInt32(id));
    }
    finally
    {
      CoreConsts.FilterRecords = filterRecords;
    }
    if (service1 != null)
    {
      IConfiguration configuration = service1.Open(name) ?? service1.Create(name);
      if (configuration != null)
      {
        string str = string.Join<int>(",", (IEnumerable<int>) intList);
        configuration.SetProperty("attributes", str);
      }
    }
    ISelectedItems items = this.Items;
    ObjectCommands.AddToWindowsClipboard(items, this.ContextServices, this.AdditionalInfo);
    ArrayList idList = new ArrayList(items.Count);
    for (int index = 0; index < items.Count; ++index)
    {
      object itemData1 = items.GetItemData(index, typeof (IDBTypedObjectID));
      object itemData2 = items.GetItemData(index, typeof (IDBRelationID));
      if (itemData1 != null)
      {
        ClipboardObject clipboardObject = new ClipboardObject(itemData1 as IDBTypedObjectID, itemData2 as IDBRelationID);
        idList.Add((object) clipboardObject);
      }
    }
    IIOSource service2 = (IIOSource) (this.ContextServices.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView);
    IClipboard service3 = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false);
    if (service3 == null)
      return false;
    service3.SetDataObject((object) new CopyWithRelationAttributesCommand.CopyWithAttrClipboardObjectsList(idList, false, service2)
    {
      CopyRelAttrs = intList
    });
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="session"></param>
  protected override void DoAfterProceedItems(IUserSession session)
  {
    base.DoAfterProceedItems(session);
    TechCardSelectedItemsCommand.ClearCheckedItems(this.ContextServices);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <summary>Создать список объектов</summary>
  /// <param name="idList">Список объектов</param>
  /// <param name="isCut">true - объекты были помещены в список с помощью команды Вырезать</param>
  /// <param name="source">Информация об источнике объектов idList</param>
  internal class CopyWithAttrClipboardObjectsList(ArrayList idList, bool isCut, IIOSource source) : 
    ClipboardObjectsList(idList, isCut, source, (IDBTypedObjectID) null)
  {
    /// <summary>
    /// 
    /// </summary>
    public List<int> CopyRelAttrs { get; set; }
  }
}
