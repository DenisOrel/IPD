// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ScriptEditCon
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Docking;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

public class ScriptEditCon : DockControl, IDisposable, IOpenAsObjectSupport
{
  public ScriptEdit2 scriptEditor;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  /// <summary>GUID окна для восстановления</summary>
  public static readonly Guid ScriptWindowGuid = new Guid("{BEA01358-7ED6-4ed3-9D1D-B1CC53570D25}");

  protected ScriptEditCon()
  {
    this.AllowedStates = DockLocation.Document;
    this.InitializeComponent();
    this.scriptEditor = new ScriptEdit2();
  }

  public ScriptEditCon(ExpertScriptType eType, long templId, string newObjName)
  {
    this.AllowedStates = DockLocation.Document;
    this.scriptEditor = new ScriptEdit2(eType);
    this.InitializeComponent();
    this.scriptEditor.scriptCaption = newObjName;
    this.scriptEditor.newObjName = newObjName;
    this.scriptEditor.ExecuteForCreate(templId, false);
  }

  public ScriptEditCon(long scrId)
  {
    this.AllowedStates = DockLocation.Document;
    this.scriptEditor = new ScriptEdit2();
    this.InitializeComponent();
    this.scriptEditor.ExecuteForEdit(scrId, false);
  }

  private void InitializeComponent()
  {
    this.components = new System.ComponentModel.Container();
    this.SuspendLayout();
    this.Controls.Add((Control) this.scriptEditor);
    this.scriptEditor.Dock = DockStyle.Fill;
    this.scriptEditor.Changed += new EventHandler(this.scriptEditor_Changed);
    this.scriptEditor.Closed += new EventHandler(this.scriptEditor_Closed);
    this.FloatingSize = new Size(250, 300);
    this.Name = "ImDocumentEditorFormBase";
    this.Size = new Size(292, 273);
    this.ResumeLayout(false);
    this.Guid = ScriptEditCon.ScriptWindowGuid;
  }

  private void scriptEditor_Closed(object sender, EventArgs e) => this.Close();

  private void scriptEditor_Changed(object sender, EventArgs e)
  {
    this.UpdateDocumentWindowCaption();
  }

  /// <summary>Dispose</summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.scriptEditor != null && !this.scriptEditor.IsDisposed)
      {
        this.scriptEditor.Parent = (Control) null;
        this.scriptEditor.Dispose();
        this.scriptEditor = (ScriptEdit2) null;
      }
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this.scriptEditor.scriptChanged)
    {
      if (this.scriptEditor.needCloseQuery)
      {
        int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_438"), LocalizationHolder.rm.GetString("Expert.Editor_437"), MessageBoxButtons.YesNoCancel);
        if (num == 6 && !this.scriptEditor.SaveScript())
          e.Cancel = true;
        if (num == 2)
          e.Cancel = true;
      }
      else if (MessageBox.Show(LocalizationHolder.rm.GetString("Expert.Editor_440"), LocalizationHolder.rm.GetString("Expert.Editor_439"), MessageBoxButtons.YesNo) == DialogResult.Yes)
        e.Cancel = true;
    }
    if (e.Cancel || !(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.scriptEditor.ObjectChanged));
  }

  /// <summary>Заголовок документа</summary>
  public virtual string DocumentCaption
  {
    [DebuggerStepThrough] get
    {
      return this.scriptEditor != null ? this.scriptEditor.scriptCaption : LocalizationHolder.rm.GetString("Expert.Editor_549");
    }
  }

  public virtual void UpdateDocumentWindowCaption() => this.Text = this.FormatDocWindowCaption();

  public virtual string FormatDocWindowCaption()
  {
    bool flag = false;
    if (this.scriptEditor != null)
      flag = this.scriptEditor.scriptChanged;
    return this.DocumentCaption + (this.ReadOnly ? " " + LocalizationHolder.rm.GetString("Expert.Editor_548") : "") + (flag ? "*" : "");
  }

  /// <summary>Режим только для чтения</summary>
  public virtual bool ReadOnly
  {
    [DebuggerStepThrough] get => this.scriptEditor == null || this.scriptEditor.readOnly;
    set
    {
      if (this.scriptEditor == null || this.ReadOnly == value)
        return;
      this.scriptEditor.readOnly = value;
      this.scriptEditor.UpdateReadOnlyState();
    }
  }

  /// <summary>вернуть раздел справки для контрола</summary>
  public override string HelpID => "1327";

  void IDisposable.Dispose() => this.Dispose(true);

  /// <summary>Получить данные необходимые для восстановления окна при загрузке IMClient</summary>
  /// <returns></returns>
  protected override string GetPersistString()
  {
    HybridDictionary graph = new HybridDictionary();
    graph[(object) "ScriptId"] = (object) Convert.ToString(this.scriptEditor.scriptID);
    if (this.ReadOnly)
      graph[(object) "ReadOnly"] = (object) this.ReadOnly;
    string empty = string.Empty;
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) graph);
      return Convert.ToBase64String(serializationStream.ToArray());
    }
  }

  public bool CanBeOpenedInNewWindowsAsObject => this.scriptEditor.scriptID != -1L;

  public void OpenNewInstanceAsObject()
  {
    ScriptEditCon.DoOpenInNewWindowCommand(new long[1]
    {
      this.scriptEditor.scriptID
    });
  }

  private static void DoOpenInNewWindowCommand(long[] objIDs)
  {
    ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(objIDs);
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (IViewState), (object) new ViewStateService());
    ServiceContainer viewServices2 = viewServices1;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand("OpenInNewWindow", Intermech.Navigator.ContextMenu.Services.GetCommandsTable(items, (System.IServiceProvider) viewServices2), (System.IServiceProvider) viewServices1);
  }
}
