
// Type: IMClient.OutputView




using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace IMClient
{
    public class OutputView : DockControl, IOutputView, IWindowWithFind, IOutputViewHistory
    {
      private IContainer components;
      private Intermech.Bars.ToolBar toolBar;
      private ComboBoxItem ddPages;
      private ButtonItem btClearPage;
      private ButtonItem btToggleWordWrap;
      private Bevel bvTopSeparator;
      private Panel pnOutputHost;
      private ButtonItem btFind;
      private ButtonItem btSave;
      private SaveFileDialog sfdSaveOutput;
      private ComboBox categoryList;
      private Dictionary<string, OutputView.CategoryItem> categoryIndex;

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OutputView));
        this.toolBar = new Intermech.Bars.ToolBar();
        this.ddPages = new ComboBoxItem();
        this.btFind = new ButtonItem();
        this.btClearPage = new ButtonItem();
        this.btToggleWordWrap = new ButtonItem();
        this.bvTopSeparator = new Bevel();
        this.pnOutputHost = new Panel();
        this.btSave = new ButtonItem();
        this.sfdSaveOutput = new SaveFileDialog();
        this.SuspendLayout();
        this.toolBar.FullMenus = true;
        this.toolBar.Guid = new Guid("647ef792-3e7a-47fb-b533-ca948656f477");
        this.toolBar.Hidden = false;
        this.toolBar.Items.AddRange(new ToolbarItemBase[5]
        {
          (ToolbarItemBase) this.ddPages,
          (ToolbarItemBase) this.btSave,
          (ToolbarItemBase) this.btFind,
          (ToolbarItemBase) this.btClearPage,
          (ToolbarItemBase) this.btToggleWordWrap
        });
        componentResourceManager.ApplyResources((object) this.toolBar, "toolBar");
        this.toolBar.Name = "toolBar";
        componentResourceManager.ApplyResources((object) this.ddPages, "ddPages");
        this.ddPages.MinimumControlWidth = 50;
        this.ddPages.Padding.Bottom = 0;
        this.ddPages.Padding.Left = 1;
        this.ddPages.Padding.Right = 1;
        this.ddPages.Padding.Top = 0;
        this.ddPages.Stretch = true;
        this.btFind.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btFind, "btFind");
        this.btFind.Enabled = false;
        this.btFind.Click += new EventHandler(this.btFind_Click);
        this.btClearPage.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btClearPage, "btClearPage");
        this.btClearPage.Enabled = false;
        this.btClearPage.Click += new EventHandler(this.btClearPage_Click);
        this.btToggleWordWrap.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btToggleWordWrap, "btToggleWordWrap");
        this.btToggleWordWrap.Enabled = false;
        this.btToggleWordWrap.Click += new EventHandler(this.btToggleWordWrap_Click);
        componentResourceManager.ApplyResources((object) this.bvTopSeparator, "bvTopSeparator");
        this.bvTopSeparator.Name = "bvTopSeparator";
        componentResourceManager.ApplyResources((object) this.pnOutputHost, "pnOutputHost");
        this.pnOutputHost.Name = "pnOutputHost";
        this.btSave.BeginGroup = true;
        componentResourceManager.ApplyResources((object) this.btSave, "btSave");
        this.btSave.Enabled = false;
        this.btSave.Click += new EventHandler(this.btSave_Click);
        this.sfdSaveOutput.DefaultExt = "txt";
        componentResourceManager.ApplyResources((object) this.sfdSaveOutput, "sfdSaveOutput");
        this.sfdSaveOutput.SupportMultiDottedExtensions = true;
        this.sfdSaveOutput.RestoreDirectory = true;
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
        this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
        this.Controls.Add((Control) this.pnOutputHost);
        this.Controls.Add((Control) this.bvTopSeparator);
        this.Controls.Add((Control) this.toolBar);
        componentResourceManager.ApplyResources((object) this, "$this");
        this.Guid = ViewGuids.OutputView_Guid;
        this.HideOnClose = true;
        this.Name = nameof (OutputView);
        this.ShowHint = DockState.DockBottomAutoHide;
        this.ResumeLayout(false);
      }

      public OutputView()
      {
        this.InitializeComponent();
        this.CreateControl();
      }

      public void Initialize()
      {
        this.categoryList = this.ddPages.ComboBox;
        this.categoryList.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
        this.categoryList.DropDownStyle = ComboBoxStyle.DropDownList;
        this.categoryList.ItemHeight = 13;
        this.categoryList.Sorted = true;
        this.categoryIndex = new Dictionary<string, OutputView.CategoryItem>(16 /*0x10*/, (IEqualityComparer<string>) OutputView.CategoryItem.Comparer);
        INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, true);
        this.TabImageIndex = service.ImageIndex("imgOutput");
        this.toolBar.ImageList = service.ImageList;
        this.btToggleWordWrap.ImageIndex = service.ImageIndex("imgWordWrap");
        this.btClearPage.ImageIndex = service.ImageIndex("imgClearAll");
        this.btFind.ImageIndex = service.ImageIndex("imgFind");
        this.btSave.ImageIndex = service.ImageIndex("imgSave");
      }

      private void CheckInitialized()
      {
        if (this.categoryList == null)
          throw new InvalidOperationException("Object must be initialized first.");
      }

      private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
      {
        this.CheckInitialized();
        this.pnOutputHost.SuspendLayout();
        try
        {
          this.pnOutputHost.Controls.Clear();
          if (this.categoryList.SelectedItem != null)
          {
            OutputView.CategoryItem selectedItem = (OutputView.CategoryItem) this.categoryList.SelectedItem;
            selectedItem.Output.Parent = (Control) this.pnOutputHost;
            this.btToggleWordWrap.Checked = selectedItem.Output.WordWrap;
            this.btSave.Enabled = true;
            this.btFind.Enabled = true;
            this.btClearPage.Enabled = true;
            this.btToggleWordWrap.Enabled = true;
          }
          else
          {
            this.btSave.Enabled = false;
            this.btFind.Enabled = false;
            this.btClearPage.Enabled = false;
            this.btToggleWordWrap.Enabled = false;
          }
        }
        finally
        {
          this.pnOutputHost.ResumeLayout(true);
        }
      }

      private void btToggleWordWrap_Click(object sender, EventArgs e)
      {
        this.CheckInitialized();
        if (this.categoryList.SelectedItem == null)
          return;
        OutputView.CategoryItem selectedItem = (OutputView.CategoryItem) this.categoryList.SelectedItem;
        bool flag = !selectedItem.Output.WordWrap;
        selectedItem.Output.WordWrap = flag;
        selectedItem.Output.ScrollBars = flag ? ScrollBars.Vertical : ScrollBars.Both;
        this.btToggleWordWrap.Checked = flag;
      }

      private void btClearPage_Click(object sender, EventArgs e)
      {
        this.CheckInitialized();
        if (this.categoryList.SelectedItem == null)
          return;
        ((OutputView.CategoryItem) this.categoryList.SelectedItem).Output.Clear();
      }

      private void btFind_Click(object sender, EventArgs e)
      {
        this.CheckInitialized();
        if (this.categoryList.SelectedItem == null)
          return;
        IFindController window = FindOrReplaceService.ShowFindWindow((IWindowWithFind) this);
        ((Form) window).FormClosed += new FormClosedEventHandler(this.FindFinished);
        ((Control) window).VisibleChanged += new EventHandler(this.FindFinished);
        this.btFind.Enabled = false;
      }

      private void FindFinished(object sender, EventArgs e)
      {
        this.btFind.Enabled = true;
        if (this.categoryList.SelectedItem != null)
          ((OutputView.CategoryItem) this.categoryList.SelectedItem).Output.Focus();
        else
          this.Focus();
      }

      private void btSave_Click(object sender, EventArgs e)
      {
        this.CheckInitialized();
        if (this.categoryList.SelectedItem == null)
          return;
        OutputView.CategoryItem selectedItem = (OutputView.CategoryItem) this.categoryList.SelectedItem;
        StringBuilder stringBuilder = new StringBuilder(selectedItem.Name);
        char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
        if (selectedItem.Name.IndexOfAny(invalidFileNameChars) >= 0)
        {
          foreach (char oldChar in invalidFileNameChars)
            stringBuilder.Replace(oldChar, '_');
        }
        this.sfdSaveOutput.FileName = stringBuilder.ToString();
        this.sfdSaveOutput.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        if (this.sfdSaveOutput.ShowDialog() != DialogResult.OK)
          return;
        File.WriteAllText(this.sfdSaveOutput.FileName, selectedItem.Output.Text, Encoding.Default);
      }

      public void WriteString(string category, string text)
      {
        this.CheckInitialized();
        if (this.InvokeRequired)
        {
          this.BeginInvoke((Delegate) new OutputView.WriteStringHandler(this.WriteString), (object) category, (object) text);
        }
        else
        {
          OutputView.CategoryItem category1 = this.FindCategory(category, true);
          if (this.categoryList.SelectedItem == null)
            this.categoryList.SelectedItem = (object) category1;
          if (category1.Output.IsDisposed)
            return;
          category1.Output.AppendText(text);
          if (text.EndsWith(Environment.NewLine))
            return;
          category1.Output.AppendText(Environment.NewLine);
        }
      }

      public void ClearText(string category)
      {
        this.CheckInitialized();
        if (this.InvokeRequired)
        {
          this.BeginInvoke((Delegate) new OutputView.TextHandler(this.ClearText), (object) category);
        }
        else
        {
          OutputView.CategoryItem category1 = this.FindCategory(category, false);
          if (category1 == null || category1.Output.IsDisposed)
            return;
          category1.Output.Clear();
        }
      }

      private OutputView.CategoryItem FindCategory(string category, bool create)
      {
        OutputView.CategoryItem category1;
        if (!this.categoryIndex.TryGetValue(category, out category1) & create)
        {
          category1 = new OutputView.CategoryItem(category);
          this.categoryIndex.Add(category, category1);
          this.categoryList.Items.Add((object) category1);
        }
        return category1;
      }

      public void Activate(string category)
      {
        this.CheckInitialized();
        if (this.InvokeRequired)
        {
          this.BeginInvoke((Delegate) new OutputView.TextHandler(this.Activate), (object) category);
        }
        else
        {
          OutputView.CategoryItem category1 = this.FindCategory(category, true);
          this.categoryList.SelectedItem = (object) category1;
          if (!this.IsOpen)
            this.ShowViewInternal();
          category1.ScrollToEnd();
        }
      }

      public void ShowView()
      {
        this.CheckInitialized();
        this.BeginInvoke((Delegate) new MethodInvoker(this.ShowViewInternal));
      }

      private void ShowViewInternal()
      {
        if (this.Manager != null)
          this.Open();
        else
          this.Show(ServiceUtils.GetService<DockManager>((object) ServicesManager.ServiceContainer, true));
        if (this.categoryList.SelectedIndex != -1 || this.categoryList.Items.Count <= 0)
          return;
        this.categoryList.SelectedIndex = 0;
      }

      System.Type IWindowWithFind.GetFindSetupFormClass()
      {
        this.CheckInitialized();
        return typeof (SimpleFindForm);
      }

      void IWindowWithFind.FindNext(IFindController findController)
      {
        this.CheckInitialized();
        if (this.categoryList.SelectedItem == null)
          return;
        OutputView.CategoryItem selectedItem = (OutputView.CategoryItem) this.categoryList.SelectedItem;
        IFindData interfaceObject = (IFindData) findController.InterfaceObject;
        int startIndex = selectedItem.Output.SelectionStart;
        if (selectedItem.Output.SelectionLength > 0)
          startIndex += selectedItem.Output.SelectionLength;
        bool flag;
        int num;
        do
        {
          flag = false;
          num = selectedItem.Output.Text.IndexOf(interfaceObject.FindWhat, startIndex, StringComparison.CurrentCultureIgnoreCase);
          if (num < 0 && startIndex > 0)
          {
            flag = true;
            startIndex = 0;
          }
        }
        while (flag);
        if (num < 0)
          return;
        selectedItem.Output.SelectionStart = num;
        selectedItem.Output.SelectionLength = interfaceObject.FindWhat.Length;
        selectedItem.Output.ScrollToCaret();
      }

      public List<Tuple<string, string>> GetOutputHistory()
      {
        List<Tuple<string, string>> outputHistory = new List<Tuple<string, string>>(this.categoryList.Items.Count);
        foreach (OutputView.CategoryItem categoryItem in this.categoryList.Items)
          outputHistory.Add(Tuple.Create<string, string>(categoryItem.Name, categoryItem.Output.Text));
        return outputHistory;
      }

      private delegate void WriteStringHandler(string cat, string text);

      private delegate void TextHandler(string cat);

      private sealed class CategoryItem
      {
        private readonly string name;
        private readonly TextBox output;
        public static readonly StringComparer Comparer = StringComparer.CurrentCultureIgnoreCase;

        public CategoryItem(string name)
        {
          this.name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentException("Name cannot be empty", nameof (name));
          this.output = new TextBox();
          this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
          this.output.Name = nameof (output);
          this.output.Font = new Font(FontFamily.GenericMonospace, 9f);
          this.output.Dock = DockStyle.Fill;
          this.output.Multiline = true;
          this.output.ReadOnly = true;
          this.output.WordWrap = false;
          this.output.ScrollBars = ScrollBars.Both;
          this.output.HideSelection = false;
        }

        public string Name => this.name;

        public TextBox Output => this.output;

        public void ScrollToEnd()
        {
          int textLength = this.Output.TextLength;
          if (this.Output.SelectionStart < textLength)
            this.Output.SelectionStart = textLength;
          this.Output.ScrollToCaret();
        }

        public override bool Equals(object obj)
        {
          return !(obj is OutputView.CategoryItem categoryItem) ? base.Equals(obj) : OutputView.CategoryItem.Comparer.Compare(this.name, categoryItem.name) == 0;
        }

        public override int GetHashCode() => OutputView.CategoryItem.Comparer.GetHashCode(this.name);

        public override string ToString() => this.name;
      }
    }
}
