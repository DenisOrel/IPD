// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Editor.ImDocumentEditorForm
// Assembly: IMDocumentEditor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 105C08B1-9CA8-4A5F-8603-7439747D5610
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\IMDocumentEditor\IMDocumentEditor.exe

using Intermech.Bars;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Document.Editor;

public class ImDocumentEditorForm : ImDocumentEditorFormBase
{
  private string fileName;
  private DocumentFileType docFileType;

  public override string DocumentCaption
  {
    get
    {
      string documentCaption = string.IsNullOrEmpty(this.FileName) ? Path.GetFileNameWithoutExtension(this.DefaultFileName) : new FileInfo(this.FileName).Name;
      if (string.IsNullOrEmpty(documentCaption))
        documentCaption = LocalizationHolder.rm.GetString("Document.Editor_27");
      return documentCaption;
    }
  }

  public bool PackedFile
  {
    [DebuggerStepThrough] get
    {
      return this.docFileType == DocumentFileType.ImDocument_IsPacked || this.docFileType == DocumentFileType.ImDocumentsComplect_IsPacked;
    }
  }

  public DocumentFileType DocFileType
  {
    [DebuggerStepThrough] get => this.docFileType;
    set => this.docFileType = value;
  }

  public string FileName
  {
    [DebuggerStepThrough] get => this.fileName;
    set
    {
      this.fileName = value;
      this.DefaultFileName = Path.GetFileNameWithoutExtension(value);
      if (value == null)
        value = "";
      this.ToolTipText = value;
    }
  }

  public ImDocumentEditorForm(
    IImDocumentManager documentManager,
    bool createDocument,
    bool createFirstPage)
    : base(documentManager, createDocument, createFirstPage)
  {
    this.InitializeComponent();
  }

  public ImDocumentEditorForm(
    IImDocumentManager documentManager,
    ImDocument document,
    bool readOnly)
    : base(documentManager, document, readOnly)
  {
    this.InitializeComponent();
  }

  public ImDocumentEditorForm(
    IImDocumentManager documentManager,
    DocumentControl documentControl,
    bool readOnly)
    : base(documentManager, documentControl, readOnly)
  {
    this.InitializeComponent();
  }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.Name = nameof (ImDocumentEditorForm);
    this.ResumeLayout(false);
  }

  protected override void Dispose(bool disposing) => base.Dispose(disposing);

  protected override void Init() => base.Init();

  public override bool Execute(ICommandState commandState)
  {
    DocumentTreeNode[] context = NodeContextMenu.ContextForContextMenu;
    if (context == null || !NodeContextMenu.ContextMenuCommand)
      context = this.DocumentControl.GetSelectedNodes();
    if (this.MenuHelper != null && this.MenuHelper.Execute(commandState, context, this.DocumentControl))
    {
      NodeContextMenu.ContextForContextMenu = (DocumentTreeNode[]) null;
      NodeContextMenu.ContextMenuCommand = false;
      return true;
    }
    return base.Execute(commandState);
  }

  public override void AssignDocumentControl(DocumentControl value)
  {
    base.AssignDocumentControl(value);
  }

  public override bool QueryStatus(ICommandState commandState)
  {
    DocumentTreeNode[] context = NodeContextMenu.ContextForContextMenu;
    if (context == null || !NodeContextMenu.ContextMenuCommand)
      context = this.DocumentControl.GetSelectedNodes();
    return base.QueryStatus(commandState) || this.MenuHelper != null && this.MenuHelper.QueryStatus(commandState, context, this.DocumentControl);
  }
}
