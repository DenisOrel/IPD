
// Type: IMClient.ClipboardView




using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient
{
    public class ClipboardView : DockControl, IClipboard, ISkipTargetActivate
    {
      private System.ComponentModel.Container _components;
      private ColumnHeader _colHeader;
      private ListView _listView;
      private System.IServiceProvider _serviceProvider;
      private ListViewItem _oldFocused;
      private ClipboardView.SavedItem[] _saved;
      private ClipboardView.SavedItem _focused;
      private Button _btnClear;
      private bool _canPop;

      public ClipboardView(System.IServiceProvider provider)
      {
        this._serviceProvider = provider;
        this.InitializeComponent();
        INamedImageList service = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
        this._listView.SmallImageList = (this._serviceProvider.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService).ImageList;
        this.TabImageIndex = service.ImageIndex("imgPaste");
        IntPtr handle = this._listView.Handle;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this._components != null)
          this._components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ClipboardView));
        this._listView = new ListView();
        this._colHeader = new ColumnHeader();
        this._btnClear = new Button();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this._listView, "_listView");
        this._listView.BackColor = SystemColors.Control;
        this._listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this._listView.Columns.AddRange(new ColumnHeader[1]
        {
          this._colHeader
        });
        this._listView.FullRowSelect = true;
        this._listView.GridLines = true;
        this._listView.HeaderStyle = ColumnHeaderStyle.None;
        this._listView.HideSelection = false;
        this._listView.MultiSelect = false;
        this._listView.Name = "_listView";
        this._listView.UseCompatibleStateImageBehavior = false;
        this._listView.View = View.Details;
        this._listView.SelectedIndexChanged += new EventHandler(this.On_listView_SelectedIndexChanged);
        this._listView.DoubleClick += new EventHandler(this.On_listView_DoubleClick);
        this._listView.Layout += new LayoutEventHandler(this.On_listView_Layout);
        componentResourceManager.ApplyResources((object) this._colHeader, "_colHeader");
        componentResourceManager.ApplyResources((object) this._btnClear, "_btnClear");
        this._btnClear.Name = "_btnClear";
        this._btnClear.UseVisualStyleBackColor = true;
        this._btnClear.Click += new EventHandler(this.On_btnClear_Click);
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Float;
        this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
        this.Controls.Add((Control) this._listView);
        this.Controls.Add((Control) this._btnClear);
        this.Guid = ViewGuids.ClipboardView_Guid;
        this.HideOnClose = true;
        this.Name = nameof (ClipboardView);
        this.ShowHint = DockState.DockLeftAutoHide;
        this.ResumeLayout(false);
      }

      private void On_btnClear_Click(object sender, EventArgs e)
      {
        this._listView.Items.Clear();
        ((BarManager) this._serviceProvider.GetService(typeof (BarManager))).MenuBar.FindMenuItem("Edit.Paste").Enabled = false;
        this._listView.Scrollable = false;
        this._listView.Refresh();
        this._listView.Scrollable = true;
      }

      private void On_listView_DoubleClick(object sender, EventArgs e)
      {
        if (this._listView.FocusedItem == null)
          return;
        ICommandManager service = (ICommandManager) this._serviceProvider.GetService(typeof (ICommandManager));
        service?.Execute(service.FindCommand("Paste"));
      }

      private void On_listView_Layout(object sender, LayoutEventArgs e)
      {
        try
        {
          this._listView.Columns[0].Width = -2;
        }
        catch (Exception ex)
        {
        }
      }

      private void On_listView_SelectedIndexChanged(object sender, EventArgs e)
      {
        if (this._listView.FocusedItem == null || this._listView.FocusedItem == this._oldFocused)
          return;
        this._oldFocused = this._listView.FocusedItem;
        this.OnChanged();
      }

      public event EventHandler Changed;

      public event EventHandler ContextChanged;

      public object[] GetDataObjects() => this.GetDataObjects((System.Type) null);

      public object[] GetDataObjects(System.Type needType)
      {
        ListView.ListViewItemCollection items = this._listView.Items;
        int count = items.Count;
        ArrayList arrayList = new ArrayList(count);
        for (int index = 0; index < count; ++index)
        {
          object tag = items[index].Tag;
          if (tag != null && (needType == (System.Type) null || needType.IsAssignableFrom(tag.GetType())))
            arrayList.Add(tag);
        }
        return arrayList.ToArray();
      }

      public object GetDataObject()
      {
        if (this._listView.Items.Count == 0)
          return (object) null;
        return (this._listView.FocusedItem ?? this._listView.Items[0])?.Tag;
      }

      public void Pop()
      {
        if (!this._canPop)
          return;
        ListViewItem listViewItem1 = (ListViewItem) null;
        try
        {
          this._listView.BeginUpdate();
          this._listView.Items.Clear();
          if (this._saved == null)
            return;
          int length = this._saved.Length;
          for (int index = 0; index < length; ++index)
          {
            ClipboardView.SavedItem savedItem = this._saved[index];
            ListViewItem listViewItem2 = this._listView.Items.Add(savedItem._name, this.GetImageIndex(savedItem._tag));
            listViewItem2.Tag = savedItem._tag;
            if (this._focused == savedItem)
              listViewItem1 = listViewItem2;
          }
        }
        finally
        {
          this._listView.EndUpdate();
          if (listViewItem1 != null)
          {
            this._listView.FocusedItem = listViewItem1;
            listViewItem1.EnsureVisible();
          }
        }
      }

      public void Push()
      {
        int count = this._listView.Items.Count;
        if (count == 0)
        {
          this._saved = (ClipboardView.SavedItem[]) null;
          this._focused = (ClipboardView.SavedItem) null;
        }
        else
        {
          this._saved = new ClipboardView.SavedItem[count];
          ListViewItem focusedItem = this._listView.FocusedItem;
          for (int index = 0; index < count; ++index)
          {
            ListViewItem listViewItem = this._listView.Items[index];
            this._saved[index] = new ClipboardView.SavedItem(listViewItem.Tag, listViewItem.Text);
            if (listViewItem == focusedItem)
              this._focused = this._saved[index];
          }
        }
        this._canPop = true;
      }

      public void RefreshImage()
      {
        if (this._listView.Items.Count == 0)
          return;
        ListViewItem focusedItem = this._listView.FocusedItem;
        if (focusedItem == null)
          return;
        focusedItem.ImageIndex = this.GetImageIndex(focusedItem.Tag);
      }

      public void RemoveDataObjects(System.Type type)
      {
        ListView.ListViewItemCollection items = this._listView.Items;
        int count1 = items.Count;
        List<ListViewItem> listViewItemList = new List<ListViewItem>(count1);
        for (int index = 0; index < count1; ++index)
        {
          object tag = items[index].Tag;
          if (tag != null && (type == (System.Type) null || type.IsAssignableFrom(tag.GetType())))
            listViewItemList.Add(items[index]);
        }
        if (listViewItemList.Count <= 0)
          return;
        int count2 = listViewItemList.Count;
        for (int index = 0; index < count2; ++index)
          items.Remove(listViewItemList[index]);
        this.OnContextChanged();
      }

      public void RemoveCurrentDataObject()
      {
        if (this._listView.Items.Count == 0)
          return;
        ListViewItem focusedItem = this._listView.FocusedItem;
        if (focusedItem == null)
        {
          ListViewItem listViewItem = this._listView.Items[0];
        }
        else
          this._listView.Items.Remove(focusedItem);
      }

      public void SetDataObject(object clipboardObject)
      {
        if (clipboardObject == null)
          throw new ArgumentException("Parametr can be null", nameof (clipboardObject));
        this.SetDataObject(clipboardObject, clipboardObject.ToString());
      }

      public void SetDataObject(object clipboardObject, string title)
      {
        if (clipboardObject == null)
          throw new ArgumentException("Parametr can be null", nameof (clipboardObject));
        if (this._listView == null)
          return;
        int count = this._listView.Items.Count;
        ListViewItem listViewItem = (ListViewItem) null;
        for (int index = 0; index < count; ++index)
        {
          listViewItem = this._listView.Items[index];
          if (!object.Equals(clipboardObject, listViewItem.Tag))
            listViewItem = (ListViewItem) null;
          else
            break;
        }
        if (listViewItem == null)
        {
          listViewItem = this._listView.Items.Insert(0, title, this.GetImageIndex(clipboardObject));
          listViewItem.Tag = clipboardObject;
        }
        listViewItem.Focused = true;
        listViewItem.Selected = true;
        this._listView.EnsureVisible(listViewItem.Index);
        this.OnContextChanged();
      }

      private int GetImageIndex(object clipboardObject)
      {
        ITypedIDCollection typedIdCollection = clipboardObject as ITypedIDCollection;
        ICategoryTypeIconService service = this._serviceProvider.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
        if (typedIdCollection != null && typedIdCollection.Count > 0)
        {
          if (clipboardObject is ICutCopy)
            return service.IndexOf(4, (typedIdCollection[0] as ClipboardObject).IDBTypedObjectID.ObjectType);
          if (clipboardObject is DBAttributeIDCollection)
            return service.IndexOf(3, 0);
        }
        return !(clipboardObject is ICutCopy) ? -1 : (clipboardObject as ICutCopy).ImageIndex;
      }

      private void OnChanged()
      {
        if (this.Changed == null)
          return;
        this.Changed((object) this, EventArgs.Empty);
      }

      private void OnContextChanged()
      {
        if (this.ContextChanged == null)
          return;
        this.ContextChanged((object) this, EventArgs.Empty);
      }

      internal class SavedItem
      {
        internal object _tag;
        internal string _name;

        internal SavedItem(object tag, string name)
        {
          this._name = name;
          this._tag = tag;
        }
      }
    }
}
