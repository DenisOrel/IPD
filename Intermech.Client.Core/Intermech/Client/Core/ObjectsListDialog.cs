
// Type: Intermech.Client.Core.ObjectsListDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Диалог отображения списка объектов, с возможностью редактирования или без онной</summary>
public class ObjectsListDialog : 
  IpsBaseDialog,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  IControlServiceContainer,
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  /// <summary>Перечисление идентификаторов типов объектов, которые могут быть добавлены в список</summary>
  [NotNull]
  private readonly IReadOnlyCollection<int> _objectTypes;
  /// <summary>В том случае, если в _objectTypes несколько типов объектов, - заголовок ноды в диалоге добавления объектов, объединяющая эти типы объектов</summary>
  [CanBeNull]
  private readonly string _objectTypesCaption;
  /// <summary>Перечисление идентификаторов версий объектов в списке</summary>
  [NotNull]
  private readonly IReadOnlyCollection<long> _objectVerIDs;
  /// <summary>Перечисление идентификаторов версий объектов, удаление которых заблокировано</summary>
  [NotNull]
  private readonly IReadOnlyCollection<long> _protectedObjectVerIDs;
  private ObjectsListUserControl _objectsListUserControl;

  [NotNull]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  [DebuggerHidden]
  protected ObjectsListUserControl ObjectsListUserControl
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._objectsListUserControl.CheckInitializedIn<ObjectsListUserControl>("InitializeComponent");
    }
  }

  protected ObjectsListDialog()
  {
    Intermech.Diagnostics.Check.ObjectState(this.InDesignMode, "Only design mode constructor");
    this.InitializeComponent();
    this._objectTypes = (IReadOnlyCollection<int>) Array.Empty<int>();
    this._objectVerIDs = (IReadOnlyCollection<long>) Array.Empty<long>();
    this._protectedObjectVerIDs = (IReadOnlyCollection<long>) Array.Empty<long>();
  }

  /// <summary>Конструктор диалога</summary>
  /// <param name="ownerServices">сервисы контекста</param>
  /// <param name="contextName">Наименование операции, а рамках которой был вызван диалог</param>
  /// <param name="objectTypes">Перечисление идентификаторов типов объектов, которые могут быть добавлены в список</param>
  /// <param name="objectTypesCaption">В том случае, если в _objectTypes несколько типов объектов, - заголовок ноды в диалоге добавления
  /// объектов, объединяющая эти типы объектов</param>
  /// <param name="objectVerIDs">Перечисление идентификаторов версий объектов в списке</param>
  /// <param name="protectedObjectVerIDs">Перечисление идентификаторов версий объектов, удаление которых заблокировано</param>
  protected ObjectsListDialog(
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [NotNull] IEnumerable<int> objectTypes,
    [CanBeNull] string objectTypesCaption,
    [NotNull] IEnumerable<long> objectVerIDs,
    [CanBeNull] IEnumerable<long> protectedObjectVerIDs = null)
    : base(ownerServices)
  {
    this.InitializeComponent();
    if (!string.IsNullOrWhiteSpace(contextName))
      this.ContextName = contextName;
    this._objectTypes = objectTypes.AsReadOnlyCollection<int>();
    this._objectTypesCaption = objectTypesCaption;
    this._objectVerIDs = objectVerIDs.AsReadOnlyCollection<long>();
    this._protectedObjectVerIDs = (IReadOnlyCollection<long>) ((protectedObjectVerIDs != null ? (object) protectedObjectVerIDs.AsReadOnlyCollection<long>() : (object) null) ?? (object) Array.Empty<long>());
  }

  /// <summary>Показать диалог со списком объектов.</summary>
  /// <param name="ownerServices">сервисы контекста</param>
  /// <param name="contextName">Наименование операции, а рамках которой был вызван диалог</param>
  /// <param name="objectTypes">Перечисление идентификаторов типов объектов, которые могут быть добавлены в список</param>
  /// <param name="objectTypesCaption">В том случае, если в _objectTypes несколько типов объектов, - заголовок ноды в диалоге добавления
  /// объектов, объединяющая эти типы объектов</param>
  /// <param name="objectVerIDs">Перечисление идентификаторов версий объектов в списке</param>
  /// <param name="protectedObjectVerIDs">Перечисление идентификаторов версий объектов, удаление которых заблокировано</param>
  /// <returns>Коллекция идентификаторов версий выбранных объектов.</returns>
  [NotNull]
  public static IReadOnlyCollection<long> Show(
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [NotNull] IEnumerable<int> objectTypes,
    [CanBeNull] string objectTypesCaption,
    [NotNull] IEnumerable<long> objectVerIDs,
    [CanBeNull] IEnumerable<long> protectedObjectVerIDs = null)
  {
    using (ObjectsListDialog objectsListDialog1 = new ObjectsListDialog(ownerServices, contextName, objectTypes, objectTypesCaption, objectVerIDs, protectedObjectVerIDs))
    {
      ObjectsListDialog objectsListDialog2 = objectsListDialog1;
      if (!(ownerServices is IWin32Window owner))
        owner = ownerServices != null ? ownerServices.GetService<IWin32Window>(false) : (IWin32Window) null;
      return objectsListDialog2.ShowDialog(owner) == DialogResult.OK ? objectsListDialog1.SelectedObjectVersionIDs : (IReadOnlyCollection<long>) Array.Empty<long>();
    }
  }

  /// <summary>При показе окна</summary>
  protected override void OnShown(EventArgs e)
  {
    this.ObjectsListUserControl.Init(this.Services, this._objectTypes, this._objectTypesCaption, this._objectVerIDs, this._protectedObjectVerIDs);
    base.OnShown(e);
  }

  /// <summary>Интерфейс идентификатора ноды сфокусированной ноды</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [CanBeNull]
  public INodeID FocusedNodeID
  {
    [DebuggerHidden] get => this.ObjectsListUserControl.FocusedNodeId;
  }

  /// <summary>Идентификатор сфокусированной версии объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectVersionID
  {
    [DebuggerHidden] get => this.ObjectsListUserControl.FocusedObjectVersionId;
  }

  /// <summary>Идентификатор типа сфокусированного объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? FocusedObjectTypeID
  {
    [DebuggerHidden] get => this.ObjectsListUserControl.FocusedObjectTypeId;
  }

  /// <summary>Идентификаторы выбранных версий объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<long> SelectedObjectVersionIDs
  {
    [DebuggerHidden] get => this.ObjectsListUserControl.SelectedObjectVersionIDs;
  }

  /// <summary>Уникальные идентификаторы типов выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyCollection<int> SelectedObjectTypeIDs
  {
    [DebuggerHidden] get => this.ObjectsListUserControl.SelectedObjectTypeIDs;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._objectsListUserControl = new ObjectsListUserControl();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 440);
    this._pnlDialogButtons.Size = new Size(577, 36);
    this._pnlDialogButtons.TabIndex = 1;
    this._bevelDialogButtons.Location = new Point(0, 438);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(577, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(404, 0);
    this._objectsListUserControl.Description = "Выбранные объекты:";
    this._objectsListUserControl.Dock = DockStyle.Fill;
    this._objectsListUserControl.Location = new Point(0, 0);
    this._objectsListUserControl.Name = "ObjectsListUserControl";
    this._objectsListUserControl.ContextName = (string) null;
    this._objectsListUserControl.Size = new Size(577, 438);
    this._objectsListUserControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.ClientSize = new Size(577, 476);
    this.Controls.Add((Control) this.ObjectsListUserControl);
    this.FormBorderStyle = FormBorderStyle.Sizable;
    this.MinimumSize = new Size(465, 395);
    this.Name = nameof (ObjectsListDialog);
    this.Text = "Объекты";
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this.ObjectsListUserControl, 0);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
