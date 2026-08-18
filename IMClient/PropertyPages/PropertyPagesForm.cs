
// Type: IMClient.PropertyPages.PropertyPagesForm




using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace IMClient.PropertyPages
{
    internal class PropertyPagesForm : Form
    {
      private IContainer components;
      private Button _okButton;
      private Button _cancelButton;
      private SplitContainer _splitContainer;
      private TreeView _treeView;
      private Bevel _bevel;
      private Panel _panel;
      private PropertyGrid _propertyGrid;
      private TextBox _textBox;
      private Button _findButton;
      private Dictionary<string, IPropertyPage> _propertyPageDictionary = new Dictionary<string, IPropertyPage>();
      private bool _propertyPageDictionaryChanged;
      private Dictionary<string, IPropertyPage> _filteredPropertyPageDictionary = new Dictionary<string, IPropertyPage>();
      private PropertyPagesService _propertyPagesService;
      private System.IServiceProvider _serviceProvider;
      private int _folderOpenedIndex;
      private int _folderIndex;
      private int _pageIndex;
      private int _selectedIndex;
      private IPropertyPage _currentPageCache;
      private Control _currentPageControl;
      private string _defaultNodePath;
      private bool _dirty;

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PropertyPagesForm));
        this._splitContainer = new SplitContainer();
        this._treeView = new TreeView();
        this._panel = new Panel();
        this._propertyGrid = new PropertyGrid();
        this._bevel = new Bevel();
        this._okButton = new Button();
        this._cancelButton = new Button();
        this._textBox = new TextBox();
        this._findButton = new Button();
        this._splitContainer.BeginInit();
        this._splitContainer.Panel1.SuspendLayout();
        this._splitContainer.Panel2.SuspendLayout();
        this._splitContainer.SuspendLayout();
        this._panel.SuspendLayout();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this._splitContainer, "_splitContainer");
        this._splitContainer.Name = "_splitContainer";
        this._splitContainer.Panel1.Controls.Add((Control) this._treeView);
        this._splitContainer.Panel2.Controls.Add((Control) this._panel);
        this._splitContainer.Panel2.Controls.Add((Control) this._bevel);
        componentResourceManager.ApplyResources((object) this._treeView, "_treeView");
        this._treeView.FullRowSelect = true;
        this._treeView.HideSelection = false;
        this._treeView.Name = "_treeView";
        this._treeView.Sorted = true;
        this._treeView.AfterSelect += new TreeViewEventHandler(this.TreeView_AfterSelect);
        this._panel.Controls.Add((Control) this._propertyGrid);
        componentResourceManager.ApplyResources((object) this._panel, "_panel");
        this._panel.Name = "_panel";
        componentResourceManager.ApplyResources((object) this._propertyGrid, "_propertyGrid");
        this._propertyGrid.LineColor = SystemColors.Control;
        this._propertyGrid.Name = "_propertyGrid";
        this._propertyGrid.PropertySort = PropertySort.Alphabetical;
        this._propertyGrid.ToolbarVisible = false;
        this._propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
        componentResourceManager.ApplyResources((object) this._bevel, "_bevel");
        this._bevel.Name = "_bevel";
        componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
        this._okButton.Name = "_okButton";
        this._okButton.Click += new EventHandler(this.OKButton_Click);
        componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
        this._cancelButton.DialogResult = DialogResult.Cancel;
        this._cancelButton.Name = "_cancelButton";
        this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
        componentResourceManager.ApplyResources((object) this._textBox, "_textBox");
        this._textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        this._textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        this._textBox.Name = "_textBox";
        this._textBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
        componentResourceManager.ApplyResources((object) this._findButton, "_findButton");
        this._findButton.Name = "_findButton";
        this._findButton.UseVisualStyleBackColor = true;
        this._findButton.Click += new EventHandler(this.FindButton_Click);
        componentResourceManager.ApplyResources((object) this, "$this");
        this.CancelButton = (IButtonControl) this._cancelButton;
        this.Controls.Add((Control) this._findButton);
        this.Controls.Add((Control) this._textBox);
        this.Controls.Add((Control) this._splitContainer);
        this.Controls.Add((Control) this._cancelButton);
        this.Controls.Add((Control) this._okButton);
        this.HelpButton = true;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = nameof (PropertyPagesForm);
        this.SizeGripStyle = SizeGripStyle.Show;
        this.Tag = (object) " ";
        this.HelpButtonClicked += new CancelEventHandler(this.PropertyPagesForm_HelpButtonClicked);
        this.FormClosing += new FormClosingEventHandler(this.PropertyPagesForm_FormClosing);
        this.FormClosed += new FormClosedEventHandler(this.PropertyPagesForm_FormClosed);
        this.Load += new EventHandler(this.PropertyPagesForm_Load);
        this.HelpRequested += new HelpEventHandler(this.PropertyPagesForm_HelpRequested);
        this._splitContainer.Panel1.ResumeLayout(false);
        this._splitContainer.Panel2.ResumeLayout(false);
        this._splitContainer.EndInit();
        this._splitContainer.ResumeLayout(false);
        this._panel.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
      }

      public PropertyPagesForm(
        System.IServiceProvider serviceProvider,
        PropertyPagesService propertyPagesService)
      {
        if (serviceProvider == null)
          throw new ArgumentNullException(nameof (serviceProvider));
        if (propertyPagesService == null)
          throw new ArgumentNullException(nameof (propertyPagesService));
        this._serviceProvider = serviceProvider;
        this._propertyPagesService = propertyPagesService;
        this._propertyPagesService.Changed += new EventHandler(this.OnPropertyPagesService_Changed);
        this.InitializeComponent();
        INamedImageList service = (INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList));
        if (service != null)
        {
          this._treeView.ImageList = service.ImageList;
          this._folderIndex = service.ImageIndex("imgFolder");
          this._folderOpenedIndex = service.ImageIndex("imgFolderOpened");
          this._selectedIndex = service.ImageIndex("imgForward");
          this._pageIndex = service.ImageIndex("imgPropPage");
        }
        this.Dirty = false;
      }

      internal bool Dirty
      {
        get => this._dirty;
        set
        {
          this._dirty = value;
          this._okButton.Enabled = this._dirty;
          Application.DoEvents();
        }
      }

      private void OnPropertyPagesService_Changed(object sender, EventArgs e)
      {
        if (!this.IsHandleCreated)
          return;
        this.Dirty = true;
      }

      internal void AddPage(string path, IPropertyPage propertyPage)
      {
        if (string.IsNullOrEmpty(path))
          throw new ArgumentNullException(nameof (path));
        this._propertyPageDictionary[path] = propertyPage != null ? propertyPage : throw new ArgumentNullException(nameof (propertyPage));
        this._propertyPageDictionaryChanged = true;
      }

      public DialogResult ShowDialog(string defaultNodePath)
      {
        this._defaultNodePath = defaultNodePath;
        return this.ShowDialog();
      }

      private void PropertyPagesForm_FormClosing(object sender, FormClosingEventArgs e)
      {
        if (!this.Dirty)
          return;
        switch (MessageBox.Show("Текущие настройки были изменены. Сохранить изменения?", "Внимание", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
        {
          case DialogResult.Cancel:
            e.Cancel = true;
            break;
          case DialogResult.Yes:
            this.OKButton_Click((object) this, EventArgs.Empty);
            break;
          case DialogResult.No:
            this.CancelButton_Click((object) this, EventArgs.Empty);
            break;
        }
      }

      private void PropertyPagesForm_Load(object sender, EventArgs e)
      {
        HybridDictionary hybridDictionary = new HybridDictionary(0, true);
        FormStorage.LoadLayout((Control) this, (IDictionary) hybridDictionary);
        object obj = hybridDictionary[(object) "Splitter"];
        this._splitContainer.SplitterDistance = obj != null ? (int) obj : (int) byte.MaxValue;
        foreach (KeyValuePair<string, IPropertyPage> propertyPage in this._propertyPageDictionary)
          this.RaiseInitializePage(propertyPage.Value);
        if (this._propertyPageDictionaryChanged)
        {
          this.UpdateFilteredPropertyPageDictionary();
          this.UpdateTreeView();
          this._propertyPageDictionaryChanged = false;
        }
        if (string.IsNullOrEmpty(this._defaultNodePath))
          return;
        TreeNode parent = (TreeNode) null;
        string defaultNodePath = this._defaultNodePath;
        char[] chArray = new char[1]{ '\\' };
        foreach (string name in defaultNodePath.Split(chArray))
        {
          parent = this.FindNode(name, parent);
          if (parent == null)
            break;
        }
        if (parent != null)
        {
          parent.Expand();
          this._treeView.SelectedNode = parent;
        }
        this._defaultNodePath = (string) null;
      }

      private void PropertyPagesForm_FormClosed(object sender, FormClosedEventArgs e)
      {
        FormStorage.SaveLayout((Control) this, (IDictionary) new HybridDictionary(0, true)
        {
          [(object) "Splitter"] = (object) this._splitContainer.SplitterDistance
        });
        if (this._currentPageCache == null)
          return;
        this.ResetCurrentPageCache();
      }

      private void PropertyPagesForm_HelpRequested(object sender, HelpEventArgs hlpevent)
      {
        this.ShowHelp();
      }

      private void PropertyPagesForm_HelpButtonClicked(object sender, CancelEventArgs e)
      {
        e.Cancel = true;
        this.ShowHelp();
      }

      private void TextBox_TextChanged(object sender, EventArgs e)
      {
        this.Find();
        this._textBox.Focus();
      }

      private void FindButton_Click(object sender, EventArgs e) => this.Find();

      private void PropertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
      {
        this._propertyPagesService.OnChanged();
      }

      private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
      {
        for (TreeNode node = e.Node; node != null; node = node.Nodes[0])
        {
          if (node.IsExpanded)
            node.EnsureVisible();
          if (node.Tag != null)
          {
            IPropertyPage tag = node.Tag as IPropertyPage;
            if (this._currentPageCache == tag)
              break;
            if (tag != null)
            {
              object control = tag.Control;
              if (control != null)
              {
                if (this._currentPageControl != null)
                {
                  this.RaiseBeforeDeactivatePage(this._currentPageCache);
                  this._currentPageControl.Visible = false;
                  this._currentPageControl.Parent = (Control) null;
                  this.RaiseAfterDeactivatePage(this._currentPageCache);
                  this._currentPageControl = (Control) null;
                }
                else
                {
                  this._propertyGrid.Visible = false;
                  this._propertyGrid.SelectedObject = (object) null;
                }
                if (control is Control)
                {
                  this._currentPageControl = (Control) control;
                  this.RaiseBeforeActivatePage(tag);
                  this._currentPageControl.Parent = (Control) this._panel;
                  this._currentPageControl.Dock = DockStyle.Fill;
                  this._currentPageControl.Visible = true;
                  this.RaiseAfterActivatePage(tag);
                  this._panel.BorderStyle = BorderStyle.Fixed3D;
                }
                else
                {
                  this._propertyGrid.PropertySort = tag is ISortedPropertyGrid ? ((ISortedPropertyGrid) tag).Sort : PropertySort.Alphabetical;
                  this._propertyGrid.SelectedObject = control;
                  this._propertyGrid.Visible = true;
                  this._panel.BorderStyle = BorderStyle.None;
                }
              }
            }
            this._currentPageCache = tag;
            break;
          }
          if (node.Nodes.Count <= 0)
            break;
        }
      }

      private void RaiseInitializePage(IPropertyPage page)
      {
        if (!(page is IPropertyPageActivationEvents activationEvents))
          return;
        activationEvents.InitializePage();
      }

      private void RaiseBeforeActivatePage(IPropertyPage page)
      {
        if (!(page is IPropertyPageActivationEvents activationEvents))
          return;
        activationEvents.BeforeActivatePage();
      }

      private void RaiseAfterActivatePage(IPropertyPage page)
      {
        if (!(page is IPropertyPageActivationEvents activationEvents))
          return;
        activationEvents.AfterActivatePage();
      }

      private void RaiseBeforeDeactivatePage(IPropertyPage page)
      {
        if (!(page is IPropertyPageActivationEvents activationEvents))
          return;
        activationEvents.BeforeDeactivatePage();
      }

      private void RaiseAfterDeactivatePage(IPropertyPage page)
      {
        if (!(page is IPropertyPageActivationEvents activationEvents))
          return;
        activationEvents.AfterDeactivatePage();
      }

      private void OKButton_Click(object sender, EventArgs e)
      {
        if (this._dirty)
          this._propertyPagesService.Apply();
        this.Dirty = false;
        this.Close();
      }

      private void CancelButton_Click(object sender, EventArgs e)
      {
        this._propertyPagesService.Cancel();
        this.Dirty = false;
        this.Close();
      }

      private TreeNode FindNode(string name, TreeNode parent)
      {
        if (parent == null)
        {
          foreach (TreeNode node in this._treeView.Nodes)
          {
            if (node.Parent == null && node.Text == name)
              return node;
          }
        }
        else
        {
          foreach (TreeNode node in parent.Nodes)
          {
            if (node.Text == name)
              return node;
          }
        }
        return (TreeNode) null;
      }

      private TreeNode AddNodeInHierarchy(string path)
      {
        TreeNode node = (TreeNode) null;
        string str1 = path;
        char[] chArray = new char[1]{ '\\' };
        foreach (string str2 in str1.Split(chArray))
        {
          TreeNode parent = node;
          node = this.FindNode(str2, parent);
          if (node == null)
          {
            node = new TreeNode(str2);
            if (parent != null)
              parent.Nodes.Add(node);
            else
              this._treeView.Nodes.Add(node);
          }
        }
        if (node != null)
        {
          for (TreeNode parent = node.Parent; parent != null; parent = parent.Parent)
          {
            parent.ImageIndex = this._folderIndex;
            parent.SelectedImageIndex = this._folderOpenedIndex;
          }
        }
        return node;
      }

      private void ShowHelp()
      {
        if (this._treeView.SelectedNode == null || !(this._treeView.SelectedNode.Tag is IPropertyPage tag))
          return;
        HelpProvidersClass.ShowHelpTopic(tag.HelpTopicID);
      }

      private void Find()
      {
        this.UpdateFilteredPropertyPageDictionary();
        this.UpdateTreeView();
      }

      private AutoCompleteStringCollection CreateTextBoxAutoCompleteCustomSource()
      {
        AutoCompleteStringCollection completeCustomSource = new AutoCompleteStringCollection();
        foreach (KeyValuePair<string, IPropertyPage> propertyPage in this._propertyPageDictionary)
        {
          string str = ((IEnumerable<string>) propertyPage.Key.Split('\\')).Last<string>();
          if (!string.IsNullOrEmpty(str))
            completeCustomSource.Add(str);
        }
        return completeCustomSource;
      }

      private void UpdateFilteredPropertyPageDictionary()
      {
        this._filteredPropertyPageDictionary.Clear();
        if (string.IsNullOrEmpty(this._textBox.Text))
        {
          this.AddAllFoldersToFilterList();
        }
        else
        {
          string query = this.NormalizeQuery(this._textBox.Text);
          foreach (KeyValuePair<string, IPropertyPage> propertyPage in this._propertyPageDictionary)
          {
            if (!this._filteredPropertyPageDictionary.Keys.Contains<string>(propertyPage.Key) && !this.FilterFolder(query, propertyPage.Key, propertyPage.Value) && propertyPage.Value is IPropertyPageSearchOptionEvents searchOptionEvents && searchOptionEvents.GetOptionNames().Any<string>((Func<string, bool>) (name => name.ToLowerInvariant().Contains(query))))
              this._filteredPropertyPageDictionary.Add(propertyPage.Key, propertyPage.Value);
          }
        }
      }

      private void AddAllFoldersToFilterList()
      {
        foreach (KeyValuePair<string, IPropertyPage> propertyPage in this._propertyPageDictionary)
          this._filteredPropertyPageDictionary.Add(propertyPage.Key, propertyPage.Value);
      }

      private bool FilterFolder(string normalizedQuery, string folderPath, IPropertyPage folderContent)
      {
        if (!((IEnumerable<string>) folderPath.Split('\\')).Any<string>((Func<string, bool>) (item => item.ToLowerInvariant().Contains(normalizedQuery))))
          return false;
        this._filteredPropertyPageDictionary.Add(folderPath, folderContent);
        return true;
      }

      private bool FilterFolderProperties(
        string normalizedQuery,
        string folderPath,
        IPropertyPage folderContent)
      {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(folderContent.Control);
        IEnumerable<PropertyDescriptor> source = properties != null ? properties.Cast<PropertyDescriptor>() : (IEnumerable<PropertyDescriptor>) null;
        if (source != null)
        {
          foreach (PropertyDescriptor propertyDescriptor in source.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (item => item != null)))
          {
            if (propertyDescriptor.DisplayName != null && propertyDescriptor.DisplayName.ToLowerInvariant().Contains(normalizedQuery))
            {
              this._filteredPropertyPageDictionary.Add(folderPath, folderContent);
              return true;
            }
          }
        }
        return false;
      }

      private string NormalizeQuery(string query) => query.ToLowerInvariant();

      private void UpdateTreeView()
      {
        this._treeView.BeginUpdate();
        try
        {
          this._treeView.Nodes.Clear();
          foreach (KeyValuePair<string, IPropertyPage> filteredPropertyPage in this._filteredPropertyPageDictionary)
          {
            TreeNode treeNode = this.AddNodeInHierarchy(filteredPropertyPage.Key);
            if (treeNode != null)
            {
              treeNode.Tag = (object) filteredPropertyPage.Value;
              treeNode.ImageIndex = this._pageIndex;
              treeNode.SelectedImageIndex = this._selectedIndex;
            }
          }
          if (!string.IsNullOrEmpty(this._textBox.Text))
            this._treeView.ExpandAll();
          if (this._treeView.Nodes.Count > 0)
          {
            this._treeView.SelectedNode = this._treeView.Nodes[0];
            this.ToggleCurrentPageEditor(true);
          }
          else
            this.ToggleCurrentPageEditor(false);
        }
        finally
        {
          this._treeView.EndUpdate();
        }
      }

      private void ToggleCurrentPageEditor(bool visibleState)
      {
        if (this._currentPageControl != null)
        {
          if (this._currentPageControl.Visible == visibleState)
            return;
          this._currentPageControl.Visible = visibleState;
        }
        else
        {
          if (this._propertyGrid.Visible == visibleState)
            return;
          this._propertyGrid.Visible = visibleState;
        }
      }

      private void ResetCurrentPageCache()
      {
        if (this._currentPageControl != null)
        {
          this._currentPageControl.Visible = false;
          this._currentPageControl = (Control) null;
        }
        else
          this._propertyGrid.Visible = false;
        this._currentPageCache = (IPropertyPage) null;
      }
    }
}
