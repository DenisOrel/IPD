
// Type: Intermech.Client.Core.ExpandNodesWithBeginUpdate
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class ExpandNodesWithBeginUpdate : Form
{
  /// <summary>
  /// Объект для потокобезопасного доступа к переменным при фоновом обращении к форме
  /// </summary>
  private object lockForm = new object();
  /// <summary>Узел, который требуется рекурсивно раскрыть</summary>
  private NavigatorTreeNode _node;
  /// <summary>Дерево Навигатора</summary>
  private NavigatorTreeView _tree;
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider _services;
  /// <summary>
  /// Поток, в рамках которого выполняется раскрытие узла дерева Навигатора
  /// </summary>
  private Thread _thread;
  /// <summary>Текущая форма</summary>
  public static ExpandNodesWithBeginUpdate currForm;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lbMessage;
  private Button btnCancel;
  private System.Windows.Forms.Timer timerRefresh;

  /// <summary>Создать экземпляр формы</summary>
  public ExpandNodesWithBeginUpdate() => this.InitializeComponent();

  /// <summary>
  /// Вызвать форму для рекурсивного раскрытия указанного узла дерева Навигатора
  /// </summary>
  /// <param name="node">Раскрываемый узел</param>
  /// <param name="tree">Дерево Навигатора</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  private void InternalExecute(
    NavigatorTreeNode node,
    NavigatorTreeView tree,
    System.IServiceProvider viewServices)
  {
    this._tree = tree;
    this._node = node;
    this._services = viewServices;
    this._tree.Disposed += new EventHandler(this.TreeViewDisposed);
    this.timerRefresh.Interval = 250;
    this.timerRefresh.Enabled = true;
  }

  /// <summary>
  /// Вызвать форму для рекурсивного раскрытия указанного узла дерева Навигатора
  /// </summary>
  /// <param name="node">Раскрываемый узел</param>
  /// <param name="tree">Дерево Навигатора</param>
  /// <param name="viewServices">Контейнер сервисов</param>
  public static void Execute(
    NavigatorTreeNode node,
    NavigatorTreeView tree,
    System.IServiceProvider viewServices)
  {
    if (ExpandNodesWithBeginUpdate.currForm != null)
      return;
    CDBRelationsApplicabilityCache.Reset();
    ExpandNodesWithBeginUpdate.currForm = new ExpandNodesWithBeginUpdate();
    ExpandNodesWithBeginUpdate.currForm.InternalExecute(node, tree, viewServices);
  }

  /// <summary>Инициализация формы</summary>
  private void Init()
  {
    this.Size = new Size(350, 100);
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this.UpdateControls();
  }

  /// <summary>Установить статус всех контролов формы</summary>
  private void UpdateControls()
  {
  }

  /// <summary>Закрывается форма</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ExpandNodesWithBeginUpdate_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.StopThread();
  }

  /// <summary>Форма закрыта</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ExpandNodesWithBeginUpdate_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this._tree != null)
      this._tree.Disposed -= new EventHandler(this.TreeViewDisposed);
    ExpandNodesWithBeginUpdate.currForm = (ExpandNodesWithBeginUpdate) null;
    CDBRelationsApplicabilityCache.Reset();
  }

  /// <summary>Прервать раскрытие узлов дерева Навигатора</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCancel(object sender, EventArgs e) => this.Close();

  /// <summary>Выполняется разрушение дерева Навигатора</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void TreeViewDisposed(object sender, EventArgs e) => this.Close();

  /// <summary>Событие от таймера</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void timerRefresh_Tick(object sender, EventArgs e)
  {
    this.timerRefresh.Enabled = false;
    this.StartPosition = FormStartPosition.Manual;
    this.Location = new Point(-100000, -100000);
    this.Show();
    this.Hide();
    this.Init();
    this.StartThread();
  }

  /// <summary>Остановить фоновый поток</summary>
  private void StopThread()
  {
    lock (this.lockForm)
    {
      if (this._thread != null && this._thread.IsAlive)
        this._thread.Abort();
      this._thread = (Thread) null;
    }
  }

  /// <summary>Запустить фоновый поток</summary>
  private void StartThread()
  {
    this.StopThread();
    lock (this.lockForm)
    {
      this._thread = new Thread(new ThreadStart(this.ThreadMethod))
      {
        IsBackground = true,
        Name = "Navigator.ExpandNavigatorTreeNodeWithBeginUpdate"
      };
      this._thread.Start();
    }
  }

  /// <summary>Фоновая задача</summary>
  private void ThreadMethod()
  {
    if (this._node == null)
      return;
    NavigatorTreeNode focusedNode = this._tree.FocusedNode;
    DateTime utcNow = DateTime.UtcNow;
    bool backgroundTreeTasks = OptimizationSettings.BackgroundTreeTasks;
    bool autoScrollOnExpand = this._tree.AutoScrollOnExpand;
    try
    {
      lock (this.lockForm)
      {
        this._tree.LockTreeEvents();
        this._tree.AutoScrollOnExpand = false;
        this._tree.SearchModePopulating = true;
      }
      OptimizationSettings.BackgroundTreeTasks = false;
      int level = this._node.Level;
      try
      {
        this._tree.Invoke((Delegate) new ExpandNodesWithBeginUpdate.NoArgsDelegate(this._tree.BeginUpdate));
        while (this._node != null)
        {
          lock (this.lockForm)
          {
            if (this._thread == null)
              break;
          }
          if (DateTime.UtcNow - utcNow > new TimeSpan(0, 0, 5) && !this.Visible)
          {
            this.Invoke((Delegate) new ExpandNodesWithBeginUpdate.NoArgsDelegate(((Control) this).Show));
            this.Invoke((Delegate) new ExpandNodesWithBeginUpdate.NoArgsDelegate(((Control) this).Update));
          }
          this._node = this._tree.Invoke((Delegate) new ExpandNodesWithBeginUpdate.NodeExpandDelegate(this.ExpandNextNode), (object) this._node, (object) true) as NavigatorTreeNode;
          Thread.Sleep(1);
          if (this._node != null && this._node.Level <= level)
            break;
        }
      }
      catch (ThreadAbortException ex)
      {
      }
    }
    finally
    {
      OptimizationSettings.BackgroundTreeTasks = backgroundTreeTasks;
      this._tree.Invoke((Delegate) new ExpandNodesWithBeginUpdate.NoArgsDelegate(this._tree.EndUpdate));
      this._tree.Invoke((Delegate) new ExpandNodesWithBeginUpdate.NoArgsDelegate(this.ExpandNode));
      lock (this.lockForm)
      {
        this._tree.AutoScrollOnExpand = autoScrollOnExpand;
        this._tree.SearchModePopulating = false;
        this._tree.UnlockTreeEvents();
      }
      if (focusedNode != null)
      {
        lock (this.lockForm)
          this._tree.Invoke((Delegate) new ExpandNodesWithBeginUpdate.NodeFocusDelegate(this.FocusNode), (object) focusedNode);
      }
      lock (this.lockForm)
        this._thread = (Thread) null;
      if (this._tree != null)
        this._tree.Disposed -= new EventHandler(this.TreeViewDisposed);
      ExpandNodesWithBeginUpdate.currForm = (ExpandNodesWithBeginUpdate) null;
      this._tree = (NavigatorTreeView) null;
      this._node = (NavigatorTreeNode) null;
      if (!this.IsDisposed && this.IsHandleCreated)
        this.Invoke((Delegate) new ExpandNodesWithBeginUpdate.NoArgsDelegate(((Form) this).Close));
    }
  }

  private void LockTreeView() => this._tree.SuspendLayout();

  private void UnlockTreeView() => this._tree.ResumeLayout();

  private void FocusNode(NavigatorTreeNode currentNode) => this._tree.FocusedNode = currentNode;

  private NavigatorTreeNode ExpandNextNode(NavigatorTreeNode currentNode, bool withChild)
  {
    return this._tree.ExpandNextNode(currentNode, withChild);
  }

  private void ExpandNode() => this._tree.Nodes[0].Expanded = true;

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExpandNodesWithBeginUpdate));
    this.lbMessage = new Label();
    this.btnCancel = new Button();
    this.timerRefresh = new System.Windows.Forms.Timer(this.components);
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbMessage, "lbMessage");
    this.lbMessage.Name = "lbMessage";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.DoCancel);
    this.timerRefresh.Interval = 250;
    this.timerRefresh.Tick += new EventHandler(this.timerRefresh_Tick);
    this.AcceptButton = (IButtonControl) this.btnCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.lbMessage);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ExpandNodesWithBeginUpdate);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.TopMost = true;
    this.FormClosing += new FormClosingEventHandler(this.ExpandNodesWithBeginUpdate_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.ExpandNodesWithBeginUpdate_FormClosed);
    this.ResumeLayout(false);
  }

  /// <summary>Делегат для вызова простого метода</summary>
  private delegate void NoArgsDelegate();

  /// <summary>Управление фокусировкой узла дерева Навигатора</summary>
  /// <param name="node">Узел</param>
  private delegate void NodeFocusDelegate(NavigatorTreeNode node);

  /// <summary>Управление раскрытием узла дерева Навигатора</summary>
  /// <param name="node">Узел</param>
  /// <param name="withChild">Обрабатывать дочерние узлы</param>
  private delegate NavigatorTreeNode NodeExpandDelegate(NavigatorTreeNode node, bool withChild);
}
