// Decompiled with JetBrains decompiler
// Type: Intermech.MSOffice.MSOfficeDocumentIndexer
// Assembly: Intermech.MSOffice.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D19FBC55-F588-4D57-844C-DE1B05B4B055
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MSOffice.Server.dll

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Kernel.GlobalIndex;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.MSOffice;

internal sealed class MSOfficeDocumentIndexer : CustomFileConverter
{
  private int attrId = -1;

  public override string Caption => "Конвертер документов MSOffice";

  public override string[] SupportedFileExtensions
  {
    get => new string[2]{ ".DOCX", ".XLSX" };
  }

  public override string GetPlainText(IDBAttribute attribute)
  {
    string upper = Path.GetExtension(attribute.AsString).ToUpper();
    IBlobReader blobReader = attribute as IBlobReader;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    if (blobInformation.PackedFileSize == 0L)
    {
      blobReader.CloseBlob();
      return string.Empty;
    }
    byte[] numArray = new byte[blobInformation.PackedFileSize];
    byte[] buffer = blobReader.ReadDataBlock();
    string empty = string.Empty;
    if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
    {
      IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      Stream inStream = (Stream) new MemoryStream(buffer);
      try
      {
        ImChunkedStream outStream = new ImChunkedStream();
        try
        {
          service.UnpackStream((Stream) outStream, inStream);
          return this.Read((Stream) outStream, upper);
        }
        finally
        {
          outStream.Close();
        }
      }
      finally
      {
        inStream.Close();
      }
    }
    else
    {
      Stream stream = (Stream) new MemoryStream(buffer);
      try
      {
        return this.Read(stream, upper);
      }
      finally
      {
        stream.Close();
      }
    }
  }

  private string Read(Stream stream, string @extension)
  {
    if (@extension.ToLower() == ".xlsx")
      return this.ReadXLS(stream);
    return @extension.ToLower() == ".docx" ? this.ReadDOC(stream) : string.Empty;
  }

  private string ReadDOC(Stream stream)
  {
    StringBuilder stringBuilder = new StringBuilder();
    using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(stream, false))
    {
      NameTable nameTable = new NameTable();
      XmlNamespaceManager nsmgr = new XmlNamespaceManager((XmlNameTable) nameTable);
      nsmgr.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
      XmlDocument xmlDocument = new XmlDocument((XmlNameTable) nameTable);
      xmlDocument.Load(wordprocessingDocument.MainDocumentPart.GetStream());
      foreach (XmlNode selectNode1 in xmlDocument.SelectNodes("//w:p", nsmgr))
      {
        foreach (XmlNode selectNode2 in selectNode1.SelectNodes(".//w:t", nsmgr))
        {
          if (selectNode2.InnerText != null)
          {
            string str = selectNode2.InnerText.Trim();
            if (str != string.Empty)
            {
              stringBuilder.Append(str);
              stringBuilder.Append(" ; ");
            }
          }
        }
        stringBuilder.Append("  ");
      }
    }
    return stringBuilder.ToString();
  }

  public string ReadXLS(Stream stream)
  {
    StringBuilder stringBuilder = new StringBuilder();
    using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, false))
    {
      WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
      foreach (Sheet descendant1 in workbookPart.Workbook.Descendants<Sheet>())
      {
        if (descendant1 == null)
          throw new ArgumentException("sheetName");
        foreach (Cell descendant2 in ((WorksheetPart) workbookPart.GetPartById((string) descendant1.Id)).Worksheet.Descendants<Cell>())
        {
          if (descendant2 != null)
          {
            string s = descendant2.InnerText;
            if (descendant2.DataType != null)
            {
              switch (descendant2.DataType.Value)
              {
                case CellValues.Boolean:
                  s = !(s == "0") ? "TRUE" : "FALSE";
                  break;
                case CellValues.SharedString:
                  SharedStringTablePart sharedStringTablePart = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault<SharedStringTablePart>();
                  if (sharedStringTablePart != null)
                  {
                    s = sharedStringTablePart.SharedStringTable.ElementAt<OpenXmlElement>(int.Parse(s)).InnerText;
                    break;
                  }
                  break;
              }
            }
            if (s != null)
            {
              string str = s.Trim();
              if (str != string.Empty)
              {
                stringBuilder.Append(str);
                stringBuilder.Append(" ; ");
              }
            }
          }
        }
      }
    }
    return stringBuilder.ToString();
  }
}
