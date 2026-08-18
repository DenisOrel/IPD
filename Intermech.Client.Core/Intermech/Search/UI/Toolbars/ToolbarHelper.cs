
// Type: Intermech.Search.UI.Toolbars.ToolbarHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Search.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.UI.Toolbars;

public static class ToolbarHelper
{
  public static void InitializeConfigurationToolbar(
    ToolBar configurationToolbar,
    IOutputView outputView)
  {
    if (configurationToolbar == null)
      throw new ArgumentNullException(nameof (configurationToolbar));
    if (outputView == null)
      throw new ArgumentNullException(nameof (outputView));
    IConfigurationOptionInfoProvider optionInfoProvider = ServiceLocator.Get<IConfigurationOptionInfoProvider>();
    IConfigurationOptionRepository optionRepository = ServiceLocator.Get<IConfigurationOptionRepository>();
    ICurrentUserAndRole currentUserAndRole = ServiceLocator.Get<ICurrentUserAndRole>();
    INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
    namedImageList.Add(Intermech.Search.UI.ImageHelper.GetImage("GroupSelect_16x16.png"), "GroupSelect_16x16.png");
    namedImageList.Add(Intermech.Search.UI.ImageHelper.GetImage("MenuBlue_16x16.png"), "MenuBlue_16x16.png");
    namedImageList.Add(Intermech.Search.UI.ImageHelper.GetImage("RedDocumentCross_16x16.png"), "RedDocumentCross_16x16.png");
    optionRepository.OptionChanged += new EventHandler<Intermech.Search.Configuration.ConfigurationOptionChangedEventArgs>(ToolbarHelper.ConfigurationOptionRepository_OptionChanged);
    IOrderedEnumerable<ConfigurationOptionInfo> orderedEnumerable = optionInfoProvider.GetOptionsInfo().OrderBy<ConfigurationOptionInfo, string>((Func<ConfigurationOptionInfo, string>) (o => o.Page));
    ConfigurationOptionInfo configurationOptionInfo1 = (ConfigurationOptionInfo) null;
    foreach (ConfigurationOptionInfo configurationOptionInfo2 in (IEnumerable<ConfigurationOptionInfo>) orderedEnumerable)
    {
      if (!(configurationOptionInfo2.Type != typeof (bool)))
      {
        if (!string.IsNullOrEmpty(configurationOptionInfo2.ImageKey))
        {
          try
          {
            ButtonItem buttonItem = new ButtonItem();
            buttonItem.BeginGroup = configurationOptionInfo1 == null || configurationOptionInfo1 != null && configurationOptionInfo1.Page != configurationOptionInfo2.Page;
            buttonItem.Checked = (bool) optionRepository.Find(configurationOptionInfo2.Key);
            buttonItem.Click += new EventHandler(ToolbarHelper.ButtonItem_Click);
            buttonItem.Enabled = !configurationOptionInfo2.CheckAdmin || configurationOptionInfo2.CheckAdmin && currentUserAndRole.IsAdmin;
            buttonItem.Image = namedImageList.ImageList.Images[namedImageList.ImageIndex(configurationOptionInfo2.ImageKey)];
            buttonItem.ShowText = false;
            buttonItem.Tag = (object) configurationOptionInfo2.Key;
            buttonItem.Text = configurationOptionInfo2.DisplayName;
            buttonItem.ToolTipText = configurationOptionInfo2.Description;
            buttonItem.Visible = false;
            configurationToolbar.Items.Add((ToolbarItemBase) buttonItem);
            configurationOptionInfo1 = configurationOptionInfo2;
          }
          catch (Exception ex)
          {
            outputView.WriteString("Debug", $"Не удалось создать элемент панели инструментов 'Настройка': {ex.Message}");
          }
        }
      }
    }
  }

  private static void ConfigurationOptionRepository_OptionChanged(
    object sender,
    Intermech.Search.Configuration.ConfigurationOptionChangedEventArgs e)
  {
    foreach (object obj in (CollectionBase) Holder.BarManager.FindToolbar(ToolbarGuids.ConfigurationToolbarGuid).Items)
    {
      if (obj is ButtonItem buttonItem && buttonItem.Tag as ConfigurationOptionKey == e.OptionKey)
        buttonItem.Checked = (bool) e.NewValue;
    }
  }

  private static void ButtonItem_Click(object sender, EventArgs e)
  {
    ButtonItem buttonItem = (ButtonItem) sender;
    ConfigurationOptionKey tag = (ConfigurationOptionKey) buttonItem.Tag;
    ServiceLocator.Get<IConfigurationOptionRepository>().AddOrUpdate(tag, (object) !buttonItem.Checked);
  }
}
