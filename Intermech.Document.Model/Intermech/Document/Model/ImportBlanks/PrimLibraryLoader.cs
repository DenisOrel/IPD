// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.PrimLibraryLoader
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Загрузчик примитивов из библиотеки</summary>
public class PrimLibraryLoader : PrimitiveLoader
{
  /// <summary>Сигнатура бланка</summary>
  public static string MagicStr = "PLIB";
  /// <summary>Примитивы библиотеки</summary>
  public List<RectPrimitive> Storage = new List<RectPrimitive>();

  /// <summary>Коструктор</summary>
  public PrimLibraryLoader()
  {
  }

  /// <summary>Коструктор</summary>
  /// <param name="libFileName">Имя файла библиотеки</param>
  public PrimLibraryLoader(string libFileName)
  {
    if (libFileName == null)
      return;
    this.LoadFile(libFileName);
  }

  /// <summary>Найти и загрузить библиотеку</summary>
  /// <param name="startPath">Начальный путь для поиска</param>
  public void FindAndLoad(string startPath)
  {
    if (startPath != null && startPath != "" && startPath[startPath.Length - 1] != '\\')
      startPath += "\\";
    string str1 = "IM_STD.LIB";
    string str2 = startPath + str1;
    if (!File.Exists(str2))
    {
      str2 = (string) Registry.LocalMachine.OpenSubKey("SOFTWARE\\InterMech\\BLANKS\\Primitive Library").GetValue("Library File");
      if (str2 == null || str2 == "" || !File.Exists(str2))
      {
        string startupPath = Application.StartupPath;
        str2 = startupPath + "\\BLANKS\\" + str1;
        if (!File.Exists(str2))
        {
          str2 = startupPath + str1;
          if (!File.Exists(str2))
          {
            str2 = $"{startupPath}\\Blanks2\\{str1}";
            if (!File.Exists(str2))
              str2 = (string) null;
          }
        }
      }
    }
    if (str2 == null)
      str2 = this.AskPrimitiveLibPath(startPath);
    if (str2 == null || !(str2 != ""))
      return;
    this.LoadFile(str2);
  }

  /// <summary>Запросить путь к библиотеке</summary>
  /// <param name="startPath">Начальный путь</param>
  /// <returns>Путь к библиотеке</returns>
  protected string AskPrimitiveLibPath(string startPath)
  {
    string str = (string) null;
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Title = LocalizationHolder.rm.GetString("Document.Model_502");
    openFileDialog.Filter = LocalizationHolder.rm.GetString("Document.Model_503");
    openFileDialog.InitialDirectory = startPath;
    openFileDialog.RestoreDirectory = true;
    if (openFileDialog.ShowDialog() == DialogResult.OK)
      str = openFileDialog.FileName;
    return str;
  }

  /// <summary>Загрузить</summary>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  public override void Load(string preReadedHeaderSignature)
  {
    LibHeader libHeader = new LibHeader(4);
    long position = this.Reader.BaseStream.Position;
    if (string.IsNullOrEmpty(preReadedHeaderSignature))
    {
      this.Reader.Read(libHeader.Signature, 0, 4);
    }
    else
    {
      position -= (long) libHeader.Signature.Length;
      libHeader.Signature = preReadedHeaderSignature.ToCharArray();
    }
    if (libHeader.SignatureStr != PrimLibraryLoader.MagicStr)
      throw new Exception(LocalizationHolder.rm.GetString("Document.Model_504"));
    libHeader.HeaderLen = this.Reader.ReadUInt16();
    libHeader.VersionNum = this.Reader.ReadUInt16();
    this.LoadingVersion = (int) libHeader.VersionNum;
    PrimitiveLoader.GotoEndDataBlock(position, (long) libHeader.HeaderLen, this.Reader);
    int num = this.Reader.ReadInt32();
    string str1 = (string) null;
    for (int index = 0; index < num; ++index)
    {
      this.Reader.ReadInt32();
      string str2 = this.ReadString();
      RectPrimitive rectPrimitive;
      if (this.Reader.ReadBoolean())
      {
        rectPrimitive = (RectPrimitive) null;
        str1 = str2;
      }
      else
        rectPrimitive = (RectPrimitive) this.ReadPrimitive((GroupPrimitive) null);
      if (rectPrimitive != null)
      {
        rectPrimitive.SetAdditionalAttribute(PrimitiveBase.AttributeGroupName, str1);
        this.Storage.Add(rectPrimitive);
      }
    }
  }

  /// <summary>Получить примитив по имени</summary>
  /// <param name="name">Имя примитива</param>
  /// <returns>Примитив</returns>
  public RectPrimitive GetByName(string name)
  {
    RectPrimitive byName = (RectPrimitive) null;
    for (int index = 0; index < this.Storage.Count; ++index)
    {
      RectPrimitive rectPrimitive = this.Storage[index];
      if (rectPrimitive != null && rectPrimitive.Name == name)
      {
        byName = rectPrimitive;
        break;
      }
    }
    return byName;
  }

  /// <summary>Сгенерировать документ</summary>
  /// <returns>Документ</returns>
  public ImDocument GeneateDocument()
  {
    ImDocument parentDocNode = new ImDocument((ImDocument) null, false, false);
    parentDocNode.AssignIsFormulaLib(true);
    parentDocNode.DefaultBorderLine.Width = 0.2f;
    parentDocNode.SuspendUpdateUIGeometry();
    for (int index = 0; index < this.Storage.Count; ++index)
    {
      string name = this.Storage[index].Name;
      string id = this.Storage[index].Id;
      if (!string.IsNullOrEmpty(name) && name[0] == '#')
      {
        string str = name.Remove(0, 1);
        Page page;
        if (this.Storage[index] is Area area)
        {
          page = area.CreateAsPage((DocumentTreeNode) parentDocNode);
          page.AutoSize = true;
        }
        else
        {
          page = new Page();
          page.AutoSize = true;
          parentDocNode.AddChildNode((DocumentTreeNode) page, false, false, false, false);
          this.Storage[index].CreateNewDocumentNode((DocumentTreeNode) page);
        }
        page.SetName(id, false, false);
        page.SetAttributeValue("BLN.ID", id, false, false, false);
        if (!string.IsNullOrEmpty(str))
        {
          page.SetAttributeValue("BLN.NAME", str, false, false, false);
          if (page.IdService != null)
            page.Id = page.IdService.GenerateUniqueId((object) str).ToString();
          else
            page.Id = str;
        }
      }
    }
    parentDocNode.UpdateNodeAttributeLinks(true, false, false);
    parentDocNode.SetPropertiesChangedFlag(false, true, false, false, false);
    parentDocNode.AssignTreeStructureChangedFlag(false, true);
    parentDocNode.UpdateLayout(0, true, false);
    parentDocNode.ResumeUpdateRefreshUI(true, false);
    return parentDocNode;
  }

  public static string File_Sign => PrimLibraryLoader.MagicStr;
}
