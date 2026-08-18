// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.ForumsConsts
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Forums;

/// <summary>константы для работы с объектами типа Обсуждение</summary>
public static class ForumsConsts
{
  /// <summary>
  /// Сплиттер, используемый для перечисления просмотревших сообщение пользователей
  /// </summary>
  public static char SplitterForUsers = ',';
  /// <summary>
  /// Признак того, что сейчас будет перечисление просотревших пользователей
  /// </summary>
  public static string UsersMark = "users:";
  public static char SplitterChar = '|';
  /// <summary>Разделитель между элементами кодированной строки</summary>
  public static string[] Splitter = new string[1]{ "|" };
  /// <summary>guid типа объекта Обсуждения</summary>
  public static readonly string forumObjectTypeGuid = "cadd92ce-306c-11d8-b4e9-00304f19f545";
  /// <summary>guid типа объекта Опубликованное обсуждение</summary>
  public static readonly string publishForumObjectTypeGuid = "cadd92e0-306c-11d8-b4e9-00304f19f545";
  /// <summary>guid атрибута Обсуждение</summary>
  public static readonly string forumAttributeGuid = "cadd92cf-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// guid атрибута Идентификатор версии обсуждаемого объекта
  /// </summary>
  public static readonly string discussedObjectGuidAttributeGuid = "cadd92de-306c-11d8-b4e9-00304f19f545";
  /// <summary>guid атрибута Идентификатор обсуждаемого объекта</summary>
  public static readonly string discussedGuidAttributeGuid = "cadd92df-306c-11d8-b4e9-00304f19f545";
  /// <summary>id типа объекта Обсуждения</summary>
  public static int forumObjectTypeID = -1;
  /// <summary>id типа объекта Опубликованное обсуждение</summary>
  public static int publishForumObjectTypeID = -1;
  /// <summary>id атрибута Обсуждение</summary>
  public static int forumAttributeID = 0;
  /// <summary>
  /// id атрибута Идентификатор версии обсуждаемого объекта
  /// </summary>
  public static int discussedObjectGuidAttributeID = 0;
  /// <summary>id атрибута Идентификатор обсуждаемого объекта</summary>
  public static int discussedGuidAttributeID = 0;
  /// <summary>Идентификатор типа атрибута Файл</summary>
  public static int fileAttrTypeID = 0;

  static ForumsConsts()
  {
    ForumsConsts.forumObjectTypeID = MetaDataHelper.GetObjectTypeID(ForumsConsts.forumObjectTypeGuid);
    ForumsConsts.publishForumObjectTypeID = MetaDataHelper.GetObjectTypeID(ForumsConsts.publishForumObjectTypeGuid);
    ForumsConsts.forumAttributeID = MetaDataHelper.GetAttributeTypeID(ForumsConsts.forumAttributeGuid);
    ForumsConsts.discussedObjectGuidAttributeID = MetaDataHelper.GetAttributeTypeID(ForumsConsts.discussedObjectGuidAttributeGuid);
    ForumsConsts.discussedGuidAttributeID = MetaDataHelper.GetAttributeTypeID(ForumsConsts.discussedGuidAttributeGuid);
    ForumsConsts.forumAttributeID = MetaDataHelper.GetAttributeTypeID(ForumsConsts.forumAttributeGuid);
    ForumsConsts.fileAttrTypeID = MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545");
  }
}
