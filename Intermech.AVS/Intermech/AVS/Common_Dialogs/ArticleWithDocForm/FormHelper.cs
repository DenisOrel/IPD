// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.FormHelper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>
/// Класс со статическими переменными, в основном с идентификаторами атрибутов
/// </summary>
internal class FormHelper
{
  /// <summary>Идентификатор атрибута "Обозначение"</summary>
  public static int AttributeDesignationID;
  /// <summary>Идентификатор атрибута "Наименование"</summary>
  public static int AttributeNameID;
  /// <summary>Идентификатор атрибута "Формат"</summary>
  public static int AttributeFormatID;
  /// <summary>Идентификатор атрибута "Количество"</summary>
  public static int AttributeCountID;
  /// <summary>Идентификатор атрибута "Материал"</summary>
  public static int AttributeMaterialID;
  /// <summary>Идентификатор атрибута "Размеры"</summary>
  public static int AttributeSizeID;
  /// <summary>Идентификатор атрибута "Код ОКП"</summary>
  public static int AttributeOKPCodeID;
  /// <summary>Идентификатор атрибута "Масса"</summary>
  public static int AttributeWeightID;
  /// <summary>Идентификатор атрибута "Позиция"</summary>
  public static int AttributePositionID;
  /// <summary>Идентификатор атрибута "Зона"</summary>
  public static int AttributeZoneID;
  /// <summary>Идентификатор атрибута "Примечание"</summary>
  public static int AttributeNoteID;
  /// <summary>Идентификатор атрибута "Поз. обозначение"</summary>
  public static int AttributePosDesignationID;
  /// <summary>Идентификатор атрибута "Смотри"</summary>
  public static int AttributeSearchID;
  /// <summary>
  /// Идентификатор физической величины для атрибута "Литера"
  /// </summary>
  public static int AttributeLiteraID;

  /// <summary>Инициализируем данные</summary>
  /// <param name="formType"></param>
  public static void Init(FormType formType)
  {
    FormHelper.AttributeDesignationID = MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeNameID = MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeFormatID = MetaDataHelper.GetAttributeTypeID("cad00255-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeMaterialID = MetaDataHelper.GetAttributeTypeID("cad0038c-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeSizeID = MetaDataHelper.GetAttributeTypeID("cad00277-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeOKPCodeID = MetaDataHelper.GetAttributeTypeID("cad0038a-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeCountID = MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeWeightID = MetaDataHelper.GetAttributeTypeID("cad00275-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeCountID = MetaDataHelper.GetAttributeTypeID("cad00267-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributePositionID = MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeZoneID = MetaDataHelper.GetAttributeTypeID("cad0027a-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeNoteID = MetaDataHelper.GetAttributeTypeID("cad00021-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributePosDesignationID = MetaDataHelper.GetAttributeTypeID("cad01478-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeLiteraID = MetaDataHelper.GetAttributeTypeID("cad0038b-306c-11d8-b4e9-00304f19f545");
    FormHelper.AttributeSearchID = 0;
  }
}
