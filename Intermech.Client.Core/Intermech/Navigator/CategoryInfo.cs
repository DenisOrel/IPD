
// Type: Intermech.Navigator.CategoryInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;


namespace Intermech.Navigator;

internal class CategoryInfo
{
  private int _categoryID;
  private Type _defaultNodeType;
  private Hashtable _nodeTypes;
  private ArrayList _viewProviderList;
  private Hashtable _viewProviderTable;
  private ArrayList _commandProviderList;
  private Hashtable _commandProviderTable;
  private ICategoryInheritance _inheritance;

  public CategoryInfo(int categoryID)
  {
    this._categoryID = categoryID;
    this._defaultNodeType = (Type) null;
    this._nodeTypes = new Hashtable();
    this._viewProviderList = new ArrayList();
    this._viewProviderTable = new Hashtable();
    this._commandProviderList = new ArrayList();
    this._commandProviderTable = new Hashtable();
    this._inheritance = (ICategoryInheritance) null;
  }

  public void AddNodeType(int typeID, Type nodeType)
  {
    this._nodeTypes[(object) typeID] = (object) nodeType;
  }

  public void AddViewsProvider(IViewsProvider provider)
  {
    this._viewProviderList.Add((object) provider);
  }

  public void AddViewsProvider(int typeID, IViewsProvider provider)
  {
    if (!this._viewProviderTable.ContainsKey((object) typeID))
      this._viewProviderTable[(object) typeID] = (object) new ArrayList();
    ((ArrayList) this._viewProviderTable[(object) typeID]).Add((object) provider);
  }

  public void AddCommandsProvider(ICommandsProvider provider)
  {
    this._commandProviderList.Add((object) provider);
  }

  public void RemoveCommandsProvider(ICommandsProvider provider)
  {
    this._commandProviderList.Remove((object) provider);
  }

  public void AddCommandsProvider(int typeID, ICommandsProvider provider)
  {
    if (!this._commandProviderTable.ContainsKey((object) typeID))
      this._commandProviderTable[(object) typeID] = (object) new ArrayList();
    ((ArrayList) this._commandProviderTable[(object) typeID]).Add((object) provider);
  }

  public void RemoveCommandsProvider(int typeID, ICommandsProvider provider)
  {
    if (!this._commandProviderTable.ContainsKey((object) typeID))
      return;
    ((ArrayList) this._commandProviderTable[(object) typeID]).Remove((object) provider);
  }

  public Type GetNodeType(int typeID)
  {
    Type nodeType = (Type) this._nodeTypes[(object) typeID];
    if (nodeType == (Type) null && this._inheritance != null)
    {
      foreach (int key in MetaDataHelper.GetObjectTypeParentsID(typeID).ToArray())
      {
        nodeType = (Type) this._nodeTypes[(object) key];
        if (nodeType != (Type) null)
          break;
      }
    }
    if (nodeType == (Type) null)
      nodeType = this._defaultNodeType;
    return nodeType;
  }

  public ArrayList GetViewProviders(int typeID)
  {
    ArrayList viewProviders = new ArrayList();
    if (this._viewProviderTable.ContainsKey((object) typeID))
      viewProviders.AddRange(this._viewProviderTable[(object) typeID] as ICollection);
    if (this._inheritance != null)
    {
      int[] array = MetaDataHelper.GetObjectTypeParentsID(typeID).ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (this._viewProviderTable.ContainsKey((object) array[index]))
          viewProviders.AddRange(this._viewProviderTable[(object) array[index]] as ICollection);
      }
    }
    viewProviders.AddRange((ICollection) this._viewProviderList);
    return viewProviders;
  }

  public ArrayList GetViewsProviders(int typeID)
  {
    ArrayList viewsProviders = new ArrayList();
    if (this._viewProviderTable.ContainsKey((object) typeID))
      viewsProviders.AddRange(this._viewProviderTable[(object) typeID] as ICollection);
    if (this._inheritance != null)
    {
      int[] array = MetaDataHelper.GetObjectTypeParentsID(typeID).ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (this._viewProviderTable.ContainsKey((object) array[index]))
          viewsProviders.AddRange(this._viewProviderTable[(object) array[index]] as ICollection);
      }
    }
    return viewsProviders;
  }

  public ArrayList GetCommandProviders(int typeID)
  {
    ArrayList commandProviders = new ArrayList();
    if (this._commandProviderTable.ContainsKey((object) typeID))
      commandProviders.AddRange(this._commandProviderTable[(object) typeID] as ICollection);
    if (this._inheritance != null)
    {
      int[] array = MetaDataHelper.GetObjectTypeParentsID(typeID).ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (this._commandProviderTable.ContainsKey((object) array[index]))
          commandProviders.AddRange(this._commandProviderTable[(object) array[index]] as ICollection);
      }
    }
    commandProviders.AddRange((ICollection) this._commandProviderList);
    return commandProviders;
  }

  public ArrayList GetCommandsProviders(int typeID)
  {
    ArrayList commandsProviders = new ArrayList();
    if (this._commandProviderTable.ContainsKey((object) typeID))
      commandsProviders.AddRange(this._commandProviderTable[(object) typeID] as ICollection);
    if (this._inheritance != null)
    {
      int[] array = MetaDataHelper.GetObjectTypeParentsID(typeID).ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (this._commandProviderTable.ContainsKey((object) array[index]))
          commandsProviders.AddRange(this._commandProviderTable[(object) array[index]] as ICollection);
      }
    }
    return commandsProviders;
  }

  public ArrayList GetViewsProviders()
  {
    ArrayList viewsProviders = new ArrayList();
    viewsProviders.AddRange((ICollection) this._viewProviderList);
    return viewsProviders;
  }

  public ArrayList GetCommandsProviders()
  {
    ArrayList commandsProviders = new ArrayList();
    commandsProviders.AddRange((ICollection) this._commandProviderList);
    return commandsProviders;
  }

  public Type DefaultNodeType
  {
    get => this._defaultNodeType;
    set => this._defaultNodeType = value;
  }

  public ICategoryInheritance Inheritance
  {
    get => this._inheritance;
    set => this._inheritance = value;
  }
}
