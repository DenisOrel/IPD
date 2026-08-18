
// Type: IMClient.Views.DocumentLayoutView




using Intermech.Client.Core.Visualizers;
using Intermech.Docking;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;


namespace IMClient.Views
{
    internal class DocumentLayoutView : DockControl, IDocumentLayoutSite
    {
      private PageControl _pages;
      private System.IServiceProvider _serviceProvider;
      private IDocumentLayout _layout;

      public DocumentLayoutView(System.IServiceProvider provider)
      {
        this.InitializeComponent();
        this._serviceProvider = provider;
        if (!(this._serviceProvider.GetService(typeof (IDocumentLayoutSite)) is IDocumentLayoutSite))
          (this._serviceProvider as IServiceContainer).AddService(typeof (IDocumentLayoutSite), (object) this);
        INamedImageList service = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
        this.TabImageIndex = service.ImageIndex("imgDocumentLayout");
        this._pages.ImageList = service.ImageList;
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentLayoutView));
        this._pages = new PageControl();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this._pages, "_pages");
        this._pages.BorderStyle = Intermech.Docking.Rendering.BorderStyle.None;
        this._pages.Name = "_pages";
        this._pages.TabAlignment = Intermech.Docking.TabAlignment.Bottom;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
        this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
        this.Controls.Add((Control) this._pages);
        this.DoubleBuffered = true;
        this.Guid = ViewGuids.DocumentLayoutView_Guid;
        this.HideOnClose = true;
        this.Name = nameof (DocumentLayoutView);
        this.ShowHint = DockState.DockLeftAutoHide;
        this.Tag = (object) " ";
        this.ResumeLayout(false);
      }

      public IDocumentLayout Layout
      {
        get => this._layout;
        set
        {
          if (this._layout == value)
            return;
          this._layout = value;
          this.CreateLayout();
        }
      }

      private void CreateLayout()
      {
        this._pages.TabPages.Clear();
        if (this._layout == null)
          return;
        ILayoutTab[] tabs = this._layout.Tabs;
        int length = tabs.Length;
        for (int index = 0; index < length; ++index)
        {
          ILayoutTab lt = tabs[index];
          Intermech.Docking.TabPage tabPage = new Intermech.Docking.TabPage(lt.Name);
          tabPage.TabImageIndex = lt.ImageIndex;
          this.FillTab(tabPage, lt);
          this._pages.TabPages.Add(tabPage);
        }
      }

      private void FillTab(Intermech.Docking.TabPage tab, ILayoutTab lt)
      {
        switch (lt.TabType)
        {
          case LayoutTabType.List:
            ListBox listBox = new ListBox();
            listBox.Items.AddRange((object[]) lt.Items);
            listBox.SelectedIndexChanged += new EventHandler(this.ListBox_SelectedIndexChanged);
            listBox.Dock = DockStyle.Fill;
            tab.Controls.Add((Control) listBox);
            break;
          case LayoutTabType.CheckList:
            CheckedListBox checkedListBox = new CheckedListBox();
            ILayoutItem[] items = lt.Items;
            checkedListBox.Items.AddRange((object[]) items);
            int length = items.Length;
            for (int index = 0; index < length; ++index)
              checkedListBox.SetItemChecked(index, items[index].Checked);
            checkedListBox.ItemCheck += new ItemCheckEventHandler(this.CheckedListBox_ItemCheck);
            checkedListBox.Dock = DockStyle.Fill;
            tab.Controls.Add((Control) checkedListBox);
            break;
          case LayoutTabType.Thumbs:
          case LayoutTabType.Tree:
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("IMClient_20"), (object) lt.TabType.ToString()));
        }
      }

      private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
      {
        throw new Exception("The method or operation is not implemented.");
      }

      private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
      {
        if (!((sender as ListBox).SelectedItem is ILayoutItem selectedItem))
          return;
        selectedItem.Selected = true;
      }
    }
}
