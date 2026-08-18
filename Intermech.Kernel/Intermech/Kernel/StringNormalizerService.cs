// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.StringNormalizerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public class StringNormalizerService : IStringNormalizer
{
  private bool deleteSpaces = true;
  private bool upperCase = true;
  private bool cyrillicReplace = true;
  private string[] deleteDuplicates = new string[0];
  private string[] replaceSymbols = new string[0];

  public StringNormalizerService() => this.LoadSettings();

  public void LoadSettings()
  {
    using (IDbManager dbManager = (ServerServices.GetService(typeof (IDbManagerService)) as IDbManagerService).CreateDbManager())
    {
      foreach (DataRow row in (InternalDataCollectionBase) dbManager.ExecuteDataTable("SELECT F_PARAM_NAME, F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = 'KERNEL' AND F_USER_ID = 0 AND F_SECTION_ID = 'INDEX_PARAMS'").Rows)
      {
        if (row["F_PARAM_NAME"].ToString() == "DEL_SPACES")
          this.deleteSpaces = Convert.ToBoolean(row["F_VALUE"]);
        else if (row["F_PARAM_NAME"].ToString() == "UPPER_CASE")
          this.upperCase = Convert.ToBoolean(row["F_VALUE"]);
        else if (row["F_PARAM_NAME"].ToString() == "CYRILLIC")
          this.cyrillicReplace = Convert.ToBoolean(row["F_VALUE"]);
        else if (row["F_PARAM_NAME"].ToString() == "DUPLICATES")
          this.deleteDuplicates = row["F_VALUE"].ToString().Split('|');
        else if (row["F_PARAM_NAME"].ToString() == "REPLACES")
        {
          this.replaceSymbols = row["F_VALUE"].ToString().Split('|');
          if (this.replaceSymbols.Length == 1)
            this.replaceSymbols = new string[0];
        }
      }
      ServerStringNormalizer.LoadSettings(this.GetSettings());
    }
  }

  public NormalizerSettings GetSettings()
  {
    return new NormalizerSettings(this.deleteSpaces, this.upperCase, this.cyrillicReplace, this.deleteDuplicates, this.replaceSymbols);
  }

  public string GetIndexedString(string str_to_index)
  {
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder1 = objectPoolScope.Object;
      stringBuilder1.Append(str_to_index);
      if (this.deleteSpaces)
      {
        stringBuilder1.Replace(" ", string.Empty);
        stringBuilder1.Replace(Convert.ToChar(160 /*0xA0*/).ToString(), string.Empty);
        stringBuilder1.Replace(Environment.NewLine, string.Empty);
      }
      if (stringBuilder1.Length > 0)
      {
        for (int index = 0; index < this.deleteDuplicates.Length; ++index)
        {
          if (this.deleteDuplicates[index] != string.Empty)
          {
            while (stringBuilder1.ToString().IndexOf(this.deleteDuplicates[index] + this.deleteDuplicates[index]) >= 0)
              stringBuilder1.Replace(this.deleteDuplicates[index] + this.deleteDuplicates[index], this.deleteDuplicates[index]);
          }
        }
        int num1 = 0;
        while (num1 < this.replaceSymbols.Length)
        {
          StringBuilder stringBuilder2 = stringBuilder1;
          string[] replaceSymbols1 = this.replaceSymbols;
          int index1 = num1;
          int num2 = index1 + 1;
          string oldValue = replaceSymbols1[index1];
          string[] replaceSymbols2 = this.replaceSymbols;
          int index2 = num2;
          num1 = index2 + 1;
          string newValue = replaceSymbols2[index2];
          stringBuilder2.Replace(oldValue, newValue);
        }
        if (this.upperCase)
        {
          stringBuilder1 = new StringBuilder(stringBuilder1.ToString().ToUpper());
          if (this.cyrillicReplace)
          {
            for (int index = 0; index < ServerStringNormalizer.RusLettersUpper.Length; ++index)
              stringBuilder1.Replace(ServerStringNormalizer.RusLettersUpper[index], ServerStringNormalizer.LatLettersUpper[index]);
          }
        }
        else if (this.cyrillicReplace)
        {
          for (int index = 0; index < ServerStringNormalizer.RusLettersAll.Length; ++index)
            stringBuilder1.Replace(ServerStringNormalizer.RusLettersAll[index], ServerStringNormalizer.LatLettersAll[index]);
        }
      }
      return stringBuilder1.ToString();
    }
  }
}
