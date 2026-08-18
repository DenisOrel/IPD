
// Type: Intermech.PropertyEditors.AttributeTypeAssignedGroupFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class AttributeTypeAssignedGroupFolder : AttributeGroupFolder
{
  public override bool DelEnabled => false;

  public override bool AddChildEnabled => true;

  public override bool NeedApply => false;

  public override bool NeedSave => false;

  public override bool CutEnabled => false;

  public override bool PasteEnabled => false;

  public override bool NeedPageSave => false;

  public AttributeTypeAssignedGroupFolder(Guid aInstGuid, object aNodeParent, bool useFilter)
    : base(aInstGuid, LocalizationHolder.rm.GetString("TypeAssignedGroupName"), aNodeParent, -10, false, LocalizationHolder.rm.GetString("TypeAssignedGroupNote"), string.Empty, string.Empty, Guid.Empty, useFilter)
  {
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(Statics.CategoryAttributes, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetAttributesGroup(Convert.ToInt32(-1));
  }

  public override void LoadDataTable(bool reload)
  {
    DataTable dataTable = DataHolders.AttributesHolder.LoadData((reload ? 1 : 0) != 0, (object) -1);
    this.dataTable = dataTable.Clone();
    List<int> attributesInGroup = MetaDataHelper.GetAttributesInGroup(-10);
    attributesInGroup.Sort();
    DataRow[] dataRowArray = dataTable.Select("", "F_ATTRIBUTE_ID");
    if (attributesInGroup.Count == 0)
      return;
    int index1 = 0;
    int index2 = 0;
    while (index2 < dataRowArray.Length)
    {
      int int32 = Convert.ToInt32(dataRowArray[index2]["F_ATTRIBUTE_ID"]);
      if (int32 >= attributesInGroup[index1])
      {
        if (int32 > attributesInGroup[index1])
        {
          ++index1;
          if (index1 >= attributesInGroup.Count)
            break;
          continue;
        }
        DataSetProcessor.AddRow(this.dataTable, dataRowArray[index2], false);
      }
      ++index2;
    }
    this.dataTable.AcceptChanges();
  }

  public override bool LoadDataCallback(bool reload)
  {
    PropertyGrid propertyGrid = (this.GetPropertyForm() as IConfigPage).PropertyGrid;
    if (propertyGrid == null)
      return true;
    EventsHolder.BlockOnChange = true;
    try
    {
      propertyGrid.SelectedObject = (object) this;
      this.PropDescriptorCollection[0].SetValue((object) this, (object) this.textValue);
      this.PropDescriptorCollection[1].SetValue((object) this, (object) this.noteValue);
    }
    finally
    {
      EventsHolder.BlockOnChange = false;
    }
    return true;
  }

  public override void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    base.SetContextMenuItemStatus(contextMenu);
    this.miAdd.Visible = false;
    this.miAddGroup.Visible = false;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.AttributeGroup_Name, true, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_35"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.AttributeGroup_Note, true, true, false));
  }

  public override void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, (object) TabPagesHolder.TabPages(this.instGuid).ListTabPage);
  }
}
