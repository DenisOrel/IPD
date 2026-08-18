
// Type: Intermech.Client.Core.CompositionView.CVButtonBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Базовая кнопка для ToolBar на панели "Состав"</summary>
public class CVButtonBase
{
  /// <summary>хинт для кнопки</summary>
  protected string _hint = string.Empty;
  /// <summary>имя иконки из NamedImageList</summary>
  protected string _imName = string.Empty;
  /// <summary>сам NamedImageList</summary>
  protected INamedImageList _imList;
  /// <summary>сама Картинка</summary>
  protected Image _image;
  /// <summary>Данные кнопки, если не удалось подгрузить кнопку</summary>
  protected XmlNode _xmlNodeButton;

  /// <summary>Базовый конструктор</summary>
  public CVButtonBase()
  {
    this._imList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
  }

  /// <summary>Хинт</summary>
  [CustomDisplayName("Attribute.Client.Core_16")]
  [CustomCategory("Attribute.Client.Core_17")]
  public string Hint
  {
    get => this._hint;
    set => this._hint = value;
  }

  /// <summary>Картинка</summary>
  [CustomDisplayName("Attribute.Client.Core_18")]
  [CustomCategory("Attribute.Client.Core_19")]
  [TypeConverter(typeof (CVButtonBase.iImageConverter))]
  public Image Image
  {
    get => this._image;
    set
    {
      if (value == null)
        this._image = (Image) null;
      else if (value.Size.Height != CVButtonBase.Consts.IconSize && value.Size.Width != CVButtonBase.Consts.IconSize)
        this._image = value.GetThumbnailImage(CVButtonBase.Consts.IconSize, CVButtonBase.Consts.IconSize, (Image.GetThumbnailImageAbort) null, IntPtr.Zero);
      else
        this._image = value;
    }
  }

  /// <summary>Имя картинки</summary>
  [Browsable(false)]
  public string ImageName
  {
    get => this._imName;
    set
    {
      this._imName = value;
      if (this._imList == null)
        return;
      int index = this._imList.ImageIndex(this._imName);
      if (index == -1)
        return;
      this._image = this._imList.ImageList.Images[index];
    }
  }

  /// <summary>
  /// Возвращает класс для изменения параметров кнопки
  /// (для PropertyGrid'а)
  /// </summary>
  [Browsable(false)]
  public virtual object Params => (object) new ClassWrapperForPropertyGrid((object) this);

  /// <summary>Данные кнопки</summary>
  [Browsable(false)]
  public virtual XmlNode Node
  {
    get => this._xmlNodeButton;
    set => this._xmlNodeButton = value;
  }

  /// <summary>Метод выбора настроек кнопки</summary>
  public virtual bool Select() => false;

  /// <summary>Применить параметры кнопки button к текущей кнопке</summary>
  /// <param name="button">кнопка из которой беруться параметры</param>
  public virtual void ApplyParams(CVButtonBase button)
  {
    if (button == null)
      return;
    this._hint = button._hint;
    this._imName = button._imName;
    this._image = button._image;
    this._xmlNodeButton = button._xmlNodeButton;
  }

  /// <summary>Нажатие кнопки на ToolBar, для построения дерева</summary>
  /// <returns>Дескриптор для дерева</returns>
  public virtual IDescriptor BuildTree() => (IDescriptor) null;

  /// <summary>Проверка на доступность действия</summary>
  /// <param name="sourceView">Исходное дерево</param>
  /// <param name="compView">Дерево состава</param>
  /// <param name="compManager">Вьюшки состава</param>
  /// <returns>enabled/disabled кнопок</returns>
  public virtual CVButtonEnabled Check(CVLocalButton.CVButtonArgs args) => CVButtonEnabled.Empty;

  /// <summary>Выполнение действия</summary>
  /// <param name="method">действие</param>
  /// <param name="sourceView">Исходное дерево</param>
  /// <param name="compView">Дерево состава</param>
  /// <param name="compManager">Вьюшки состава</param>
  public virtual void Click(CVLocalButton.CVButtonClickArgs args)
  {
  }

  /// <summary>Проверка на доступность действия</summary>
  /// <param name="sourceView">Исходное дерево</param>
  /// <param name="compView">Дерево состава</param>
  /// <param name="compManager">Вьюшки состава</param>
  /// <returns>enabled/disabled кнопок</returns>
  [Obsolete]
  public virtual CVButtonEnabled Check(
    NavigatorTreeView sourceView,
    NavigatorTreeView compView,
    IViewsManager compManager)
  {
    return this.Check(new CVLocalButton.CVButtonArgs(sourceView, this.GetSelectedItems(sourceView, compManager)));
  }

  /// <summary>Выполнение действия</summary>
  /// <param name="method">действие</param>
  /// <param name="sourceView">Исходное дерево</param>
  /// <param name="compView">Дерево состава</param>
  /// <param name="compManager">Вьюшки состава</param>
  [Obsolete]
  public virtual void Click(
    CVButtonMethod method,
    NavigatorTreeView sourceView,
    NavigatorTreeView compView,
    IViewsManager compManager)
  {
    this.Click(new CVLocalButton.CVButtonClickArgs(method, sourceView, this.GetSelectedItems(sourceView, compManager)));
  }

  /// <summary>Get selected items</summary>
  /// <param name="treeView"></param>
  /// <param name="viewManager"></param>
  /// <returns></returns>
  public virtual List<IDBTypedObjectID> GetSelectedItems(
    NavigatorTreeView treeView,
    IViewsManager viewManager)
  {
    return CompositionViewHelper.GetSelectedItems(treeView, viewManager);
  }

  /// <summary>
  /// Приведение типов selectedItems к требуемым,
  /// (если тип создаваемого объекта отличается от текущего)
  /// </summary>
  /// <param name="typedObjectList"></param>
  /// <returns></returns>
  public virtual List<IDBTypedObjectID> DoConvertTypes(List<IDBTypedObjectID> typedObjectList)
  {
    return typedObjectList;
  }

  /// <summary>Подготовливаемся к созданию объектов / связи</summary>
  /// <remarks>Метод необходим для инициализации кешей</remarks>
  /// <param name="ownerObjId"></param>
  /// <param name="objectIDs"></param>
  /// <param name="session"></param>
  public virtual void DoBeforeAllCreation(
    IDBTypedObjectID ownerObjId,
    List<IDBTypedObjectID> objectIDs,
    IUserSession session)
  {
  }

  /// <summary>Вызывается после всех созданий</summary>
  /// <remarks>Метод необходим для очистки временных кешей</remarks>
  /// <param name="session"></param>
  public virtual void DoAfterAllCreation(IUserSession session)
  {
  }

  /// <summary>
  /// Получение/создание нового объекта по заданным параметрам
  /// </summary>
  /// <param name="ownerObjId"></param>
  /// <param name="objectId"></param>
  /// <param name="relationHash"></param>
  /// <param name="session"></param>
  /// <param name="throwException"></param>
  /// <param name="errorString"></param>
  /// <returns></returns>
  public virtual IDBObject DoCreateObject(
    IDBTypedObjectID ownerObjId,
    IDBTypedObjectID objectId,
    Dictionary<int, List<cvRelationInfo>> relationHash,
    IUserSession session,
    bool throwException,
    out string errorString)
  {
    errorString = "";
    return session.GetObject(objectId.ObjectID, false);
  }

  /// <summary>Завершение создания объекта typedObject</summary>
  /// <param name="typedObject"></param>
  /// <param name="session"></param>
  public virtual void DoCommitObject(IDBObject typedObject, IUserSession session)
  {
  }

  /// <summary>Создание связи согласно параметрам</summary>
  /// <param name="relTypeId">Тип создваемой связи</param>
  /// <param name="newRelPros">Параметры для создвния связи</param>
  /// <param name="projTypedObjId">Родительский объект</param>
  /// <param name="partTypedObjId">Дочерний объект</param>
  /// <param name="session">Сессия</param>
  /// <returns></returns>
  /// <remarks>Achtung! Значение sorting у IDBRelationID </remarks>
  public virtual IDBRelationID DoCreateRelation(
    int relTypeId,
    NewRelationProperties newRelPros,
    IDBTypedObjectID projTypedObjId,
    IDBTypedObjectID partTypedObjId,
    IUserSession session)
  {
    if (relTypeId == -1 || session == null)
      return (IDBRelationID) null;
    IDBRelationCollection relationCollection = session.GetRelationCollection(relTypeId);
    if (relationCollection == null)
      return (IDBRelationID) null;
    IDBRelationID relation = (IDBRelationID) null;
    IDBRelation dbRelation = relationCollection.Create(newRelPros);
    if (dbRelation != null)
      relation = (IDBRelationID) new DBRelationID(dbRelation.RelationID, dbRelation.PartID, dbRelation.RelationType, 0L, dbRelation.GUID, dbRelation.ProjID);
    return relation;
  }

  /// <summary>
  /// Сохранение данных о кнопке в xml
  /// (обязательно нужно перекрывать и имя нода нужно делать typeof(...).FullName)
  /// </summary>
  /// <param name="xml">нод для сохранения</param>
  public virtual void Save(XmlNode xml) => this.SaveInternal(xml);

  /// <summary>Сохранение базовых данных</summary>
  /// <param name="xmlNodeType">нод для сохранения</param>
  /// <returns>нод для продолжения сохранения</returns>
  protected XmlNode SaveInternal(XmlNode xmlNodeType)
  {
    XmlDocument ownerDocument = xmlNodeType.OwnerDocument;
    XmlNode element = (XmlNode) ownerDocument.CreateElement(this.GetType().FullName);
    XmlAttribute attribute1 = ownerDocument.CreateAttribute("Assembly");
    attribute1.Value = this.GetType().Assembly.GetName().Name;
    element.Attributes.Append(attribute1);
    if (this._image != null)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        this._image.Save((Stream) memoryStream, ImageFormat.Bmp);
        XmlAttribute attribute2 = ownerDocument.CreateAttribute("Image");
        attribute2.Value = Convert.ToBase64String(memoryStream.ToArray());
        element.Attributes.Append(attribute2);
      }
    }
    XmlAttribute attribute3 = ownerDocument.CreateAttribute("Hint");
    attribute3.Value = this._hint;
    element.Attributes.Append(attribute3);
    xmlNodeType.AppendChild(element);
    return element;
  }

  /// <summary>Метод для загрузки кнопки из xml</summary>
  /// <param name="xmlNodeButton">нод для загрузки</param>
  /// <returns></returns>
  public static CVButtonBase Load(XmlNode xmlNodeButton)
  {
    Type type = (Type) null;
    XmlAttribute attribute = xmlNodeButton.Attributes["Assembly"];
    if (attribute != null)
    {
      try
      {
        type = Assembly.Load(attribute.Value).GetType(xmlNodeButton.Name);
      }
      catch (Exception ex)
      {
        switch (ex)
        {
          case FileNotFoundException _:
          case BadImageFormatException _:
          case FileLoadException _:
            break;
          default:
            throw;
        }
      }
    }
    else
      type = Type.GetType(xmlNodeButton.Name);
    if (type != (Type) null)
    {
      if (!(type.InvokeMember(nameof (Load), BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, (object) null, new object[1]
      {
        (object) xmlNodeButton
      }) is CVButtonBase cvButtonBase))
        return (CVButtonBase) null;
      cvButtonBase.LoadInternal(xmlNodeButton);
      return cvButtonBase;
    }
    CVButtonBase cvButtonBase1 = new CVButtonBase();
    cvButtonBase1.LoadInternal(xmlNodeButton);
    cvButtonBase1.Node = xmlNodeButton;
    return cvButtonBase1;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlNodeButton"></param>
  private void LoadInternal(XmlNode xmlNodeButton)
  {
    XmlAttribute attribute1 = xmlNodeButton.Attributes["ImageName"];
    XmlAttribute attribute2 = xmlNodeButton.Attributes["Image"];
    XmlAttribute attribute3 = xmlNodeButton.Attributes["Hint"];
    if (attribute1 != null)
      this.ImageName = attribute1.Value;
    if (attribute2 != null)
      this._image = (Image) new Bitmap((Stream) new MemoryStream(Convert.FromBase64String(attribute2.Value)));
    if (attribute3 == null)
      return;
    this.Hint = attribute3.Value;
  }

  /// <summary>Хинт</summary>
  /// <returns></returns>
  public override string ToString() => this._hint;

  /// <summary>Клонирование объекта</summary>
  /// <returns></returns>
  public virtual CVButtonBase Clone()
  {
    CVButtonBase cvButtonBase = new CVButtonBase();
    cvButtonBase.ApplyParams(this);
    return cvButtonBase;
  }

  /// <summary>Получение информации по самому объекту</summary>
  /// <param name="dbObject"></param>
  /// <returns></returns>
  public static DBTypedObjectID GetDBTypedObjectID(IDBObject dbObject)
  {
    return dbObject == null ? (DBTypedObjectID) null : new DBTypedObjectID(dbObject.ObjectType, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, Convert.ToInt64(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
  }

  /// <summary>Константы</summary>
  public static class Consts
  {
    /// <summary>Размер иконки для кнопки</summary>
    public static int IconSize = 16 /*0x10*/;
  }

  internal class iImageConverter : ImageConverter
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="culture"></param>
    /// <param name="value"></param>
    /// <param name="destinationType"></param>
    /// <returns></returns>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      return destinationType == typeof (string) ? (object) LocalizationHolder.rm.GetString("Client.Core_17") : base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => false;
  }
}
