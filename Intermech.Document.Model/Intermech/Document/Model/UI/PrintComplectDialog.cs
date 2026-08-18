// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.PrintComplectDialog
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using DevExpress.IM.XtraTreeList.Nodes.Operations;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Printing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.Document.Model.UI;

public class PrintComplectDialog : Form
{
  private PrintDocument doc;
  private PrinterSettings settings;
  private DocumentsComplect complect;
  private VisualStyleState oldVisualStyleState;
  private List<TreeListNode> pageNodes = new List<TreeListNode>();
  private bool ControlsUpdating;
  private const int DM_IN_BUFFER = 8;
  private const int DM_OUT_BUFFER = 2;
  private const int DM_IN_PROMPT = 4;
  private const int IDOK = 1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeList treeList;
  private Label label1;
  private System.Windows.Forms.GroupBox groupBox1;
  private System.Windows.Forms.RadioButton rbSome;
  private System.Windows.Forms.RadioButton rbAll;
  private Label label4;
  private NumericUpDown nCopies;
  private Label label5;
  private Label label6;
  private System.Windows.Forms.ComboBox cbPrinter;
  private Label label7;
  private System.Windows.Forms.Button bOptions;
  private System.Windows.Forms.Button bPrint;
  private System.Windows.Forms.Button bCancel;
  private System.Windows.Forms.RadioButton rbSelected;
  private ImageList stateImageList;
  private TreeListColumn treeListColumn1;
  private ImageList checkImageList;
  private Label label8;
  private Label label9;
  private Label label10;
  private Label label11;
  private Label lPortName;
  private Label lComment;
  private Label lDriverName;
  private Label lStatus;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem bCollapse;
  private ToolStripMenuItem bExpand;
  private ToolStripMenuItem bSelect;
  private ToolStripMenuItem bDeselect;
  private System.Windows.Forms.CheckBox cbCollate;
  private System.Windows.Forms.TextBox tbPages;
  private PictureBox pictureBox1;
  private System.Windows.Forms.ToolTip toolTip1;
  private System.Windows.Forms.CheckBox cbFitToPage;
  private System.Windows.Forms.Button bShiftPage;
  private System.Windows.Forms.RadioButton rbCurrent;

  private PrinterSettings Settings
  {
    get => this.settings;
    set
    {
      this.settings = value;
      this.UpdateControls();
    }
  }

  private DocumentsComplect Complect
  {
    get => this.complect;
    set
    {
      this.complect = value;
      this.CreateTree();
    }
  }

  public PrintDocument PrintDocument
  {
    get => this.doc;
    set
    {
      this.doc = value;
      if (this.doc != null)
        this.Settings = this.doc.PrinterSettings;
      else
        this.Settings = new PrinterSettings();
    }
  }

  public PrintComplectDialog(PrintDocument printDocument, DocumentsComplect complect)
  {
    this.oldVisualStyleState = Application.VisualStyleState;
    if (complect == null)
      throw new ArgumentNullException(nameof (complect));
    this.InitializeComponent();
    this.Complect = complect;
    this.Complect.ImPrintSettings.SelectedPrintPages.Clear();
    this.PrintDocument = printDocument;
    this.stateImageList.Images.Add((Image) (ImDocument.Icon as Bitmap).Clone());
    this.stateImageList.Images.Add((Image) (Intermech.Document.Model.Page.Icon as Bitmap).Clone());
    Bitmap bitmap1 = new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    CheckBoxRenderer.DrawCheckBox(Graphics.FromImage((Image) bitmap1), new Point(0, 0), CheckBoxState.UncheckedNormal);
    this.checkImageList.Images.Add((Image) bitmap1);
    Bitmap bitmap2 = new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    CheckBoxRenderer.DrawCheckBox(Graphics.FromImage((Image) bitmap2), new Point(0, 0), CheckBoxState.CheckedNormal);
    this.checkImageList.Images.Add((Image) bitmap2);
    Bitmap bitmap3 = new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    CheckBoxRenderer.DrawCheckBox(Graphics.FromImage((Image) bitmap3), new Point(0, 0), CheckBoxState.MixedNormal);
    this.checkImageList.Images.Add((Image) bitmap3);
    ImDocumentEditorConfig.Instance.LoadDocumentPrintersSettings(true);
  }

  /// <summary>Создание дерева</summary>
  private void CreateTree()
  {
    this.treeList.ClearNodes();
    int index = 0;
    foreach (DocumentTreeNode allDocument in this.complect.GetAllDocuments())
    {
      TreeListNode treeListNode1 = this.treeList.AppendNode((object) new object[1]
      {
        (object) allDocument.GetDefautCaption()
      }, (TreeListNode) null);
      this.treeList.SetNodeIndex(treeListNode1, index);
      treeListNode1.StateImageIndex = 0;
      treeListNode1.ImageIndex = 0;
      treeListNode1.SelectImageIndex = 0;
      treeListNode1.CheckState = CheckState.Unchecked;
      treeListNode1.Tag = (object) allDocument;
      ++index;
      foreach (PageData node in allDocument.Nodes)
      {
        TreeListNode treeListNode2 = this.treeList.AppendNode((object) new object[1]
        {
          (object) $"{node.GetDefautCaption()}({node.GlobalPageNumber.ToString()})"
        }, treeListNode1);
        this.pageNodes.Add(treeListNode2);
        treeListNode2.StateImageIndex = 0;
        treeListNode2.ImageIndex = 1;
        treeListNode2.SelectImageIndex = 1;
        treeListNode2.CheckState = CheckState.Unchecked;
        treeListNode2.Tag = (object) node;
        ++index;
      }
      treeListNode1.Expanded = true;
    }
  }

  private void UpdateControls()
  {
    this.ControlsUpdating = true;
    try
    {
      this.cbFitToPage.CheckState = !this.Complect.ImPrintSettings.FitToPagePrint.HasValue ? CheckState.Indeterminate : (!this.Complect.ImPrintSettings.FitToPagePrint.Value ? CheckState.Unchecked : CheckState.Checked);
      this.tbPages.Enabled = false;
      switch (this.settings.PrintRange)
      {
        case PrintRange.AllPages:
          this.rbAll.Checked = true;
          using (List<TreeListNode>.Enumerator enumerator = this.pageNodes.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.CheckState = CheckState.Checked;
            break;
          }
        case PrintRange.Selection:
          this.rbSelected.Checked = true;
          break;
        case PrintRange.SomePages:
          this.rbSome.Checked = true;
          if (this.settings.ToPage < this.settings.FromPage)
            this.settings.ToPage = this.settings.FromPage;
          this.UpdatePagesToPrint();
          this.tbPages.Enabled = true;
          break;
        case PrintRange.CurrentPage:
          this.rbCurrent.Checked = true;
          break;
      }
      this.nCopies.Maximum = (Decimal) this.settings.MaximumCopies;
      if ((int) this.settings.Copies > this.settings.MaximumCopies)
        this.settings.Copies = (short) this.settings.MaximumCopies;
      if ((Decimal) this.settings.Copies < this.nCopies.Minimum)
        this.settings.Copies = (short) this.nCopies.Minimum;
      this.nCopies.Value = (Decimal) this.settings.Copies;
      this.cbCollate.Checked = this.settings.Collate;
      if (this.cbPrinter.Items.Count == 0)
      {
        PrinterSettings.StringCollection installedPrinters = PrinterSettings.InstalledPrinters;
        this.cbPrinter.Items.Clear();
        foreach (string str in installedPrinters)
          this.cbPrinter.Items.Add((object) str);
      }
      this.cbPrinter.SelectedItem = (object) this.settings.PrinterName;
      this.bShiftPage.Visible = ImDocumentEditorConfig.Instance.IsClientPluginConfig;
    }
    finally
    {
      this.ControlsUpdating = false;
    }
  }

  private void bPrint_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void nCopies_ValueChanged(object sender, EventArgs e)
  {
    if (this.ControlsUpdating || !(this.nCopies.Value > 0M))
      return;
    this.settings.Copies = (short) this.nCopies.Value;
  }

  private void rbSome_CheckedChanged(object sender, EventArgs e)
  {
    if (this.ControlsUpdating)
      return;
    if (this.rbAll.Checked)
      this.settings.PrintRange = PrintRange.AllPages;
    if (this.rbSome.Checked)
      this.settings.PrintRange = PrintRange.SomePages;
    if (this.rbSelected.Checked)
      this.settings.PrintRange = PrintRange.Selection;
    if (this.rbCurrent.Checked)
      this.settings.PrintRange = PrintRange.CurrentPage;
    if (!this.rbSome.Checked)
      this.tbPages.Enabled = false;
    else
      this.tbPages.Enabled = true;
    this.UpdateControls();
  }

  private void treeList_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (!(e.Node.Tag is Intermech.Document.Model.Page))
      return;
    int globalPageNumber = (e.Node.Tag as Intermech.Document.Model.Page).GlobalPageNumber;
    if (e.Node.CheckState == CheckState.Checked && !this.Complect.ImPrintSettings.SelectedPrintPages.Contains(globalPageNumber))
      this.Complect.ImPrintSettings.SelectedPrintPages.Add(globalPageNumber);
    if (e.Node.CheckState != CheckState.Unchecked || !this.Complect.ImPrintSettings.SelectedPrintPages.Contains(globalPageNumber))
      return;
    this.Complect.ImPrintSettings.SelectedPrintPages.Remove(globalPageNumber);
  }

  [DllImport("kernel32.dll")]
  private static extern IntPtr GlobalLock(IntPtr hMem);

  [DllImport("kernel32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  private static extern bool GlobalUnlock(IntPtr hMem);

  [DllImport("kernel32.dll")]
  private static extern bool GlobalFree(IntPtr hMem);

  [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
  private static extern int DocumentProperties(
    IntPtr hwnd,
    IntPtr hPrinter,
    [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName,
    IntPtr pDevModeOutput,
    IntPtr pDevModeInput,
    int fMode);

  protected override void OnClosing(CancelEventArgs e)
  {
    DocumentMenuHelper.SilentRecoverVisualStyle(this.oldVisualStyleState);
    if (this.DialogResult != DialogResult.Cancel)
    {
      this.Complect.ImPrintSettings.SelectedPrintPages = new List<int>();
      if (this.rbSelected.Checked)
      {
        PrintComplectDialog.PrintNodesOperation operation = new PrintComplectDialog.PrintNodesOperation();
        this.treeList.NodesIterator.DoOperation((TreeListOperation) operation);
        this.Complect.ImPrintSettings.SelectedPrintPages = operation.Indexes;
      }
      if (this.rbSome.Checked)
      {
        List<int> pagesForPrint = PrintComplectDialog.GetPagesForPrint(this.tbPages.Text);
        if (pagesForPrint == null)
        {
          int num = (int) MessageBox.Show("Неправильно задан список страниц для печати", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          e.Cancel = true;
        }
        else
          this.Complect.ImPrintSettings.SelectedPrintPages = pagesForPrint;
      }
      PrintComplectDialog.RefreshProperties(this.settings, this.Handle);
    }
    base.OnClosing(e);
  }

  public static void RefreshProperties(PrinterSettings printerSettings, IntPtr pHandle)
  {
  }

  public static void OpenPrinterPropertiesDialog(PrinterSettings settings, IntPtr hwnd)
  {
    IntPtr hMem = settings.IsValid ? settings.GetHdevmode(settings.DefaultPageSettings) : throw new InvalidPrinterException(settings);
    IntPtr pDevModeInput = PrintComplectDialog.GlobalLock(hMem);
    IntPtr num1 = Marshal.AllocHGlobal(PrintComplectDialog.DocumentProperties(hwnd, IntPtr.Zero, settings.PrinterName, IntPtr.Zero, pDevModeInput, 0));
    int num2 = PrintComplectDialog.DocumentProperties(hwnd, IntPtr.Zero, settings.PrinterName, num1, pDevModeInput, 14);
    PrintComplectDialog.GlobalUnlock(hMem);
    if (num2 == 1)
      settings.SetHdevmode(num1);
    PrintComplectDialog.GlobalFree(hMem);
    Marshal.FreeHGlobal(num1);
  }

  private void bOptions_Click(object sender, EventArgs e)
  {
    PrintComplectDialog.OpenPrinterPropertiesDialog(this.settings, this.Handle);
    this.UpdateControls();
  }

  private void cbPrinter_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.settings.PrinterName = this.cbPrinter.Text;
    SafePrinterHandle safePrinterHandle = new SafePrinterHandle(this.settings.PrinterName);
    this.lStatus.Text = safePrinterHandle.PrinterInfo2.Status.ToString();
    this.lPortName.Text = safePrinterHandle.PrinterInfo2.PortName;
    this.lDriverName.Text = safePrinterHandle.PrinterInfo2.DriverName;
    this.lComment.Text = safePrinterHandle.PrinterInfo2.Comment;
    safePrinterHandle.Close();
    this.UpdateControls();
  }

  private void nFirst_ValueChanged(object sender, EventArgs e)
  {
    if (this.ControlsUpdating || this.settings.PrintRange != PrintRange.SomePages)
      return;
    this.UpdateControls();
  }

  private void treeList_CheckStateChanging(object sender, CheckStateEventArgs e)
  {
  }

  private void treeList_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Left || this.treeList.GetHitInfo(e.Location).HitInfoType != HitInfoType.StateImage)
      return;
    this.rbSelected.Checked = true;
  }

  private void bCollapse_Click(object sender, EventArgs e)
  {
    foreach (TreeListNode node in this.treeList.Nodes)
      this.Expand(node, true);
  }

  private void bExpand_Click(object sender, EventArgs e)
  {
    foreach (TreeListNode node in this.treeList.Nodes)
      this.Expand(node, false);
  }

  private void bSelect_Click(object sender, EventArgs e)
  {
    foreach (TreeListNode node in this.treeList.Nodes)
      this.SelectAll(node, true);
  }

  private void bDeselect_Click(object sender, EventArgs e)
  {
    foreach (TreeListNode node in this.treeList.Nodes)
      this.SelectAll(node, false);
  }

  private void SelectAll(TreeListNode node, bool select)
  {
    if (node == null)
      return;
    node.CheckState = !select ? CheckState.Unchecked : CheckState.Checked;
    foreach (TreeListNode node1 in node.Nodes)
      this.SelectAll(node1, select);
  }

  private void Expand(TreeListNode node, bool collapse)
  {
    if (node == null)
      return;
    node.Expanded = !collapse;
    foreach (TreeListNode node1 in node.Nodes)
      this.Expand(node1, collapse);
  }

  private void cbCollate_CheckedChanged(object sender, EventArgs e)
  {
    if (this.ControlsUpdating)
      return;
    this.settings.Collate = this.cbCollate.Checked;
  }

  private void tbPages_TextChanged(object sender, EventArgs e) => this.UpdatePagesToPrint();

  private void UpdatePagesToPrint()
  {
    List<int> pagesForPrint = PrintComplectDialog.GetPagesForPrint(this.tbPages.Text);
    this.treeList.BeginUpdate();
    this.treeList.NodesIterator.DoOperation((TreeListOperation) new PrintComplectDialog.CheckedTreeListOperation(pagesForPrint));
    this.treeList.EndUpdate();
  }

  public static List<int> GetPagesForPrint(string text)
  {
    List<int> pagesForPrint = new List<int>();
    string str = text;
    char[] chArray = new char[1]{ ',' };
    foreach (string s1 in str.Split(chArray))
    {
      int length = s1.IndexOf('-');
      if (length != -1)
      {
        string s2 = s1.Substring(0, length).Trim();
        string s3 = s1.Substring(length + 1);
        int result1 = 0;
        int result2 = 0;
        if (int.TryParse(s2, out result1))
        {
          if (!int.TryParse(s3, out result2))
            result2 = result1;
          if (result1 > result2)
            return (List<int>) null;
          for (int index = result1; index <= result2; ++index)
            pagesForPrint.Add(index);
        }
      }
      else
      {
        int result = 0;
        if (int.TryParse(s1, out result))
          pagesForPrint.Add(result);
      }
    }
    return pagesForPrint;
  }

  private void cbFitToPage_CheckStateChanged(object sender, EventArgs e)
  {
    if (this.ControlsUpdating)
      return;
    switch (this.cbFitToPage.CheckState)
    {
      case CheckState.Unchecked:
        this.Complect.ImPrintSettings.FitToPagePrint = new bool?(false);
        break;
      case CheckState.Checked:
        this.Complect.ImPrintSettings.FitToPagePrint = new bool?(true);
        break;
      case CheckState.Indeterminate:
        this.Complect.ImPrintSettings.FitToPagePrint = new bool?();
        break;
    }
  }

  private void bShiftPage_Click(object sender, EventArgs e)
  {
    int num = (int) ShiftPageForPrinter.Execute(this.settings.PrinterName);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    this.doc = (PrintDocument) null;
    this.complect = (DocumentsComplect) null;
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrintComplectDialog));
    this.treeList = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this.bCollapse = new ToolStripMenuItem();
    this.bExpand = new ToolStripMenuItem();
    this.bSelect = new ToolStripMenuItem();
    this.bDeselect = new ToolStripMenuItem();
    this.stateImageList = new ImageList(this.components);
    this.checkImageList = new ImageList(this.components);
    this.label1 = new Label();
    this.groupBox1 = new System.Windows.Forms.GroupBox();
    this.pictureBox1 = new PictureBox();
    this.tbPages = new System.Windows.Forms.TextBox();
    this.rbSelected = new System.Windows.Forms.RadioButton();
    this.rbSome = new System.Windows.Forms.RadioButton();
    this.rbAll = new System.Windows.Forms.RadioButton();
    this.label4 = new Label();
    this.nCopies = new NumericUpDown();
    this.label5 = new Label();
    this.label6 = new Label();
    this.cbPrinter = new System.Windows.Forms.ComboBox();
    this.label7 = new Label();
    this.bOptions = new System.Windows.Forms.Button();
    this.bPrint = new System.Windows.Forms.Button();
    this.bCancel = new System.Windows.Forms.Button();
    this.label8 = new Label();
    this.label9 = new Label();
    this.label10 = new Label();
    this.label11 = new Label();
    this.lPortName = new Label();
    this.lComment = new Label();
    this.lDriverName = new Label();
    this.lStatus = new Label();
    this.cbCollate = new System.Windows.Forms.CheckBox();
    this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
    this.cbFitToPage = new System.Windows.Forms.CheckBox();
    this.bShiftPage = new System.Windows.Forms.Button();
    this.rbCurrent = new System.Windows.Forms.RadioButton();
    this.treeList.BeginInit();
    this.contextMenuStrip1.SuspendLayout();
    this.groupBox1.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.nCopies.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.treeList, "treeList");
    this.treeList.CheckBoxes = CheckBoxesStyle.ThreeState;
    this.treeList.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeList.ContextMenuStrip = this.contextMenuStrip1;
    this.treeList.Name = "treeList";
    this.treeList.SelectImageList = this.stateImageList;
    this.treeList.StateImageList = this.checkImageList;
    this.treeList.CheckStateChanging += new CheckStateChangingEventHandler(this.treeList_CheckStateChanging);
    this.treeList.CheckStateChanged += new NodeEventHandler(this.treeList_CheckStateChanged);
    this.treeList.MouseDown += new MouseEventHandler(this.treeList_MouseDown);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.bCollapse,
      (ToolStripItem) this.bExpand,
      (ToolStripItem) this.bSelect,
      (ToolStripItem) this.bDeselect
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
    this.bCollapse.Name = "bCollapse";
    componentResourceManager.ApplyResources((object) this.bCollapse, "bCollapse");
    this.bCollapse.Click += new EventHandler(this.bCollapse_Click);
    this.bExpand.Name = "bExpand";
    componentResourceManager.ApplyResources((object) this.bExpand, "bExpand");
    this.bExpand.Click += new EventHandler(this.bExpand_Click);
    this.bSelect.Name = "bSelect";
    componentResourceManager.ApplyResources((object) this.bSelect, "bSelect");
    this.bSelect.Click += new EventHandler(this.bSelect_Click);
    this.bDeselect.Name = "bDeselect";
    componentResourceManager.ApplyResources((object) this.bDeselect, "bDeselect");
    this.bDeselect.Click += new EventHandler(this.bDeselect_Click);
    this.stateImageList.ColorDepth = ColorDepth.Depth32Bit;
    componentResourceManager.ApplyResources((object) this.stateImageList, "stateImageList");
    this.stateImageList.TransparentColor = Color.Transparent;
    this.checkImageList.ColorDepth = ColorDepth.Depth32Bit;
    componentResourceManager.ApplyResources((object) this.checkImageList, "checkImageList");
    this.checkImageList.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.groupBox1.Controls.Add((Control) this.rbCurrent);
    this.groupBox1.Controls.Add((Control) this.pictureBox1);
    this.groupBox1.Controls.Add((Control) this.tbPages);
    this.groupBox1.Controls.Add((Control) this.rbSelected);
    this.groupBox1.Controls.Add((Control) this.rbSome);
    this.groupBox1.Controls.Add((Control) this.rbAll);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.toolTip1.SetToolTip((Control) this.pictureBox1, componentResourceManager.GetString("pictureBox1.ToolTip"));
    componentResourceManager.ApplyResources((object) this.tbPages, "tbPages");
    this.tbPages.Name = "tbPages";
    this.tbPages.TextChanged += new EventHandler(this.tbPages_TextChanged);
    componentResourceManager.ApplyResources((object) this.rbSelected, "rbSelected");
    this.rbSelected.Name = "rbSelected";
    this.rbSelected.TabStop = true;
    this.rbSelected.UseVisualStyleBackColor = true;
    this.rbSelected.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbSome, "rbSome");
    this.rbSome.Name = "rbSome";
    this.rbSome.TabStop = true;
    this.rbSome.UseVisualStyleBackColor = true;
    this.rbSome.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbAll, "rbAll");
    this.rbAll.Name = "rbAll";
    this.rbAll.TabStop = true;
    this.rbAll.UseVisualStyleBackColor = true;
    this.rbAll.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.nCopies, "nCopies");
    this.nCopies.Maximum = new Decimal(new int[4]
    {
      1410065408,
      2,
      0,
      0
    });
    this.nCopies.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.nCopies.Name = "nCopies";
    this.nCopies.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.nCopies.ValueChanged += new EventHandler(this.nCopies_ValueChanged);
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    this.cbPrinter.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbPrinter, "cbPrinter");
    this.cbPrinter.Name = "cbPrinter";
    this.cbPrinter.SelectedIndexChanged += new EventHandler(this.cbPrinter_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    componentResourceManager.ApplyResources((object) this.bOptions, "bOptions");
    this.bOptions.Name = "bOptions";
    this.bOptions.UseVisualStyleBackColor = true;
    this.bOptions.Click += new EventHandler(this.bOptions_Click);
    this.bPrint.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.bPrint, "bPrint");
    this.bPrint.Name = "bPrint";
    this.bPrint.UseVisualStyleBackColor = true;
    this.bPrint.Click += new EventHandler(this.bPrint_Click);
    this.bCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label8, "label8");
    this.label8.Name = "label8";
    componentResourceManager.ApplyResources((object) this.label9, "label9");
    this.label9.Name = "label9";
    componentResourceManager.ApplyResources((object) this.label10, "label10");
    this.label10.Name = "label10";
    componentResourceManager.ApplyResources((object) this.label11, "label11");
    this.label11.Name = "label11";
    componentResourceManager.ApplyResources((object) this.lPortName, "lPortName");
    this.lPortName.Name = "lPortName";
    componentResourceManager.ApplyResources((object) this.lComment, "lComment");
    this.lComment.Name = "lComment";
    componentResourceManager.ApplyResources((object) this.lDriverName, "lDriverName");
    this.lDriverName.Name = "lDriverName";
    componentResourceManager.ApplyResources((object) this.lStatus, "lStatus");
    this.lStatus.Name = "lStatus";
    componentResourceManager.ApplyResources((object) this.cbCollate, "cbCollate");
    this.cbCollate.Name = "cbCollate";
    this.cbCollate.UseVisualStyleBackColor = true;
    this.cbCollate.CheckedChanged += new EventHandler(this.cbCollate_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbFitToPage, "cbFitToPage");
    this.cbFitToPage.Name = "cbFitToPage";
    this.cbFitToPage.ThreeState = true;
    this.cbFitToPage.UseVisualStyleBackColor = true;
    this.cbFitToPage.CheckStateChanged += new EventHandler(this.cbFitToPage_CheckStateChanged);
    componentResourceManager.ApplyResources((object) this.bShiftPage, "bShiftPage");
    this.bShiftPage.Name = "bShiftPage";
    this.bShiftPage.UseVisualStyleBackColor = true;
    this.bShiftPage.Click += new EventHandler(this.bShiftPage_Click);
    componentResourceManager.ApplyResources((object) this.rbCurrent, "rbCurrent");
    this.rbCurrent.Name = "rbCurrent";
    this.rbCurrent.TabStop = true;
    this.rbCurrent.UseVisualStyleBackColor = true;
    this.rbCurrent.CheckedChanged += new EventHandler(this.rbSome_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.bPrint;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.bShiftPage);
    this.Controls.Add((Control) this.cbFitToPage);
    this.Controls.Add((Control) this.cbCollate);
    this.Controls.Add((Control) this.lStatus);
    this.Controls.Add((Control) this.label10);
    this.Controls.Add((Control) this.lDriverName);
    this.Controls.Add((Control) this.lComment);
    this.Controls.Add((Control) this.label9);
    this.Controls.Add((Control) this.lPortName);
    this.Controls.Add((Control) this.label11);
    this.Controls.Add((Control) this.label8);
    this.Controls.Add((Control) this.label7);
    this.Controls.Add((Control) this.bOptions);
    this.Controls.Add((Control) this.bPrint);
    this.Controls.Add((Control) this.cbPrinter);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.label6);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.nCopies);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.treeList);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (PrintComplectDialog);
    this.treeList.EndInit();
    this.contextMenuStrip1.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.nCopies.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public class PrintNodesOperation : TreeListOperation
  {
    public List<int> Indexes = new List<int>();

    public override void Execute(TreeListNode node)
    {
      if (!(node.Tag is Intermech.Document.Model.Page))
        return;
      int globalPageNumber = (node.Tag as Intermech.Document.Model.Page).GlobalPageNumber;
      if (node.CheckState != CheckState.Checked)
        return;
      this.Indexes.Add(globalPageNumber);
    }

    public override bool NeedsFullIteration => true;

    public override bool NeedsVisitChildren(TreeListNode node) => true;

    public override bool CanContinueIteration(TreeListNode node) => base.CanContinueIteration(node);
  }

  private class CheckedTreeListOperation : TreeListOperation
  {
    private List<int> indexes;

    public CheckedTreeListOperation(List<int> indexes) => this.indexes = indexes;

    public override bool CanContinueIteration(TreeListNode node) => true;

    public override void Execute(TreeListNode node)
    {
      if (!(node.Tag is Intermech.Document.Model.Page))
        return;
      int globalPageNumber = (node.Tag as Intermech.Document.Model.Page).GlobalPageNumber;
      if (this.indexes == null || !this.indexes.Contains(globalPageNumber))
        node.CheckState = CheckState.Unchecked;
      else
        node.CheckState = CheckState.Checked;
    }

    public override void FinalizeOperation()
    {
    }

    public override bool NeedsFullIteration => true;

    public override bool NeedsVisitChildren(TreeListNode node) => true;
  }
}
