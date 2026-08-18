
// Type: IMClient.ServerOutputView




using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace IMClient
{
    public class ServerOutputView : DockControl, ICommandTarget
    {
      private System.ComponentModel.Container components;
      private ComboBox _categoryList;
      private Hashtable _categories = new Hashtable();
      private TextBox _output;
      private System.IServiceProvider _serviceProvider;
      private bool _updating;

      public ServerOutputView(System.IServiceProvider provider)
      {
        this._updating = false;
        this._serviceProvider = provider;
        this.InitializeComponent();
        this.TabImageIndex = ((INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList))).ImageIndex("imgOutputServer");
        this.BeforeFirstShown += new EventHandler(this.ServerOutputView_BeforeFirstShown);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          if (this.components != null)
            this.components.Dispose();
          this.BeforeFirstShown -= new EventHandler(this.ServerOutputView_BeforeFirstShown);
        }
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ServerOutputView));
        this._categoryList = new ComboBox();
        this._output = new TextBox();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this._categoryList, "_categoryList");
        this._categoryList.DropDownStyle = ComboBoxStyle.DropDownList;
        this._categoryList.Name = "_categoryList";
        this._categoryList.Sorted = true;
        this._categoryList.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
        componentResourceManager.ApplyResources((object) this._output, "_output");
        this._output.Name = "_output";
        this._output.ReadOnly = true;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
        this.Controls.Add((Control) this._output);
        this.Controls.Add((Control) this._categoryList);
        this.Guid = ViewGuids.ServerOutputView_Guid;
        this.HideOnClose = true;
        this.Name = nameof (ServerOutputView);
        this.ShowHint = DockState.DockBottomAutoHide;
        this.ResumeLayout(false);
        this.PerformLayout();
      }

      private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
      {
        if (this._updating || !(this._categoryList.SelectedItem is string selectedItem))
          return;
        object category = (object) (string) this._categories[(object) selectedItem];
        if (category == null || this._output.Tag == category)
          return;
        this._output.Tag = category;
        this._output.Text = (string) category;
      }

      public bool Execute(ICommandState commandState)
      {
        if (!(commandState.CommandName == "Refresh"))
          return false;
        this.RefreshOutput();
        return true;
      }

      public bool QueryStatus(ICommandState commandState)
      {
        if (!(commandState.CommandName == "Refresh"))
          return false;
        commandState.Enabled = true;
        return true;
      }

      private void ServerOutputView_BeforeFirstShown(object sender, EventArgs e)
      {
        this.RefreshOutput();
      }

      private void RefreshOutput()
      {
        if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IOutputViewHistory)) is IOutputViewHistory customService))
          return;
        List<Tuple<string, string>> outputHistory = customService.GetOutputHistory();
        this._categories.Clear();
        string text = this._categoryList.Text;
        this._categoryList.Items.Clear();
        foreach (Tuple<string, string> tuple in outputHistory)
        {
          this._categoryList.Items.Add((object) tuple.Item1);
          this._categories[(object) tuple.Item1] = (object) tuple.Item2;
        }
        int num = -1;
        if (this._categoryList.Items.Count > 0)
          num = text == null || text.Length <= 0 ? 0 : this._categoryList.Items.IndexOf((object) text);
        this._updating = false;
        if (num == -1)
          return;
        this._categoryList.SelectedIndex = num;
      }
    }
}
