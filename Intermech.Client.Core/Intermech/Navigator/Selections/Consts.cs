
// Type: Intermech.Navigator.Selections.Consts
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections;

/// <summary>
/// Содержит константы, используемые механизмом работы выборок и классификаторов.
/// </summary>
public sealed class Consts
{
  private static int _kindSelectionAttrID = -10000;
  private static int _kindClassifierAttrID = -10000;
  private static int _objectTypesAttrID = -10000;
  private static int _selectionsTypeID = -1;
  private static int _selectionTypeID = -1;
  private static int _classifierTypeID = -1;
  private static readonly Guid _objectTypesAttrGuid = new Guid("cad00149-306c-11d8-b4e9-00304f19f545");
  private static readonly Guid _selectionsTypeGuid = new Guid("cad00119-306c-11d8-b4e9-00304f19f545");
  private static readonly Guid _selectionTypeGuid = new Guid("cad00156-306c-11d8-b4e9-00304f19f545");
  private static readonly Guid _classifierTypeGuid = new Guid("cad00157-306c-11d8-b4e9-00304f19f545");
  private static int _objtypeClassifierCommonID = -1;
  private static int _objtypeClassifierPersonID = -1;
  private static int _objtypeClassifierFolderID = -1;
  /// <summary>
  /// Глобальный идентификатор атрибута "Искать среди объектов глобальных и локальных типов"
  /// </summary>
  public static readonly Guid attTypeSearchInLocalTypes = new Guid("cadd9971-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Глобальный идентификатор дескриптора, описывающего элемент навигации "Выборки".
  /// </summary>
  public static readonly Guid SelectionsDescriptorGuid = new Guid("9760ED60-7B32-425c-BE5F-030AE7120AA6");
  /// <summary>
  /// Глобальный идентификатор дескриптора, описывающего элемент навигации "Общие выборки".
  /// </summary>
  public static readonly Guid SelectionsCommonDescriptorGuid = new Guid("BBEDDE8E-68DA-40C1-9360-A523B9D79504");
  /// <summary>
  /// Глобальный идентификатор дескриптора, описывающего элемент навигации "Персональные выборки".
  /// </summary>
  public static readonly Guid SelectionsPersonalDescriptorGuid = new Guid("50E8E53F-5E39-4605-B29C-32075F67FCC2");
  /// <summary>
  /// Глобальный идентификатор дескриптора, описывающего элемент навигации "Классификаторы".
  /// </summary>
  public static readonly Guid ClassifiersDescriptorGuid = new Guid("1F4BF40A-00C3-4b1a-A057-21B56FB90B08");
  /// <summary>
  /// Глобальный идентификатор части элемента навигации, которая отвечает за "Выборки" и "Классификаторы".
  /// </summary>
  public static readonly Guid SelectionsPartGuid = new Guid("C37B60A8-E630-4a0c-9CBB-2C878F7A0257");
  /// <summary>
  /// Глобальный идентификатор части элемента (содержащего "Выборки" и "Классификаторы"), которая отвечает
  /// за содержимое элемента навигации.
  /// </summary>
  public static readonly Guid ContentPartGuid = new Guid("F3CB8C9D-8EAE-4f8d-8780-E0535ADA46B0");

  public static bool IsClassifier(int objectType)
  {
    if (Consts._objtypeClassifierFolderID == -1)
    {
      Consts._objtypeClassifierCommonID = Intermech.Navigator.DB.Helper.GetObjectTypeID(new Guid("cad0014e-306c-11d8-b4e9-00304f19f545"));
      Consts._objtypeClassifierPersonID = Intermech.Navigator.DB.Helper.GetObjectTypeID(new Guid("cad0014f-306c-11d8-b4e9-00304f19f545"));
      Consts._objtypeClassifierFolderID = Intermech.Navigator.DB.Helper.GetObjectTypeID(new Guid("cad00150-306c-11d8-b4e9-00304f19f545"));
    }
    return objectType == Consts.ClassifierTypeID || objectType == Consts._objtypeClassifierCommonID || objectType == Consts._objtypeClassifierPersonID || objectType == Consts._objtypeClassifierFolderID;
  }

  public static int GetKindAttributeID(int objectType)
  {
    return !Consts.IsClassifier(objectType) ? Consts.KindSelectionAttrID : Consts.KindClassifierAttrID;
  }

  /// <summary>
  /// Возврашает идентификатор спискового атрибута, содержащего тип привязки
  /// выборки - к типу объекта, к архиву, к составу объекта и т.д.
  /// </summary>
  public static int KindSelectionAttrID
  {
    get
    {
      if (Consts._kindSelectionAttrID == -10000)
        Consts._kindSelectionAttrID = Intermech.Navigator.DB.Helper.GetAttributeID(new Guid("cad00158-306c-11d8-b4e9-00304f19f545"));
      return Consts._kindSelectionAttrID;
    }
  }

  /// <summary>
  /// Возврашает идентификатор спискового атрибута, содержащего тип привязки
  /// классификатора - к типу объекта, к архиву, к составу объекта и т.д.
  /// </summary>
  public static int KindClassifierAttrID
  {
    get
    {
      if (Consts._kindClassifierAttrID == -10000)
        Consts._kindClassifierAttrID = Intermech.Navigator.DB.Helper.GetAttributeID(new Guid("cad00e8f-306c-11d8-b4e9-00304f19f545"));
      return Consts._kindClassifierAttrID;
    }
  }

  /// <summary>
  /// Возвращает идентификатор атрибута, хранящего список глобальных
  /// идентификаторов типов объектов, к которым привязана выборка.
  /// </summary>
  public static int ObjectTypesAttrID
  {
    get
    {
      if (Consts._objectTypesAttrID == -10000)
        Consts._objectTypesAttrID = Intermech.Navigator.DB.Helper.GetAttributeID(Consts._objectTypesAttrGuid);
      return Consts._objectTypesAttrID;
    }
  }

  /// <summary>
  /// Возвращает идентификатор типа "Выборки и классификаторы".
  /// </summary>
  public static int SelectionsTypeID
  {
    get
    {
      if (Consts._selectionsTypeID == -1)
        Consts._selectionsTypeID = Intermech.Navigator.DB.Helper.GetObjectTypeID(Consts._selectionsTypeGuid);
      return Consts._selectionsTypeID;
    }
  }

  /// <summary>Возвращает идентификатор типа "Выборки".</summary>
  public static int SelectionTypeID
  {
    get
    {
      if (Consts._selectionTypeID == -1)
        Consts._selectionTypeID = Intermech.Navigator.DB.Helper.GetObjectTypeID(Consts._selectionTypeGuid);
      return Consts._selectionTypeID;
    }
  }

  /// <summary>Возвращает идентификатор типа "Классификаторы".</summary>
  public static int ClassifierTypeID
  {
    get
    {
      if (Consts._classifierTypeID == -1)
        Consts._classifierTypeID = Intermech.Navigator.DB.Helper.GetObjectTypeID(Consts._classifierTypeGuid);
      return Consts._classifierTypeID;
    }
  }

  /// <summary>Рекурсивная классификация</summary>
  /// <param name="session"></param>
  /// <param name="objClassif"></param>
  /// <param name="pasteArray"></param>
  /// <returns></returns>
  public static ClassifiedError RecurClassify(
    IUserSession session,
    IObjectClassificator objClassif,
    long[] pasteArray)
  {
    ClassifiedError classifiedError = objClassif.ClassifyObjects(pasteArray);
    if (!classifiedError.FullClassified)
    {
      string str = Consts.StringDivider(classifiedError.Exception != null ? classifiedError.Exception.Message : string.Empty, 100);
      switch (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_82"), string.Format(LocalizationHolder.rm.GetString("Client.Core_677"), (object) classifiedError.ObjectID, (object) str), new IMMessageBoxButton[3]
      {
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_678"), DialogResult.Retry),
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_679"), DialogResult.Abort),
        new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_166"), DialogResult.Cancel)
      }, IMMessageBoxImage.Error))
      {
        case DialogResult.Cancel:
          return new ClassifiedError(false);
        case DialogResult.Abort:
          objClassif.SkipNonClassified = true;
          return Consts.RecurClassify(session, objClassif, pasteArray);
        case DialogResult.Retry:
          return Consts.RecurClassify(session, objClassif, pasteArray);
      }
    }
    return classifiedError;
  }

  private static string StringDivider(string inString, int count)
  {
    if (inString.Length <= count)
      return inString;
    string empty = string.Empty;
    int num = 0;
    for (int index = 0; index < inString.Length; ++index)
    {
      if (num >= count && inString[index] == ' ')
      {
        empty += "\n";
        num = 0;
      }
      empty += inString[index].ToString();
      ++num;
    }
    return empty;
  }

  /// <summary>Классификация объекта</summary>
  /// <param name="Session"></param>
  /// <param name="objClassif"></param>
  /// <param name="objectID"></param>
  /// <param name="ShowError">Показывать сообщение об ошибке</param>
  /// <returns></returns>
  public static ClassifiedError ObjectClassify(
    IUserSession Session,
    IObjectClassificator objClassif,
    long objectID,
    bool ShowError)
  {
    ClassifiedError classifiedError = objClassif.ClassifyObjects(new long[1]
    {
      objectID
    });
    if (!classifiedError.FullClassified & ShowError)
    {
      string str = Consts.StringDivider(classifiedError.Exception != null ? classifiedError.Exception.Message : string.Empty, 100);
      int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_82"), string.Format(LocalizationHolder.rm.GetString("Client.Core_677"), (object) classifiedError.ObjectID, (object) str), MessageBoxButtons.OK, IMMessageBoxImage.Error);
    }
    return classifiedError;
  }
}
