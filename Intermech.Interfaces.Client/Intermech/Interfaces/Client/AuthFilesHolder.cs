// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AuthFilesHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Checksums;
using System;
using System.IO;

#nullable disable
namespace Intermech.Interfaces.Client;

public class AuthFilesHolder
{
  /// <summary>Модуль настроек для аутентичных файлов</summary>
  public const string ModuleAuthFiles = "CLIENT";
  /// <summary>Секция настроек для аутентичных файлов</summary>
  public const string SectionAuthFiles = "AUTHFILES";
  /// <summary>
  /// Параметр алгоритма расчета контрольных сумм для аутентичных файлов
  /// </summary>
  public const string ParamAlgorithm = "ALGORITHM";
  /// <summary>
  /// Алгоритм расчета контрольных сумм для аутентичных файлов по умолчанию
  /// </summary>
  public const ChecksumAlgorithm DefaultAlgorithm = ChecksumAlgorithm.Crc32;
  /// <summary>
  /// Разрешать расчет контрольных сумм по алгоритмам, отличающимся от алгоритма по умолчанию ParamAlgorithm
  /// </summary>
  public const string ParamEnableChecksumAlternatives = "ENABLEALTERNATIVES";
  /// <summary>Разрешение по умолчанию</summary>
  public const bool DefaultEnableChecksumAlternatives = true;
  /// <summary>Список типов аутентичных файлов - напр. ".doc,.pdf"</summary>
  public const string AuthFilesExtensions = "AUTHFILESEXTENSIONS";
  /// <summary>Атрибут суффикса для имён аутентичных файлов</summary>
  public const string ParamAuthFilesSuffixAttributeGuid = "SUFFIXATTRIBUTEGUID";
  /// <summary>
  /// Атрибут суффикса для имён аутентичных файлов по умолчанию
  /// </summary>
  public static readonly string DefaultParamAuthFilesSuffixAttributeGuid = string.Empty;
  /// <summary>Имя файла с грифом секретности</summary>
  public const string FileNameWithSecrecyStamp = "FILENAMEWITHSECRECYSTAMP";
  /// <summary>
  /// Добавлять версию объекта к именам аутентичных файлов при сохранении
  /// </summary>
  public const string AddObjectVersionToAuthFilenamesWhenSave = "VERSION2AUTHFILENAME";
  /// <summary>
  /// Значение по умолчанию для добавления версии объекта к именам аутентичных файлов
  /// </summary>
  public const bool DefaultAddObjectVersionToAuthFilenamesWhenSave = false;
  /// <summary>Шаблон добавления версии к имени</summary>
  public const string AuthFilenameVersionTemplate = "[{0}]";
  /// <summary>Список по умолчанию</summary>
  public const string DefaultAuthFilesExtensions = "";
  /// <summary>
  /// Шаблон для перименования файла при назначении грифа секретности
  /// </summary>
  public const string DefaultFileNameWithSecrecyStamp = "<Гриф документа> <Наименование>.<Расширение файла>";
  /// <summary>
  /// Скобки, в которых записываются наименования атрибутов в шаблоне для назначения грифа документа.
  /// </summary>
  public const string LeftBracket = "<";
  public const string RightBracket = ">";
  public const string Extension = "Расширение файла";

  /// <summary>
  /// Читаем настройку AddObjectVersionToAuthFilenamesWhenSave - добавлять ли к именам аутентичных файлов версии объектов при сохранении на диск
  /// </summary>
  /// <returns></returns>
  public static bool GetAddObjectVersionToAuthFilenamesWhenSave()
  {
    return (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadBool("CLIENT", "AUTHFILES", "VERSION2AUTHFILENAME", false, DBConfigMode.GlobalOnly);
  }

  /// <summary>
  /// Читаем настройку ParamAuthFilesSuffixAttributeGuid - атрибут суффикса для аутентичных файлов, сохраняемых на диск
  /// </summary>
  /// <param name="imsAT"></param>
  /// <returns></returns>
  public static int GetSuffixAttributeForAuthFilenamesWhenSave(out IMSAttributeType imsAT)
  {
    int filenamesWhenSave = 0;
    imsAT = (IMSAttributeType) null;
    string g = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadString("CLIENT", "AUTHFILES", "SUFFIXATTRIBUTEGUID", AuthFilesHolder.DefaultParamAuthFilesSuffixAttributeGuid, DBConfigMode.GlobalOnly);
    if (g != string.Empty)
    {
      imsAT = MetaDataHelper.GetAttributeType(new Guid(g));
      if (imsAT != null)
        filenamesWhenSave = imsAT.AttributeID;
    }
    return filenamesWhenSave;
  }

  /// <summary>из c:\000\file.pdf сделает c:\000\file[versionId].pdf</summary>
  /// <param name="fname"></param>
  /// <param name="versionId"></param>
  /// <returns></returns>
  public static string GetAuthFilenamesWithVersion(string fname, int versionId)
  {
    return Path.Combine(Path.GetDirectoryName(fname), Path.GetFileNameWithoutExtension(fname) + $"[{versionId}]" + Path.GetExtension(fname));
  }

  public static string GetAuthFilenamesWithSuffix(string fname, string suffix)
  {
    return Path.Combine(Path.GetDirectoryName(fname), Path.GetFileNameWithoutExtension(fname) + suffix + Path.GetExtension(fname));
  }
}
