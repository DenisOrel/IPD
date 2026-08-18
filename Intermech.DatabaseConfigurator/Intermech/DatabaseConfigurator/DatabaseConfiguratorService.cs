// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DatabaseConfiguratorService
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Holders;
using Intermech.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class DatabaseConfiguratorService : IDatabaseConfiguratorService
{
  private IntList attributeGroupsByDefault = new IntList();
  private List<IAdditionalView> _views = new List<IAdditionalView>();

  public int AddAttribute(string caption, int[] attrGroup)
  {
    IntList intList = new IntList();
    if (attrGroup == null)
    {
      intList.AddRange((ICollection) this.attributeGroupsByDefault);
      if (new AttributesGroupSelector().Execute(intList) != DialogResult.OK)
        return 0;
      this.attributeGroupsByDefault.Clear();
      this.attributeGroupsByDefault.AddRange((ICollection) intList);
    }
    else
      intList.AddRange((ICollection) attrGroup);
    object[] objArray = (object[]) null;
    using (DatabaseConfiguratorServiceForm configuratorServiceForm = new DatabaseConfiguratorServiceForm())
    {
      configuratorServiceForm.Text = caption;
      objArray = configuratorServiceForm.ExecuteDialog(ConfiguratorAction.Add, 3, null, (object) intList);
    }
    return objArray == null || objArray.Length == 0 || objArray[0] == null ? 0 : (int) objArray[0];
  }

  public bool EditAttribute(string caption, int attributeId)
  {
    object[] objArray = (object[]) null;
    using (DatabaseConfiguratorServiceForm configuratorServiceForm = new DatabaseConfiguratorServiceForm())
    {
      configuratorServiceForm.Text = caption;
      objArray = configuratorServiceForm.ExecuteDialog(ConfiguratorAction.Edit, 3, (object) attributeId);
    }
    return objArray != null && objArray.Length != 0 && objArray[0] != null;
  }

  public int RegisterCategoryProps(int category, ICategoryProps iCategoryProps)
  {
    return CategoryPropsHolder.RegisterCategoryProps(category, iCategoryProps);
  }

  public void UnregisterCategoryProps(int categoryPropsId)
  {
    CategoryPropsHolder.UnregisterCategoryProps(categoryPropsId);
  }

  public void RegisterDocumentAdditionalView(IAdditionalView view) => this._views.Add(view);

  public IAdditionalView[] DocumentAdditionalViews
  {
    get => this._views.Count != 0 ? this._views.ToArray() : (IAdditionalView[]) null;
  }
}
