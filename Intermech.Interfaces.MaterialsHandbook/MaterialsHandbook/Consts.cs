// Decompiled with JetBrains decompiler
// Type: Intermech.MaterialsHandbook.Consts
// Assembly: Intermech.Interfaces.MaterialsHandbook, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C75FAC17-15DB-4F73-814B-B278FC9C1B73
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MaterialsHandbook.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.MaterialsHandbook;

/// <summary>
/// Глобальные константы и переменные марочника материалов.
/// </summary>
public class Consts
{
  /// <summary>Неопределенная категория виртуальных узлов</summary>
  public static int IMHEmptyCategoryID = -1;
  /// <summary>Идентификатор категории узла "Марочник материалов"</summary>
  public static int IMHRootNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Справочник материалов"</summary>
  public static int IMHMaterialsHandbookNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Справочник клеев"</summary>
  public static int IMHGluesHandbookNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Справочник покрытий"</summary>
  public static int IMHCoatingsHandbookNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Масла и смазки"</summary>
  public static int IMHOilHandbookNodeCategoryID = -1;
  /// <summary>
  /// Идентификатор категории узла "Лакокрасочные материалы"
  /// </summary>
  public static int IMHVarnishHandbookNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Материалы"</summary>
  public static int IMHMaterialsNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Сортамент"</summary>
  public static int IMHAssortmentNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Профили"</summary>
  public static int IMHProfilesNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "ГОСТ"</summary>
  public static int IMHStandardNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Виды покрытий"</summary>
  public static int IMHCoatingsVarietiesNodeCategoryID = -1;
  /// <summary>Идентификатор категории узла "Материал детали"</summary>
  public static int IMHDetailsMaterialNodeCategoryID = -1;
  /// <summary>Идентификатор категории "Папка ГОСТа"</summary>
  public static int IMHStandartFolderCategoryID = -1;
  public static readonly Guid IMHRootNodeGuid = new Guid("{6D91894D-4ECA-43db-9482-3780805AF234}");
  public static readonly Guid IMHMaterialsHandbookNodeGuid = new Guid("{E066E829-C0DE-4ff8-B947-D5EE20402E8A}");
  public static readonly Guid IMHGluesHandbookNodeGuid = new Guid("{A6836620-A2C8-4dcc-8FEE-5CE2895F91BE}");
  public static readonly Guid IMHCoatingsHandbookNodeGuid = new Guid("{FD33323B-4C9B-40c9-B1D3-A7BC1600A0E1}");
  public static readonly Guid IMHOilHandbookNodeGuid = new Guid("{B1596ED4-26C7-45D2-BEEA-CC93F4ABC5AD}");
  public static readonly Guid IMHVarnishHandbookNodeGuid = new Guid("{DDB64F00-14A6-4D06-9780-88948709AEC9}");
  public static readonly Guid IMHMaterialsNodeGuid = new Guid("{82DE52B4-5266-4817-9B51-7DF73BE45215}");
  public static readonly Guid IMHAssortmentNodeGuid = new Guid("{40898A06-D90D-47fc-B7A7-5B7DA5F839BC}");
  public static readonly Guid IMHProfilesNodeGuid = new Guid("{77572577-72C1-4aeb-800E-E174C456AC72}");
  public static readonly Guid IMHStandardNodeGuid = new Guid("{CF281F5F-E9A8-4570-B0F5-CF21557B21DB}");
  public static readonly Guid IMHCoatingsVarietiesNodeGuid = new Guid("{8EF407AC-71D5-4b90-BFA7-319DC9825546}");
  public static readonly Guid IMHDetailsMaterialNodeGuid = new Guid("{C3AA5140-59A0-4d00-BDCC-89623810BD9C}");
  public static readonly Guid IMHStandartFolderNodeGuid = new Guid("{F263F5F1-C657-47d4-9A4D-911D015FDC81}");
  /// <summary>Структура каталога используется для узла "Материалы"</summary>
  public const string BASE_MATERIALS_CTL = "BASE_MATERIALS_CTL";
  /// <summary>Структура каталога используется для узла "Профили"</summary>
  public const string ADDITION_MATERIALS_CTL = "ADDITION_MATERIALS_CTL";
  /// <summary>Структура папки используется для узла "Сортамент"</summary>
  public const string ASSORTMENT_FOLDER_NAME = "ASSORTMENT_FOLDER_NAME";
  /// <summary>
  /// Структура папки используется для узла "Справочник клеев"
  /// </summary>
  public const string GLUE_FOLDER_NAME = "GLUE_FOLDER_NAME";
  /// <summary>Структура папки используется для узла "Виды покрытий"</summary>
  public const string COATING_FOLDER_NAME = "COATING_FOLDER_NAME";
  /// <summary>
  /// Структура папки используется для узла "Масла и смазки"
  /// </summary>
  public const string OIL_FOLDER_NAME = "OIL_FOLDER_NAME";
  /// <summary>
  /// Структура папки используется для узла "Лакокрасочные материалы"
  /// </summary>
  public const string VARNISH_FOLDER_NAME = "VARNISH_FOLDER_NAME";
  /// <summary>Таблица используется для заменителей материалов</summary>
  public const string MATERIAL_SUBSTITUTES_TABLE_NAME = "MATERIAL_SUBSTITUTES_TABLE_NAME";
  public const string MATERIAL_SUBSTITUTES_COLUMN_MATERIAL = "MATERIAL_SUBSTITUTES_COLUMN_MATERIAL";
  public const string MATERIAL_SUBSTITUTES_COLUMN_SUBSTITUTES = "MATERIAL_SUBSTITUTES_COLUMN_SUBSTITUTES";
  /// <summary>Таблица используется для узла "Материал детали"</summary>
  public const string MATERIAL_GROUPS_TABLE_NAME = "MATERIAL_GROUPS_TABLE_NAME";
  public const string MATERIAL_GROUPS_COLUMN_NAME = "MATERIAL_GROUPS_COLUMN_NAME";
  /// <summary>Таблица свойств материалов</summary>
  public const string MATERIAL_PROPERTIES_TABLE_NAME = "MATERIAL_PROPERTIES_TABLE_NAME";
  public const string MATERIAL_PROPERTIES_COLUMN_MATERIAL = "MATERIAL_PROPERTIES_COLUMN_MATERIAL";
  public const string MATERIAL_PROPERTIES_COLUMN_OBJECT = "MATERIAL_PROPERTIES_COLUMN_OBJECT";
  /// <summary>Таблица свойств покрытий</summary>
  public const string COATING_PROPERTIES_TABLE_NAME = "COATING_PROPERTIES_TABLE_NAME";
  public const string COATING_PROPERTIES_COLUMN_COATING = "COATING_PROPERTIES_COLUMN_COATING";
  public const string COATING_PROPERTIES_COLUMN_MATERIAL = "COATING_PROPERTIES_COLUMN_MATERIAL";
  public const string COATING_PROPERTIES_COLUMN_PURPOSE = "COATING_PROPERTIES_COLUMN_PURPOSE";
  public const string COATING_PROPERTIES_COLUMN_INSTRUCTIONS = "COATING_PROPERTIES_COLUMN_INSTRUCTIONS";
  /// <summary>Таблица "Группы материалов для клеев"</summary>
  public const string GLUE_MATERIAL_GROUPS_TABLE_NAME = "GLUE_MATERIAL_GROUPS_TABLE_NAME";
  public const string GLUE_MATERIAL_GROUPS_COLUMN_NAME = "GLUE_MATERIAL_GROUPS_COLUMN_NAME";
  /// <summary>Таблица "Клеи - группы материалов"</summary>
  public const string GLUE_TABLE_NAME = "GLUE_TABLE_NAME";
  public const string GLUE_COLUMN_MATERIAL1 = "GLUE_COLUMN_MATERIAL1";
  public const string GLUE_COLUMN_MATERIAL2 = "GLUE_COLUMN_MATERIAL2";
  public const string GLUE_COLUMN_GLUE = "GLUE_COLUMN_GLUE";
  /// <summary>Таблица "Материалы поверхностей"</summary>
  public const string SURFACE_MATERIALS_TABLE_NAME = "SURFACE_MATERIALS_TABLE_NAME";
  public const string SURFACE_MATERIALS_COLUMN_NAME = "SURFACE_MATERIALS_COLUMN_NAME";
  /// <summary>Таблица "Покрытие - материалы"</summary>
  public const string COATING_MATERIALS_TABLE_NAME = "COATING_MATERIALS_TABLE_NAME";
  public const string COATING_MATERIALS_COLUMN_COATING = "COATING_MATERIALS_COLUMN_COATING";
  public const string COATING_MATERIALS_COLUMN_MATERIALS = "COATING_MATERIALS_COLUMN_MATERIALS";
  /// <summary>Таблица "Условия эксплуатации"</summary>
  public const string TERMS_USE_TABLE_NAME = "TERMS_USE_TABLE_NAME";
  public const string TERMS_USE_COLUMN_NAME = "TERMS_USE_COLUMN_NAME";
  /// <summary>Таблица "Покрытие - условие эксплуатации"</summary>
  public const string COATING_TERMS_USE_TABLE_NAME = "COATING_TERMS_USE_TABLE_NAME";
  public const string COATING_TERMS_USE_COLUMN_COATING = "COATING_TERMS_USE_COLUMN_COATING";
  public const string COATING_TERMS_USE_COLUMN_TERMS = "COATING_TERMS_USE_COLUMN_TERMS";
  /// <summary>Таблица "Покрытие - сфера использования"</summary>
  public const string COATING_SPHERE_USE_TABLE_NAME = "COATING_SPHERE_USE_TABLE_NAME";
  public const string COATING_SPHERE_USE_COLUMN_COATING = "COATING_SPHERE_USE_COLUMN_COATING";
  public const string COATING_SPHERE_USE_COLUMN_SPHERE = "COATING_SPHERE_USE_COLUMN_SPHERE";
  /// <summary>Таблица "Внутр.-наружное покрытие"</summary>
  public const string COATING_INTERNAL_EXTERNAL_TABLE_NAME = "COATING_INTERNAL_EXTERNAL_TABLE_NAME";
  public const string COATING_INTERNAL_EXTERNAL_INTERNAL_COLUMN = "COATING_INTERNAL_EXTERNAL_INTERNAL_COLUMN";
  public const string COATING_INTERNAL_EXTERNAL_EXTERNAL_WITH_CONDITION_COLUMN = "COATING_INTERNAL_EXTERNAL_EXTERNAL_WITH_CONDITION_COLUMN";
  /// Таблица "Покрытие - преимущ. назначения"
  public const string COATING_PREFERRED_DESTINATION_TABLE_NAME = "COATING_PREFERRED_DESTINATION_TABLE_NAME";
  public const string COATING_PREFERRED_DESTINATION_COLUMN_COATING = "COATING_PREFERRED_DESTINATION_COLUMN_COATING";
  public const string COATING_PREFERRED_DESTINATION_COLUMN_PURPOSE = "COATING_PREFERRED_DESTINATION_COLUMN_PURPOSE";
  /// Таблица "Покрытие - цвет"
  public const string COATING_COLOR_TABLE_NAME = "COATING_COLOR_TABLE_NAME";
  public const string COATING_COLOR_COLUMN_COATING = "COATING_COLOR_COLUMN_COATING";
  public const string COATING_COLOR_COLUMN_COLOR = "COATING_COLOR_COLUMN_COLOR";
  /// Таблица "Цвета по РАЛ"
  public const string COATING_COLOR_RAL_TABLE_NAME = "COATING_COLOR_RAL_TABLE_NAME";
  /// <summary>Атрибут основного материала</summary>
  public const string BASE_MATERIAL_ATTR = "BASE_MATERIAL_ATTR";
  /// <summary>Атрибут цвет (для лакокрасочных материалов)</summary>
  public const string COLOR_VARNISH_ATTR = "COLOR_VARNISH_ATTR";
  /// <summary>Отображение строк с отрицательной применяемостью</summary>
  public const string DISPLAY_SETTING_SHOW_RECORDS = "DISPLAY_SETTING_SHOW_RECORDS";
  /// <summary>Идентификатор типа объектов "Материал"</summary>
  public static int MaterialObjTypeID = -1;
  /// <summary>GUID роли Администратор НСИ</summary>
  public static readonly Guid NSIAdminRoleGUID = new Guid("c5bd692a-348e-44e3-a4ee-b634d53fedbc");
  /// <summary>Идентификатор роли Администратор НСИ</summary>
  public static long NSIAdminRoleId = -1;
  public static int CoatingClassAttrTypeId;
  public static readonly Guid CoatingClassAttrTypeGuid = new Guid("cadd99f0-306c-11d8-b4e9-00304f19f545");
  public static int CoatingGroupAttrTypeId;
  public static readonly Guid CoatingGroupAttrTypeGuid = new Guid("cadd99ef-306c-11d8-b4e9-00304f19f545");
  public static int TermsOfUseAttrTypeId;
  public static readonly Guid TermsOfUseAttrTypeGuid = new Guid("cadd99f1-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid VarnishColorAttrTypeGuid = new Guid("cae0ef4a-3950-4586-91a4-8ffebaf0d493");
  public static readonly Guid SubstitutesForMaterialsTableGuid = new Guid("cae04888-ca6d-4abb-9d89-081f728e32a7");
  public static readonly Guid SubstitutesForMaterialsMaterialFieldGuid = new Guid("cae0a3ff-0881-46b4-bc08-ba1a71d4885f");
  public static readonly Guid SubstitutesForMaterialsSubstitutesFieldGuid = new Guid("cae07350-9a4f-47d3-8ede-ca27337903b6");
  public static readonly Guid MaterialGroupsTableGuid = new Guid("cae0ac73-6e56-4511-8d8d-d928fb764876");
  public static readonly Guid MaterialGroupsMaterialofDetailFieldGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MaterialPropsTableGuid = new Guid("cae0ec77-9c68-4185-81b8-4e6fb196f9c6");
  public static readonly Guid MaterialPropsMaterialFieldGuid = new Guid("cadd941f-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid MaterialPropsObjectFieldGuid = new Guid("cadd941e-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid CoatingPropsTableGuid = new Guid("cae00d9b-27af-4947-8822-553baef22008");
  public static readonly Guid CoatingPropsCoatingFieldGuid = new Guid("cae017d7-5480-42e4-9b7c-64e71aac0756");
  public static readonly Guid CoatingPropsMaterialFieldGuid = new Guid("cae0a3ff-0881-46b4-bc08-ba1a71d4885f");
  public static readonly Guid CoatingPropsDestinationFieldGuid = new Guid("cad00390-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid CoatingPropsAddInstructionsFieldGuid = new Guid("cae06a4e-2fb2-4b83-96e6-f4ff17de9fb8");
  public static readonly Guid MaterialGroupsForGluesTableGuid = new Guid("cae0ce6c-4493-459b-8ae0-50936b6bd2d0");
  public static readonly Guid MaterialGroupsForGluesMaterialNameFieldGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid GluesTableGuid = new Guid("cae067e7-5d3f-4398-b679-415198cb7581");
  public static readonly Guid GluesMaterial1FieldGuid = new Guid("cae09464-be45-4be2-9696-bbe1a85b2b71");
  public static readonly Guid GluesMaterial2FieldGuid = new Guid("cae039e6-3acf-4c00-9af6-53f637801b12");
  public static readonly Guid GluesGlueFieldGuid = new Guid("cae0b7ca-d7d3-45a3-a098-294e46f649de");
  public static readonly Guid MaterialsOfSurfaceTableGuid = new Guid("cae0f68e-d071-4c53-9f9e-40cc48674b58");
  public static readonly Guid MaterialsOfSurfaceFieldGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid CoatingMaterialsTableGuid = new Guid("cae074df-03be-4d77-825e-58e6fbe6fafa");
  public static readonly Guid CoatingMaterialsMaterialFieldGuid = new Guid("cae0a3ff-0881-46b4-bc08-ba1a71d4885f");
  public static readonly Guid CoatingMaterialsCoatingFieldGuid = new Guid("cae0221b-7c31-4801-8c85-1ff2889ac52a");
  public static readonly Guid TermsOfUseTableGuid = new Guid("cae04e14-ee00-4ec4-b5f1-f67aded4f381");
  public static readonly Guid TermsOfUseFieldGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid CoatingTermsOfUseTableGuid = new Guid("cae0b99d-f833-4694-a452-6fa92564d7d6");
  public static readonly Guid CoatingTermsOfUseTermsOfUseFieldGuid = new Guid("cae0119e-a8ea-4bcb-837e-8fe9f418bc7a");
  public static readonly Guid CoatingTermsOfUseCoatingFieldGuid = new Guid("cae0221b-7c31-4801-8c85-1ff2889ac52a");
  public static readonly Guid CoatingSphereTableGuid = new Guid("cae04328-8656-4d58-9ff3-2b91f51efa19");
  public static readonly Guid CoatingSphereSphereFieldGuid = new Guid("cae08e35-9cb7-46e1-98c6-ab6a1a9336ff");
  public static readonly Guid CoatingSphereCoatingFieldGuid = new Guid("cae09b8d-e0b0-424d-925e-abced1236837");
  public static readonly Guid InternalExternalCoatingTableGuid = new Guid("cae02bfc-497f-4c74-9aca-a1768d418813");
  public static readonly Guid InternalExternalCoatingInternalFieldGuid = new Guid("cae0d2bf-865f-44ae-9e49-ff7813182eee");
  public static readonly Guid InternalExternalCoatingExternalWithTermsOfUseFieldGuid = new Guid("cae00921-7e96-49a0-9be2-099fbec8f7df");
  public static readonly Guid CoatingDestinationTableGuid = new Guid("cae01615-5b6a-4d70-ac81-435db9678217");
  public static readonly Guid CoatingDestinationCoatingFieldGuid = new Guid("cae0221b-7c31-4801-8c85-1ff2889ac52a");
  public static readonly Guid CoatingDestinationDestinationFieldGuid = new Guid("cad00390-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid CoatingColorTableGuid = new Guid("cae09d6d-ea0b-4a68-841a-7e32ef999f5c");
  public static readonly Guid CoatingColorCoatingFieldGuid = new Guid("cae00f0a-0df7-49eb-a856-699f7b983866");
  public static readonly Guid CoatingColorColorFieldGuid = new Guid("cae0ef4a-3950-4586-91a4-8ffebaf0d493");
  public static readonly Guid CoatingColorRalTableGuid = new Guid("cae08903-5fe9-435d-ac52-06fa4ec7bf07");

  static Consts()
  {
    Consts.CoatingClassAttrTypeId = MetaDataHelper.GetAttributeTypeID(Consts.CoatingClassAttrTypeGuid);
    Consts.CoatingGroupAttrTypeId = MetaDataHelper.GetAttributeTypeID(Consts.CoatingGroupAttrTypeGuid);
    Consts.TermsOfUseAttrTypeId = MetaDataHelper.GetAttributeTypeID(Consts.TermsOfUseAttrTypeGuid);
  }
}
