// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Commands.BlankSetupCommand
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Document.Client.Configs.Visual;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Client.Commands;

internal class BlankSetupCommand
{
  public BlankSetupCommand(BlankSetupCommandMode commandMode) => this.CommandMode = commandMode;

  private BlankSetupCommandMode CommandMode { get; }

  public void Execute(ISelectedItems items, IServiceProvider viewServices, object additionalInfo)
  {
    if (items == null || items.Count != 1 || !(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    Rules rules;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      rules = DocumentConfigLoader.Load(itemData.Value, sessionKeeper.Session);
    string str = !string.IsNullOrEmpty(itemData.Caption) ? itemData.Caption : LocalizationHolder.rm.GetString("TechCard.Document_187");
    BlankSetupPage blankSetupPage = new BlankSetupPage();
    blankSetupPage.Rules = rules;
    blankSetupPage.ReadOnly = this.CommandMode == BlankSetupCommandMode.View;
    blankSetupPage.Text = str;
    blankSetupPage.TabText = str;
    blankSetupPage.Show(ApplicationServices.Container.GetService<DockManager>(), DockState.Document);
  }
}
