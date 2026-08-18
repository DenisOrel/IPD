// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.UserSessionMetadataUpdateExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.Interfaces;

public static class UserSessionMetadataUpdateExtensions
{
  [Obsolete("[Переименование] Используйте метод CheckMaximumPluginDbVersion с теми же параметрами")]
  public static bool CheckPluginDbVersion(
    [NotNull] this IUserSession session,
    [NotNull] string moduleName,
    int maximumVersion,
    int maximumRevision,
    bool quiet = false)
  {
    return session.CheckMaximumPluginDbVersion(moduleName, maximumVersion, maximumRevision, quiet);
  }

  public static bool CheckMaximumPluginDbVersion(
    [NotNull] this IUserSession userSession,
    [NotNull, NotWhitespace] string moduleName,
    [ZeroOrPositiveNumber] int maximumVersion,
    [ZeroOrPositiveNumber] int maximumRevision,
    bool quiet = false)
  {
    UserSession userSession1 = userSession as UserSession;
    int num1 = 0;
    int num2 = 0;
    string moduleName1 = moduleName;
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    userSession1.GetDBVersionEx(moduleName1, ref local1, ref local2);
    if (maximumVersion >= num1 && (maximumVersion != num1 || maximumRevision >= num2))
      return true;
    if (quiet)
      return false;
    throw new WrongPluginVersionException(moduleName, maximumVersion, new int?(maximumRevision), $"Обнаружена база данных плагина {moduleName} более свежей версии ({num1}.{num2}), " + $"чем та, для которой разработан данный плагин ({maximumVersion}.{maximumRevision})");
  }

  public static bool IsRightDbVersion(
    [NotNull] this IUserSession userSession,
    [NotNull, NotWhitespace] string moduleName,
    [NotNull, NotWhitespace] string wantedVersions)
  {
    UserSession userSession1 = userSession as UserSession;
    int num1 = -1;
    int num2 = -1;
    string moduleName1 = moduleName;
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    userSession1.GetDBVersionEx(moduleName1, ref local1, ref local2);
    string str1 = wantedVersions;
    char[] chArray = new char[1]{ ',' };
    foreach (string str2 in str1.Split(chArray))
    {
      string[] strArray = str2.Trim().Split('.');
      if (strArray.Length != 0)
      {
        int int32 = Convert.ToInt32(strArray[0]);
        int num3 = strArray.Length > 1 ? Convert.ToInt32(strArray[1]) : -1;
        int num4 = num1;
        if (int32 == num4 && (num3 == -1 || num3 == num2))
          return true;
      }
    }
    return false;
  }

  public static bool IsRightDbVersion(
    [NotNull] this IUserSession userSession,
    [NotNull, NotWhitespace] string moduleName,
    [ZeroOrPositiveNumber] int wantedVersion,
    [ZeroOrPositiveNumber] int wantedRevision)
  {
    UserSession userSession1 = userSession as UserSession;
    int num1 = -1;
    int num2 = -1;
    string moduleName1 = moduleName;
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    userSession1.GetDBVersionEx(moduleName1, ref local1, ref local2);
    return num1 == wantedVersion && num2 == wantedRevision;
  }

  [NotNull]
  [MustUseReturnValue]
  public static IMetadataUpdateKeeper UpdateDbVersion(
    [NotNull] this IUserSession userSession,
    [NotNull, NotWhitespace] string moduleName,
    bool inTransaction,
    [ZeroOrPositiveNumber] int updateVersion,
    [ZeroOrPositiveNumber] int updateRevision,
    bool saveNewDbVersion = true)
  {
    return (IMetadataUpdateKeeper) new MetadataUpdateKeeper(userSession as UserSession, moduleName, inTransaction, updateVersion, updateRevision, saveNewDbVersion);
  }

  [NotNull]
  [MustUseReturnValue]
  public static IMetadataUpdateKeeper UpdateDbVersion(
    [NotNull] this IUserSession userSession,
    [NotNull, NotWhitespace] string moduleName,
    bool inTransaction,
    [ZeroOrPositiveNumber] int updateRevision,
    bool saveNewDbVersion = true)
  {
    return (IMetadataUpdateKeeper) new MetadataUpdateKeeper(userSession as UserSession, moduleName, inTransaction, updateRevision, 0, saveNewDbVersion);
  }

  public static bool CheckPluginDbVersion(
    [NotNull] this IUserSession userSession,
    [NotNull, NotWhitespace] string moduleName,
    [ZeroOrPositiveNumber] int needVersion,
    [CanBeNull, ZeroOrPositiveNumber] int? needRevision,
    bool quiet = false)
  {
    UserSession userSession1 = userSession as UserSession;
    int num1 = needRevision.HasValue ? 1 : 0;
    int num2 = 0;
    int num3 = 0;
    string moduleName1 = moduleName;
    ref int local1 = ref num2;
    ref int local2 = ref num3;
    userSession1.GetDBVersionEx(moduleName1, ref local1, ref local2);
    if (needVersion == num2 && (needVersion != num2 || !needRevision.HasValue || needRevision.Value == num3))
      return true;
    if (quiet)
      return false;
    throw new WrongPluginVersionException(moduleName, needVersion, needRevision, needRevision.HasValue ? $"{$"Обнаружена база данных плагина {moduleName} версии ({num2}.{num3}), отличной от той, "}{$"для которой разработан данный плагин ({needVersion}.{needRevision.Value}). "}Возможно патч БД не прошёл успешно" : $"Обнаружена база данных плагина {moduleName} версии ({num2}.{num3}), отличной от той, " + $"для которой разработан данный плагин ({needVersion}). Возможно патч БД не прошёл успешно");
  }
}
