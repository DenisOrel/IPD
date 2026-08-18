// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.SynchStrHelper
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using Intermech.Kernel.Search;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// 
/// </summary>
public static class SynchStrHelper
{
  /// <summary>Все значения.</summary>
  public static string AllValues { get; private set; }

  /// <summary>Синхронизирован.</summary>
  public static string Synchronized { get; private set; }

  /// <summary>Не синхронизирован.</summary>
  public static string NotSynchronized { get; private set; }

  /// <summary>Не нуждается в синхронизации.</summary>
  public static string NotNeedToSync { get; private set; }

  /// <summary>F_OBJECT_ID.</summary>
  public static string COLUMN_NAME_OBJECT_ID
  {
    get => Convert.ToString((object) ObligatoryObjectAttributes.F_OBJECT_ID);
  }

  /// <summary>CAPTION.</summary>
  public static string COLUMN_NAME_CAPTION
  {
    get => Convert.ToString((object) ObligatoryObjectAttributes.CAPTION);
  }

  /// <summary>
  /// 
  /// </summary>
  public static string COLUMN_NAME_IMBASE_OBJECT_REF
  {
    get => Convert.ToString(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
  }

  /// <summary>
  /// 
  /// </summary>
  public static string COLUMN_NAME_IMBASE_TABLE_REF => Convert.ToString(Intermech.Imbase.Consts.ImbaseTableRefAttID);

  /// <summary>
  /// 
  /// </summary>
  public static string COLUMN_NAME_RECORD_ID => Convert.ToString(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);

  /// <summary>
  /// 
  /// </summary>
  public static string COLUMN_NAME_CLASSIF_KEY => Convert.ToString(Intermech.Imbase.Consts.ClassifFolderKeyAttId);

  /// <summary>STATUS.</summary>
  public static string COLUMN_NAME_STATUS => "STATUS";

  /// <summary>REPORT.</summary>
  public static string COLUMN_NAME_REPORT => "REPORT";

  /// <summary>IMBASE_OBJECT_ID.</summary>
  public static string COLUMN_NAME_IMBASE_ID => "IMBASE_OBJECT_ID";

  /// <summary>IMBASE_OBJECT_CAPTION.</summary>
  public static string COLUMN_NAME_IMBASE_CAPTION => "IMBASE_OBJECT_CAPTION";

  /// <summary>Конструктор.</summary>
  static SynchStrHelper()
  {
    SynchStrHelper.AllValues = LocalizationHolder.rm.GetString("Imbase_AllValues");
    SynchStrHelper.Synchronized = LocalizationHolder.rm.GetString("Imbase_Synchronized");
    SynchStrHelper.NotSynchronized = LocalizationHolder.rm.GetString("Imbase_NotSynchronized");
    SynchStrHelper.NotNeedToSync = LocalizationHolder.rm.GetString("Imbase_NotNeedToSync");
  }
}
