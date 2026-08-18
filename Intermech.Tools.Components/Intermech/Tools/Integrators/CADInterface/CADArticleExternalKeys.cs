// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADArticleExternalKeys
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Text;
using Intermech.Tools.Components.Properties;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public static class CADArticleExternalKeys
{
  internal static StringKey[] GetKeyNames()
  {
    return new StringKey[2]
    {
      (StringKey) CADDocumentResources.EMB_ArticleExternalKey,
      (StringKey) CADDocumentResources.EMB_ArticleLegacyExternalKey
    };
  }

  public static string GetExternalKey(ValueBag configurationAttributes, string configurationName)
  {
    if (string.IsNullOrEmpty(configurationName))
      throw new ArgumentException();
    string signedExternalKey = CADArticleExternalKeys.GetSignedExternalKey(configurationAttributes);
    if (!string.IsNullOrEmpty(signedExternalKey))
    {
      string externalKey;
      string configurationName1;
      CADArticleExternalKeys.ParseExternalKey(signedExternalKey, out externalKey, out configurationName1);
      if (!string.IsNullOrEmpty(externalKey) && string.Compare(configurationName1, TextServices.Trim(configurationName), true) == 0)
        return externalKey;
    }
    return (string) null;
  }

  internal static string GetSignedExternalKey(ValueBag configurationAttributes)
  {
    ValueRecord valueRecord1 = configurationAttributes != null ? configurationAttributes.Find((StringKey) CADDocumentResources.EMB_ArticleExternalKey) : throw new ArgumentNullException();
    if (valueRecord1 != null && !valueRecord1.IsNull && valueRecord1.DataType == typeof (string))
      return valueRecord1.Read<string>(string.Empty);
    ValueRecord valueRecord2 = configurationAttributes.Find((StringKey) CADDocumentResources.EMB_ArticleLegacyExternalKey);
    return valueRecord2 != null && !valueRecord2.IsNull && valueRecord2.DataType == typeof (string) ? valueRecord2.Read<string>(string.Empty) : (string) null;
  }

  internal static void UpdateSignedExternalKey(
    ValueBag configurationAttributes,
    string signedExternalKey,
    bool allowAppend,
    bool throwSetException)
  {
    ValueRecord valueRecord = configurationAttributes != null ? configurationAttributes.Find((StringKey) CADDocumentResources.EMB_ArticleLegacyExternalKey) : throw new ArgumentNullException();
    if (valueRecord != null && !valueRecord.IsNull && valueRecord.DataType == typeof (string))
      valueRecord.Value = (object) string.Empty;
    configurationAttributes.Update((StringKey) CADDocumentResources.EMB_ArticleExternalKey, (object) signedExternalKey, allowAppend);
    configurationAttributes.SetFlag((StringKey) CADDocumentResources.EMB_ArticleExternalKey, NamedFlags.ThrowSetException, throwSetException);
  }

  internal static string SignExternalKey(string externalKey, string configurationName)
  {
    return $"01_{externalKey.ToUpper()}_{TextServices.Trim(configurationName).ToUpper()}";
  }

  internal static void ParseExternalKey(
    string signedExternalKey,
    out string externalKey,
    out string configurationName)
  {
    if (!string.IsNullOrEmpty(signedExternalKey) && signedExternalKey.Length > 40 && signedExternalKey.StartsWith("01_"))
    {
      externalKey = signedExternalKey.Substring(3, 36);
      configurationName = signedExternalKey.Substring(40, signedExternalKey.Length - 40);
    }
    else
    {
      externalKey = string.Empty;
      configurationName = string.Empty;
    }
  }
}
