
// Type: Intermech.PropertyEditors.TabPages
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Configurator;
using Intermech.Client.Core.FormDesigner.TabPages;
using System;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class TabPages : IDisposable
{
  private Guid instGuid = Guid.Empty;
  private PropertyTabPage _PropertyTabPage;
  private ListTabPage _ListTabPage;
  private EmptyTabPage _EmptyTabPage;
  private SecurityTabPage _SecurityTabPage;
  private Attr4ObjTypeTabPage _Attr4ObjTypeTabPage;
  private ObjTypeApplTabPage _ObjTypeApplTabPage;
  private Attr4RelTypeTabPage _Attr4RelTypeTabPage;
  private ObjTypes4AttrTabPage _ObjTypes4AttrTabPage;
  private DocObjTypeTabPage _DocObjTypeTabPage;
  private RelTypes4AttrTabPage _RelTypes4AttrTabPage;
  private LCSchema4ObjTypeTabPage _LCSchema4ObjTypeTabPage;
  private Forms4ObjectTypePage _forms4ObjectTypePage;
  private Forms4RelationTypePage _forms4RelationTypePage;
  private ActionsTabPage _actionsTabPage;
  private ConfigurationTabPage _configurationTabPage;
  private AttrGroupsListTabPage _attrGroupsListTabPage;
  private ImbaseTablesTabPage _attrImbaseTablesListTabPage;
  private ParentObjectTypeTabPage parentTypeTabPage;

  public TabPages(Guid aInstGuid) => this.instGuid = aInstGuid;

  private void DisposeTabPage(TabPage tabPage)
  {
  }

  public void Dispose()
  {
    this.DisposeTabPage((TabPage) this._PropertyTabPage);
    this._PropertyTabPage = (PropertyTabPage) null;
    this.DisposeTabPage((TabPage) this._ListTabPage);
    this._ListTabPage = (ListTabPage) null;
    this.DisposeTabPage((TabPage) this._EmptyTabPage);
    this._EmptyTabPage = (EmptyTabPage) null;
    this.DisposeTabPage((TabPage) this._SecurityTabPage);
    this._SecurityTabPage = (SecurityTabPage) null;
    this.DisposeTabPage((TabPage) this._Attr4ObjTypeTabPage);
    this._Attr4ObjTypeTabPage = (Attr4ObjTypeTabPage) null;
    this.DisposeTabPage((TabPage) this._ObjTypeApplTabPage);
    this._ObjTypeApplTabPage = (ObjTypeApplTabPage) null;
    this.DisposeTabPage((TabPage) this._Attr4RelTypeTabPage);
    this._Attr4RelTypeTabPage = (Attr4RelTypeTabPage) null;
    this.DisposeTabPage((TabPage) this._LCSchema4ObjTypeTabPage);
    this._LCSchema4ObjTypeTabPage = (LCSchema4ObjTypeTabPage) null;
    this.DisposeTabPage((TabPage) this._actionsTabPage);
    this._actionsTabPage = (ActionsTabPage) null;
    this.DisposeTabPage((TabPage) this._attrGroupsListTabPage);
    this._attrGroupsListTabPage = (AttrGroupsListTabPage) null;
  }

  public PropertyTabPage PropertyTabPage
  {
    get
    {
      if (this._PropertyTabPage == null)
        this._PropertyTabPage = new PropertyTabPage(this.instGuid);
      return this._PropertyTabPage;
    }
  }

  public ListTabPage ListTabPage
  {
    get
    {
      if (this._ListTabPage == null)
        this._ListTabPage = new ListTabPage(this.instGuid);
      return this._ListTabPage;
    }
  }

  public EmptyTabPage EmptyTabPage
  {
    get
    {
      if (this._EmptyTabPage == null)
        this._EmptyTabPage = new EmptyTabPage(this.instGuid);
      return this._EmptyTabPage;
    }
  }

  public SecurityTabPage SecurityTabPage
  {
    get
    {
      if (this._SecurityTabPage == null)
        this._SecurityTabPage = new SecurityTabPage(this.instGuid);
      return this._SecurityTabPage;
    }
  }

  public Attr4ObjTypeTabPage Attr4ObjTypeTabPage
  {
    get
    {
      if (this._Attr4ObjTypeTabPage == null)
        this._Attr4ObjTypeTabPage = new Attr4ObjTypeTabPage(this.instGuid);
      return this._Attr4ObjTypeTabPage;
    }
  }

  public ObjTypeApplTabPage ObjTypeApplTabPage
  {
    get
    {
      if (this._ObjTypeApplTabPage == null)
        this._ObjTypeApplTabPage = new ObjTypeApplTabPage(this.instGuid);
      return this._ObjTypeApplTabPage;
    }
  }

  public Attr4RelTypeTabPage Attr4RelTypeTabPage
  {
    get
    {
      if (this._Attr4RelTypeTabPage == null)
        this._Attr4RelTypeTabPage = new Attr4RelTypeTabPage(this.instGuid);
      return this._Attr4RelTypeTabPage;
    }
  }

  public ObjTypes4AttrTabPage ObjTypes4AttrTabPage
  {
    get
    {
      if (this._ObjTypes4AttrTabPage == null)
        this._ObjTypes4AttrTabPage = new ObjTypes4AttrTabPage(this.instGuid);
      return this._ObjTypes4AttrTabPage;
    }
  }

  public DocObjTypeTabPage DocObjTypeTabPage
  {
    get
    {
      if (this._DocObjTypeTabPage == null)
        this._DocObjTypeTabPage = new DocObjTypeTabPage(this.instGuid);
      return this._DocObjTypeTabPage;
    }
  }

  public RelTypes4AttrTabPage RelTypes4AttrTabPage
  {
    get
    {
      if (this._RelTypes4AttrTabPage == null)
        this._RelTypes4AttrTabPage = new RelTypes4AttrTabPage(this.instGuid);
      return this._RelTypes4AttrTabPage;
    }
  }

  public LCSchema4ObjTypeTabPage LCSchema4ObjTypeTabPage
  {
    get
    {
      if (this._LCSchema4ObjTypeTabPage == null)
        this._LCSchema4ObjTypeTabPage = new LCSchema4ObjTypeTabPage(this.instGuid);
      return this._LCSchema4ObjTypeTabPage;
    }
  }

  public Forms4ObjectTypePage Forms4ObjectTypePage
  {
    get
    {
      if (this._forms4ObjectTypePage == null)
        this._forms4ObjectTypePage = new Forms4ObjectTypePage(this.instGuid);
      return this._forms4ObjectTypePage;
    }
  }

  public Forms4RelationTypePage Forms4RelationTypePage
  {
    get
    {
      if (this._forms4RelationTypePage == null)
        this._forms4RelationTypePage = new Forms4RelationTypePage(this.instGuid);
      return this._forms4RelationTypePage;
    }
  }

  public ActionsTabPage ActionsTabPage
  {
    get
    {
      if (this._actionsTabPage == null)
        this._actionsTabPage = new ActionsTabPage(this.instGuid);
      return this._actionsTabPage;
    }
  }

  public ConfigurationTabPage ConfigurationTabPage
  {
    get
    {
      if (this._configurationTabPage == null)
        this._configurationTabPage = new ConfigurationTabPage(this.instGuid);
      return this._configurationTabPage;
    }
  }

  public AttrGroupsListTabPage AttrGroupsListTabPage
  {
    get
    {
      if (this._attrGroupsListTabPage == null)
        this._attrGroupsListTabPage = new AttrGroupsListTabPage(this.instGuid);
      return this._attrGroupsListTabPage;
    }
  }

  public ImbaseTablesTabPage AttrImbaseTablesListTabPage
  {
    get
    {
      if (this._attrImbaseTablesListTabPage == null)
        this._attrImbaseTablesListTabPage = new ImbaseTablesTabPage(this.instGuid);
      return this._attrImbaseTablesListTabPage;
    }
  }

  public ParentObjectTypeTabPage ParentTypeTabPage
  {
    get
    {
      if (this.parentTypeTabPage == null)
        this.parentTypeTabPage = new ParentObjectTypeTabPage(this.instGuid);
      return this.parentTypeTabPage;
    }
  }
}
