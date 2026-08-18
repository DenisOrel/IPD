// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.PrimitiveBase
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Базовый класс примитива</summary>
[Serializable]
public class PrimitiveBase : ICloneable
{
  /// <summary>Имя примитива</summary>
  public string name;
  /// <summary>Дополнительные атрибуты</summary>
  private Dictionary<string, string> additionalAttributes;
  public static string AttributeGroupName = "GroupName";
  /// <summary>Положение примитива</summary>
  public Point Org = Point.Empty;
  /// <summary>Идентификатор</summary>
  public string id = "";
  /// <summary>Горизонтальное выравнивание примитива</summary>
  public HorAlignment horAlign;
  /// <summary>Вертикальное выравнивание примитива</summary>
  public VertAlignment vertAlign;
  /// <summary>Владелец</summary>
  [NonSerialized]
  public GroupPrimitive Owner;
  /// <summary>Узел документа</summary>
  [NonSerialized]
  public DocumentTreeNode DocumentNode;
  /// <summary>Узел данных документа</summary>
  [NonSerialized]
  public DocumentTreeNode DocumentDataNode;

  /// <summary>Имя примитива</summary>
  public string Name
  {
    [DebuggerStepThrough] get => this.name;
  }

  /// <summary>Установить дополнительный атрибут</summary>
  /// <param name="name">Имя</param>
  /// <param name="value">Значение</param>
  public void SetAdditionalAttribute(string name, string value)
  {
    if (this.additionalAttributes == null)
      this.additionalAttributes = new Dictionary<string, string>();
    this.additionalAttributes[name] = value;
  }

  /// <summary>Положение примитива</summary>
  public PointF OrgMm
  {
    [DebuggerStepThrough] get => new PointF((float) this.Org.X / 20f, (float) this.Org.Y / 20f);
  }

  /// <summary>Идентификатор</summary>
  public string Id
  {
    [DebuggerStepThrough] get => this.id;
  }

  internal static void SetNodeId(DocumentTreeNode docNode, string id)
  {
    if (id.IsEmpty<char>())
      return;
    string str = id;
    if (id != docNode.Id && docNode.IdService != null)
      str = docNode.IdService.GenerateUniqueId((object) id).ToString();
    docNode.Id = str;
    docNode.SetAttributeValue("BLN.ID", id, false, false, false);
  }

  public void SetNodeId(DocumentTreeNode docNode) => PrimitiveBase.SetNodeId(docNode, this.Id);

  /// <summary>Горизонтальное выравнивание примитива</summary>
  public HorAlignment HorAlign
  {
    [DebuggerStepThrough] get => this.horAlign;
  }

  /// <summary>Вертикальное выравнивание примитива</summary>
  public VertAlignment VertAlign
  {
    [DebuggerStepThrough] get => this.vertAlign;
  }

  /// <summary>Имя класса</summary>
  public string ClassName
  {
    [DebuggerStepThrough] get => this.GetType().Name;
  }

  /// <summary>Конструктор</summary>
  /// <param name="owner">Владелец</param>
  public PrimitiveBase(GroupPrimitive owner) => this.Owner = owner;

  /// <summary>Конструктор</summary>
  public PrimitiveBase()
  {
  }

  /// <summary>Преобразовать два байта в word</summary>
  /// <param name="low">Нижний байт</param>
  /// <param name="hi">Верхний байт</param>
  /// <returns>Слово</returns>
  public static int ConvertBytesToWord(byte low, byte hi) => ((int) hi << 8) + (int) low;

  /// <summary>Создать строку идентификатора из массива байт</summary>
  /// <param name="id">Идентификатор в форме массива байт</param>
  /// <returns>Идентификатор в форме строки</returns>
  public static string MakeIdStr(byte[] id)
  {
    Encoding encoding = Encoding.GetEncoding(1251);
    string str = "";
    bool flag = false;
    int word = PrimitiveBase.ConvertBytesToWord(id[0], id[1]);
    for (int index = 0; index < 4; ++index)
    {
      if (id[index] < (byte) 32 /*0x20*/ && id[index] != (byte) 0)
      {
        flag = true;
        break;
      }
      if (id[index] == (byte) 0 && word < 8192 /*0x2000*/ && index < 3 && id[index + 1] != (byte) 0)
      {
        flag = true;
        break;
      }
    }
    if (((word >= 8192 /*0x2000*/ ? 0 : (word > 1000 ? 1 : 0)) | (flag ? 1 : 0)) != 0)
    {
      if (id[2] != (byte) 0)
        str = new string(encoding.GetChars(id, 2, 1));
      if (id[3] != (byte) 0)
      {
        char[] chars = encoding.GetChars(id, 3, 1);
        str += new string(chars);
      }
      if (word != 0)
        str += word.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }
    else
    {
      if (id[0] != (byte) 0)
        str = new string(encoding.GetChars(id, 0, 1));
      if (id[1] != (byte) 0)
      {
        char[] chars = encoding.GetChars(id, 1, 1);
        str += new string(chars);
      }
      if (id[2] != (byte) 0)
      {
        char[] chars = encoding.GetChars(id, 2, 1);
        str += new string(chars);
      }
      if (id[3] != (byte) 0)
      {
        char[] chars = encoding.GetChars(id, 3, 1);
        str += new string(chars);
      }
    }
    return str;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик</param>
  public virtual void Load(PrimitiveLoader loader)
  {
    BinaryReader reader = loader.Reader;
    this.name = loader.ReadString();
    this.Org.X = reader.ReadInt32();
    this.Org.Y = reader.ReadInt32();
    byte[] numArray = new byte[4];
    reader.Read(numArray, 0, 4);
    this.id = PrimitiveBase.MakeIdStr(numArray);
    this.horAlign = (HorAlignment) reader.ReadByte();
    this.vertAlign = (VertAlignment) reader.ReadByte();
  }

  /// <summary>Дать отчет о примитиве</summary>
  /// <returns>Отчет</returns>
  public virtual string Report()
  {
    return $"{this.Name} / {this.GetType().ToString()} / Org = {this.OrgMm.X}, {this.OrgMm.Y}";
  }

  /// <summary>Создать узел документа</summary>
  /// <returns>Узел документа</returns>
  public virtual DocumentTreeNode CreateNewDocumentNode(DocumentTreeNode parentDocNode)
  {
    TextBoxElement newDocumentNode = new TextBoxElement();
    newDocumentNode.AssignReadOnly(true);
    this.SetNodeId((DocumentTreeNode) newDocumentNode);
    parentDocNode?.AddChildNode((DocumentTreeNode) newDocumentNode, false, false);
    this.InitNewDocumentNode((DocumentTreeNode) newDocumentNode);
    newDocumentNode.Text = this.GetType().Name;
    return (DocumentTreeNode) newDocumentNode;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public virtual void InitNewDocumentNode(DocumentTreeNode node)
  {
    this.DocumentNode = node;
    if (!this.Name.IsEmpty<char>())
      node.Name = this.Name;
    this.SetNodeId(node);
    node.SetAttributeValue("BLN.TYPE", this.GetType().Name, false, false, false);
    node.AssignCloneByTemplateWithParent(true);
    if (this.additionalAttributes == null)
      return;
    foreach (KeyValuePair<string, string> additionalAttribute in this.additionalAttributes)
      node.SetAttributeValue(additionalAttribute.Key, additionalAttribute.Value);
  }

  /// <summary>Внутренние единицы измерения в миллиметры</summary>
  /// <param name="unit">Внутренние единицы измерения</param>
  /// <returns>миллиметры</returns>
  public static float BlankUnitToMm(int unit) => (float) unit / 20f;

  /// <summary>Точку во внутренних единицах измерения в миллиметры</summary>
  /// <param name="point">Точку</param>
  /// <returns>миллиметры</returns>
  public static PointF BlankUnitToMm(Point point)
  {
    return new PointF(PrimitiveBase.BlankUnitToMm(point.X), PrimitiveBase.BlankUnitToMm(point.Y));
  }

  /// <summary>перевести размер в мм</summary>
  /// <param name="size">Размер</param>
  /// <returns>мм</returns>
  public static SizeF BlankUnitToMm(Size size)
  {
    return new SizeF(PrimitiveBase.BlankUnitToMm(size.Width), PrimitiveBase.BlankUnitToMm(size.Height));
  }

  /// <summary>Прямоугольник в мм</summary>
  /// <param name="rec">Прямоугольник</param>
  /// <returns>мм</returns>
  public static RectangleF BlankUnitToMm(Rectangle rec)
  {
    return new RectangleF(PrimitiveBase.BlankUnitToMm(rec.Location), PrimitiveBase.BlankUnitToMm(rec.Size));
  }

  object ICloneable.Clone() => (object) this.Clone();

  /// <summary>Клонировать примитив</summary>
  /// <returns>Клон примитива</returns>
  public virtual PrimitiveBase Clone()
  {
    PrimitiveBase primitiveBase = (PrimitiveBase) null;
    Stream serializationStream = (Stream) new MemoryStream();
    try
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      binaryFormatter.Context = new StreamingContext(StreamingContextStates.All);
      binaryFormatter.Serialize(serializationStream, (object) this);
      serializationStream.Position = 0L;
      primitiveBase = (PrimitiveBase) binaryFormatter.Deserialize(serializationStream);
      if (primitiveBase is GroupPrimitive groupPrimitive)
        groupPrimitive.RestoreOwnersRecurcive();
    }
    finally
    {
      serializationStream.Close();
    }
    return primitiveBase;
  }

  /// <summary>Найти примитив по идентификатору</summary>
  /// <param name="primId">Идентификатор примитива</param>
  /// <returns>Примитив</returns>
  public virtual PrimitiveBase FindById(string primId)
  {
    return this.id == primId ? this : (PrimitiveBase) null;
  }
}
