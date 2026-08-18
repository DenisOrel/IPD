
// Type: Intermech.Navigator.DBObjects.UserToRolesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Вьюшка для назначения и отбирания ролей у пользователя
/// </summary>
[ViewDescriptionProvider(typeof (UserToRolesView.UserToRolesViewDescriptionProvider))]
public class UserToRolesView : UserControl, IView
{
  private System.ComponentModel.Container components;
  public long ObjectID = -1;
  public long ObjectType = -1;
  public string ObjectName = "";
  public long ID = -1;
  private int _imageIndex = -1;
  private bool _initmode;
  private bool _loaded;
  private UserToRolesForm EditorForm;

  public UserToRolesView()
  {
    this.InitializeComponent();
    this._initmode = false;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    this.ObjectID = -1L;
    this.ObjectType = -1L;
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  public void Initialize(long objID, int objType, long relID, System.IServiceProvider services)
  {
    this.ObjectID = objID;
    this.ObjectType = (long) objType;
    this.ObjectName = "";
    this._initmode = true;
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserToRolesView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (UserToRolesView);
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items.Count < 1)
    {
      this.ObjectID = -1L;
    }
    else
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
      this.ObjectID = itemData.ObjectID;
      this.ObjectType = (long) itemData.ObjectType;
      this._initmode = true;
      this._loaded = false;
    }
  }

  /// <summary>
  /// Вернуть номер изображения вьюшки из глобального списка
  /// </summary>
  public int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgUserRoles");
      return this._imageIndex;
    }
  }

  /// <summary>Вернуть порядковый номер вьюшки в списке всех вьюшек</summary>
  public int OrderID => 16 /*0x10*/;

  /// <summary>Вернуть заголовок вьюшки</summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_733");

  /// <summary>
  /// Выполнить действия при активации объекта подходящего типа
  /// </summary>
  /// <param name="previousView">Предыдущая вьюшка</param>
  public void Activate(IView previousView)
  {
    if (this._initmode)
    {
      this.ObjectName = "";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectID);
        this.ID = dbObject.ID;
        this.ObjectName = dbObject.Caption;
      }
      if (this.EditorForm == null)
      {
        this.EditorForm = new UserToRolesForm();
        this.EditorForm.SetParent((Control) this);
      }
      this._initmode = false;
    }
    if (!this._loaded)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this.EditorForm.ObjectID = this.ObjectID;
        this.EditorForm.ObjectName = this.ObjectName;
        ArrayList roles = new ArrayList();
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(sessionKeeper.Session.IdentHelper.SimpleRelationTypeID);
        if (relationCollection != null)
        {
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(-7, RelationalOperators.Equal, (object) sessionKeeper.Session.IdentHelper.RolesTypeID, LogicalOperators.NONE, 0, true)
          }, new ColumnDescriptor[3]
          {
            new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
            new ColumnDescriptor((object) new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
          });
          foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersIn(paramSet, this.ID).Rows)
            roles.Add((object) new UserToRoles(Convert.ToInt64(row[1]), Convert.ToString(row[2]), Convert.ToInt64(row[0])));
          this.EditorForm.IconAsByteArray = sessionKeeper.Session.GetObjectType(new Guid("cad00007-306c-11d8-b4e9-00304f19f545")).Icon;
        }
        this.EditorForm.LoadObjectData(roles, sessionKeeper.Session.IsAdmin);
      }
    }
    this._loaded = true;
  }

  public void Deactivate(IView nextView)
  {
    if (this.EditorForm == null || !this.EditorForm.IsChanged)
      return;
    if (MessageBox.Show($"{LocalizationHolder.rm.GetString(sc_4592.ssp_imclient_4593())}\"{this.ObjectName}\"?", LocalizationHolder.rm.GetString("Client.Core_781"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
      this.EditorForm.SaveObjectData();
    else
      this.EditorForm.IsChanged = false;
  }

  private sealed class UserToRolesViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_733"),
        ImageIndex = Holder.NamedImageList.ImageIndex("imgUserRoles"),
        OrderID = 16 /*0x10*/
      };
    }
  }
}
