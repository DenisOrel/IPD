// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.Obsolete.TableProperties
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.Obsolete;

internal sealed class TableProperties : DocumentConfigElement
{
  private const string StrName = "name";
  private const string StrNumber = "Number";
  private const string StrObjType = "Objtype";
  private const string StrFlags = "Flags";
  private const string StrDigits = "Digits";
  private const string StrChilds = "Childs";
  private const string StrChild = "Child";
  public int DefaultDigits = 3;
  private int _number;
  private IFieldContents _fieldContents;
  private IFieldContents _condition;
  private IMSObjectType _objType;
  private TablePropertiesFlags _flags;
  private int _digits;
  private bool _isModified;
  private SketchTypes _sketchType = SketchTypes.Dwg;

  public TableProperties()
  {
    this._number = 0;
    this._fieldContents = (IFieldContents) null;
    this._condition = (IFieldContents) null;
    this._objType = (IMSObjectType) null;
    this._flags = TablePropertiesFlags.None;
    this._digits = this.DefaultDigits;
    this.Interior = false;
  }

  public int Number
  {
    get => this._number;
    set
    {
      this._number = value;
      this._isModified = true;
    }
  }

  public IMSObjectType ObjType
  {
    get => this._objType;
    set
    {
      this._objType = value;
      this._isModified = true;
    }
  }

  public void SetObjTypeGuid(Guid objTypeGuid)
  {
    this._objType = objTypeGuid != Guid.Empty ? MetaDataHelper.GetObjectType(objTypeGuid) : (IMSObjectType) null;
  }

  public bool NotRepeated
  {
    get => this._flags.HasFlag((Enum) TablePropertiesFlags.NotRepeated);
    set
    {
      this._flags = value ? this._flags.AddFlags<TablePropertiesFlags>(TablePropertiesFlags.NotRepeated) : this._flags.RemoveFlags<TablePropertiesFlags>(TablePropertiesFlags.NotRepeated);
      this._isModified = true;
    }
  }

  public bool OnDetail
  {
    get => this._flags.HasFlag((Enum) TablePropertiesFlags.OnDetail);
    set
    {
      this._flags = value ? this._flags.AddFlags<TablePropertiesFlags>(TablePropertiesFlags.OnDetail) : this._flags.RemoveFlags<TablePropertiesFlags>(TablePropertiesFlags.OnDetail);
      this._isModified = true;
    }
  }

  public bool SketchField
  {
    get => this._flags.HasFlag((Enum) TablePropertiesFlags.SketchField);
    set
    {
      this._flags = value ? this._flags.AddFlags<TablePropertiesFlags>(TablePropertiesFlags.SketchField) : this._flags.RemoveFlags<TablePropertiesFlags>(TablePropertiesFlags.SketchField);
      this._isModified = true;
    }
  }

  public SketchTypes SketchType
  {
    get => this._sketchType;
    set
    {
      this._isModified = this._sketchType != value;
      if (!this._isModified)
        return;
      this._sketchType = value;
    }
  }

  public bool CalcOnFill
  {
    get => this._flags.HasFlag((Enum) TablePropertiesFlags.CalcOnFill);
    set
    {
      this._flags = value ? this._flags.AddFlags<TablePropertiesFlags>(TablePropertiesFlags.CalcOnFill) : this._flags.RemoveFlags<TablePropertiesFlags>(TablePropertiesFlags.CalcOnFill);
      this._isModified = true;
    }
  }

  public int Digits
  {
    get => this._digits;
    set
    {
      this._number = value;
      this._isModified = true;
    }
  }

  public bool IsModified
  {
    get => this._isModified;
    set => this._isModified = value;
  }

  public IFieldContents FieldContents
  {
    get => this._fieldContents;
    set => this._fieldContents = value;
  }

  public IFieldContents Condition
  {
    get => this._condition;
    set => this._condition = value;
  }

  public TablePropertiesFlags Flags
  {
    get => this._flags;
    set => this._flags = value;
  }

  public bool Interior { get; set; }

  public List<string> ChildsList { get; } = new List<string>();

  public override DocumentConfigElementType ElementType => DocumentConfigElementType.Unknown;

  public void SetChildList(IEnumerable<VariantConfig> props)
  {
    this.ChildsList.Clear();
    foreach (VariantConfig prop in props)
    {
      this.ChildsList.Add(prop.Id);
      prop.Interior = true;
    }
  }

  public void LoadChilds(BlankConfig props, XElement xNode)
  {
    foreach (XElement element in xNode.Elements())
      this.ChildsList.Add(element.Value);
  }

  public void Load(BlankConfig props, XElement xNode)
  {
    TechCardDocumentConfigLoadService service = ApplicationServices.Container.GetService<TechCardDocumentConfigLoadService>();
    foreach (XElement element in xNode.Elements())
    {
      string localName = element.Name.LocalName;
      string str = Convert.ToString(element.Value);
      switch (localName)
      {
        case "Childs":
          this.LoadChilds(props, element);
          continue;
        case "ConditionType":
          this._condition = service?.Load(element) as IFieldContents;
          continue;
        case "Digits":
          this._digits = int.Parse(str);
          continue;
        case "FieldContents":
          this._fieldContents = service?.Load(element) as IFieldContents;
          continue;
        case "Flags":
          this._flags = str.ToEnum<TablePropertiesFlags>();
          continue;
        case "Id":
          this.Id = str;
          continue;
        case "Number":
          this._number = int.Parse(str);
          continue;
        case "Objtype":
          this._objType = MetaDataHelper.GetObjectType(new Guid(str));
          continue;
        case "sketch_type":
          this.SketchType = str.ToEnum<SketchTypes>();
          continue;
        default:
          continue;
      }
    }
  }

  public XElement Save()
  {
    XElement xelement = new XElement((XName) "Element", new object[6]
    {
      (object) new XAttribute((XName) "name", (object) this.Id),
      (object) new XElement((XName) "Id", (object) this.Id),
      (object) new XElement((XName) "Number", (object) this._number.ToString()),
      (object) new XElement((XName) "Flags", (object) this._flags.ToString()),
      (object) new XElement((XName) "Digits", (object) this._digits.ToString()),
      (object) new XElement((XName) "sketch_type", (object) this.SketchType.ToString())
    });
    if (this._objType != null)
      xelement.Add((object) new XElement((XName) "Objtype", (object) this._objType.Guid.ToString()));
    TechCardDocumentConfigSerializeService service = ApplicationServices.Container.GetService<TechCardDocumentConfigSerializeService>();
    if (this._fieldContents != null)
    {
      XElement content = service?.Serialize(this._fieldContents as IDocumentConfigElement);
      if (content != null)
        xelement.Add((object) content);
    }
    if (this._condition != null)
    {
      XElement content = service?.Serialize(this._condition as IDocumentConfigElement);
      if (content != null)
        xelement.Add((object) content);
    }
    if (this.ChildsList.Count > 0)
    {
      XElement content = new XElement((XName) "Childs", (object) this.ChildsList.Select<string, XElement>((Func<string, XElement>) (item => new XElement((XName) "Child", (object) item))));
      xelement.Add((object) content);
    }
    return xelement;
  }

  public bool IsDefault()
  {
    return this._number == 0 && this._fieldContents == null && this._condition == null && this._objType == null && this._flags == TablePropertiesFlags.None && this._digits == this.DefaultDigits && this.ChildsList.Count == 0;
  }

  public string TemplateText()
  {
    return this._fieldContents != null ? this._fieldContents.ToString() : string.Empty;
  }

  public string ConditionText()
  {
    return this._condition != null ? this._condition.ToString() : string.Empty;
  }

  public void GetAttributeTypes(List<AttributeSettings> attrs)
  {
    attrs.Clear();
    if (this.FieldContents != null)
      this.FieldContents.CollectAttributeSettings((ICollection<AttributeSettings>) attrs);
    if (this.Condition == null)
      return;
    this.Condition.CollectAttributeSettings((ICollection<AttributeSettings>) attrs);
  }

  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new TableProperties();
  }

  public override void Clear()
  {
    this.Id = string.Empty;
    this._number = 0;
    this._fieldContents = (IFieldContents) null;
    this._condition = (IFieldContents) null;
    this._objType = (IMSObjectType) null;
    this._flags = TablePropertiesFlags.None;
    this._digits = this.DefaultDigits;
    this.Interior = false;
    this.SketchType = SketchTypes.Unsupported;
    this.ChildsList.Clear();
  }

  public override void Assign(object source)
  {
    this.Clear();
    if (source is TableProperties tableProperties)
    {
      this.Id = tableProperties.Id;
      this._number = tableProperties._number;
      this._fieldContents = (tableProperties._fieldContents is ICloneable fieldContents ? fieldContents.Clone() : (object) null) as IFieldContents;
      this._condition = (tableProperties._condition is ICloneable condition ? condition.Clone() : (object) null) as IFieldContents;
      this._objType = tableProperties._objType;
      this._flags = tableProperties._flags;
      this._digits = tableProperties._digits;
      this.Interior = tableProperties.Interior;
      this.SketchField = tableProperties.SketchField;
      this.SketchType = tableProperties.SketchType;
      this.ChildsList.AddRange((IEnumerable<string>) tableProperties.ChildsList);
    }
    this._isModified = false;
  }
}
