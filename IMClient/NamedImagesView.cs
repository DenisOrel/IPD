
// Type: IMClient.NamedImagesView




using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace IMClient
{
    public class NamedImagesView : DockControl
    {
      private System.ComponentModel.Container components;
      private ListView _listView;
      private NamedImageList _namedImageList;

      public NamedImagesView(System.IServiceProvider provider)
      {
        this.InitializeComponent();
        this._namedImageList = (NamedImageList) provider.GetService(typeof (INamedImageList));
        if (this._namedImageList != null)
          this._namedImageList.Changed += new EventHandler(this.NamedImageList_Changed);
        this.BuildList();
      }

      private void BuildList()
      {
        if (this._namedImageList == null)
          return;
        this._listView.SmallImageList = this._namedImageList.ImageList;
        this._listView.BeginUpdate();
        try
        {
          this._listView.Items.Clear();
          foreach (string key in (IEnumerable) this._namedImageList.Keys)
          {
            ListViewItem listViewItem = new ListViewItem();
            int num = this._namedImageList.ImageIndex(key);
            listViewItem.Text = $"{key}({num})";
            listViewItem.ImageIndex = num;
            this._listView.Items.Add(listViewItem);
          }
        }
        finally
        {
          this._listView.EndUpdate();
        }
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NamedImagesView));
        this._listView = new ListView();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this._listView, "_listView");
        this._listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this._listView.Name = "_listView";
        this._listView.Sorting = SortOrder.Ascending;
        this._listView.UseCompatibleStateImageBehavior = false;
        this._listView.View = View.List;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
        this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
        this.Controls.Add((Control) this._listView);
        this.Guid = ViewGuids.NamedImagesView_Guid;
        this.HideOnClose = true;
        this.Name = nameof (NamedImagesView);
        this.ShowHint = DockState.DockLeftAutoHide;
        this.Tag = (object) " ";
        this.ResumeLayout(false);
      }

      private void NamedImageList_Changed(object sender, EventArgs e) => this.BuildList();
    }
}
