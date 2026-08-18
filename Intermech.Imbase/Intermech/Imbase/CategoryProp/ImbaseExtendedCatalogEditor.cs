// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.CategoryProp.ImbaseExtendedCatalogEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.CategoryProp;

public class ImbaseExtendedCatalogEditor : UITypeEditor
{
  private ImbaseCatalogSelectMode _selectMode;
  private int[] _imObjTypeIDs;
  private IList<long> _imCatalogIDs;

  private bool SelectImbaseCatalog(out List<long> catalogIDs)
  {
    catalogIDs = new List<long>();
    IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    long[] collection = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase.Client_150"), string.Empty, rootDescriptor, SelectionOptions.Default | SelectionOptions.HideTree);
    if (collection == null || collection.Length == 0)
      return false;
    catalogIDs.AddRange((IEnumerable<long>) collection);
    return true;
  }

  private bool SelectImbaseCatalog4Type(int[] objTypeIDs, out List<long> catalogIDs)
  {
    catalogIDs = new List<long>();
    if (objTypeIDs == null || objTypeIDs.Length == 0)
      return false;
    IList<long> imCatalogs = this.GetImCatalogs(objTypeIDs);
    if (imCatalogs.Count == 0)
    {
      string text;
      if (objTypeIDs.Length == 1)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeIDs[0]);
        text = string.Format(LocalizationHolder.rm.GetString("Imbase.Client_149"), (object) objectType.ObjectTypeName, (object) objectType.ObjectTypeID);
      }
      else
        text = LocalizationHolder.rm.GetString("Imbase.Client_151");
      int num = (int) MessageBox.Show(text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return false;
    }
    IDescriptor rootDescriptor = (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, Intermech.Imbase.Consts.ImbaseCatalogTypeID, string.Empty, (IList) imCatalogs.ToList<long>());
    long[] collection = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase.Client_150"), string.Empty, rootDescriptor, SelectionOptions.Default | SelectionOptions.HideTree);
    if (collection == null || collection.Length == 0)
      return false;
    catalogIDs.AddRange((IEnumerable<long>) collection);
    return true;
  }

  public ImbaseExtendedCatalogEditor()
    : this(ImbaseCatalogSelectMode.imcmNone)
  {
  }

  public ImbaseExtendedCatalogEditor(ImbaseCatalogSelectMode selectMode)
  {
    this._selectMode = selectMode;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    List<long> aList = new List<long>();
    if (value is List<long> longList)
      aList = longList;
    int[] objTypeIDs = (int[]) null;
    if (context?.Instance is AttributeFolder instance1 && instance1.PropDescriptorCollection[8].GetValue((object) instance1) is ObjectTypeMultiPropertyClass multiPropertyClass)
      objTypeIDs = multiPropertyClass.ObjectTypeList.ToArray();
    if (context?.Instance is Attr4ObjTypeClass instance2)
    {
      ArrayList typeListByAttrId = ObjectEditor.GetObjTypeListByAttrId(instance2.Attribute4ObjectTypeProperties.AttributeID);
      if (typeListByAttrId != null)
      {
        List<int> intList = new List<int>(typeListByAttrId.Count);
        foreach (object obj in typeListByAttrId)
        {
          int result;
          if (obj != null && int.TryParse(obj.ToString(), out result))
            intList.Add(result);
        }
        objTypeIDs = intList.ToArray();
      }
    }
    if (objTypeIDs == null || objTypeIDs.Length == 0)
      return value;
    List<long> catalogIDs;
    if (this._selectMode == ImbaseCatalogSelectMode.imcmSelectFolder || objTypeIDs.Length == 1 && objTypeIDs[0] == -1)
    {
      if (!this.SelectImbaseCatalog(out catalogIDs))
        return value;
    }
    else if (!this.SelectImbaseCatalog4Type(objTypeIDs, out catalogIDs))
      return value;
    return GenericListHelper.Compare<long>((IList<long>) aList, (IList<long>) catalogIDs) == 0 ? value : (object) catalogIDs;
  }

  public IList<long> GetImCatalogs(int[] objTypeIDs)
  {
    if (GenericHelper.ArrayEquals<int>(this._imObjTypeIDs, objTypeIDs) && this._imCatalogIDs != null)
      return this._imCatalogIDs;
    this._imObjTypeIDs = objTypeIDs;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._imCatalogIDs = (IList<long>) ImbaseUtils.GetCatalogIDForObjType(objTypeIDs, sessionKeeper.Session);
    return this._imCatalogIDs;
  }

  public ImbaseCatalogSelectMode SelectMode
  {
    [DebuggerStepThrough] get => this._selectMode;
    [DebuggerStepThrough] set => this._selectMode = value;
  }
}
