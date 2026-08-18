// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.TableRefsView
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase.Views;

public class TableRefsView : ObjectsViewBase
{
  private static int _imageIndex = -1;

  public override string Caption
  {
    [DebuggerStepThrough] get => TableRefsObjectNode.NodeName;
  }

  public override int OrderID
  {
    [DebuggerStepThrough] get => 1;
  }

  protected override int StateStreamCategoryID => 1;

  public override string StateStreamPrefix => "TableReferencesView";

  public override int ImageIndex
  {
    get
    {
      if (TableRefsView._imageIndex >= 0)
        return TableRefsView._imageIndex;
      TableRefsView._imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("imgImbaseTablesRefType");
      return TableRefsView._imageIndex;
    }
  }
}
