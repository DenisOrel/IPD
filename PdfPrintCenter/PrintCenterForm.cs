
// Type: Intermech.PdfPrintCenter.PrintCenterForm




using Infralution.Controls.VirtualTree;
using Intermech.PdfPrintCenter.Connector;
using Intermech.PdfPrintCenter.Controls.PdfViewer;
using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using Intermech.PdfPrintCenter.PrintCenterTools.PrintReportTools;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeModels;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.PrintQueueNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.TreeNodes.WorkspaceNodes;
using Intermech.PdfPrintCenter.PrintCenterTreeUtils.Trees;
using Intermech.PdfPrintCenter.Utils;
using Intermech.PdfPrintCenter.Utils.Events;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using Ninject;
using NJFLib.Controls;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter
{
    internal class PrintCenterForm : Form
    {
      private const string ReportFileName = "report.html";
      private IPrintCenterFormServices services;
      private string _reportTempPath;
      private IContainer components;
      private MenuStrip menuStrip;
      private ToolStripMenuItem toolStripMenuItemFile;
      private ToolStripMenuItem toolStripMenuItemExit;
      private ToolStripMenuItem toolStripMenuItemTools;
      private ToolStripMenuItem toolStripMenuItemPrinterSetup;
      private ToolStripMenuItem toolStripMenuItemLayoutEditor;
      private ToolStripMenuItem toolStripMenuItemWatermarkSetup;
      private WorkspaceTree virtualTreeWorkspace;
      private Button buttonDefaultAdd;
      private Label labelCopies;
      private Label labelPrinters;
      private Label labelLayouts;
      private NumericUpDown numericUpDownCopies;
      private Button buttonAutoAdd;
      private ReadOnlyComboBox comboBoxPrinters;
      private ReadOnlyComboBox comboBoxLayouts;
      private PrintQueueTree virtualTreePrintQueue;
      private Button buttonPrint;
      private Button buttonShowReport;
      private AdvToolbarPdfViewer pdfViewer;
      private ToolStripMenuItem видToolStripMenuItem;
      private ToolStripMenuItem toolStripMenuItemShowWithLayout;
      private ToolStripMenuItem toolStripMenuItemShowWithWatermark;
      private Panel panelWorkspace;
      private CollapsibleSplitter collapsibleSplitterPdfCenter;
      private Panel panelPdfViewer;
      private Panel panelPrintQueue;
      private Panel panelWorkspaceTree;
      private CollapsibleSplitter collapsibleSplitterWorkspace;
      private CheckBox checkBoxFitDocument;

      public PrintCenterForm()
      {
        this.InitializeComponent();
        this.InitializeFormSettings();
      }

      [Inject]
      public PrintCenterForm(IPrintCenterFormServices services)
      {
        this.InitializeComponent();
        this.InitializeServices(services);
        this.InitializeFormSettings();
      }

      public void AddFilesFromPdm(List<PDMDocumentInfo> documents)
      {
        if (documents == null || !documents.Any<PDMDocumentInfo>())
          return;
        if (this.InvokeRequired)
        {
          this.Invoke((Delegate) new Action<List<PDMDocumentInfo>>(this.AddFilesFromPdm), (object) documents);
        }
        else
        {
          this.AddFilesToWorkspace(documents);
          this.Activate();
        }
      }

      private void InitializeFormSettings()
      {
        this.InitializePrintQueueTree();
        this.InitializeComboBoxPrinters();
        this.InitializeComboBoxLayouts();
        this.InitializeFormSize();
        this.InitializeHandlers();
        this.InitializePdfViewer();
        this.InitializeReportTempPath();
      }

      private void InitializeComboBoxPrinters()
      {
        ControlUtils.LoadPrinters((ComboBox) this.comboBoxPrinters, this.services.PrintersSettingsService.GetPrintersSettings().PrintersOrder);
        this.comboBoxPrinters.SelectedIndex = this.comboBoxPrinters.Items.IndexOf((object) new PrinterSettings().PrinterName);
      }

      private void InitializeComboBoxLayouts()
      {
        ControlUtils.LoadLayouts((ComboBox) this.comboBoxLayouts, this.services.LayoutSettingsService.LoadAllLayouts());
        this.comboBoxLayouts.SelectedIndex = 0;
      }

      private void InitializeFormSize()
      {
        Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings windowSettings = this.services.WindowSettingsService.GetWindowSettings();
        if (windowSettings.Size.Width == 0 || windowSettings.Size.Height == 0 || !this.IsWindowParametersValid(windowSettings))
          return;
        this.StartPosition = FormStartPosition.Manual;
        this.Location = windowSettings.Location;
        this.Size = windowSettings.Size;
        this.WindowState = windowSettings.WindowState;
      }

      private bool IsWindowParametersValid(Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings windowParameters)
      {
        Point point;
        ref Point local1 = ref point;
        Rectangle virtualScreen1 = SystemInformation.VirtualScreen;
        int x = virtualScreen1.X;
        virtualScreen1 = SystemInformation.VirtualScreen;
        int y = virtualScreen1.Y;
        local1 = new Point(x, y);
        Size size1;
        ref Size local2 = ref size1;
        Rectangle virtualScreen2 = SystemInformation.VirtualScreen;
        int width1 = virtualScreen2.Width;
        virtualScreen2 = SystemInformation.VirtualScreen;
        int height1 = virtualScreen2.Height;
        local2 = new Size(width1, height1);
        bool flag = true;
        Size size2 = windowParameters.Size;
        int width2 = size2.Width;
        size2 = this.MinimumSize;
        int width3 = size2.Width;
        if (width2 >= width3)
        {
          Size size3 = windowParameters.Size;
          if (size3.Width <= size1.Width)
          {
            size3 = windowParameters.Size;
            int height2 = size3.Height;
            size3 = this.MinimumSize;
            int height3 = size3.Height;
            if (height2 >= height3)
            {
              size3 = windowParameters.Size;
              if (size3.Height <= size1.Height)
              {
                Point location = windowParameters.Location;
                if (location.X >= point.X)
                {
                  location = windowParameters.Location;
                  if (location.X <= point.X + size1.Width)
                  {
                    location = windowParameters.Location;
                    if (location.Y >= point.Y)
                    {
                      location = windowParameters.Location;
                      if (location.Y <= point.Y + size1.Height)
                        goto label_9;
                    }
                  }
                }
              }
            }
          }
        }
        flag = false;
    label_9:
        return flag;
      }

      private void InitializeHandlers()
      {
        this.virtualTreePrintQueue.VirtualTreeModify += new Delegates.VirtualTreeModifyHandler(this.OnModifyVirtualTree);
        this.virtualTreeWorkspace.VirtualTreeModify += new Delegates.VirtualTreeModifyHandler(this.OnModifyVirtualTree);
      }

      private void InitializePdfViewer() => this.pdfViewer.SetSize();

      private void InitializePrintQueueTree()
      {
        this.virtualTreePrintQueue.LayoutSettingsService = this.services.LayoutSettingsService;
        this.virtualTreePrintQueue.PrintersSettingsService = this.services.PrintersSettingsService;
      }

      private void InitializeReportTempPath()
      {
        string randomFileName = Path.GetRandomFileName();
        Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), randomFileName));
        this._reportTempPath = Path.Combine(Path.GetTempPath(), randomFileName, "report.html");
      }

      private void InitializeServices(IPrintCenterFormServices services) => this.services = services;

      private void ButtonAutoAdd_Click(object sender, EventArgs e)
      {
        this.AutoAddToPrintQueue(this.virtualTreeWorkspace.GetSelectedNodes());
      }

      private void ButtonDefaultAdd_Click(object sender, EventArgs e)
      {
        this.AddFilesToPrintQueue(this.virtualTreeWorkspace.GetSelectedNodes(), this.GetPrintParameters());
      }

      private void ButtonPrint_Click(object sender, EventArgs e) => this.DoPrintJobs();

      private void ButtonShowReport_Click(object sender, EventArgs e)
      {
        if (!File.Exists(this._reportTempPath))
          return;
        int num = (int) new PrintReportForm(Path.GetFullPath(this._reportTempPath)).ShowDialog();
      }

      private void CollapsibleSplitterPdfViewer_SplitterMoved(object sender, SplitterEventArgs e)
      {
        this.pdfViewer.SetSize();
      }

      private void CollapsibleSplitterPdfViewer_SplitterMoving(object sender, SplitterEventArgs e)
      {
        int num1 = this.panelWorkspace.Location.X + this.panelWorkspace.MinimumSize.Width;
        if (e.SplitX < num1)
          e.SplitX = num1;
        int num2 = this.panelWorkspace.Width + this.panelPdfViewer.Width - this.collapsibleSplitterPdfCenter.Width - this.panelPdfViewer.MinimumSize.Width;
        if (e.SplitX <= num2)
          return;
        e.SplitX = num2;
      }

      private void CollapsibleSplitterWorkspace_SplitterMoving(object sender, SplitterEventArgs e)
      {
        int num1 = this.panelWorkspaceTree.Location.X + this.panelWorkspaceTree.MinimumSize.Height;
        if (e.SplitY < num1)
          e.SplitY = num1;
        int num2 = this.panelWorkspace.Height - this.collapsibleSplitterWorkspace.Height - this.panelPrintQueue.MinimumSize.Height;
        if (e.SplitY <= num2)
          return;
        e.SplitY = num2;
      }

      private void OnModifyVirtualTree(object sender, OnModifyVirtualTreeEventArgs e)
      {
        switch (e.Command)
        {
          case "Авто":
            this.AutoAddToPrintQueue(e.SelectedNodes);
            break;
          case "Добавить":
            this.AddFilesToPrintQueue(e.SelectedNodes, this.GetPrintParameters());
            break;
          case "Удалить":
            switch (sender)
            {
              case PrintQueueTree _:
                this.RemoveFilesFromPrintQueue(e.SelectedNodes);
                break;
              case WorkspaceTree _:
                this.RemoveFilesFromWorkspace(e.SelectedNodes);
                break;
            }
            break;
          case "Печать":
            if (sender is PrintQueueTree)
            {
              this.DoPrintJobs();
              break;
            }
            break;
          case "Drag'n'Drop":
            this.DropFiles(sender as PrintCenterTree, e.SelectedNodes, e.DestinationNode);
            break;
        }
        this.CheckButtons();
      }

      private void PanelPdfViewer_Resize(object sender, EventArgs e)
      {
        ((Control) sender).Invalidate();
        this.pdfViewer.SetSize();
      }

      private void PanelPrintQueue_Resize(object sender, EventArgs e)
      {
        ((Control) sender).Invalidate();
      }

      private void PanelWorkspace_Resize(object sender, EventArgs e) => ((Control) sender).Invalidate();

      private void PanelWorkspaceTree_Resize(object sender, EventArgs e)
      {
        ((Control) sender).Invalidate();
      }

      private void PrintCenterForm_Resize(object sender, EventArgs e) => this.pdfViewer.SetSize();

      private void PrintCenterForm_Shown(object sender, EventArgs e)
      {
        this.services.PrintCenterStartupService.SetStarted();
      }

      private void PrintCenterForm_FormClosing(object sender, FormClosingEventArgs e)
      {
        Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings windowParameters = new Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings();
        try
        {
          if (!this.services.PDMSystemService.IsPDMSystemConnected)
            return;
          switch (this.WindowState)
          {
            case FormWindowState.Normal:
            case FormWindowState.Minimized:
              windowParameters.WindowState = FormWindowState.Normal;
              windowParameters.Location = this.Location;
              windowParameters.Size = this.Size;
              break;
            case FormWindowState.Maximized:
              windowParameters = this.services.WindowSettingsService.GetWindowSettings().Clone() as Intermech.PdfPrintCenter.PrintCenterTools.WindowSettings.WindowSettings;
              windowParameters.WindowState = FormWindowState.Maximized;
              break;
          }
          windowParameters.Freeze();
          this.services.WindowSettingsService.PutWindowSettings(windowParameters);
        }
        catch (Exception ex)
        {
        }
      }

      private void ToolStripMenuItemExit_Click(object sender, EventArgs e) => this.Close();

      private void ToolStripMenuItemShowWithLayout_Click(object sender, EventArgs e)
      {
        this.ShowPdfFromPrintQueueTree();
      }

      private void ToolStripMenuItemShowWithWatermark_Click(object sender, EventArgs e)
      {
        this.ShowPdfFromPrintQueueTree();
      }

      private void ToolStripMenuItemPrinterSetup_Click(object sender, EventArgs e)
      {
        int num = (int) this.services.PrintCenterSettingsFactory.CreatePrintersSettingsForm().ShowDialog();
        this.InitializeComboBoxPrinters();
      }

      private void ToolStripMenuItemLayoutEditor_Click(object sender, EventArgs e)
      {
        LayoutEditor layoutEditor = this.services.PrintCenterSettingsFactory.CreateLayoutEditor();
        int num = (int) layoutEditor.ShowDialog();
        this.InitializeComboBoxLayouts();
        this.UpdateLayouts(layoutEditor.RenamedLayouts);
        this.ShowPdfFromPrintQueueTree();
      }

      private void ToolStripMenuItemWatermark_Click(object sender, EventArgs e)
      {
        if (this.services.PrintCenterSettingsFactory.CreateWatermarkForm().ShowDialog() != DialogResult.OK || !this.virtualTreePrintQueue.Focused)
          return;
        this.ShowPdfFromTree((PrintCenterTree) this.virtualTreePrintQueue);
      }

      private void VirtualTreePrintQueue_SelectionChanged(object sender, EventArgs e)
      {
        this.CheckButtons();
        this.ShowPdfFromTree((PrintCenterTree) this.virtualTreePrintQueue);
      }

      private void VirtualTreeWorkspace_SelectionChanged(object sender, EventArgs e)
      {
        this.CheckButtons();
        this.ShowPdfFromTree((PrintCenterTree) this.virtualTreeWorkspace);
      }

      private void VirtualTree_GotFocus(object sender, EventArgs e)
      {
        this.ShowPdfFromTree(sender as PrintCenterTree);
      }

      private void AddFilesToPrintQueue(
        List<PrintCenterNode> selectedNodes,
        PrintParameters printParameters)
      {
        if (string.IsNullOrWhiteSpace(printParameters.PrinterName))
        {
          int num1 = (int) MessageBox.Show("Выберите принтер для добавления документов в очередь печати", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
          Dictionary<string, HashSet<WorkspacePagesTreeNode>> nodesToFilename = LayoutsUtils.GroupNodesByFilename(selectedNodes.OfType<WorkspaceTreeNode>().ToList<WorkspaceTreeNode>());
          Dictionary<string, HashSet<WorkspacePagesTreeNode>> cannotBeDistributed = LayoutsUtils.GetNodesCannotBeDistributed(printParameters.Layout, nodesToFilename);
          if (cannotBeDistributed.Any<KeyValuePair<string, HashSet<WorkspacePagesTreeNode>>>())
          {
            StringBuilder stringBuilder = new StringBuilder($"Следующие элементы не могут быть добавлены на макет \"{printParameters.Layout.ToString()}\":\n");
            foreach (string key in cannotBeDistributed.Keys)
            {
              string pages = string.Join(", ", cannotBeDistributed[key].Select<WorkspacePagesTreeNode, string>((Func<WorkspacePagesTreeNode, string>) (node => node.Pages)));
              string str = PageIntervalsUtils.IsManyPages(pages) ? "страница" : "страницы";
              stringBuilder.AppendLine($"- файл {key}.pdf: {str} {pages};");
            }
            int num2 = (int) MessageBox.Show(stringBuilder.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          else
          {
            this.comboBoxPrinters.Enabled = false;
            this.comboBoxLayouts.Enabled = false;
            this.virtualTreePrintQueue.AddNodes(selectedNodes, printParameters);
            this.virtualTreeWorkspace.RemoveNodes(selectedNodes);
            this.comboBoxPrinters.Enabled = true;
            this.comboBoxLayouts.Enabled = true;
          }
        }
      }

      private void AddFilesToWorkspace(List<PDMDocumentInfo> documents)
      {
        if (documents.Count == 0)
          return;
        foreach (string str in documents.SelectMany<PDMDocumentInfo, string>((Func<PDMDocumentInfo, IEnumerable<string>>) (doc => (IEnumerable<string>) doc.FilePaths)).ToList<string>())
        {
          string filePath = str;
          string withoutExtension = Path.GetFileNameWithoutExtension(filePath);
          if (this.FileAlreadyAdded(withoutExtension))
          {
            int num = (int) MessageBox.Show(withoutExtension + " уже был добавлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            documents.RemoveAll((Predicate<PDMDocumentInfo>) (item => item.FilePaths.Contains(filePath)));
          }
        }
        if (!documents.Any<PDMDocumentInfo>())
          return;
        List<WorkspaceAddNodesResult> source = this.virtualTreeWorkspace.AddNodes(documents);
        if (source == null)
          return;
        StringBuilder stringBuilder = new StringBuilder();
        foreach (WorkspaceAddNodesResult workspaceAddNodesResult in source.OfType<WorkspaceAddNodesResult>())
        {
          if (workspaceAddNodesResult.RootNode == null && !workspaceAddNodesResult.AddedNodesPages.Any<string>())
            stringBuilder.AppendLine($"Файл {workspaceAddNodesResult.FileName}.pdf не был добавлен: у страниц не задан размер");
          else if (workspaceAddNodesResult.AddedNodesPages.Any<string>() && workspaceAddNodesResult.NodesWithEmptyPages.Any<string>())
          {
            string str = string.Join(", ", (IEnumerable<string>) workspaceAddNodesResult.NodesWithEmptyPages);
            stringBuilder.AppendLine($"Страницы {str} файла {workspaceAddNodesResult.FileName}.pdf не будут добавлены: их размер не задан.");
          }
        }
        string text = stringBuilder.ToString();
        if (string.IsNullOrEmpty(text))
          return;
        int num1 = (int) MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      private void AutoAddToPrintQueue(List<PrintCenterNode> selectedNodes)
      {
        Intermech.PdfPrintCenter.PrintCenterTools.PrintersSettings.PrintersSettings printersSettings = this.services.PrintersSettingsService.GetPrintersSettings();
        if (printersSettings != null)
        {
          IDictionary<string, List<string>> formatsToPrinters = printersSettings.FormatsToPrinters;
        }
        List<string> stringList = new List<string>();
        List<NodesToPrintQueue> source = new List<NodesToPrintQueue>();
        Dictionary<string, List<NodesToPrintQueue>> dictionary = new Dictionary<string, List<NodesToPrintQueue>>();
        foreach (KeyValuePair<string, HashSet<WorkspacePagesTreeNode>> keyValuePair in LayoutsUtils.GroupNodesByFilename(selectedNodes.OfType<WorkspaceTreeNode>().ToList<WorkspaceTreeNode>()))
        {
          string key = keyValuePair.Key;
          Dictionary<KnownPaperFormat, List<WorkspacePagesTreeNode>> knownFormatToPages = LayoutsUtils.GroupPagesByAptFormats(keyValuePair.Value.ToList<WorkspacePagesTreeNode>());
          if (knownFormatToPages == null)
          {
            stringList.Add(key);
          }
          else
          {
            AutoAddDocumentResult addDocumentResult = this.AutoAddFromDocument(knownFormatToPages);
            if (addDocumentResult == null)
            {
              stringList.Add(key);
            }
            else
            {
              source.AddRange((IEnumerable<NodesToPrintQueue>) addDocumentResult.OnMinLayout);
              if (addDocumentResult.NotOnMinLayout.Any<NodesToPrintQueue>())
                dictionary.Add(key, addDocumentResult.NotOnMinLayout);
            }
          }
        }
        if (dictionary.Any<KeyValuePair<string, List<NodesToPrintQueue>>>())
        {
          AddNodesToLayoutDialog nodesToLayoutDialog = new AddNodesToLayoutDialog(dictionary, !source.Any<NodesToPrintQueue>());
          if (nodesToLayoutDialog.ShowDialog() == DialogResult.OK)
          {
            switch (nodesToLayoutDialog.Action)
            {
              case AddNodesToLayoutDialog.Actions.AddAll:
                foreach (NodesToPrintQueue nodesToPrintQueue in source)
                  this.AddFilesToPrintQueue(nodesToPrintQueue.Nodes.OfType<PrintCenterNode>().ToList<PrintCenterNode>(), nodesToPrintQueue.PrintParameters);
                using (Dictionary<string, List<NodesToPrintQueue>>.KeyCollection.Enumerator enumerator = dictionary.Keys.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    string current = enumerator.Current;
                    foreach (NodesToPrintQueue nodesToPrintQueue in dictionary[current])
                      this.AddFilesToPrintQueue(nodesToPrintQueue.Nodes.OfType<PrintCenterNode>().ToList<PrintCenterNode>(), nodesToPrintQueue.PrintParameters);
                  }
                  break;
                }
              case AddNodesToLayoutDialog.Actions.AddPartly:
                using (List<NodesToPrintQueue>.Enumerator enumerator = source.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    NodesToPrintQueue current = enumerator.Current;
                    this.AddFilesToPrintQueue(current.Nodes.OfType<PrintCenterNode>().ToList<PrintCenterNode>(), current.PrintParameters);
                  }
                  break;
                }
            }
          }
        }
        else
        {
          foreach (NodesToPrintQueue nodesToPrintQueue in source)
            this.AddFilesToPrintQueue(nodesToPrintQueue.Nodes.OfType<PrintCenterNode>().ToList<PrintCenterNode>(), nodesToPrintQueue.PrintParameters);
        }
        if (!stringList.Any<string>())
          return;
        int num = (int) MessageBox.Show($"Следующие файлы не удалось добавить автоматически: {string.Join(", ", (IEnumerable<string>) stringList)}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      private AutoAddDocumentResult AutoAddFromDocument(
        Dictionary<KnownPaperFormat, List<WorkspacePagesTreeNode>> knownFormatToPages)
      {
        AutoAddDocumentResult addDocumentResult = new AutoAddDocumentResult();
        string commonPrinter = this.services.LayoutsAnalyzerService.FindCommonPrinter(knownFormatToPages.Keys.ToList<KnownPaperFormat>());
        if (commonPrinter != null)
        {
          PrintParameters printParameters = this.GetPrintParameters(commonPrinter);
          List<WorkspacePagesTreeNode> list = knownFormatToPages.SelectMany<KeyValuePair<KnownPaperFormat, List<WorkspacePagesTreeNode>>, WorkspacePagesTreeNode>((Func<KeyValuePair<KnownPaperFormat, List<WorkspacePagesTreeNode>>, IEnumerable<WorkspacePagesTreeNode>>) (item => (IEnumerable<WorkspacePagesTreeNode>) item.Value)).ToList<WorkspacePagesTreeNode>();
          addDocumentResult.OnMinLayout.Add(new NodesToPrintQueue(list, printParameters));
          return addDocumentResult;
        }
        List<NodesToPrintQueue> nodesToPrintQueueList1 = new List<NodesToPrintQueue>();
        foreach (KnownPaperFormat knownPaperFormat in knownFormatToPages.Keys.ToList<KnownPaperFormat>())
        {
          string firstAptPrinter = this.services.LayoutsAnalyzerService.FindFirstAptPrinter(knownPaperFormat);
          if (firstAptPrinter != null)
          {
            addDocumentResult.OnMinLayout.Add(new NodesToPrintQueue(knownFormatToPages[knownPaperFormat], this.GetPrintParameters(firstAptPrinter)));
            knownFormatToPages.Remove(knownPaperFormat);
          }
        }
        List<NodesToPrintQueue> nodesToPrintQueueList2 = new List<NodesToPrintQueue>();
        foreach (KnownPaperFormat knownPaperFormat in knownFormatToPages.Keys.ToList<KnownPaperFormat>())
        {
          KnownPaperFormat format = knownPaperFormat;
          LayoutDescriptor minAptLayout = this.services.LayoutsAnalyzerService.FindMinAptLayout(format);
          if (minAptLayout == null)
            return (AutoAddDocumentResult) null;
          string firstAptPrinter = this.services.LayoutsAnalyzerService.FindFirstAptPrinter(KnownPaperFormats.GetFormat(minAptLayout.MainFormat.BaseName));
          if (firstAptPrinter == null)
            return (AutoAddDocumentResult) null;
          if (minAptLayout.InternalFormats.FirstOrDefault<FormatLocation>((Func<FormatLocation, bool>) (internalFormat => internalFormat.Format.BaseName == format.BaseName)) == null)
            addDocumentResult.NotOnMinLayout.Add(new NodesToPrintQueue(knownFormatToPages[format], this.GetPrintParameters(firstAptPrinter, minAptLayout)));
          addDocumentResult.OnMinLayout.Add(new NodesToPrintQueue(knownFormatToPages[format], this.GetPrintParameters(firstAptPrinter, minAptLayout)));
          knownFormatToPages.Remove(format);
        }
        return knownFormatToPages.Any<KeyValuePair<KnownPaperFormat, List<WorkspacePagesTreeNode>>>() ? (AutoAddDocumentResult) null : addDocumentResult;
      }

      private void CheckButtons()
      {
        this.buttonAutoAdd.Enabled = this.virtualTreeWorkspace.SelectedItems.Count != 0 && this.virtualTreeWorkspace.SelectedItems.OfType<WorkspaceTreeModel>().Count<WorkspaceTreeModel>() == 0;
        this.buttonDefaultAdd.Enabled = this.virtualTreeWorkspace.SelectedItems.Count != 0 && this.virtualTreeWorkspace.SelectedItems.OfType<WorkspaceTreeModel>().Count<WorkspaceTreeModel>() == 0;
        this.buttonPrint.Enabled = this.virtualTreePrintQueue.CheckNodesSelected();
      }

      private void ClearPrintQueueTree()
      {
        List<PrintCenterNode> printCenterNodeList = new List<PrintCenterNode>();
        this.virtualTreePrintQueue.RemoveNodes(this.virtualTreePrintQueue.SelectedItems.OfType<PrintQueueTreeModel>().Count<PrintQueueTreeModel>() == 0 ? this.virtualTreePrintQueue.GetSelectedNodes() : this.virtualTreePrintQueue.Nodes);
        this.virtualTreePrintQueue.Focus();
        this.ShowPdfFromTree((PrintCenterTree) this.virtualTreePrintQueue);
      }

      private void CreateReport(List<PrinterNode> nodesSelectedForPrint)
      {
        List<PrintCenterNode> nodes = this.virtualTreePrintQueue.Nodes;
        string directoryName = Path.GetDirectoryName(this._reportTempPath);
        if (File.Exists(this._reportTempPath))
          File.WriteAllText(this._reportTempPath, string.Empty);
        else if (!Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
        using (StreamWriter streamWriter = File.AppendText(this._reportTempPath))
        {
          string htmlReport = PrintReportCreator.CreateHtmlReport(nodesSelectedForPrint);
          streamWriter.Write(htmlReport);
        }
      }

      private void DoPrintJobs()
      {
        List<PrinterNode> selectedForPrint = this.virtualTreePrintQueue.GetNodesSelectedForPrint();
        this.PrintDocuments(selectedForPrint);
        this.CreateReport(selectedForPrint);
        this.ClearPrintQueueTree();
        this.CheckButtons();
        this.buttonShowReport.Enabled = true;
      }

      private void DropFiles(
        PrintCenterTree sender,
        List<PrintCenterNode> selectedNodes,
        object destinationNode)
      {
        switch (sender)
        {
          case PrintQueueTree _:
            if (destinationNode is PrintQueueTreeModel)
            {
              this.AddFilesToPrintQueue(selectedNodes, this.GetPrintParameters());
              break;
            }
            LayoutNode layoutNode = (LayoutNode) null;
            if (destinationNode is LayoutNode)
              layoutNode = destinationNode as LayoutNode;
            else if (destinationNode is PrintQueuePagesNode)
              layoutNode = (destinationNode as PrintQueuePagesNode).Parent as LayoutNode;
            if (!(layoutNode.Parent is PrinterNode parent))
              break;
            PrintParameters printParameters = new PrintParameters((short) this.numericUpDownCopies.Value, parent.PrinterName, layoutNode.Layout, this.checkBoxFitDocument.Checked);
            this.AddFilesToPrintQueue(selectedNodes, printParameters);
            break;
          case WorkspaceTree _:
            switch (destinationNode)
            {
              case WorkspaceTreeModel _:
              case WorkspaceObjectTreeNode _:
              case WorkspacePagesTreeNode _:
                this.RemoveFilesFromPrintQueue(selectedNodes);
                return;
              default:
                return;
            }
        }
      }

      private bool FileAlreadyAdded(string filename)
      {
        return this.virtualTreeWorkspace.Contains(filename) || this.virtualTreePrintQueue.Contains(filename);
      }

      private PrintParameters GetPrintParameters()
      {
        return new PrintParameters((short) this.numericUpDownCopies.Value, this.comboBoxPrinters.Text, this.comboBoxLayouts.SelectedItem as IPdfPageProducer, this.checkBoxFitDocument.Checked);
      }

      private PrintParameters GetPrintParameters(string printer, LayoutDescriptor layout)
      {
        return new PrintParameters((short) this.numericUpDownCopies.Value, printer, (IPdfPageProducer) layout, this.checkBoxFitDocument.Checked);
      }

      private PrintParameters GetPrintParameters(string printer)
      {
        return new PrintParameters((short) this.numericUpDownCopies.Value, printer, (IPdfPageProducer) new LayoutAsItIs(), this.checkBoxFitDocument.Checked);
      }

      private void PrintDocuments(List<PrinterNode> nodesSelectedForPrint)
      {
        foreach (PrinterNode printerNode in nodesSelectedForPrint)
        {
          foreach (LayoutNode layoutNode in printerNode.Children.OfType<LayoutNode>())
          {
            Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings settingsWithSubstitutes = this.services.WatermarkSettingsService.GetWatermarkSettingsWithSubstitutes();
            Dictionary<string, List<PrintQueuePagesNode>> dictionary1 = LayoutsUtils.GroupNodesByFilename(layoutNode.Children.OfType<PrintQueuePagesNode>().ToList<PrintQueuePagesNode>());
            List<PrintDocument> printDocumentList = new List<PrintDocument>();
            foreach (IEnumerable<PrintQueuePagesNode> source in dictionary1.Values)
            {
              Dictionary<bool, List<PrintQueuePagesNode>> dictionary2 = source.GroupBy<PrintQueuePagesNode, bool>((Func<PrintQueuePagesNode, bool>) (x => x.FitToPage)).ToDictionary<IGrouping<bool, PrintQueuePagesNode>, bool, List<PrintQueuePagesNode>>((Func<IGrouping<bool, PrintQueuePagesNode>, bool>) (x => x.Key), (Func<IGrouping<bool, PrintQueuePagesNode>, List<PrintQueuePagesNode>>) (x => x.ToList<PrintQueuePagesNode>()));
              foreach (bool key in dictionary2.Keys)
              {
                PdfDocument pdfDocument = PdfDocumentUtils.MakePdfDocumentWithChosenLayout(dictionary2[key], settingsWithSubstitutes);
                if (pdfDocument != null)
                {
                  PdfPrintMode printMode = key ? PdfPrintMode.ShrinkToMargin : PdfPrintMode.CutMargin;
                  PrintDocument printDocument = pdfDocument.CreatePrintDocument(printMode);
                  printDocument.PrinterSettings.PrinterName = printerNode.MainColumnCaption;
                  printDocument.PrinterSettings.Copies = (short) 1;
                  printDocumentList.Add(printDocument);
                }
              }
            }
            foreach (PrintDocument printDocument in printDocumentList)
              printDocument.Print();
          }
        }
      }

      private void RemoveFilesFromPrintQueue(List<PrintCenterNode> selectedNodes)
      {
        this.virtualTreeWorkspace.AddNodes(selectedNodes);
        this.virtualTreePrintQueue.RemoveNodes(selectedNodes);
      }

      private void RemoveFilesFromWorkspace(List<PrintCenterNode> selectedNodes)
      {
        List<string> filesInPrintQueue = new List<string>();
        foreach (PrintCenterNode selectedNode in selectedNodes)
        {
          string fileName = selectedNode.FileName;
          if (this.virtualTreePrintQueue.Contains(fileName))
            filesInPrintQueue.Add(fileName);
        }
        if (filesInPrintQueue.Count == 0)
        {
          this.virtualTreeWorkspace.RemoveNodes(selectedNodes);
        }
        else
        {
          bool allInPrintQueue = filesInPrintQueue.Count == selectedNodes.Count;
          DeleteFilesDialog deleteFilesDialog = new DeleteFilesDialog(filesInPrintQueue, allInPrintQueue);
          if (deleteFilesDialog.ShowDialog() == DialogResult.OK)
          {
            switch (deleteFilesDialog.Action)
            {
              case DeleteFilesDialog.Actions.DeleteAll:
                List<PrintCenterNode> nodesInPrintQueue = new List<PrintCenterNode>();
                filesInPrintQueue.ForEach((Action<string>) (objectName => nodesInPrintQueue.AddRange((IEnumerable<PrintCenterNode>) this.virtualTreePrintQueue.GetNodesFromFile(objectName))));
                this.virtualTreePrintQueue.RemoveNodes(nodesInPrintQueue);
                this.virtualTreeWorkspace.RemoveNodes(selectedNodes);
                break;
              case DeleteFilesDialog.Actions.DeletePartly:
                this.virtualTreeWorkspace.RemoveNodes(selectedNodes.Where<PrintCenterNode>((Func<PrintCenterNode, bool>) (node => !filesInPrintQueue.Contains(node.MainColumnCaption))).ToList<PrintCenterNode>());
                break;
            }
          }
        }
        this.ShowPdfFromTree((PrintCenterTree) this.virtualTreeWorkspace);
      }

      private void ShowPdfFromTree(PrintCenterTree tree)
      {
        if (tree.NodesSelecting)
          return;
        if (tree.Focused && tree.SelectedItems.Count == 0)
          this.pdfViewer.HideContent();
        switch (tree.SelectedItem)
        {
          case PrintCenterNode node:
            Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings = this.toolStripMenuItemShowWithWatermark.Checked ? this.services.WatermarkSettingsService.GetWatermarkSettings() : (Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings) null;
            this.pdfViewer.ShowDocument(node, this.toolStripMenuItemShowWithLayout.Checked, watermarkSettings);
            break;
          case PrintCenterTreeModel _:
            this.pdfViewer.HideContent();
            break;
        }
      }

      private void ShowPdfFromPrintQueueTree()
      {
        if (!this.virtualTreePrintQueue.Focused)
          return;
        this.ShowPdfFromTree((PrintCenterTree) this.virtualTreePrintQueue);
      }

      private void UpdateLayouts(List<RenamedLayout> renamedLayouts)
      {
        this.virtualTreePrintQueue.UpdateLayoutsNames(renamedLayouts);
        this.virtualTreePrintQueue.UpdateLayouts(this.comboBoxLayouts.Items.OfType<IPdfPageProducer>().ToList<IPdfPageProducer>());
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.components = (IContainer) new System.ComponentModel.Container();
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrintCenterForm));
        this.menuStrip = new MenuStrip();
        this.toolStripMenuItemFile = new ToolStripMenuItem();
        this.toolStripMenuItemExit = new ToolStripMenuItem();
        this.видToolStripMenuItem = new ToolStripMenuItem();
        this.toolStripMenuItemShowWithLayout = new ToolStripMenuItem();
        this.toolStripMenuItemShowWithWatermark = new ToolStripMenuItem();
        this.toolStripMenuItemTools = new ToolStripMenuItem();
        this.toolStripMenuItemPrinterSetup = new ToolStripMenuItem();
        this.toolStripMenuItemLayoutEditor = new ToolStripMenuItem();
        this.toolStripMenuItemWatermarkSetup = new ToolStripMenuItem();
        this.buttonDefaultAdd = new Button();
        this.labelCopies = new Label();
        this.labelPrinters = new Label();
        this.labelLayouts = new Label();
        this.numericUpDownCopies = new NumericUpDown();
        this.buttonAutoAdd = new Button();
        this.buttonPrint = new Button();
        this.buttonShowReport = new Button();
        this.panelWorkspace = new Panel();
        this.panelPrintQueue = new Panel();
        this.checkBoxFitDocument = new CheckBox();
        this.virtualTreePrintQueue = new PrintQueueTree();
        this.comboBoxPrinters = new ReadOnlyComboBox();
        this.comboBoxLayouts = new ReadOnlyComboBox();
        this.collapsibleSplitterWorkspace = new CollapsibleSplitter();
        this.panelWorkspaceTree = new Panel();
        this.virtualTreeWorkspace = new WorkspaceTree();
        this.collapsibleSplitterPdfCenter = new CollapsibleSplitter();
        this.panelPdfViewer = new Panel();
        this.pdfViewer = new AdvToolbarPdfViewer();
        this.menuStrip.SuspendLayout();
        this.numericUpDownCopies.BeginInit();
        this.panelWorkspace.SuspendLayout();
        this.panelPrintQueue.SuspendLayout();
        this.virtualTreePrintQueue.BeginInit();
        this.panelWorkspaceTree.SuspendLayout();
        this.virtualTreeWorkspace.BeginInit();
        this.panelPdfViewer.SuspendLayout();
        this.SuspendLayout();
        this.menuStrip.ImageScalingSize = new Size(20, 20);
        this.menuStrip.Items.AddRange(new ToolStripItem[3]
        {
          (ToolStripItem) this.toolStripMenuItemFile,
          (ToolStripItem) this.видToolStripMenuItem,
          (ToolStripItem) this.toolStripMenuItemTools
        });
        this.menuStrip.Location = new Point(0, 0);
        this.menuStrip.Name = "menuStrip";
        this.menuStrip.Padding = new Padding(8, 2, 0, 2);
        this.menuStrip.Size = new Size(1332, 28);
        this.menuStrip.TabIndex = 0;
        this.menuStrip.Text = "menuStrip";
        this.toolStripMenuItemFile.DropDownItems.AddRange(new ToolStripItem[1]
        {
          (ToolStripItem) this.toolStripMenuItemExit
        });
        this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
        this.toolStripMenuItemFile.Size = new Size(57, 24);
        this.toolStripMenuItemFile.Text = "Файл";
        this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
        this.toolStripMenuItemExit.Size = new Size(128 /*0x80*/, 26);
        this.toolStripMenuItemExit.Text = "Выход";
        this.toolStripMenuItemExit.Click += new EventHandler(this.ToolStripMenuItemExit_Click);
        this.видToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
        {
          (ToolStripItem) this.toolStripMenuItemShowWithLayout,
          (ToolStripItem) this.toolStripMenuItemShowWithWatermark
        });
        this.видToolStripMenuItem.Name = "видToolStripMenuItem";
        this.видToolStripMenuItem.Size = new Size(47, 24);
        this.видToolStripMenuItem.Text = "Вид";
        this.toolStripMenuItemShowWithLayout.Checked = true;
        this.toolStripMenuItemShowWithLayout.CheckOnClick = true;
        this.toolStripMenuItemShowWithLayout.CheckState = CheckState.Checked;
        this.toolStripMenuItemShowWithLayout.Name = "toolStripMenuItemShowWithLayout";
        this.toolStripMenuItemShowWithLayout.Size = new Size(363, 26);
        this.toolStripMenuItemShowWithLayout.Text = "Включить просмотр с учётом макета";
        this.toolStripMenuItemShowWithLayout.Click += new EventHandler(this.ToolStripMenuItemShowWithLayout_Click);
        this.toolStripMenuItemShowWithWatermark.Checked = true;
        this.toolStripMenuItemShowWithWatermark.CheckOnClick = true;
        this.toolStripMenuItemShowWithWatermark.CheckState = CheckState.Checked;
        this.toolStripMenuItemShowWithWatermark.Name = "toolStripMenuItemShowWithWatermark";
        this.toolStripMenuItemShowWithWatermark.Size = new Size(363, 26);
        this.toolStripMenuItemShowWithWatermark.Text = "Включить отображение водяного знака";
        this.toolStripMenuItemShowWithWatermark.Click += new EventHandler(this.ToolStripMenuItemShowWithWatermark_Click);
        this.toolStripMenuItemTools.DropDownItems.AddRange(new ToolStripItem[3]
        {
          (ToolStripItem) this.toolStripMenuItemPrinterSetup,
          (ToolStripItem) this.toolStripMenuItemLayoutEditor,
          (ToolStripItem) this.toolStripMenuItemWatermarkSetup
        });
        this.toolStripMenuItemTools.Name = "toolStripMenuItemTools";
        this.toolStripMenuItemTools.Size = new Size(115, 24);
        this.toolStripMenuItemTools.Text = "Инструменты";
        this.toolStripMenuItemPrinterSetup.Name = "toolStripMenuItemPrinterSetup";
        this.toolStripMenuItemPrinterSetup.Size = new Size(271, 26);
        this.toolStripMenuItemPrinterSetup.Text = "Настройка принтеров";
        this.toolStripMenuItemPrinterSetup.Click += new EventHandler(this.ToolStripMenuItemPrinterSetup_Click);
        this.toolStripMenuItemLayoutEditor.Name = "toolStripMenuItemLayoutEditor";
        this.toolStripMenuItemLayoutEditor.Size = new Size(271, 26);
        this.toolStripMenuItemLayoutEditor.Text = "Редактор макетов";
        this.toolStripMenuItemLayoutEditor.Click += new EventHandler(this.ToolStripMenuItemLayoutEditor_Click);
        this.toolStripMenuItemWatermarkSetup.Name = "toolStripMenuItemWatermarkSetup";
        this.toolStripMenuItemWatermarkSetup.Size = new Size(271, 26);
        this.toolStripMenuItemWatermarkSetup.Text = "Настройка водяного знака";
        this.toolStripMenuItemWatermarkSetup.Click += new EventHandler(this.ToolStripMenuItemWatermark_Click);
        this.buttonDefaultAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonDefaultAdd.Enabled = false;
        this.buttonDefaultAdd.Location = new Point(572, 83);
        this.buttonDefaultAdd.Margin = new Padding(4, 4, 4, 4);
        this.buttonDefaultAdd.Name = "buttonDefaultAdd";
        this.buttonDefaultAdd.Size = new Size((int) sbyte.MaxValue, 28);
        this.buttonDefaultAdd.TabIndex = 8;
        this.buttonDefaultAdd.Text = "Добавить ↓";
        this.buttonDefaultAdd.UseVisualStyleBackColor = true;
        this.buttonDefaultAdd.Click += new EventHandler(this.ButtonDefaultAdd_Click);
        this.labelCopies.AutoSize = true;
        this.labelCopies.Location = new Point(7, 21);
        this.labelCopies.Margin = new Padding(4, 0, 4, 0);
        this.labelCopies.Name = "labelCopies";
        this.labelCopies.Size = new Size(53, 17);
        this.labelCopies.TabIndex = 0;
        this.labelCopies.Text = "Копии:";
        this.labelPrinters.AutoSize = true;
        this.labelPrinters.Location = new Point(7, 55);
        this.labelPrinters.Margin = new Padding(4, 0, 4, 0);
        this.labelPrinters.Name = "labelPrinters";
        this.labelPrinters.Size = new Size(69, 17);
        this.labelPrinters.TabIndex = 1;
        this.labelPrinters.Text = "Принтер:";
        this.labelLayouts.AutoSize = true;
        this.labelLayouts.Location = new Point(7, 90);
        this.labelLayouts.Margin = new Padding(4, 0, 4, 0);
        this.labelLayouts.Name = "labelLayouts";
        this.labelLayouts.Size = new Size(53, 17);
        this.labelLayouts.TabIndex = 2;
        this.labelLayouts.Text = "Макет:";
        this.numericUpDownCopies.Location = new Point(88, 18);
        this.numericUpDownCopies.Margin = new Padding(4, 4, 4, 4);
        this.numericUpDownCopies.Minimum = new Decimal(new int[4]
        {
          1,
          0,
          0,
          0
        });
        this.numericUpDownCopies.Name = "numericUpDownCopies";
        this.numericUpDownCopies.Size = new Size(65, 22);
        this.numericUpDownCopies.TabIndex = 3;
        this.numericUpDownCopies.Value = new Decimal(new int[4]
        {
          1,
          0,
          0,
          0
        });
        this.buttonAutoAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.buttonAutoAdd.Enabled = false;
        this.buttonAutoAdd.Location = new Point(572, 12);
        this.buttonAutoAdd.Margin = new Padding(4, 4, 4, 4);
        this.buttonAutoAdd.Name = "buttonAutoAdd";
        this.buttonAutoAdd.Size = new Size((int) sbyte.MaxValue, 28);
        this.buttonAutoAdd.TabIndex = 7;
        this.buttonAutoAdd.Text = "Авто ↓";
        this.buttonAutoAdd.UseVisualStyleBackColor = true;
        this.buttonAutoAdd.Click += new EventHandler(this.ButtonAutoAdd_Click);
        this.buttonPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.buttonPrint.Enabled = false;
        this.buttonPrint.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
        this.buttonPrint.Location = new Point(572, 382);
        this.buttonPrint.Margin = new Padding(4, 4, 4, 4);
        this.buttonPrint.Name = "buttonPrint";
        this.buttonPrint.Size = new Size((int) sbyte.MaxValue, 31 /*0x1F*/);
        this.buttonPrint.TabIndex = 11;
        this.buttonPrint.Text = "Печать";
        this.buttonPrint.UseVisualStyleBackColor = true;
        this.buttonPrint.Click += new EventHandler(this.ButtonPrint_Click);
        this.buttonShowReport.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        this.buttonShowReport.Enabled = false;
        this.buttonShowReport.Location = new Point(7, 382);
        this.buttonShowReport.Margin = new Padding(4, 4, 4, 4);
        this.buttonShowReport.Name = "buttonShowReport";
        this.buttonShowReport.Size = new Size(152, 31 /*0x1F*/);
        this.buttonShowReport.TabIndex = 10;
        this.buttonShowReport.Text = "Смотреть отчёт";
        this.buttonShowReport.UseVisualStyleBackColor = true;
        this.buttonShowReport.Click += new EventHandler(this.ButtonShowReport_Click);
        this.panelWorkspace.Controls.Add((Control) this.panelPrintQueue);
        this.panelWorkspace.Controls.Add((Control) this.collapsibleSplitterWorkspace);
        this.panelWorkspace.Controls.Add((Control) this.panelWorkspaceTree);
        this.panelWorkspace.Dock = DockStyle.Left;
        this.panelWorkspace.Location = new Point(0, 28);
        this.panelWorkspace.Margin = new Padding(4, 4, 4, 4);
        this.panelWorkspace.MinimumSize = new Size(703, 729);
        this.panelWorkspace.Name = "panelWorkspace";
        this.panelWorkspace.Size = new Size(703, 730);
        this.panelWorkspace.TabIndex = 28;
        this.panelWorkspace.Resize += new EventHandler(this.PanelWorkspace_Resize);
        this.panelPrintQueue.Controls.Add((Control) this.checkBoxFitDocument);
        this.panelPrintQueue.Controls.Add((Control) this.virtualTreePrintQueue);
        this.panelPrintQueue.Controls.Add((Control) this.buttonPrint);
        this.panelPrintQueue.Controls.Add((Control) this.numericUpDownCopies);
        this.panelPrintQueue.Controls.Add((Control) this.labelLayouts);
        this.panelPrintQueue.Controls.Add((Control) this.comboBoxPrinters);
        this.panelPrintQueue.Controls.Add((Control) this.buttonDefaultAdd);
        this.panelPrintQueue.Controls.Add((Control) this.labelCopies);
        this.panelPrintQueue.Controls.Add((Control) this.buttonAutoAdd);
        this.panelPrintQueue.Controls.Add((Control) this.comboBoxLayouts);
        this.panelPrintQueue.Controls.Add((Control) this.labelPrinters);
        this.panelPrintQueue.Controls.Add((Control) this.buttonShowReport);
        this.panelPrintQueue.Dock = DockStyle.Fill;
        this.panelPrintQueue.Location = new Point(0, 310);
        this.panelPrintQueue.Margin = new Padding(4, 4, 4, 4);
        this.panelPrintQueue.MinimumSize = new Size(0, 308);
        this.panelPrintQueue.Name = "panelPrintQueue";
        this.panelPrintQueue.Size = new Size(703, 420);
        this.panelPrintQueue.TabIndex = 28;
        this.panelPrintQueue.Resize += new EventHandler(this.PanelPrintQueue_Resize);
        this.checkBoxFitDocument.AutoSize = true;
        this.checkBoxFitDocument.Location = new Point(196, 18);
        this.checkBoxFitDocument.Margin = new Padding(3, 2, 3, 2);
        this.checkBoxFitDocument.Name = "checkBoxFitDocument";
        this.checkBoxFitDocument.Size = new Size(203, 21);
        this.checkBoxFitDocument.TabIndex = 4;
        this.checkBoxFitDocument.Text = "Вписать в область печати";
        this.checkBoxFitDocument.UseVisualStyleBackColor = true;
        this.virtualTreePrintQueue.AllowDrop = true;
        this.virtualTreePrintQueue.AllowIndividualRowResize = false;
        this.virtualTreePrintQueue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.virtualTreePrintQueue.ImageList = (ImageList) null;
        this.virtualTreePrintQueue.LayoutSettingsService = (ILayoutSettingsService) null;
        this.virtualTreePrintQueue.LineStyle = LineStyle.Dot;
        this.virtualTreePrintQueue.Location = new Point(7, 121);
        this.virtualTreePrintQueue.Margin = new Padding(4, 4, 4, 4);
        this.virtualTreePrintQueue.Name = "virtualTreePrintQueue";
        this.virtualTreePrintQueue.PrintersSettingsService = (IPrintersSettingsService) null;
        this.virtualTreePrintQueue.Size = new Size(692, 253);
        this.virtualTreePrintQueue.TabIndex = 9;
        this.virtualTreePrintQueue.SelectionChanged += new EventHandler(this.VirtualTreePrintQueue_SelectionChanged);
        this.virtualTreePrintQueue.Click += new EventHandler(this.VirtualTree_GotFocus);
        this.comboBoxPrinters.FormattingEnabled = true;
        this.comboBoxPrinters.Location = new Point(88, 52);
        this.comboBoxPrinters.Margin = new Padding(4, 4, 4, 4);
        this.comboBoxPrinters.Name = "comboBoxPrinters";
        this.comboBoxPrinters.Size = new Size(349, 24);
        this.comboBoxPrinters.TabIndex = 5;
        this.comboBoxLayouts.FormattingEnabled = true;
        this.comboBoxLayouts.Location = new Point(88, 86);
        this.comboBoxLayouts.Margin = new Padding(4, 4, 4, 4);
        this.comboBoxLayouts.Name = "comboBoxLayouts";
        this.comboBoxLayouts.Size = new Size(349, 24);
        this.comboBoxLayouts.TabIndex = 6;
        this.collapsibleSplitterWorkspace.AnimationDelay = 20;
        this.collapsibleSplitterWorkspace.AnimationStep = 20;
        this.collapsibleSplitterWorkspace.BorderStyle3D = Border3DStyle.Flat;
        this.collapsibleSplitterWorkspace.ControlToHide = (Control) this.panelWorkspaceTree;
        this.collapsibleSplitterWorkspace.Dock = DockStyle.Top;
        this.collapsibleSplitterWorkspace.ExpandParentForm = false;
        this.collapsibleSplitterWorkspace.Location = new Point(0, 306);
        this.collapsibleSplitterWorkspace.Margin = new Padding(4, 4, 4, 4);
        this.collapsibleSplitterWorkspace.Name = "collapsibleSplitterWorkspace";
        this.collapsibleSplitterWorkspace.TabIndex = 29;
        this.collapsibleSplitterWorkspace.TabStop = false;
        this.collapsibleSplitterWorkspace.UseAnimations = false;
        this.collapsibleSplitterWorkspace.VisualStyle = VisualStyles.Mozilla;
        this.collapsibleSplitterWorkspace.SplitterMoving += new SplitterEventHandler(this.CollapsibleSplitterWorkspace_SplitterMoving);
        this.panelWorkspaceTree.Controls.Add((Control) this.virtualTreeWorkspace);
        this.panelWorkspaceTree.Dock = DockStyle.Top;
        this.panelWorkspaceTree.Location = new Point(0, 0);
        this.panelWorkspaceTree.Margin = new Padding(4, 4, 4, 4);
        this.panelWorkspaceTree.MinimumSize = new Size(0, 185);
        this.panelWorkspaceTree.Name = "panelWorkspaceTree";
        this.panelWorkspaceTree.Size = new Size(703, 306);
        this.panelWorkspaceTree.TabIndex = 0;
        this.panelWorkspaceTree.Resize += new EventHandler(this.PanelWorkspaceTree_Resize);
        this.virtualTreeWorkspace.AllowDrop = true;
        this.virtualTreeWorkspace.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.virtualTreeWorkspace.ImageList = (ImageList) null;
        this.virtualTreeWorkspace.LineStyle = LineStyle.Dot;
        this.virtualTreeWorkspace.Location = new Point(7, 2);
        this.virtualTreeWorkspace.Margin = new Padding(4, 4, 4, 4);
        this.virtualTreeWorkspace.Name = "virtualTreeWorkspace";
        this.virtualTreeWorkspace.Size = new Size(692, 299);
        this.virtualTreeWorkspace.TabIndex = 0;
        this.virtualTreeWorkspace.SelectionChanged += new EventHandler(this.VirtualTreeWorkspace_SelectionChanged);
        this.virtualTreeWorkspace.Click += new EventHandler(this.VirtualTree_GotFocus);
        this.collapsibleSplitterPdfCenter.AnimationDelay = 20;
        this.collapsibleSplitterPdfCenter.AnimationStep = 20;
        this.collapsibleSplitterPdfCenter.BorderStyle3D = Border3DStyle.Flat;
        this.collapsibleSplitterPdfCenter.ControlToHide = (Control) this.panelWorkspace;
        this.collapsibleSplitterPdfCenter.ExpandParentForm = false;
        this.collapsibleSplitterPdfCenter.Location = new Point(703, 28);
        this.collapsibleSplitterPdfCenter.Margin = new Padding(4, 4, 4, 4);
        this.collapsibleSplitterPdfCenter.Name = "collapsibleSplitter1";
        this.collapsibleSplitterPdfCenter.TabIndex = 29;
        this.collapsibleSplitterPdfCenter.TabStop = false;
        this.collapsibleSplitterPdfCenter.UseAnimations = false;
        this.collapsibleSplitterPdfCenter.VisualStyle = VisualStyles.Mozilla;
        this.collapsibleSplitterPdfCenter.SplitterMoving += new SplitterEventHandler(this.CollapsibleSplitterPdfViewer_SplitterMoving);
        this.collapsibleSplitterPdfCenter.SplitterMoved += new SplitterEventHandler(this.CollapsibleSplitterPdfViewer_SplitterMoved);
        this.panelPdfViewer.Controls.Add((Control) this.pdfViewer);
        this.panelPdfViewer.Dock = DockStyle.Fill;
        this.panelPdfViewer.Location = new Point(707, 28);
        this.panelPdfViewer.Margin = new Padding(4, 4, 4, 4);
        this.panelPdfViewer.MinimumSize = new Size(267, 0);
        this.panelPdfViewer.Name = "panelPdfViewer";
        this.panelPdfViewer.Size = new Size(625, 730);
        this.panelPdfViewer.TabIndex = 30;
        this.panelPdfViewer.Resize += new EventHandler(this.PanelPdfViewer_Resize);
        this.pdfViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.pdfViewer.BackColor = SystemColors.AppWorkspace;
        this.pdfViewer.BorderStyle = BorderStyle.FixedSingle;
        this.pdfViewer.Document = (IPdfDocument) null;
        this.pdfViewer.Location = new Point(5, 0);
        this.pdfViewer.Margin = new Padding(5, 5, 5, 5);
        this.pdfViewer.Name = "pdfViewer";
        this.pdfViewer.Size = new Size(613, 723);
        this.pdfViewer.TabIndex = 0;
        this.AutoScaleDimensions = new SizeF(8f, 16f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(1332, 758);
        this.Controls.Add((Control) this.panelPdfViewer);
        this.Controls.Add((Control) this.collapsibleSplitterPdfCenter);
        this.Controls.Add((Control) this.panelWorkspace);
        this.Controls.Add((Control) this.menuStrip);
        this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
        this.MainMenuStrip = this.menuStrip;
        this.Margin = new Padding(3, 2, 3, 2);
        this.MinimumSize = new Size(1347, 795);
        this.Name = nameof (PrintCenterForm);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Центр печати PDF";
        this.FormClosing += new FormClosingEventHandler(this.PrintCenterForm_FormClosing);
        this.Shown += new EventHandler(this.PrintCenterForm_Shown);
        this.Resize += new EventHandler(this.PrintCenterForm_Resize);
        this.menuStrip.ResumeLayout(false);
        this.menuStrip.PerformLayout();
        this.numericUpDownCopies.EndInit();
        this.panelWorkspace.ResumeLayout(false);
        this.panelPrintQueue.ResumeLayout(false);
        this.panelPrintQueue.PerformLayout();
        this.virtualTreePrintQueue.EndInit();
        this.panelWorkspaceTree.ResumeLayout(false);
        this.virtualTreeWorkspace.EndInit();
        this.panelPdfViewer.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
      }
    }
}
