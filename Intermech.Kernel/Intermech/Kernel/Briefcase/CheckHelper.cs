// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal static class CheckHelper
{
  public const string notFoundInDB = "notFoundInDB";
  public const string notFoundInDBObjectType = "notFoundInDBObjectType";
  public const string notFoundInBriefObjType = "notFoundInBriefObjType";
  public const string notFoundRelations = "notFoundRelations";

  public static bool CheckArea(
    UserSession session,
    DataSet metaData,
    DataRow briefRow,
    string subjectAreas)
  {
    string conformitySubjectAreas = Helper.GetConformitySubjectAreas((IUserSession) session, metaData, Convert.ToString(briefRow["F_AREA_ID"]));
    foreach (char subjectArea in subjectAreas)
    {
      if (conformitySubjectAreas.IndexOf(subjectArea) < 0)
        return false;
    }
    return true;
  }

  public static bool CheckLanguageID(
    UserSession session,
    DataSet metaData,
    DataRow briefRow,
    string languageID)
  {
    string conformityLanguage = Helper.GetConformityLanguage(session, metaData, Convert.ToString(briefRow["F_LANGUAGE_ID"]));
    foreach (char ch in languageID)
    {
      if (conformityLanguage.IndexOf(ch) < 0)
        return false;
    }
    return true;
  }

  public static bool CompareString(DataRow briefRow, string fieldName, string value2)
  {
    return string.Equals(Convert.ToString(briefRow[fieldName]), value2);
  }

  public static bool CompareBoolean(DataRow briefRow, string fieldName, bool value2)
  {
    return object.Equals((object) Convert.ToBoolean(briefRow[fieldName]), (object) value2);
  }

  public static Hashtable CheckOptions(DataRow briefRow, AttributeOptions options)
  {
    Hashtable hashtable = new Hashtable()
    {
      {
        (object) AttributeOptions.SaveCommonHistory,
        (object) CheckResult.Equal
      },
      {
        (object) AttributeOptions.DisableManualEdit,
        (object) CheckResult.Equal
      },
      {
        (object) AttributeOptions.DisableNulls,
        (object) CheckResult.Equal
      },
      {
        (object) AttributeOptions.GetDescriptionEvent,
        (object) CheckResult.Equal
      },
      {
        (object) AttributeOptions.Internal,
        (object) CheckResult.Equal
      },
      {
        (object) AttributeOptions.ModifyInBase,
        (object) CheckResult.Equal
      },
      {
        (object) AttributeOptions.SaveInLog,
        (object) CheckResult.Equal
      },
      {
        (object) AttributeOptions.SavePrivateHistory,
        (object) CheckResult.Equal
      }
    };
    AttributeOptions int32 = (AttributeOptions) Convert.ToInt32(briefRow["F_OPTIONS"]);
    hashtable[(object) AttributeOptions.SaveCommonHistory] = (object) (CheckResult) ((options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory != ((int32 & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory) ? 5 : 1);
    hashtable[(object) AttributeOptions.DisableManualEdit] = (object) (CheckResult) ((options & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit != ((int32 & AttributeOptions.DisableManualEdit) == AttributeOptions.DisableManualEdit) ? 5 : 1);
    if ((options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls && (int32 & AttributeOptions.DisableNulls) != AttributeOptions.DisableNulls)
      hashtable[(object) AttributeOptions.DisableNulls] = (object) CheckResult.ErrorNotSinhronize;
    else if ((options & AttributeOptions.DisableNulls) != AttributeOptions.DisableNulls && (int32 & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls)
      hashtable[(object) AttributeOptions.DisableNulls] = (object) CheckResult.ErrorSinhronize;
    hashtable[(object) AttributeOptions.GetDescriptionEvent] = (object) (CheckResult) ((options & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent != ((int32 & AttributeOptions.GetDescriptionEvent) == AttributeOptions.GetDescriptionEvent) ? 5 : 1);
    hashtable[(object) AttributeOptions.Internal] = (object) (CheckResult) ((options & AttributeOptions.Internal) == AttributeOptions.Internal != ((int32 & AttributeOptions.Internal) == AttributeOptions.Internal) ? 5 : 1);
    hashtable[(object) AttributeOptions.ModifyInBase] = (object) (CheckResult) ((options & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase != ((int32 & AttributeOptions.ModifyInBase) == AttributeOptions.ModifyInBase) ? 5 : 1);
    hashtable[(object) AttributeOptions.SaveInLog] = (object) (CheckResult) ((options & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog != ((int32 & AttributeOptions.SaveInLog) == AttributeOptions.SaveInLog) ? 5 : 1);
    hashtable[(object) AttributeOptions.SavePrivateHistory] = (object) (CheckResult) ((options & AttributeOptions.SavePrivateHistory) == AttributeOptions.SavePrivateHistory != ((int32 & AttributeOptions.SavePrivateHistory) == AttributeOptions.SavePrivateHistory) ? 5 : 1);
    return hashtable;
  }

  public static bool CheckDefaultValue(
    IDBAttributeType attr,
    DataRow briefRow,
    object defaultValue)
  {
    try
    {
      return (attr as DBAttributeType).CompareValues(briefRow["F_DEFAULT_VALUE"], defaultValue);
    }
    catch (Exception ex)
    {
      throw new Exception($"Ошибка при сравнении значений по умолчанию \"{briefRow["F_DEFAULT_VALUE"]}\" и \"{defaultValue}\" для атрибута {attr.Name}: {ex.Message}", ex);
    }
  }

  public static bool CheckComputed(DataRow briefRow, ComputeValueModes computeValueMode)
  {
    return (ComputeValueModes) Convert.ToInt32(briefRow["F_COMPUTED"]) == computeValueMode;
  }

  public static CheckResult CheckUniqueValueModes(
    DataRow briefRow,
    UniqueValueModes uniqueValueMode,
    bool synhronize)
  {
    UniqueValueModes int32 = (UniqueValueModes) Convert.ToInt32(briefRow["F_UNIQUE"]);
    if (int32 == uniqueValueMode)
      return CheckResult.Equal;
    return ((int32 == UniqueValueModes.NotUnique ? 0 : (uniqueValueMode == UniqueValueModes.NotUnique ? 1 : 0)) & (synhronize ? 1 : 0)) != 0 ? CheckResult.Error : CheckResult.Warning;
  }

  public static bool CheckLevelID(
    UserSession session,
    DataTable levels,
    DataRow briefRow,
    int levelID)
  {
    if (levelID <= 0)
      return Convert.ToInt32(briefRow["F_LEVEL_ID"]) == 0;
    if (Convert.ToInt32(briefRow["F_LEVEL_ID"]) <= 0)
      return true;
    IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel(levelID);
    return new Guid(levels.Rows.Find(briefRow["F_LEVEL_ID"])["F_GUID"].ToString()) == lifecycleLevel.GUID;
  }

  public static bool CheckOptimizationModes(DataRow briefRow, OptimizationModes optimizationMode)
  {
    return (OptimizationModes) Convert.ToInt32(briefRow["F_INVIEW"]) == optimizationMode;
  }

  public static CheckResult CheckAnyAttributes(DataRow briefRow, bool anyAttributes)
  {
    bool flag = Convert.ToInt32(briefRow["F_ANY_ATTRIBUTES"]) != 0;
    if (!flag & anyAttributes)
      return CheckResult.Error;
    return flag != anyAttributes ? CheckResult.Warning : CheckResult.Equal;
  }

  public static bool CheckBlob(object briefBlob, byte[] newBlob)
  {
    return SqlHelper.IsEqual(briefBlob as byte[], newBlob);
  }

  public static bool CheckIcons(DataRow briefRow, byte[] icon)
  {
    return CheckHelper.CheckBlob(briefRow["F_ICON"], icon);
  }

  public static CheckResult CheckSourceAttributes(
    int sourceAttributeID,
    DataRow briefRow,
    DataSet metaData,
    IUserSession session)
  {
    if (briefRow["F_SOURCE_ID"] != null && Convert.ToString(briefRow["F_SOURCE_ID"]) != string.Empty && Convert.ToInt32(briefRow["F_SOURCE_ID"]) > 0)
    {
      DataRow sourceAttributeRow = Helper.GetSourceAttributeRow(briefRow, metaData);
      IDBAttributeType attributeType = session.GetAttributeType(new Guid(Convert.ToString(sourceAttributeRow["F_GUID"])), false);
      if (attributeType == null)
        return CheckResult.NotFound;
      if (attributeType != null && attributeType.AttributeID != sourceAttributeID)
        return CheckResult.NotEqual;
    }
    else if (sourceAttributeID > 0)
      return CheckResult.NotEqual;
    return CheckResult.Equal;
  }

  public static CheckResult CheckMasterAttributes(
    int masterAttributeID,
    DataRow briefRow,
    DataSet metaData,
    IUserSession session)
  {
    if (briefRow["F_MASTER_ID"] != null && Convert.ToString(briefRow["F_MASTER_ID"]) != string.Empty && Convert.ToInt32(briefRow["F_MASTER_ID"]) > 0)
    {
      DataRow masterAttributeRow = Helper.GetMasterAttributeRow(briefRow, metaData);
      IDBAttributeType attributeType = session.GetAttributeType(new Guid(Convert.ToString(masterAttributeRow["F_GUID"])), false);
      if (attributeType == null)
        return CheckResult.NotFound;
      if (attributeType != null && attributeType.AttributeID != masterAttributeID)
        return CheckResult.NotEqual;
    }
    else if (masterAttributeID > 0)
      return CheckResult.NotEqual;
    return CheckResult.Equal;
  }
}
