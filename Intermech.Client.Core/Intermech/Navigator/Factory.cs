
// Type: Intermech.Navigator.Factory
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search.GlobalNodes;
using System;
using System.Collections;
using System.Linq;
using System.Text;


namespace Intermech.Navigator;

public class Factory : IFactory, INodesFactory
{
  private Hashtable _categories;
  private ArrayList _viewProviders;
  private ArrayList _commandProviders;
  /// <summary>Текущий шаблон контекстных меню</summary>
  private MenuTemplate _defaultContextMenuTemplate = new MenuTemplate();
  private MenuTemplate _configuredContextMenuTemplate = new MenuTemplate();

  public Factory()
  {
    this._categories = new Hashtable(32 /*0x20*/);
    this._viewProviders = new ArrayList(4);
    this._commandProviders = new ArrayList(4);
    this._defaultContextMenuTemplate.OnChanged += new EventHandler(this.DefaultContextMenuTemplate_Changed);
  }

  public void AddNodeType(int categoryID, Type nodeType)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4543()));
    if (nodeType == (Type) null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4544()));
    lock (this._categories)
    {
      if (!this._categories.ContainsKey((object) categoryID))
        this._categories[(object) categoryID] = (object) new CategoryInfo(categoryID);
      ((CategoryInfo) this._categories[(object) categoryID]).DefaultNodeType = nodeType;
    }
  }

  public void AddNodeType(int categoryID, Type nodeType, ICategoryInheritance inheritance)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4545()));
    if (nodeType == (Type) null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4546()));
    if (inheritance == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4547()));
    lock (this._categories)
    {
      if (!this._categories.ContainsKey((object) categoryID))
        this._categories[(object) categoryID] = (object) new CategoryInfo(categoryID);
      CategoryInfo category = (CategoryInfo) this._categories[(object) categoryID];
      category.DefaultNodeType = nodeType;
      category.Inheritance = inheritance;
    }
  }

  public void AddNodeType(int categoryID, int typeID, Type nodeType)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4548()));
    lock (this._categories)
    {
      if (!this._categories.ContainsKey((object) categoryID))
        this._categories[(object) categoryID] = (object) new CategoryInfo(categoryID);
      ((CategoryInfo) this._categories[(object) categoryID]).AddNodeType(typeID, nodeType);
    }
  }

  public void AddViewsProvider(IViewsProvider provider)
  {
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4549()));
    lock (this._viewProviders)
      this._viewProviders.Add((object) provider);
  }

  public void AddViewsProvider(int categoryID, IViewsProvider provider)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4550()));
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4551()));
    lock (this._categories)
    {
      if (!this._categories.ContainsKey((object) categoryID))
        this._categories[(object) categoryID] = (object) new CategoryInfo(categoryID);
      ((CategoryInfo) this._categories[(object) categoryID]).AddViewsProvider(provider);
    }
  }

  public void AddViewsProvider(int categoryID, int typeID, IViewsProvider provider)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4552()));
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4553()));
    lock (this._categories)
    {
      if (!this._categories.ContainsKey((object) categoryID))
        this._categories[(object) categoryID] = (object) new CategoryInfo(categoryID);
      ((CategoryInfo) this._categories[(object) categoryID]).AddViewsProvider(typeID, provider);
    }
  }

  public void AddCommandsProvider(ICommandsProvider provider)
  {
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4554()));
    lock (this._commandProviders)
      this._commandProviders.Add((object) provider);
  }

  public void AddCommandsProvider(int categoryID, ICommandsProvider provider)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString("Client.Core_768"));
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4555()));
    lock (this._categories)
    {
      if (!this._categories.ContainsKey((object) categoryID))
        this._categories[(object) categoryID] = (object) new CategoryInfo(categoryID);
      ((CategoryInfo) this._categories[(object) categoryID]).AddCommandsProvider(provider);
    }
  }

  public void AddCommandsProvider(int categoryID, int typeID, ICommandsProvider provider)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString("Client.Core_768"));
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4556()));
    lock (this._categories)
    {
      if (!this._categories.ContainsKey((object) categoryID))
        this._categories[(object) categoryID] = (object) new CategoryInfo(categoryID);
      ((CategoryInfo) this._categories[(object) categoryID]).AddCommandsProvider(typeID, provider);
    }
  }

  /// <summary>
  /// Удаляет провайдер команд контекстного меню, который
  /// использовался для элементов навигации любой категории и типа.
  /// </summary>
  /// <param name="provider">Провайдер команд</param>
  public void RemoveCommandsProvider(ICommandsProvider provider)
  {
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString("Client.Core_772"));
    lock (this._commandProviders)
      this._commandProviders.Remove((object) provider);
  }

  /// <summary>
  /// Удаляет провайдер команд контекстного меню, который
  /// использовался для элементов навигации любого типа из указанной
  /// категории.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="provider">Провайдер команд</param>
  public void RemoveCommandsProvider(int categoryID, ICommandsProvider provider)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4557()));
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4558()));
    lock (this._categories)
      ((CategoryInfo) this._categories[(object) categoryID])?.RemoveCommandsProvider(provider);
  }

  /// <summary>
  /// Удаляет провайдер команд контекстного меню, который
  /// использовался для элементов навигации указанной категории и типа.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории</param>
  /// <param name="typeID">Идентификатор типа</param>
  /// <param name="provider">Провайдер команд</param>
  public void RemoveCommandsProvider(int categoryID, int typeID, ICommandsProvider provider)
  {
    if (((IGuidMapper) ServicesManager.GetService(typeof (IGuidMapper)))[categoryID] == Guid.Empty)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4559()));
    if (provider == null)
      throw new NavigatorFactoryException(LocalizationHolder.rm.GetString(sc_4542.ssp_imclient_4560()));
    lock (this._categories)
      ((CategoryInfo) this._categories[(object) categoryID])?.RemoveCommandsProvider(typeID, provider);
  }

  /// <summary>
  /// Регистрирует элемент из пространства навигации, которых должен быть
  /// включен в корень основной иерархии навигатора "Информационное пространство".
  /// </summary>
  /// <param name="descriptorGuid"></param>
  /// <param name="descriptor">Дескриптор, описывающий элемент</param>
  /// <param name="orderID">Положение дескриптора в списке дескрипторов.</param>
  public void AddGlobalNode(Guid descriptorGuid, IDescriptor descriptor, int orderID)
  {
    ((IGlobalNodeRegistry) ServicesManager.GetService(typeof (IGlobalNodeRegistry))).RegisterGlobalNode(descriptorGuid, descriptor, orderID);
  }

  /// <summary>
  /// Возвращает элемент из пространства навигации указанной категории и типа.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элемента</param>
  /// <param name="typeID">Идентификатор типа элемента</param>
  /// <returns>Ссылка на основной интерфейс элемента навигации.</returns>
  public INode GetNode(int categoryID, int typeID)
  {
    lock (this._categories)
    {
      if (this._categories.ContainsKey((object) categoryID))
      {
        Type nodeType = ((CategoryInfo) this._categories[(object) categoryID]).GetNodeType(typeID);
        if (nodeType != (Type) null)
        {
          try
          {
            return Activator.CreateInstance(nodeType) as INode;
          }
          catch (MissingMethodException ex)
          {
            throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_773"), (object) nodeType.FullName), (Exception) ex);
          }
        }
      }
    }
    return (INode) null;
  }

  /// <summary>
  /// Возвращает элемент из пространства навигации указанной категории и типа.
  /// </summary>
  /// <param name="nodeID">Унифицированный идентификатор элемента.</param>
  /// <param name="args">Массив параметров, которые будут переданы конструктору элемента.</param>
  /// <returns>Ссылка на основной интерфейс элемента навигации.</returns>
  public INode GetNode(INodeID nodeID, params object[] args)
  {
    lock (this._categories)
    {
      if (this._categories.ContainsKey((object) nodeID.CategoryID))
      {
        Type nodeType = this.GetNodeType(nodeID);
        if (nodeType != (Type) null)
        {
          try
          {
            return Activator.CreateInstance(nodeType, args) as INode;
          }
          catch (MissingMethodException ex)
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(args[0].GetType().Name);
            for (int index = 1; index < args.Length; ++index)
            {
              stringBuilder.Append(", ");
              if (args[index] != null)
                stringBuilder.Append(args[index].GetType().Name);
              else
                stringBuilder.Append("unknown type");
            }
            string str = stringBuilder.ToString();
            throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_774"), (object) nodeType.FullName, (object) str), (Exception) ex);
          }
        }
      }
    }
    return (INode) null;
  }

  internal Type GetNodeType(INodeID nodeID)
  {
    lock (this._categories)
      return !this._categories.ContainsKey((object) nodeID.CategoryID) ? (Type) null : ((CategoryInfo) this._categories[(object) nodeID.CategoryID]).GetNodeType(nodeID.TypeID);
  }

  /// <summary>
  /// Возвращает массив провайдеров закладок для элемента навигации указанной
  /// категории и типа. Если ни одного зарегистрированного провайдера
  /// найти не удалось, то метод возвращает null.
  /// </summary>
  /// <param name="categoryID">Идентификатор категории элемента</param>
  /// <param name="typeID">Идентификатор типа элемента</param>
  /// <returns>Массив провайдеров</returns>
  public IViewsProvider[] GetViewsProviders(int categoryID, int typeID)
  {
    ArrayList arrayList1 = new ArrayList();
    lock (this._categories)
    {
      if (this._categories.ContainsKey((object) categoryID))
      {
        CategoryInfo category = (CategoryInfo) this._categories[(object) categoryID];
        arrayList1.AddRange((ICollection) category.GetViewsProviders(typeID));
      }
    }
    ArrayList arrayList2 = new ArrayList();
    for (int index = 0; index < arrayList1.Count; ++index)
    {
      if (arrayList1[index] is IViewsProvider)
        arrayList2.Add(arrayList1[index]);
    }
    return arrayList2.Count != 0 ? (IViewsProvider[]) arrayList2.ToArray(typeof (IViewsProvider)) : (IViewsProvider[]) null;
  }

  public IViewsProvider[] GetViewsProviders(int categoryID)
  {
    ArrayList arrayList1 = new ArrayList();
    lock (this._categories)
    {
      if (this._categories.ContainsKey((object) categoryID))
      {
        CategoryInfo category = (CategoryInfo) this._categories[(object) categoryID];
        arrayList1.AddRange((ICollection) category.GetViewsProviders());
      }
    }
    ArrayList arrayList2 = new ArrayList();
    for (int index = 0; index < arrayList1.Count; ++index)
    {
      if (arrayList1[index] is IViewsProvider)
        arrayList2.Add(arrayList1[index]);
    }
    return arrayList2.Count != 0 ? (IViewsProvider[]) arrayList2.ToArray(typeof (IViewsProvider)) : (IViewsProvider[]) null;
  }

  public IViewsProvider[] GetViewsProviders()
  {
    ArrayList arrayList1 = new ArrayList();
    lock (this._viewProviders)
      arrayList1.AddRange((ICollection) this._viewProviders);
    ArrayList arrayList2 = new ArrayList();
    for (int index = 0; index < arrayList1.Count; ++index)
    {
      if (arrayList1[index] is IViewsProvider)
        arrayList2.Add(arrayList1[index]);
    }
    return arrayList2.Count != 0 ? (IViewsProvider[]) arrayList2.ToArray(typeof (IViewsProvider)) : (IViewsProvider[]) null;
  }

  public ICommandsProvider[] GetCommandsProviders(int categoryID, int typeID)
  {
    ArrayList arrayList = new ArrayList();
    lock (this._categories)
    {
      if (this._categories.ContainsKey((object) categoryID))
      {
        CategoryInfo category = (CategoryInfo) this._categories[(object) categoryID];
        arrayList.AddRange((ICollection) category.GetCommandsProviders(typeID));
      }
    }
    return arrayList.Count != 0 ? (ICommandsProvider[]) arrayList.ToArray(typeof (ICommandsProvider)) : (ICommandsProvider[]) null;
  }

  public ICommandsProvider[] GetCommandsProviders(int categoryID)
  {
    ArrayList arrayList = new ArrayList();
    lock (this._categories)
    {
      if (this._categories.ContainsKey((object) categoryID))
      {
        CategoryInfo category = (CategoryInfo) this._categories[(object) categoryID];
        arrayList.AddRange((ICollection) category.GetCommandsProviders());
      }
    }
    return arrayList.Count != 0 ? (ICommandsProvider[]) arrayList.ToArray(typeof (ICommandsProvider)) : (ICommandsProvider[]) null;
  }

  public ICommandsProvider[] GetCommandsProviders()
  {
    ArrayList arrayList = new ArrayList();
    lock (this._commandProviders)
      arrayList.AddRange((ICollection) this._commandProviders);
    return arrayList.Count != 0 ? (ICommandsProvider[]) arrayList.ToArray(typeof (ICommandsProvider)) : (ICommandsProvider[]) null;
  }

  /// <summary>Текущий шаблон контекстного меню</summary>
  public MenuTemplate ContextMenuTemplate => this._defaultContextMenuTemplate;

  /// <summary>
  /// Обработчик события, уведомляющего об изменении шаблона
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void DefaultContextMenuTemplate_Changed(object sender, EventArgs e)
  {
    this.SynchronizeMenuTemplateNodes(this._defaultContextMenuTemplate.Nodes, this._configuredContextMenuTemplate.Nodes);
  }

  private void SynchronizeMenuTemplateNodes(
    MenuTemplateNodeCollection defaultNodes,
    MenuTemplateNodeCollection configuredNodes)
  {
    foreach (MenuTemplateNode defaultNode1 in defaultNodes)
    {
      MenuTemplateNode defaultNode = defaultNode1;
      MenuTemplateNode configuredNode = configuredNodes.FirstOrDefault<MenuTemplateNode>((Func<MenuTemplateNode, bool>) (o => o.Name == defaultNode.Name));
      if (configuredNode != null)
        this.SynchronizeMenuTemplateNode(defaultNode, configuredNode);
      else
        configuredNodes.Add((MenuTemplateNode) defaultNode.Clone());
    }
    foreach (MenuTemplateNode menuTemplateNode in configuredNodes.ToArray<MenuTemplateNode>())
    {
      MenuTemplateNode configuredNode = menuTemplateNode;
      if (defaultNodes.FirstOrDefault<MenuTemplateNode>((Func<MenuTemplateNode, bool>) (o => o.Name == configuredNode.Name)) == null)
        configuredNodes.Remove(configuredNode);
    }
  }

  private void SynchronizeMenuTemplateNode(
    MenuTemplateNode defaultNode,
    MenuTemplateNode configuredNode)
  {
    configuredNode.Image = defaultNode.Image;
    configuredNode.ImageIndex = defaultNode.ImageIndex;
    configuredNode.ImageListSource = defaultNode.ImageListSource;
    configuredNode.Text = defaultNode.Text;
    this.SynchronizeMenuTemplateNodes(defaultNode.Nodes, configuredNode.Nodes);
  }

  /// <summary>Шаблон контекстного меню по умолчанию</summary>
  public MenuTemplate ContextMenuTemplateDefault => this._defaultContextMenuTemplate;

  public MenuTemplate ConfiguredContextMenuTemplate
  {
    get => this._configuredContextMenuTemplate;
    set => this._configuredContextMenuTemplate = value ?? new MenuTemplate();
  }

  /// <summary>
  /// Событие генерируется перед каждым построением контекстных меню. Позволяет
  /// выполнять изменение элементов шаблона контекстного меню перед тем, как на
  /// их основе будет сформировано контекстное меню.
  /// </summary>
  public event MenuTemplateNodeTransformEventHandler OnMenuTemplateNodeTransformEventHandler;

  /// <summary>
  /// Выполнить преобразование элемента шаблона контекстного меню, если есть обработчик
  /// </summary>
  /// <param name="node">Преобразуемый элемент шаблона контекстного меню</param>
  /// <param name="items"></param>
  /// <param name="services"></param>
  public void MenuTemplateNodeTransform(
    MenuTemplateNode node,
    ISelectedItems items,
    IServiceProvider services)
  {
    if (node == null || this.OnMenuTemplateNodeTransformEventHandler == null)
      return;
    this.OnMenuTemplateNodeTransformEventHandler((object) this, new MenuTemplateNodeTransformEventArgs(node, items, services));
  }
}
