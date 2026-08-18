// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionExpTableSetup
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Bars;
using Intermech.Expert;
using Intermech.Expert.Editor;
using Intermech.Expert.Table;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.Protection;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public sealed class AutoSelectionExpTableSetup
{
  public static bool EditTables(ref eTable[] expTables)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    using (TableSetup form = new TableSetup(expTables))
    {
      form.AllowAnyObjectType = true;
      form.AllowEmptyTableName = true;
      form.Controls["panel2"].Controls["panel3"].Controls["cbType"].Enabled = false;
      form.Controls["panel2"].Controls["tbName"].Enabled = false;
      ListBox control = form.Controls["panel9"].Controls["gbResult"].Controls["panel11"].Controls["lbResult"] as ListBox;
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(AutosSelectConsts.ImbaseObjectLinkAttrGuid);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (control != null)
        {
          if (expTables == null)
          {
            CommonTypeHolder commonTypeHolder = new CommonTypeHolder(-1, attributeTypeId, sessionKeeper.Session);
            control.Items.Add((object) commonTypeHolder);
          }
          control.Enabled = false;
        }
        int num2 = form.ShowTopDialog().Equals((object) DialogResult.OK) ? 1 : 0;
        if (num2 != 0)
          expTables = form.Tables;
        return num2 != 0;
      }
    }
  }

  public static bool EditTableData(ref eTable[] expTables)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    using (AutoSelectionTableEditForm form = new AutoSelectionTableEditForm(expTables))
    {
      Control control1 = form.Controls["pnlClient"];
      if (control1 != null)
      {
        Control control2 = control1.Controls["tecExpTable"];
        if (control2 != null && control2.Controls["menuBar1"] is MenuBar control3)
        {
          MenuItemBase menuItem = control3.FindMenuItem("menu.menu_Setup");
          if (menuItem != null)
            menuItem.Visible = false;
        }
      }
      int num2 = form.ShowTopDialog().Equals((object) DialogResult.OK) ? 1 : 0;
      if (num2 != 0)
        expTables = form.Tables;
      return num2 != 0;
    }
  }
}
