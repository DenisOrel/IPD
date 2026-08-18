// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDF.PDFFormatter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Tools.PDF;

internal sealed class PDFFormatter : OpenMetadataValueBagFormatter
{
  private static readonly Regex isCyrillic = new Regex("\\p{IsCyrillic}+", RegexOptions.IgnoreCase);

  public override bool IsContainerSupported(IValueBagContainer container)
  {
    return container is OpenPDFFile;
  }

  private OpenPDFFile GetOpenFile(IValueBagContainer container) => (OpenPDFFile) container;

  protected override ValueBag DoRead(IValueBagContainer container, ICollection<StringKey> valueKeys)
  {
    OpenPDFFile openFile = this.GetOpenFile(container);
    try
    {
      ValueBag valueBag = new ValueBag(valueKeys.Count);
      foreach (StringKey valueKey in (IEnumerable<StringKey>) valueKeys)
      {
        string internalName = this.ToInternalName(valueKey);
        string str;
        if (openFile.Reader.Info.TryGetValue(internalName, out str))
        {
          str = TextServices.Trim(str);
          if (str != null)
            valueBag.Add(valueKey, (object) str);
        }
      }
      return valueBag;
    }
    catch (BadPasswordException ex)
    {
      throw new FaultException($"Не удается прочитать значения атрибутов из файла '{openFile.FileName}. Файл защищен паролем.");
    }
  }

  protected override void DoWrite(
    IValueBagContainer container,
    ContainerValues values,
    ICollection<StringKey> changedValues)
  {
    if (container == null)
      throw new ArgumentNullException(nameof (container));
    if (values == null)
      throw new ArgumentNullException(nameof (values));
    if (changedValues == null)
      throw new ArgumentNullException(nameof (changedValues));
    OpenPDFFile openFile = this.GetOpenFile(container);
    try
    {
      if (openFile.Stamper.MoreInfo == null)
        openFile.Stamper.MoreInfo = (IDictionary<string, string>) new Dictionary<string, string>();
      foreach (ValueRecord valueRecord in values.Bag.FindAll((Predicate<ValueRecord>) (record => changedValues.Contains(record.Key))))
      {
        if (valueRecord.DataType == typeof (string))
        {
          string internalName = this.ToInternalName(valueRecord.Key);
          openFile.Stamper.MoreInfo[internalName] = valueRecord.Read<string>(string.Empty);
        }
      }
    }
    catch (BadPasswordException ex)
    {
      throw new FaultException(string.Format(LocalizationHolder.rm.GetString("SR_226"), (object) openFile.FileName));
    }
  }

  private string ToInternalName(StringKey key)
  {
    return PDFFormatter.isCyrillic.IsMatch((string) key) ? SQLStringHelper.Translit((string) key) : (string) key;
  }
}
