// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StructFileParser
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class StructFileParser
{
  public DataTable ParseFile(FileContent fileContent, FileContent fieldLayout)
  {
    if (fileContent == null)
      throw new ArgumentNullException(nameof (fileContent), Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_14"));
    List<StructFileParser.StructFileField> structFileFieldList = fieldLayout != null ? this.ParseFieldsLayout(fieldLayout) : throw new ArgumentNullException(nameof (fieldLayout), Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_15"));
    this.CheckSyntax(fileContent, fieldLayout, structFileFieldList);
    DataTable structTable = this.CreateStructTable(structFileFieldList);
    this.ParseFileContent(fileContent, structFileFieldList, structTable);
    return structTable;
  }

  private void CheckSyntax(
    FileContent fileContent,
    FileContent fieldLayout,
    List<StructFileParser.StructFileField> fields)
  {
    if (fileContent.Lines.Length == 0)
      return;
    int num = 0;
    for (int index = 0; index < fields.Count; ++index)
      num += fields[index].Length;
    for (int index = 0; index < fileContent.Lines.Length; ++index)
    {
      int length = fileContent.Lines[index].Length;
      if (length != num)
        throw new StructFileParsingException($"Неправильный формат обменного файла: длина строки {index + 1}, равная {length} символам, не соответствует сумме длин полей в файле '{fieldLayout.FileName}', равной {num} символам.");
    }
  }

  public string ToFile(DataTable structTable, FileContent fieldLayout)
  {
    if (structTable == null)
      throw new ArgumentNullException(nameof (structTable), Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_16"));
    List<StructFileParser.StructFileField> structFileFieldList = fieldLayout != null ? this.ParseFieldsLayout(fieldLayout) : throw new ArgumentNullException(nameof (fieldLayout), Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_17"));
    StringBuilder stringBuilder = new StringBuilder();
    for (int index1 = 0; index1 < structTable.Rows.Count; ++index1)
    {
      DataRow row = structTable.Rows[index1];
      for (int index2 = 0; index2 < structFileFieldList.Count; ++index2)
      {
        StructFileParser.StructFileField fileField = structFileFieldList[index2];
        string str = this.AddSpaces(structTable.Columns.Contains(fileField.Name) ? Convert.ToString(row[fileField.Name]) : string.Empty, fileField);
        stringBuilder.Append(str);
      }
      stringBuilder.AppendLine();
    }
    return stringBuilder.ToString();
  }

  private List<StructFileParser.StructFileField> ParseFieldsLayout(FileContent fieldLayout)
  {
    List<StructFileParser.StructFileField> fieldsLayout = new List<StructFileParser.StructFileField>();
    int num = 0;
    for (int lineIndex = 0; lineIndex < fieldLayout.Lines.Length; ++lineIndex)
    {
      StructFileParser.StructFileField field = this.ParseField(fieldLayout.Lines[lineIndex].Trim(), lineIndex, fieldLayout);
      field.Offset = num;
      num += field.Length;
      fieldsLayout.Add(field);
    }
    return fieldsLayout;
  }

  private StructFileParser.StructFileField ParseField(
    string line,
    int lineIndex,
    FileContent fieldLayout)
  {
    string[] strArray = line.Split(TextServices.WordsSplitPatterns, StringSplitOptions.RemoveEmptyEntries);
    string str1 = strArray.Length == 2 ? strArray[0] : throw new StructFileConfigException(fieldLayout.FileName, string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_18"), (object) line));
    string str2 = strArray[1];
    if (str2.Length != 7)
      throw new StructFileConfigException(fieldLayout.FileName, string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_19"), (object) str1, (object) str2));
    if (str2[0] != 'C' && str2[0] != 'c')
      throw new StructFileConfigException(fieldLayout.FileName, string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_20"), (object) str1));
    int result;
    if (!int.TryParse(str2.Substring(1, 3), out result))
      throw new StructFileConfigException(fieldLayout.FileName, string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_21"), (object) str1, (object) str2));
    return new StructFileParser.StructFileField(str1.ToUpper(), result);
  }

  private DataTable CreateStructTable(List<StructFileParser.StructFileField> fileFields)
  {
    DataTable structTable = new DataTable();
    structTable.TableName = "StructTable";
    for (int index = 0; index < fileFields.Count; ++index)
    {
      DataColumn column = new DataColumn(fileFields[index].Name, typeof (string));
      structTable.Columns.Add(column);
    }
    return structTable;
  }

  private void ParseFileContent(
    FileContent fileContent,
    List<StructFileParser.StructFileField> fileFields,
    DataTable structTable)
  {
    for (int index1 = 0; index1 < fileContent.Lines.Length; ++index1)
    {
      DataRow row = structTable.NewRow();
      for (int index2 = 0; index2 < fileFields.Count; ++index2)
      {
        StructFileParser.StructFileField fileField = fileFields[index2];
        try
        {
          string str = fileContent.Lines[index1].Substring(fileField.Offset, fileField.Length);
          row[fileField.Name] = (object) str.Trim();
        }
        catch (ArgumentOutOfRangeException ex)
        {
          throw new StructFileParsingException(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_22"), (object) fileField.Name, (object) ex.Message));
        }
      }
      structTable.Rows.Add(row);
    }
    structTable.AcceptChanges();
  }

  private string AddSpaces(string text, StructFileParser.StructFileField fileField)
  {
    int count = fileField.Length - text.Length;
    if (count < 0)
      throw new StructFileException(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_23"), (object) fileField.Name, (object) text, (object) fileField.Length));
    return text + new string(' ', count);
  }

  private class StructFileField
  {
    private string name;
    private int offset;
    private int length;

    public StructFileField(string name, int length)
    {
      this.name = name;
      this.length = length;
    }

    public string Name => this.name;

    public int Offset
    {
      get => this.offset;
      set => this.offset = value;
    }

    public int Length => this.length;
  }
}
