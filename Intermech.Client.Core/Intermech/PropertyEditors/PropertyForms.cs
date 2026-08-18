
// Type: Intermech.PropertyEditors.PropertyForms
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Configurator;
using Intermech.Client.Core.FormDesigner.TabPages;
using Intermech.Interfaces.Client;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class PropertyForms : IDisposable
{
  private Guid instGuid = Guid.Empty;
  private PropertyTabPageForm _PropertyTabPageForm;
  private PropertyForm _PropertyForm;
  private ListForm _ListForm;
  private EmptyForm _EmptyForm;
  private SecurityForm _SecurityForm;
  private DocObjTypeForm _DocObjTypeForm;
  private Attr4ObjTypeForm _Attr4ObjTypeForm;
  private ObjTypeApplForm _ObjTypeApplForm;
  private Attr4RelTypeForm _Attr4RelTypeForm;
  private ObjTypes4AttrForm _ObjTypes4AttrForm;
  private RelTypes4AttrForm _RelTypes4AttrForm;
  private TabPageForm _LCSchema4ObjTypeForm;
  private Forms4TypeForm _forms4Type;
  private ActionsForm _actionForm;
  private ConfigurationForm _configurationForm;
  private AttrGroupsListForm _attrGroupsListForm;
  private ParentObjectTypeForm parentTypeForm;
  private ImbaseTablesListForm _attrTablesListForm;

  public PropertyForms(Guid aInstGuid) => this.instGuid = aInstGuid;

  private void DisposeForm(UserControl form)
  {
  }

  public void Dispose()
  {
    this.DisposeForm((UserControl) this._PropertyTabPageForm);
    this._PropertyTabPageForm = (PropertyTabPageForm) null;
    this.DisposeForm((UserControl) this._PropertyForm);
    this._PropertyForm = (PropertyForm) null;
    this.DisposeForm((UserControl) this._ListForm);
    this._ListForm = (ListForm) null;
    this.DisposeForm((UserControl) this._EmptyForm);
    this._EmptyForm = (EmptyForm) null;
    this.DisposeForm((UserControl) this._SecurityForm);
    this._SecurityForm = (SecurityForm) null;
    this.DisposeForm((UserControl) this._Attr4ObjTypeForm);
    this._Attr4ObjTypeForm = (Attr4ObjTypeForm) null;
    this.DisposeForm((UserControl) this._ObjTypeApplForm);
    this._ObjTypeApplForm = (ObjTypeApplForm) null;
    this.DisposeForm((UserControl) this._Attr4RelTypeForm);
    this._Attr4RelTypeForm = (Attr4RelTypeForm) null;
    this.DisposeForm((UserControl) this._LCSchema4ObjTypeForm);
    this._LCSchema4ObjTypeForm = (TabPageForm) null;
    this.DisposeForm((UserControl) this._actionForm);
    this._actionForm = (ActionsForm) null;
    this.DisposeForm((UserControl) this._attrGroupsListForm);
    this._attrGroupsListForm = (AttrGroupsListForm) null;
  }

  public PropertyTabPageForm PropertyTabPageForm
  {
    get
    {
      if (this._PropertyTabPageForm == null)
        this._PropertyTabPageForm = new PropertyTabPageForm(this.instGuid);
      return this._PropertyTabPageForm;
    }
  }

  public PropertyForm PropertyForm
  {
    get
    {
      if (this._PropertyForm == null)
        this._PropertyForm = new PropertyForm(this.instGuid);
      return this._PropertyForm;
    }
  }

  public ListForm ListForm
  {
    get
    {
      if (this._ListForm == null)
        this._ListForm = new ListForm(this.instGuid);
      return this._ListForm;
    }
  }

  public EmptyForm EmptyForm
  {
    get
    {
      if (this._EmptyForm == null)
        this._EmptyForm = new EmptyForm(this.instGuid);
      return this._EmptyForm;
    }
  }

  public SecurityForm SecurityForm
  {
    get
    {
      if (this._SecurityForm == null)
        this._SecurityForm = new SecurityForm(this.instGuid);
      return this._SecurityForm;
    }
  }

  public DocObjTypeForm DocObjTypeForm
  {
    get
    {
      if (this._DocObjTypeForm == null)
        this._DocObjTypeForm = new DocObjTypeForm(this.instGuid);
      return this._DocObjTypeForm;
    }
  }

  public Attr4ObjTypeForm Attr4ObjTypeForm
  {
    get
    {
      if (this._Attr4ObjTypeForm == null)
        this._Attr4ObjTypeForm = new Attr4ObjTypeForm(this.instGuid);
      return this._Attr4ObjTypeForm;
    }
  }

  public ObjTypeApplForm ObjTypeApplForm
  {
    get
    {
      if (this._ObjTypeApplForm == null)
        this._ObjTypeApplForm = new ObjTypeApplForm(this.instGuid);
      return this._ObjTypeApplForm;
    }
  }

  public Attr4RelTypeForm Attr4RelTypeForm
  {
    get
    {
      if (this._Attr4RelTypeForm == null)
        this._Attr4RelTypeForm = new Attr4RelTypeForm(this.instGuid);
      return this._Attr4RelTypeForm;
    }
  }

  public ObjTypes4AttrForm ObjTypes4AttrForm
  {
    get
    {
      if (this._ObjTypes4AttrForm == null)
        this._ObjTypes4AttrForm = new ObjTypes4AttrForm(this.instGuid);
      return this._ObjTypes4AttrForm;
    }
  }

  public RelTypes4AttrForm RelTypes4AttrForm
  {
    get
    {
      if (this._RelTypes4AttrForm == null)
        this._RelTypes4AttrForm = new RelTypes4AttrForm(this.instGuid);
      return this._RelTypes4AttrForm;
    }
  }

  public TabPageForm LCSchema4ObjTypeForm
  {
    get
    {
      if (this._LCSchema4ObjTypeForm == null)
        this._LCSchema4ObjTypeForm = !(ServicesManager.GetService(typeof (ILCSchema4ObjTypeFormProvider)) is ILCSchema4ObjTypeFormProvider service) ? new TabPageForm(this.instGuid) : (TabPageForm) service.GetForm(this.instGuid);
      return this._LCSchema4ObjTypeForm;
    }
  }

  public Forms4TypeForm Forms4Type
  {
    get
    {
      if (this._forms4Type == null)
        this._forms4Type = new Forms4TypeForm(this.instGuid);
      return this._forms4Type;
    }
  }

  public ActionsForm ActionsFormType
  {
    get
    {
      if (this._actionForm == null)
        this._actionForm = new ActionsForm(this.instGuid);
      return this._actionForm;
    }
  }

  public ConfigurationForm ConfigurationFormType
  {
    get
    {
      if (this._configurationForm == null)
        this._configurationForm = new ConfigurationForm(this.instGuid);
      return this._configurationForm;
    }
  }

  public AttrGroupsListForm AttrGroupsList
  {
    get
    {
      if (this._attrGroupsListForm == null)
        this._attrGroupsListForm = new AttrGroupsListForm(this.instGuid);
      return this._attrGroupsListForm;
    }
  }

  public ParentObjectTypeForm ParentTypeForm
  {
    get
    {
      if (this.parentTypeForm == null)
        this.parentTypeForm = new ParentObjectTypeForm(this.instGuid);
      return this.parentTypeForm;
    }
  }

  public ImbaseTablesListForm AttrImbaseTablesList
  {
    get
    {
      if (this._attrTablesListForm == null)
        this._attrTablesListForm = new ImbaseTablesListForm(this.instGuid);
      return this._attrTablesListForm;
    }
  }
}
