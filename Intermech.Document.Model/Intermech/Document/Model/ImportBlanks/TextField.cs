// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ImportBlanks.TextField
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Document.Model.ImportBlanks;

/// <summary>Текстовое поле</summary>
[Serializable]
/// <summary>Конструктор</summary>
/// <param name="owner">Владелец</param>
public class TextField(GroupPrimitive owner) : TextPrimitive(owner)
{
  /// <summary>If the field can expand, set direction</summary>
  public GenAlignment expandTo;
  /// <summary>Маска текста</summary>
  public string textMask;
  /// <summary>Тип поля</summary>
  public FldType fieldType;
  /// <summary>If this flag is set, the fields with the same ID always contain the same text</summary>
  public bool uniqueID;
  /// <summary>Нужно ли посылать программе-клиенту сообщения:
  /// а) Когда пользователь начинает редактирование поля
  /// б) После завершения редактирования поля
  /// в) После ввода каждого символа (Validate).</summary>
  public EditEvent editEvents;

  /// <summary>If the field can expand, set direction</summary>
  public GenAlignment ExpandTo
  {
    [DebuggerStepThrough] get => this.expandTo;
  }

  /// <summary>Маска текста</summary>
  public string TextMask
  {
    [DebuggerStepThrough] get => this.textMask;
  }

  /// <summary>Тип поля</summary>
  public FldType FieldType
  {
    [DebuggerStepThrough] get => this.fieldType;
  }

  /// <summary>If this flag is set, the fields with the same ID always contain the same text</summary>
  public bool UniqueID
  {
    [DebuggerStepThrough] get => this.uniqueID;
  }

  /// <summary>Нужно ли посылать программе-клиенту сообщения:
  /// а) Когда пользователь начинает редактирование поля
  /// б) После завершения редактирования поля
  /// в) После ввода каждого символа (Validate).</summary>
  public EditEvent EditEvents
  {
    [DebuggerStepThrough] get => this.editEvents;
  }

  /// <summary>Загрузить</summary>
  /// <param name="loader">Загрузчик примитивов</param>
  public override void Load(PrimitiveLoader loader)
  {
    base.Load(loader);
    BinaryReader reader = loader.Reader;
    byte num = reader.ReadByte();
    if (num < (byte) 0 || num > (byte) 6)
      num = (byte) 6;
    this.expandTo = (GenAlignment) num;
    this.textMask = loader.ReadString();
    this.fieldType = (FldType) reader.ReadByte();
    this.uniqueID = reader.ReadBoolean();
    this.editEvents = (EditEvent) reader.ReadByte();
    if (loader.LoadingVersion >= 256 /*0x0100*/ && !loader.CurrentPrimitiveIsLoaded)
      this.needFrame = reader.ReadBoolean();
    else
      this.needFrame = false;
  }

  /// <summary>Инициализировать узел документа данными примитива</summary>
  /// <param name="node">Узел</param>
  public override void InitNewDocumentNode(DocumentTreeNode node)
  {
    base.InitNewDocumentNode(node);
    if (this.uniqueID && this.Id != null && this.Id != "" && this.Id != node.Id && !this.IsCellInArea)
    {
      DocumentTreeNode node1 = node.FindNode(this.Id);
      if (node is TextData textData)
      {
        ReferenceToNodeAttribute referenceToNodeAttribute = new ReferenceToNodeAttribute((DocumentTreeNode) null, BaseReferenceNodeType.ntSelectedNode, this.Id, DocumentTreeNode.AttributeName_Text);
        if (node1 != null)
          referenceToNodeAttribute.AssignNodeLink(node1);
        textData.AssignReferenceToTextSource((ReferenceBase) referenceToNodeAttribute, true, false, false);
      }
    }
    if (!(node is TextBoxElement textBoxElement))
      return;
    switch (this.expandTo)
    {
      case GenAlignment.gaLeft:
        textBoxElement.SetAttributeValue("BLN.ExpandTo", this.expandTo.ToString(), false, false, false);
        break;
      case GenAlignment.gaTop:
      case GenAlignment.gaBottom:
        if (this.AutoFillTextBox)
        {
          textBoxElement.AssignAutoSizeHeight(true, false, false, false);
          textBoxElement.RemoveAttribute("BLN.AutoMove", false, false);
          break;
        }
        textBoxElement.SetAttributeValue("BLN.ExpandTo", this.expandTo.ToString(), false, false, false);
        break;
      case GenAlignment.gaHCenter:
      case GenAlignment.gaVCenter:
        textBoxElement.SetAttributeValue("BLN.ExpandTo", this.expandTo.ToString(), false, false, false);
        break;
      case GenAlignment.gaRight:
        textBoxElement.SetAttributeValue("BLN.ExpandTo", this.expandTo.ToString(), false, false, false);
        break;
    }
    if (this.textMask != null && this.textMask != "")
      textBoxElement.SetAttributeValue("BLN.TextMask", this.textMask, false, false, false);
    if (this.fieldType != FldType.ftyAnyText)
      textBoxElement.SetAttributeValue("BLN.FieldType", this.fieldType.ToString(), false, false, false);
    if (this.editEvents == (EditEvent) 0)
      return;
    textBoxElement.SetAttributeValue("BLN.EditEvents", this.editEvents.ToString(), false, false, false);
  }
}
