
// Type: IMClient.ToolbarControls.RulesDropDownControl




using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace IMClient.ToolbarControls
{
    internal sealed class RulesDropDownControl : ObjectsDropDownControl
    {
      internal MainForm mainForm;

      public RulesDropDownControl(
        MainForm mainForm,
        DropDownMenuItem menu,
        Image image,
        long selectedItem)
        : base(menu, ObjectsDropDownOptions.Default, LocalizationHolder.rm.GetString("IMClient_86"), image, (MyObjectElement) null, (IList<long>) null, (IList<int>) new int[1]
        {
          MetaDataHelper.GetObjectTypeID("cad001b3-306c-11d8-b4e9-00304f19f545")
        }, selectedItem)
      {
        this.mainForm = mainForm;
      }

      protected override void UpdateControls()
      {
        base.UpdateControls();
        if (this.mainForm == null)
          return;
        MyObjectElementEx tag = this.menu.Tag as MyObjectElementEx;
      }

      private void DoRuleVariant(object sender, EventArgs e)
      {
      }

      private void DoRuleBrowse(object sender, EventArgs e)
      {
      }

      private void DoRuleHint(object sender, EventArgs e)
      {
      }
    }
}
