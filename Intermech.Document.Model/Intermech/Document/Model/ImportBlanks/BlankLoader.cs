// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.BlankLoader
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Загрузчик бланка</summary>
[Serializable]
public class BlankLoader : PrimitiveLoader
{
  /// <summary>Список примитивов</summary>
  public List<PrimitiveBase> PrimitiveList = new List<PrimitiveBase>();
  /// <summary>Рабочая область</summary>
  public Area WorkSpace;
  /// <summary>Библиотечные примитивы отдельно от бланка</summary>
  public bool LinkToLib;
  /// <summary>Сигнатура бланка</summary>
  public static string MagicStr = "BLNK";
  /// <summary>Сигнатура бланка</summary>
  public static string BlankSign = "BLNK";
  [NonSerialized]
  private PrimLibraryLoader primLib;
  private BlankHeader head;

  /// <summary>Вывести отчет о загруженных примитивах</summary>
  /// <returns>Строка с отчетом</returns>
  public string Report()
  {
    string str = "";
    for (int index = 0; index < this.PrimitiveList.Count; ++index)
      str = str + Environment.NewLine + this.PrimitiveList[index].Report();
    return str;
  }

  /// <summary>Загрузить</summary>
  /// <param name="preReadedHeaderSignature">Сигнатура которая была зачитана ранее для распознавания формата файла</param>
  public override void Load(string preReadedHeaderSignature)
  {
    this.head = new BlankHeader(4);
    long position = this.Reader.BaseStream.Position;
    if (string.IsNullOrEmpty(preReadedHeaderSignature))
    {
      this.Reader.Read(this.head.Signature, 0, 4);
    }
    else
    {
      position -= (long) this.head.Signature.Length;
      this.head.Signature = preReadedHeaderSignature.ToCharArray();
    }
    if (this.head.SignatureStr != BlankLoader.MagicStr)
      throw new Exception(LocalizationHolder.rm.GetString("Document.Model_152"));
    this.head.HeaderLen = this.Reader.ReadUInt16();
    this.head.VersionNum = this.Reader.ReadUInt16();
    this.LoadingVersion = (int) this.head.VersionNum;
    PrimitiveLoader.GotoEndDataBlock(position, (long) this.head.HeaderLen, this.Reader);
    this.LinkToLib = this.head.VersionNum < (ushort) 110 || (int) this.head.VersionNum % 2 == 0;
    int num1 = this.Reader.ReadInt32();
    for (int index = 0; index < num1; ++index)
      this.PrimitiveList.Add(this.ReadPrimitive((GroupPrimitive) null));
    int num2 = this.Reader.ReadInt32();
    if (num2 == PrimitiveLoader.WorkSign || num2 == PrimitiveLoader.WorkSignStrings)
    {
      this.WorkSpace = this.ReadPrimitive((GroupPrimitive) null) as Area;
      if (this.LoadingVersion < 270)
        this.WorkSpace.needFrame = true;
    }
    else
      this.WorkSpace = (Area) null;
    this.ReplaceUserPrimitives();
  }

  /// <summary>Создать примитив на базе пользовательского заменив его на примитив из библиотеки</summary>
  /// <param name="userPrimitive">Пользовательский примитив</param>
  /// <returns></returns>
  public RectPrimitive CreatePrimitiveFromUserPrimitive(UserPrimitive userPrimitive)
  {
    RectPrimitive fromUserPrimitive = userPrimitive != null ? this.FindStrictUserPrimitive(userPrimitive) : throw new ArgumentNullException(nameof (userPrimitive));
    if (fromUserPrimitive != null)
    {
      fromUserPrimitive = (RectPrimitive) fromUserPrimitive.Clone();
      fromUserPrimitive.Org = userPrimitive.Org;
    }
    return fromUserPrimitive;
  }

  /// <summary>Заменить пользовательский примитив</summary>
  public virtual void ReplaceUserPrimitives()
  {
    for (int index = 0; index < this.PrimitiveList.Count; ++index)
    {
      if (this.PrimitiveList[index] is GroupPrimitive primitive2)
        primitive2.ReplaceUserPrimitives(this);
      else if (this.PrimitiveList[index] is UserPrimitive primitive1)
      {
        RectPrimitive fromUserPrimitive = this.CreatePrimitiveFromUserPrimitive(primitive1);
        if (fromUserPrimitive != null)
        {
          this.PrimitiveList[index] = (PrimitiveBase) fromUserPrimitive;
          if (this.PrimitiveList[index] is GroupPrimitive primitive)
            primitive.ReplaceUserPrimitives(this);
        }
        else
        {
          int num = (int) MessageBox.Show("Can't Find Library Primitive");
        }
      }
    }
    if (this.WorkSpace == null)
      return;
    this.WorkSpace.ReplaceUserPrimitives(this);
  }

  /// <summary>Найти пользовательский примитив</summary>
  /// <param name="prim">Пользовательский примитив</param>
  /// <returns>Реальный примитив</returns>
  internal RectPrimitive FindStrictUserPrimitive(UserPrimitive prim)
  {
    RectPrimitive strictUserPrimitive = this.PrimLib.GetByName(prim.Name);
    if (strictUserPrimitive != null && prim.Id != strictUserPrimitive.Id)
      strictUserPrimitive = (RectPrimitive) null;
    return strictUserPrimitive;
  }

  /// <summary>Сгенерировать документ</summary>
  /// <returns>Документ</returns>
  public ImDocument GeneateDocument()
  {
    ImDocument template = ImDocumentData.CreateTemplate(typeof (ImDocument), false) as ImDocument;
    template.SetAttributeValue("ConvertFromBLNVersion", this.head.VersionNum.ToString(), false, false, false);
    template.DefaultBorderLine.Width = 0.2f;
    template.SuspendUpdateUIGeometry();
    for (int index = 0; index < this.PrimitiveList.Count; ++index)
      (this.PrimitiveList[index] ?? throw new Exception(LocalizationHolder.rm.GetString("Document.Model_154"))).CreateNewDocumentNode((DocumentTreeNode) template);
    FlowID flowId = new FlowID(LocalizationHolder.rm.GetString("Document.Model_155"));
    template.DocumentFlows.Insert(0, flowId);
    string str = (string) null;
    BlankList blankList = (BlankList) null;
    for (int index = 0; index < this.PrimitiveList.Count; ++index)
    {
      if (this.PrimitiveList[index] is BlankList primitive && primitive.HasWorkspace)
      {
        str = primitive.Id;
        if (!primitive.CanBeFirst)
          break;
      }
    }
    if (str == null)
    {
      for (int index = 0; index < this.PrimitiveList.Count; ++index)
      {
        if (this.PrimitiveList[index] is BlankList primitive)
        {
          str = primitive.Id;
          if (!primitive.CanBeFirst)
            break;
        }
      }
    }
    blankList = (BlankList) null;
    bool flag = true;
    if (this.WorkSpace != null)
    {
      for (int index = 0; index < this.PrimitiveList.Count; ++index)
      {
        if (this.PrimitiveList[index] is BlankList primitive && primitive.HasWorkspace)
        {
          Page node = (Page) template.FindNode(primitive.Id);
          node.NextPageTemplateId = str;
          TableElement tableElement;
          if (flag)
          {
            Area workSpace1 = this.WorkSpace;
            Rectangle workspaceRect = primitive.WorkspaceRect;
            Point location = workspaceRect.Location;
            workSpace1.Org = location;
            Area workSpace2 = this.WorkSpace;
            workspaceRect = primitive.WorkspaceRect;
            Size size = workspaceRect.Size;
            workSpace2.Size = size;
            tableElement = (TableElement) this.WorkSpace.CreateNewDocumentNode((DocumentTreeNode) node);
            PrimitiveBase.SetNodeId((DocumentTreeNode) tableElement, LocalizationHolder.rm.GetString("Document.Model_157"));
            tableElement.AssignDrawGridToBottom(false, false);
            flag = false;
          }
          else
          {
            tableElement = new TableElement();
            tableElement.SetUsePreviousTableTemplates(true, false, false);
            PrimitiveBase.SetNodeId((DocumentTreeNode) tableElement, LocalizationHolder.rm.GetString("Document.Model_157"));
            tableElement.AssignDrawGridToBottom(false, false);
            node.AddChildNode((DocumentTreeNode) tableElement, false, false);
          }
          tableElement.Name = LocalizationHolder.rm.GetString("Document.Model_156");
          tableElement.AssignCloneByTemplateWithParent(true);
          tableElement.AssignBounds(PrimitiveBase.BlankUnitToMm(primitive.WorkspaceRect), false, false, false);
          tableElement.AssignMaxHeight(tableElement.Bounds.Height, false, false, true);
          tableElement.SetFlowID(flowId, false, false);
          tableElement.IsPageFlow = true;
        }
      }
    }
    template.FindAndLinkTextWithSomeBlankID();
    template.UpdateNodeAttributeLinks(true, false, false);
    template.SetPropertiesChangedFlag(false, true, false, false, false);
    template.AssignTreeStructureChangedFlag(false, true);
    template.UpdateLayout(0, true, false);
    template.ResumeUpdateRefreshUI(true, false);
    return template;
  }

  /// <summary>Найти и загрузить файл бланка</summary>
  /// <param name="fileName">Имя файла</param>
  /// <param name="defaultPath">Путь по умолчанию для поиска бланка</param>
  public void FindAndLoad(string fileName, string defaultPath)
  {
    string path = (string) null;
    string str1;
    if ((fileName.IndexOf("\\") >= 0 || fileName.IndexOf("/") >= 0) && File.Exists(path))
    {
      str1 = fileName;
    }
    else
    {
      fileName = Path.GetFileName(fileName);
      string str2 = defaultPath;
      if (str2 != null && str2 != "" && str2[str2.Length - 1] != '\\')
        str2 += "\\";
      str1 = str2 + fileName;
      if (str1 == null || str1 == "" || !File.Exists(str1))
      {
        string str3 = (string) Registry.LocalMachine.OpenSubKey("SOFTWARE\\InterMech\\BLANKS\\Blank Editor").GetValue("Blank_Directory");
        if (str3 != null && str3 != "" && str3[str3.Length - 1] != '\\')
          str3 += "\\";
        str1 = str3 + fileName;
        if (str1 == null || str1 == "" || !File.Exists(str1))
        {
          string startupPath = Application.StartupPath;
          str1 = startupPath + "\\BLANKS\\" + fileName;
          if (!File.Exists(str1))
          {
            str1 = startupPath + fileName;
            if (!File.Exists(str1))
            {
              str1 = $"{startupPath}\\Blanks2\\{fileName}";
              if (!File.Exists(str1))
                str1 = fileName;
            }
          }
        }
      }
    }
    if (!File.Exists(str1))
      str1 = (string) null;
    if (str1 == null)
      str1 = this.AskBlankPath(fileName);
    if (str1 == null || !(str1 != ""))
      throw new Exception(LocalizationHolder.rm.GetString("Document.Model_158"));
    this.LoadFile(str1);
  }

  /// <summary>Запросить путь к бланку</summary>
  /// <param name="startPath">Начальный путь</param>
  /// <returns>Путь к бланку</returns>
  protected string AskBlankPath(string startPath)
  {
    string str = (string) null;
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Title = LocalizationHolder.rm.GetString("Document.Model_159");
    openFileDialog.Filter = LocalizationHolder.rm.GetString("Document.Model_160");
    openFileDialog.InitialDirectory = Path.GetDirectoryName(startPath);
    openFileDialog.FileName = Path.GetFileName(startPath);
    openFileDialog.RestoreDirectory = true;
    if (openFileDialog.ShowDialog() == DialogResult.OK)
      str = openFileDialog.FileName;
    return str;
  }

  /// <summary>Получить примитив по идентификатору</summary>
  /// <param name="primId">Идентификатор</param>
  /// <returns>Примитив</returns>
  public PrimitiveBase PrimWithId(string primId)
  {
    PrimitiveBase primitiveBase = (PrimitiveBase) null;
    if (this.WorkSpace != null)
      primitiveBase = this.WorkSpace.FindById(primId);
    if (primitiveBase == null)
    {
      for (int index = 0; index < this.PrimitiveList.Count; ++index)
      {
        primitiveBase = this.PrimitiveList[index].FindById(primId);
        if (primitiveBase != null)
          break;
      }
    }
    return primitiveBase;
  }

  /// <summary>Библиотека примитивов</summary>
  public PrimLibraryLoader PrimLib
  {
    [DebuggerStepThrough] get
    {
      if (this.primLib == null)
      {
        this.primLib = new PrimLibraryLoader();
        this.primLib.FindAndLoad(Path.GetDirectoryName(this.LoadingFile));
      }
      return this.primLib;
    }
  }
}
