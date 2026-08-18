// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.BlankConfig
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.Obsolete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

[DocumentConfigElementType(DocumentConfigElementType.Document)]
public sealed class BlankConfig : DocumentConfigElement
{
  [CanBeNull]
  private IDocumentConfigElement ReplaceOldElement([CanBeNull] IDocumentConfigElement oldElement)
  {
    if (oldElement == null)
      return (IDocumentConfigElement) null;
    if (oldElement.ElementType != DocumentConfigElementType.Unknown || !(oldElement is TableProperties tableProperties))
      return oldElement;
    IDocumentConfigElement documentElementConfig = DocumentConfigElementFactory.CreateDocumentElementConfig(tableProperties.FieldContents == null ? (!tableProperties.SketchField ? DocumentConfigElementType.Variant : DocumentConfigElementType.PictureField) : DocumentConfigElementType.TextField);
    if (documentElementConfig != null)
    {
      documentElementConfig?.Assign((object) oldElement);
      this.Elements.Remove(oldElement);
      this.Elements.Add(documentElementConfig);
    }
    return documentElementConfig ?? oldElement;
  }

  private void LoadChilds(XElement xNode)
  {
    foreach (XElement element in xNode.Elements())
      this.ChildList.Add(Convert.ToString(element.Value));
  }

  private void LoadElement(XElement xNode)
  {
    if (string.CompareOrdinal(xNode.Name.ToString(), "Element") != 0)
      return;
    TableProperties oldElement = new TableProperties();
    oldElement.Load(this, xNode);
    IDocumentConfigElement documentConfigElement = this.ReplaceOldElement((IDocumentConfigElement) oldElement);
    if (documentConfigElement == null || documentConfigElement != oldElement)
      return;
    this.Elements.Add(documentConfigElement);
  }

  private void LoadBlank(XElement xNode)
  {
    foreach (XElement element in xNode.Elements())
    {
      string localName = element.Name.LocalName;
      string str = Convert.ToString(element.Value);
      switch (localName)
      {
        case "CharactersInDocumentNumber":
          this.CharactersInDocumentNumber = int.Parse(str);
          continue;
        case "Childs":
          this.LoadChilds(element);
          continue;
        case "DocumentType":
          this.DocumentType = str.ToEnum<DocumentOwnership>();
          continue;
        case "FirstNumberPageInDocument":
          this.FirstNumberPageInDocument = int.Parse(str);
          continue;
        case "Flags":
          this.Flags = str.ToEnum<BlankFlags>();
          continue;
        case "MaterialSetup":
          this.MaterialSetup = str.ToEnum<MaterialSetupType>();
          continue;
        case "NewShopSetup":
          this.NewShopSetup = str.ToEnum<NewShopSetupType>();
          continue;
        case "NumberingInterval":
          this.NumberingInterval = int.Parse(str);
          continue;
        case "StepSetup":
          this.StepSetup = str.ToEnum<StepSetupType>();
          continue;
        case "ToolSetup":
          this.ToolSetup = str.ToEnum<ToolSetupType>();
          continue;
        default:
          continue;
      }
    }
  }

  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new BlankConfig();
  }

  public BlankConfig() => this.Clear();

  public DocumentOwnership DocumentType { get; set; }

  public string DocumentName { get; set; }

  public BlankFlags Flags { get; set; }

  public bool Contents
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfContents);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfContents) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfContents);
    }
  }

  public bool Statement
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfStatement);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfStatement) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfStatement);
    }
  }

  public bool RouteCard
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfRouteCard);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfRouteCard) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfRouteCard);
    }
  }

  public bool OperatingCard
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfOperatingCard);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfOperatingCard) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfOperatingCard);
    }
  }

  public bool ShopToolList
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfShopToolList);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfShopToolList) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfShopToolList);
    }
  }

  public bool OperationalList
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfOperationalList);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfOperationalList) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfOperationalList);
    }
  }

  public bool PickingCard
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfPickingCard);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfPickingCard) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfPickingCard);
    }
  }

  public bool PickingCardStructure
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfPickingCardStructure);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfPickingCardStructure) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfPickingCardStructure);
    }
  }

  public bool EmptyStringBeforeOperation
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfEmptyStringBeforeOperation);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfEmptyStringBeforeOperation) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfEmptyStringBeforeOperation);
    }
  }

  public bool EnterInContents
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfEnterInContents);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfEnterInContents) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfEnterInContents);
    }
  }

  public bool DocumentNotInSet
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfDocumentNotInSet);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfDocumentNotInSet) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfDocumentNotInSet);
    }
  }

  public bool DoNotNumberPages
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfDoNotNumberPages);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfDoNotNumberPages) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfDoNotNumberPages);
    }
  }

  public bool ForPartDocument
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfForPartDocument);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfForPartDocument) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfForPartDocument);
    }
  }

  public bool PartGroupDocument
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfPartGroupDocument);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfPartGroupDocument) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfPartGroupDocument);
    }
  }

  public bool SketchDocument
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfSketchDocument);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfSketchDocument) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfSketchDocument);
    }
  }

  public bool ShowToolType
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfShowToolType);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfShowToolType) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfShowToolType);
    }
  }

  public bool NoRepeatTool
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfNoRepeatTool);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfNoRepeatTool) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfNoRepeatTool);
    }
  }

  public bool PlaceToolIntoEmptyFields
  {
    get => this.Flags.HasFlag((Enum) BlankFlags.BfPlaceToolIntoEmptyFields);
    set
    {
      this.Flags = value ? this.Flags.AddFlags<BlankFlags>(BlankFlags.BfPlaceToolIntoEmptyFields) : this.Flags.RemoveFlags<BlankFlags>(BlankFlags.BfPlaceToolIntoEmptyFields);
    }
  }

  public int CharactersInDocumentNumber { get; set; }

  public int FirstNumberPageInDocument { get; set; }

  public int NumberingInterval { get; set; }

  public Guid DocumentGroup { get; set; }

  public NewShopSetupType NewShopSetup { get; set; }

  public StepSetupType StepSetup { get; set; }

  public ToolSetupType ToolSetup { get; set; }

  public MaterialSetupType MaterialSetup { get; set; }

  public List<IDocumentConfigElement> Elements { get; } = new List<IDocumentConfigElement>();

  public int Production { get; set; }

  public long Sorting { get; set; }

  public long Language { get; set; }

  public TPStructureObjectsConfigs ObjectsConfigs { get; set; } = new TPStructureObjectsConfigs();

  public List<string> ChildList { get; } = new List<string>();

  public BlankConfig Load(XElement element)
  {
    foreach (XElement element1 in element.Elements())
    {
      if (element1.HasAttributes)
      {
        XAttribute xattribute = element1.Attribute(XName.Get("name"));
        if (xattribute != null)
        {
          if (Convert.ToString(xattribute.Value) == "blank")
            this.LoadBlank(element1);
          else
            this.LoadElement(element1);
        }
      }
      if (element1.Name == (XName) "Childs")
        this.LoadChilds(element1);
    }
    TechCardDocumentConfigLoadService service = ApplicationServices.Container.GetService<TechCardDocumentConfigLoadService>();
    XElement rootElement = element.Element((XName) "tp_structure_objects_configs");
    if (rootElement != null)
      this.ObjectsConfigs = service?.Load(rootElement) as TPStructureObjectsConfigs;
    return this;
  }

  public override DocumentConfigElementType ElementType => DocumentConfigElementType.Document;

  public override void Assign(object source)
  {
    base.Assign(source);
    if (!(source is BlankConfig source1))
      return;
    this.Flags = source1.Flags;
    this.ChildList.AddRange((IEnumerable<string>) source1.ChildList);
    this.ObjectsConfigs.Assign((object) source1);
    foreach (ICloneable element in source1.Elements)
    {
      if (element?.Clone() is IDocumentConfigElement documentConfigElement)
        this.Elements.Add(documentConfigElement);
    }
    this.DocumentType = source1.DocumentType;
    this.CharactersInDocumentNumber = source1.CharactersInDocumentNumber;
    this.FirstNumberPageInDocument = source1.FirstNumberPageInDocument;
    this.NumberingInterval = source1.NumberingInterval;
    this.NewShopSetup = source1.NewShopSetup;
    this.StepSetup = source1.StepSetup;
    this.ToolSetup = source1.ToolSetup;
    this.MaterialSetup = source1.MaterialSetup;
  }

  public override void Clear()
  {
    base.Clear();
    this.Flags = BlankFlags.BfNone;
    this.ChildList.Clear();
    this.ObjectsConfigs.Clear();
    this.Elements.Clear();
    this.DocumentName = string.Empty;
    this.DocumentType = DocumentOwnership.Process;
    this.CharactersInDocumentNumber = 1;
    this.FirstNumberPageInDocument = 1;
    this.NumberingInterval = 1;
    this.NewShopSetup = NewShopSetupType.OnSelectPage;
    this.StepSetup = StepSetupType.StringsOtpNotAlternate;
    this.ToolSetup = ToolSetupType.InLine;
    this.MaterialSetup = MaterialSetupType.InLine;
    this.Production = 1;
    this.Sorting = 0L;
    this.DocumentGroup = Guid.Empty;
  }

  public IDocumentConfigElement FindOrCreateElement(string id, DocumentConfigElementType configType = DocumentConfigElementType.Unknown)
  {
    if (configType == DocumentConfigElementType.Document)
      return (IDocumentConfigElement) this;
    IDocumentConfigElement oldElement = this.Elements.Where<IDocumentConfigElement>((Func<IDocumentConfigElement, bool>) (elem => elem.Id == id)).FirstOrDefault<IDocumentConfigElement>();
    if (oldElement != null)
    {
      if (oldElement.ElementType != configType && configType != DocumentConfigElementType.Unknown)
        oldElement = this.ReplaceOldElement(oldElement);
      return oldElement;
    }
    IDocumentConfigElement documentElementConfig = DocumentConfigElementFactory.CreateDocumentElementConfig(configType);
    if (documentElementConfig == null)
      return (IDocumentConfigElement) null;
    ((DocumentConfigElement) documentElementConfig).Id = id;
    this.Elements.Add(documentElementConfig);
    return documentElementConfig;
  }

  public IDocumentConfigElement FindElement(string id, DocumentConfigElementType configType = DocumentConfigElementType.Unknown)
  {
    return configType == DocumentConfigElementType.Document ? (IDocumentConfigElement) this : this.ReplaceOldElement(this.Elements.Where<IDocumentConfigElement>((Func<IDocumentConfigElement, bool>) (elem => elem.Id == id)).FirstOrDefault<IDocumentConfigElement>());
  }

  public void SetChildList(List<VariantConfig> props)
  {
    this.ChildList.Clear();
    foreach (DocumentConfigElement prop in props)
      this.ChildList.Add(prop.Id);
  }
}
