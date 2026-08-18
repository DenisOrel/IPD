// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.VisSchemeParms
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Expert;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Pdm;

[Serializable]
public class VisSchemeParms
{
  /// <summary>ID схемы</summary>
  public long SchemeId;
  /// <summary>Название схемы</summary>
  public string Name = string.Empty;
  /// <summary>Условие фильтрации (выборка)</summary>
  public long SelectionId;
  /// <summary>Наименование выборки</summary>
  public string SelectionName = string.Empty;
  /// <summary>Режим использования допустимых замен</summary>
  public UseZamens useZamens;
  /// <summary>Режим работы со скрытыми объектами</summary>
  public HiddenContentsMode hiddenMode;
  /// <summary>
  /// Максимальное количество уровней (-1 = без ограничений)
  /// </summary>
  public int maxLevels = -1;
  /// <summary>Типы объектов</summary>
  public List<GlobalType> ObjectTypes = new List<GlobalType>();
  /// <summary>
  /// Типы объектов для которых требуется разворачивать состав (если не указано - разворачивать все типы)
  /// </summary>
  public List<GlobalType> TypesToExpand = new List<GlobalType>();
  /// <summary>
  /// Типы объектов для которых состав разворачивать не нужно
  /// </summary>
  public List<GlobalType> TypesToDisableExpand = new List<GlobalType>();
  /// <summary>Типы связей</summary>
  public List<GlobalType> RelationTypes = new List<GlobalType>();
  /// <summary>Типы объектов, для которых надо показывать Preview</summary>
  public List<GlobalType> PreviewTypes = new List<GlobalType>();
  /// <summary>Собираемые атрибуты объектов</summary>
  public List<GlobalType> ObjectAttrs = new List<GlobalType>();
  /// <summary>Собираемые атрибуты связей</summary>
  public List<GlobalType> RelationAttrs = new List<GlobalType>();
  /// <summary>Правило подбора версий</summary>
  public Guid VersionRule = Guid.Empty;

  public VisSchemeParms()
  {
  }

  public VisSchemeParms(long objId, IUserSession ius)
    : this()
  {
    this.LoadFromObject(ius, objId);
  }

  /// <summary>Загрузить схему из указанного объекта базы данных</summary>
  /// <param name="session">Сессия</param>
  /// <param name="anObjectID">Идентификатор версии объекта типа "Опция"</param>
  public void LoadFromObject(IUserSession session, long anObjectID)
  {
    this.SchemeId = anObjectID;
    if (anObjectID == 0L)
      return;
    IDBObject dbObject1 = session.GetObject(anObjectID);
    this.Name = dbObject1.Caption;
    IDBAttribute attributeByGuid1 = dbObject1.GetAttributeByGuid(new Guid("cad00621-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1.Value != null && attributeByGuid1.Value != DBNull.Value)
    {
      IDBObject dbObject2 = session.GetObject(Convert.ToInt64(attributeByGuid1.Value));
      this.SelectionId = dbObject2.ObjectID;
      this.SelectionName = dbObject2.Caption != string.Empty ? dbObject2.Caption : $"<{dbObject2.ObjectID}>";
    }
    IDBAttribute attributeByGuid2 = dbObject1.GetAttributeByGuid(new Guid(SearchConsts.attributeVersionRule));
    if (attributeByGuid2 != null && attributeByGuid2.Value != DBNull.Value)
      this.VersionRule = new Guid(attributeByGuid2.AsString);
    byte[] zipScr = (byte[]) null;
    if (dbObject1.GetAttributeByID(ExpertConsts.Consts.attrObjData) is IDBShortBlobAttribute attributeById)
      zipScr = attributeById.GetData();
    XmlElement documentElement = ZlibHelper.UnpackXmlBuffer(zipScr).DocumentElement;
    if (!documentElement.HasChildNodes)
      return;
    foreach (XmlNode childNode in documentElement.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "MaxLevels")
        this.maxLevels = Convert.ToInt32(childNode.InnerText);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "DopZamens")
      {
        switch (childNode.InnerText)
        {
          case "A":
            this.useZamens = UseZamens.AllVariants;
            break;
          case "C":
            this.useZamens = UseZamens.AsClient;
            break;
          case "M":
            this.useZamens = UseZamens.MainVariant;
            break;
        }
      }
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "HiddenSostav")
      {
        switch (childNode.InnerText)
        {
          case "A":
            this.hiddenMode = HiddenContentsMode.ShowAllHidden;
            break;
          case "O":
            this.hiddenMode = HiddenContentsMode.HideOnlyHidden;
            break;
          case "R":
            this.hiddenMode = HiddenContentsMode.HideHiddenAndRoots;
            break;
          case "C":
            this.hiddenMode = HiddenContentsMode.HiddenAsClient;
            break;
        }
      }
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "ObjectTypes")
        this.LoadGlobList(childNode, 4, ref this.ObjectTypes, session);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "ExpandTypes")
        this.LoadGlobList(childNode, 4, ref this.TypesToExpand, session);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "NoExpandTypes")
        this.LoadGlobList(childNode, 4, ref this.TypesToDisableExpand, session);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "RelTypes")
        this.LoadGlobList(childNode, 6, ref this.RelationTypes, session);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "PreviewTypes")
        this.LoadGlobList(childNode, 4, ref this.PreviewTypes, session);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "ObjectAttrs")
        this.LoadGlobList(childNode, 3, ref this.ObjectAttrs, session);
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "RelAttrs")
        this.LoadGlobList(childNode, 3, ref this.RelationAttrs, session);
    }
  }

  public void SaveToObject(IUserSession session)
  {
    IDBObject dbObject = session.GetObject(this.SchemeId);
    dbObject.Caption = this.Name;
    dbObject.Attributes.AddAttribute(new Guid("cad00621-306c-11d8-b4e9-00304f19f545"), false).Value = this.SelectionId < 0L ? (object) 0L : (object) this.SelectionId;
    IDBAttribute dbAttribute1 = dbObject.GetAttributeByGuid(new Guid(SearchConsts.attributeVersionRule));
    if (this.VersionRule == Guid.Empty)
    {
      if (dbAttribute1 != null && dbAttribute1.Value != DBNull.Value)
        dbAttribute1.Clear();
    }
    else
    {
      if (dbAttribute1 == null)
        dbAttribute1 = dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(SearchConsts.attributeVersionRule), false);
      dbAttribute1.Value = (object) this.VersionRule;
    }
    MemoryStream memoryStream = new MemoryStream();
    MemoryStream outStream = new MemoryStream();
    XmlTextWriter writer = new XmlTextWriter((Stream) memoryStream, Encoding.UTF8);
    try
    {
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      writer.WriteStartElement("VisScheme");
      writer.WriteAttributeString("xmlns", (string) null, "http://www.intermech.ru/Visualizer");
      writer.WriteElementString("MaxLevels", this.maxLevels.ToString());
      string str = "A";
      switch (this.useZamens)
      {
        case UseZamens.AsClient:
          str = "C";
          break;
        case UseZamens.MainVariant:
          str = "M";
          break;
      }
      writer.WriteElementString("DopZamens", str);
      switch (this.hiddenMode)
      {
        case HiddenContentsMode.ShowAllHidden:
          str = "A";
          break;
        case HiddenContentsMode.HideOnlyHidden:
          str = "O";
          break;
        case HiddenContentsMode.HideHiddenAndRoots:
          str = "R";
          break;
        case HiddenContentsMode.HiddenAsClient:
          str = "C";
          break;
      }
      writer.WriteElementString("HiddenSostav", str);
      this.WriteGlobList(writer, this.ObjectTypes, "ObjectTypes");
      this.WriteGlobList(writer, this.TypesToExpand, "ExpandTypes");
      this.WriteGlobList(writer, this.TypesToDisableExpand, "NoExpandTypes");
      this.WriteGlobList(writer, this.RelationTypes, "RelTypes");
      this.WriteGlobList(writer, this.PreviewTypes, "PreviewTypes");
      this.WriteGlobList(writer, this.ObjectAttrs, "ObjectAttrs");
      this.WriteGlobList(writer, this.RelationAttrs, "RelAttrs");
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Flush();
      memoryStream.Position = 0L;
      ZLibStreamHelper.PackStream((Stream) memoryStream, ZLibCompressLevels.Level3, (Stream) outStream);
    }
    finally
    {
      writer?.Close();
    }
    byte[] array1 = outStream.ToArray();
    if (dbObject.Attributes.AddAttribute(ExpertConsts.Consts.attrObjData, false) is IBlobWriter blobWriter)
    {
      BlobInformation blobInfo = new BlobInformation((long) array1.Length, (long) array1.Length, DateTime.Now, "", ArcMethods.NotPacked, "");
      if (blobWriter.OpenBlob(blobInfo, false))
        blobWriter.WriteDataBlock(array1);
    }
    IDBAttribute dbAttribute2 = dbObject.GetAttributeByGuid(new Guid(ExpertAttrGUIDs.objTypeGUIDs)) ?? dbObject.Attributes.AddAttribute(new Guid(ExpertAttrGUIDs.objTypeGUIDs), false);
    HashSet<string> stringSet1 = new HashSet<string>();
    stringSet1.UnionWith((IEnumerable<string>) this.ObjectTypes.ConvertAll<string>((Converter<GlobalType, string>) (gt => gt.TypeGuid.ToString())));
    stringSet1.UnionWith((IEnumerable<string>) this.TypesToExpand.ConvertAll<string>((Converter<GlobalType, string>) (gt => gt.TypeGuid.ToString())));
    stringSet1.UnionWith((IEnumerable<string>) this.TypesToDisableExpand.ConvertAll<string>((Converter<GlobalType, string>) (gt => gt.TypeGuid.ToString())));
    stringSet1.UnionWith((IEnumerable<string>) this.PreviewTypes.ConvertAll<string>((Converter<GlobalType, string>) (gt => gt.TypeGuid.ToString())));
    if (stringSet1.Count != 0)
    {
      string[] array2 = new string[stringSet1.Count];
      stringSet1.CopyTo(array2);
      dbAttribute2.Values = (object[]) array2;
    }
    IDBAttribute dbAttribute3 = dbObject.GetAttributeByGuid(new Guid(ExpertAttrGUIDs.attrGUIDs)) ?? dbObject.Attributes.AddAttribute(new Guid(ExpertAttrGUIDs.attrGUIDs), false);
    HashSet<string> stringSet2 = new HashSet<string>();
    stringSet2.UnionWith((IEnumerable<string>) this.ObjectAttrs.ConvertAll<string>((Converter<GlobalType, string>) (gt => gt.TypeGuid.ToString())));
    stringSet2.UnionWith((IEnumerable<string>) this.RelationAttrs.ConvertAll<string>((Converter<GlobalType, string>) (gt => gt.TypeGuid.ToString())));
    if (stringSet2.Count <= 0)
      return;
    string[] array3 = new string[stringSet2.Count];
    stringSet2.CopyTo(array3);
    dbAttribute3.Values = (object[]) array3;
  }

  private void ReadAttributeToCollection(
    IUserSession session,
    IDBObject scheme,
    Guid attributeGuid,
    List<GlobalType> collection)
  {
    collection.Clear();
    IDBAttribute attributeByGuid = scheme.GetAttributeByGuid(attributeGuid);
    if (attributeByGuid == null || attributeByGuid.ValuesCount <= 0)
      return;
    foreach (object obj in attributeByGuid.Values)
    {
      if (obj != null && obj != DBNull.Value)
        collection.Add(new GlobalType(obj.ToString(), 4, session));
    }
  }

  private void SaveCollectionToAttribute(
    IDBObject scheme,
    Guid attributeGuid,
    List<GlobalType> collection)
  {
    IDBAttribute dbAttribute = scheme.GetAttributeByGuid(attributeGuid);
    if (collection.Count == 0)
    {
      dbAttribute?.ClearValues();
    }
    else
    {
      if (dbAttribute == null)
        dbAttribute = scheme.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(attributeGuid), false);
      dbAttribute.Values = (object[]) collection.ConvertAll<string>((Converter<GlobalType, string>) (item => item.TypeGuid.ToString())).ToArray();
    }
  }

  /// <summary>Загрузить список чего-нибудь из XML</summary>
  /// <param name="root">Корневой узел</param>
  /// <param name="category">Категория (типы объектов, типы связей или атрибутов)</param>
  /// <param name="list">Список, в который надо загрузить данные</param>
  /// <param name="ius">Пользовательская сессия</param>
  private void LoadGlobList(
    XmlNode root,
    int category,
    ref List<GlobalType> list,
    IUserSession ius)
  {
    list.Clear();
    if (!root.HasChildNodes)
      return;
    foreach (XmlNode childNode in root.ChildNodes)
    {
      if (childNode.Name == "TypeId")
      {
        GlobalType globalType = new GlobalType(Convert.ToInt32(childNode.InnerText), category, ius);
        list.Add(globalType);
      }
    }
  }

  /// <summary>Записать список чего-нибудь в XML</summary>
  /// <param name="writer">Средство для записи в XML</param>
  /// <param name="list">Список, типы из которого записываются</param>
  /// <param name="name">Имя всего списка</param>
  private void WriteGlobList(XmlTextWriter writer, List<GlobalType> list, string name)
  {
    writer.WriteStartElement(name);
    foreach (GlobalType globalType in list)
      writer.WriteElementString("TypeId", globalType.TypeID.ToString());
    writer.WriteEndElement();
  }

  public void SortByName(List<GlobalType> list)
  {
    list.Sort(new Comparison<GlobalType>(this.CompareNames));
  }

  internal int CompareNames(GlobalType g1, GlobalType g2)
  {
    return StringComparer.CurrentCulture.Compare(g1.TypeName, g2.TypeName);
  }
}
