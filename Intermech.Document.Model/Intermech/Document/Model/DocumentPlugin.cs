// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.DocumentPlugin
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Базовый плагин редактора документов</summary>
public class DocumentPlugin : IDocumentPlugin
{
  /// <summary>Плагин редактора документов инициализирован</summary>
  public static bool Initialized { get; private set; }

  /// <summary>Инициализация плагина</summary>
  public static void InitDocumentPlugin()
  {
    if (DocumentPlugin.Initialized)
      return;
    ImRtfEditor imRtfEditor = new ImRtfEditor();
    imRtfEditor.TerSetFlags5(true, 1073741824 /*0x40000000*/);
    imRtfEditor.TerSetFlags(false, 134217728 /*0x08000000*/);
    imRtfEditor.TerCreateControl();
    imRtfEditor.TerRepaginate(false);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (ContainerElement).Name] = (object) new EmptyConstructorDelegate(ContainerElement.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) ImDocumentData.TypeNameForConstructorDictionary] = (object) new EmptyConstructorDelegate(ImDocument.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (LabelElement).Name] = (object) new EmptyConstructorDelegate(LabelElement.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (Page).Name] = (object) new EmptyConstructorDelegate(Page.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (Polyline).Name] = (object) new EmptyConstructorDelegate(Polyline.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (TableElement).Name] = (object) new EmptyConstructorDelegate(TableElement.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (TextBoxElement).Name] = (object) new EmptyConstructorDelegate(TextBoxElement.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (VirtualColumn).Name] = (object) new EmptyConstructorDelegate(VirtualColumn.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (ImDocumentData).Name] = (object) new EmptyConstructorDelegate(ImDocument.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (PageData).Name] = (object) new EmptyConstructorDelegate(Page.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (TableData).Name] = (object) new EmptyConstructorDelegate(TableElement.EmptyConstructor);
    DocumentTreeNode.TypeConstructorDictionary[(object) typeof (TextData).Name] = (object) new EmptyConstructorDelegate(TextBoxElement.EmptyConstructor);
    Type type1 = typeof (ReferenceToNodeAttribute);
    DocumentTreeNode.TypeNameDictionary[(object) type1.Name] = (object) type1;
    ReferenceBase.ReferenceClassList.Add(type1);
    DocumentTreeNode.TypeConstructorDictionary[(object) type1.Name] = (object) new EmptyConstructorDelegate(ReferenceToNodeAttribute.EmptyConstructor);
    DocumentTreeNode.TypeNameDictionary[(object) "ReferenceToNodeAttributeBase"] = (object) type1;
    DocumentTreeNode.TypeConstructorDictionary[(object) "ReferenceToNodeAttributeBase"] = (object) new EmptyConstructorDelegate(ReferenceToNodeAttribute.EmptyConstructor);
    Type type2 = typeof (UnknownReferenceToObject);
    DocumentTreeNode.TypeNameDictionary[(object) type2.Name] = (object) type2;
    ReferenceBase.ReferenceClassList.Add(type2);
    DocumentTreeNode.TypeConstructorDictionary[(object) type2.Name] = (object) new EmptyConstructorDelegate(UnknownReferenceToObject.EmptyConstructor);
    Type type3 = typeof (UnknownReferenceToTextSource);
    DocumentTreeNode.TypeNameDictionary[(object) type3.Name] = (object) type3;
    ReferenceBase.ReferenceClassList.Add(type3);
    DocumentTreeNode.TypeConstructorDictionary[(object) type3.Name] = (object) new EmptyConstructorDelegate(UnknownReferenceToTextSource.EmptyConstructor);
    Type type4 = typeof (ReferenceToDBObjectBase);
    DocumentTreeNode.TypeNameDictionary[(object) type4.Name] = (object) type4;
    ReferenceBase.ReferenceClassList.Add(type4);
    if (!DocumentTreeNode.TypeNameDictionary.Contains((object) "RefToDB"))
      DocumentTreeNode.TypeNameDictionary[(object) "RefToDB"] = (object) type4;
    DocumentTreeNode.TypeConstructorDictionary[(object) type4.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectBase.EmptyConstructor);
    if (!DocumentTreeNode.TypeConstructorDictionary.Contains((object) "RefToDB"))
      DocumentTreeNode.TypeConstructorDictionary[(object) "RefToDB"] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectBase.EmptyConstructor);
    Type type5 = typeof (ReferenceToDBObjectAttributeBase);
    ReferenceBase.ReferenceClassList.Add(type5);
    DocumentTreeNode.TypeNameDictionary[(object) type5.Name] = (object) type5;
    if (!DocumentTreeNode.TypeNameDictionary.Contains((object) "RefToDBAttr"))
      DocumentTreeNode.TypeNameDictionary[(object) "RefToDBAttr"] = (object) type5;
    DocumentTreeNode.TypeConstructorDictionary[(object) type5.Name] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttributeBase.EmptyConstructor);
    if (!DocumentTreeNode.TypeConstructorDictionary.Contains((object) "RefToDBAttr"))
      DocumentTreeNode.TypeConstructorDictionary[(object) "RefToDBAttr"] = (object) new EmptyConstructorDelegate(ReferenceToDBObjectAttributeBase.EmptyConstructor);
    Type type6 = typeof (ReferenceToSignBase);
    ReferenceBase.ReferenceClassList.Add(type6);
    DocumentTreeNode.TypeNameDictionary[(object) type6.Name] = (object) type6;
    if (!DocumentTreeNode.TypeNameDictionary.Contains((object) ReferenceToSignBase.XmlTypeName))
      DocumentTreeNode.TypeNameDictionary[(object) ReferenceToSignBase.XmlTypeName] = (object) type6;
    DocumentTreeNode.TypeConstructorDictionary[(object) type6.Name] = (object) new EmptyConstructorDelegate(ReferenceToSignBase.EmptyConstructor);
    if (!DocumentTreeNode.TypeConstructorDictionary.Contains((object) ReferenceToSignBase.XmlTypeName))
      DocumentTreeNode.TypeConstructorDictionary[(object) ReferenceToSignBase.XmlTypeName] = (object) new EmptyConstructorDelegate(ReferenceToSignBase.EmptyConstructor);
    DocumentTreeNode.OverridePropertyAttributes = (IDictionary) new HybridDictionary();
    DocumentTreeNode.OverridePropertyAttributes[(object) "Template"] = (object) new PropertyAttributeWrapper("Template", typeof (DocumentTreeNode), (Attribute) new EditorAttribute(typeof (TemplateEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "TopBorderLine"] = (object) new PropertyAttributeWrapper("TopBorderLine", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "LeftBorderLine"] = (object) new PropertyAttributeWrapper("LeftBorderLine", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "InnerBorderLine"] = (object) new PropertyAttributeWrapper("InnerBorderLine", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "RightBorderLine"] = (object) new PropertyAttributeWrapper("RightBorderLine", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "BottomBorderLine"] = (object) new PropertyAttributeWrapper("BottomBorderLine", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "TopBorderLineTE"] = (object) new PropertyAttributeWrapper("TopBorderLineTE", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "LeftBorderLineTE"] = (object) new PropertyAttributeWrapper("LeftBorderLineTE", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "RightBorderLineTE"] = (object) new PropertyAttributeWrapper("RightBorderLineTE", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "BottomBorderLineTE"] = (object) new PropertyAttributeWrapper("BottomBorderLineTE", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "InnerVerticalLineTE"] = (object) new PropertyAttributeWrapper("InnerVerticalLineTE", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "InnerHorizontalLineTE"] = (object) new PropertyAttributeWrapper("InnerHorizontalLineTE", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (BorderLineUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "AdditionalAttributes"] = (object) new PropertyAttributeWrapper("AdditionalAttributes", typeof (DocumentTreeNode), (Attribute) new EditorAttribute(typeof (AdditionalAttributesEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "Style"] = (object) new PropertyAttributeWrapper("Style", typeof (BorderLine), (Attribute) new EditorAttribute(typeof (BorderStylesUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "LineStyleVisual"] = (object) new PropertyAttributeWrapper("LineStyleVisual", typeof (Polyline), (Attribute) new EditorAttribute(typeof (LineDashStyleUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "StyleTE"] = (object) new PropertyAttributeWrapper("StyleTE", typeof (BorderLineTE), (Attribute) new EditorAttribute(typeof (BorderStylesUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "ColorTE"] = (object) new PropertyAttributeWrapper("ColorTE", typeof (BorderLineTE), (Attribute) new EditorAttribute(typeof (ColorUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "UnderlineColor"] = (object) new PropertyAttributeWrapper("UnderlineColor", typeof (BorderLineTE), (Attribute) new EditorAttribute(typeof (ColorUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "TextColorForUser"] = (object) new PropertyAttributeWrapper("TextColorForUser", typeof (CharFormat), (Attribute) new EditorAttribute(typeof (ColorUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "TextBkColorForUser"] = (object) new PropertyAttributeWrapper("TextBkColorForUser", typeof (CharFormat), (Attribute) new EditorAttribute(typeof (ColorUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "FontFamily"] = (object) new PropertyAttributeWrapper("FontFamily", typeof (CharFormat), (Attribute) new EditorAttribute(typeof (FontNameUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "BackColorTE"] = (object) new PropertyAttributeWrapper("BackColorTE", typeof (TableElement), (Attribute) new EditorAttribute(typeof (ColorUIEditor), typeof (UITypeEditor)));
    PropertyAttributeForType[] attributesForTypes = new PropertyAttributeForType[3]
    {
      new PropertyAttributeForType(typeof (TextData), (Attribute) new EditorAttribute(typeof (CharFormatUIEditor), typeof (UITypeEditor))),
      new PropertyAttributeForType(typeof (TableElement), (Attribute) new EditorAttribute(typeof (CharFormatUIEditor), typeof (UITypeEditor))),
      new PropertyAttributeForType(typeof (VirtualColumn), (Attribute) new EditorAttribute(typeof (CharFormatUIEditor), typeof (UITypeEditor)))
    };
    DocumentTreeNode.OverridePropertyAttributes[(object) "CharFormat"] = (object) new PropertyAttributeWrapper("CharFormat", attributesForTypes);
    attributesForTypes[0] = new PropertyAttributeForType(typeof (TextData), (Attribute) new EditorAttribute(typeof (ParagraphFormatUIEditor), typeof (UITypeEditor)));
    attributesForTypes[1] = new PropertyAttributeForType(typeof (TableElement), (Attribute) new EditorAttribute(typeof (ParagraphFormatUIEditor), typeof (UITypeEditor)));
    attributesForTypes[2] = new PropertyAttributeForType(typeof (VirtualColumn), (Attribute) new EditorAttribute(typeof (ParagraphFormatUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "ParagraphFormat"] = (object) new PropertyAttributeWrapper("ParagraphFormat", attributesForTypes);
    DocumentTreeNode.OverridePropertyAttributes[(object) "Reference"] = (object) new PropertyAttributeWrapper("Reference", new PropertyAttributeForType[1]
    {
      new PropertyAttributeForType(typeof (TextData), (Attribute) new EditorAttribute(typeof (ReferenceToTextSourceUIEditor), typeof (UITypeEditor)))
    });
    DocumentTreeNode.OverridePropertyAttributes[(object) "LeftForUser"] = (object) new PropertyAttributeWrapper("LeftForUser", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (RectangleUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "RightForUser"] = (object) new PropertyAttributeWrapper("RightForUser", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (RectangleUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "WidthForUser"] = (object) new PropertyAttributeWrapper("WidthForUser", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (RectangleUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "TopForUser"] = (object) new PropertyAttributeWrapper("TopForUser", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (RectangleUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "BottomForUser"] = (object) new PropertyAttributeWrapper("BottomForUser", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (RectangleUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "HeightForUser"] = (object) new PropertyAttributeWrapper("HeightForUser", typeof (RectangleElement), (Attribute) new EditorAttribute(typeof (RectangleUIEditor), typeof (UITypeEditor)));
    DocumentTreeNode.OverridePropertyAttributes[(object) "PathPoints"] = (object) new PropertyAttributeWrapper("PathPoints", typeof (DocumentTreeNode), (Attribute) new EditorAttribute(typeof (PolylinePointArrayEditor), typeof (UITypeEditor)));
    FormatCommandsList.Commands.Add("DocEditor.InsertFormula");
    FormatCommandsList.Commands.Add("CallEditor");
    FormatCommandsList.Commands.Add("DocEditor.EditFormula");
    DocumentPlugin.Initialized = true;
  }

  void IDocumentPlugin.Init() => DocumentPlugin.InitDocumentPlugin();
}
