// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.CadmechWeldingSeamsReader
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Data;
using Intermech.IO;
using Intermech.Text;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.Services.WeldingJoints;

internal sealed class CadmechWeldingSeamsReader
{
  private static readonly string[] emptyStringArray = new string[0];
  private static readonly byte[] emptyByteArray = new byte[0];
  private static readonly WeldingSeamComponent[] emptyComponentArray = new WeldingSeamComponent[0];

  public bool IsUIFocusLost { get; private set; }

  private bool MustReadFromConfigurationFile { get; set; }

  private bool MustOpenInVisibleMode { get; set; }

  private bool MustPreloadConfigurationFiles { get; set; }

  private bool CanGrabUIFocus { get; set; }

  private IIntegrator CurrentIntegrator { get; set; }

  private ModelConfigurationProxy CurrentDocumentConfiguration { get; set; }

  public List<WeldingSeamExternalData> Read(
    long documentId,
    string documentFilePath,
    IIntegrator integrator)
  {
    if (documentId == 0L)
      throw new ArgumentException("Не задан идентификатор версии документа IPS.", nameof (documentId));
    if (string.IsNullOrEmpty(documentFilePath))
      throw new ArgumentException("Не задан путь к мастер-файлу документа IPS.", nameof (documentFilePath));
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    using (CADApiSession cadApiSession = new CADApiSession(integrator))
    {
      CADSystemProxy application = cadApiSession.Application;
      this.InitializeReaderParameters(application);
      List<WeldingSeamExternalData> seamExternalDataList;
      try
      {
        this.CurrentIntegrator = integrator;
        seamExternalDataList = this.MustReadFromConfigurationFile ? this.ReadFromConfigurationFiles(documentId, documentFilePath, application) : this.ReadFromDefaultConfiguration(documentId, documentFilePath, application);
      }
      finally
      {
        this.CurrentIntegrator = (IIntegrator) null;
        this.IsUIFocusLost = this.MustOpenInVisibleMode && this.CanGrabUIFocus;
      }
      int count = seamExternalDataList.Count;
      return seamExternalDataList;
    }
  }

  private void InitializeReaderParameters(CADSystemProxy cadSystem)
  {
    this.MustReadFromConfigurationFile = (cadSystem.Capabilities & CADSystemCapabilities.SingleConfigurationPerFile) != 0;
    this.MustOpenInVisibleMode = false;
    this.MustPreloadConfigurationFiles = false;
    this.CanGrabUIFocus = false;
    switch (this.GetCADSystemSelector(cadSystem))
    {
      case CadmechWeldingSeamsReader.CADSystemSelector.Inventor:
        this.MustOpenInVisibleMode = true;
        this.CanGrabUIFocus = true;
        break;
      case CadmechWeldingSeamsReader.CADSystemSelector.SolidWorks:
        this.MustOpenInVisibleMode = true;
        this.CanGrabUIFocus = true;
        break;
      case CadmechWeldingSeamsReader.CADSystemSelector.Unigraphics:
        this.MustOpenInVisibleMode = cadSystem.GetOpenFiles(true).Count == 0;
        break;
    }
  }

  private CadmechWeldingSeamsReader.CADSystemSelector GetCADSystemSelector(CADSystemProxy cadSystem)
  {
    string name = cadSystem.Name;
    if (name.StartsWith("Inventor"))
      return CadmechWeldingSeamsReader.CADSystemSelector.Inventor;
    if (name.StartsWith("SolidWorks"))
      return CadmechWeldingSeamsReader.CADSystemSelector.SolidWorks;
    return name.StartsWith("Unigraphics") ? CadmechWeldingSeamsReader.CADSystemSelector.Unigraphics : CadmechWeldingSeamsReader.CADSystemSelector.Default;
  }

  [Conditional("DEBUG")]
  private void ValidateResult(List<WeldingSeamExternalData> seams)
  {
    foreach (WeldingSeamExternalData seam in seams)
    {
      if (seam.ConfigurationNames.Count == 0)
        throw new InvalidOperationException();
    }
  }

  private List<WeldingSeamExternalData> ReadFromConfigurationFiles(
    long documentId,
    string documentFilePath,
    CADSystemProxy cadSystem)
  {
    List<WeldingSeamExternalData> seams = new List<WeldingSeamExternalData>();
    foreach (ModelConfigurationProxy imtextDocumentProvider in new ModelConfigurationsWalk().Walk(cadSystem.OpenDocument(documentFilePath, this.MustOpenInVisibleMode), this.MustOpenInVisibleMode))
    {
      try
      {
        this.CurrentDocumentConfiguration = imtextDocumentProvider;
        if (!string.IsNullOrEmpty(imtextDocumentProvider.FullPath))
        {
          if (File.Exists(imtextDocumentProvider.FullPath))
          {
            if (this.MustPreloadConfigurationFiles)
              cadSystem.OpenDocument(imtextDocumentProvider.FullPath, this.MustOpenInVisibleMode);
            this.ReadSeamList((IIMTextDocumentProvider) imtextDocumentProvider, seams);
          }
        }
        else if (imtextDocumentProvider.Document.IsMasterDocument)
          this.ReadSeamList((IIMTextDocumentProvider) imtextDocumentProvider.Document, seams);
      }
      finally
      {
        this.CurrentDocumentConfiguration = (ModelConfigurationProxy) null;
      }
    }
    return seams;
  }

  private List<WeldingSeamExternalData> ReadFromDefaultConfiguration(
    long documentId,
    string documentFilePath,
    CADSystemProxy cadSystem)
  {
    List<WeldingSeamExternalData> seams = new List<WeldingSeamExternalData>();
    CADDocumentProxy document = cadSystem.OpenDocument(documentFilePath, this.MustOpenInVisibleMode);
    StringKey name = document.DefaultConfiguration.Name;
    try
    {
      foreach (ModelConfigurationProxy configurationProxy in new ModelConfigurationsWalk().Walk(document, this.MustOpenInVisibleMode))
      {
        try
        {
          this.CurrentDocumentConfiguration = configurationProxy;
          this.ReadSeamList((IIMTextDocumentProvider) configurationProxy.Document, seams);
        }
        finally
        {
          this.CurrentDocumentConfiguration = (ModelConfigurationProxy) null;
        }
      }
    }
    finally
    {
      if (document.DefaultConfiguration.Name != name)
        document.GetConfiguration((string) name, true);
    }
    return seams;
  }

  private void ReadSeamList(
    IIMTextDocumentProvider imtextDocumentProvider,
    List<WeldingSeamExternalData> seams)
  {
    string name = (string) this.CurrentDocumentConfiguration.Name;
    IMTextDocumentProxy textDocumentProxy = (IMTextDocumentProxy) null;
    IMTextAttributeManagerProxy attributeManagerProxy = (IMTextAttributeManagerProxy) null;
    try
    {
      textDocumentProxy = imtextDocumentProvider.GetIMTextDocument(true);
      attributeManagerProxy = textDocumentProxy.GetAttrManager();
      foreach (IMTextFaceAttributeProxy imtextAttribute in attributeManagerProxy.GetAllFaceAttrsByType(IMTextFaceAttributeType.Wj))
      {
        if (this.IsVisibleInModelConfiguration(imtextAttribute))
        {
          Guid imtextAttributeGuid = this.ParseGuid((object) imtextAttribute.GUID, Guid.Empty);
          bool flag = this.ParseBool(imtextAttribute.GetProperty("BothSide"), false);
          WeldingSeamExternalData seamExternalData1 = seams.Find((Predicate<WeldingSeamExternalData>) (item => item.AnchorGuid == imtextAttributeGuid && !item.IsOnBackSide));
          if (seamExternalData1 == null)
          {
            seamExternalData1 = this.ReadSeamDataFromFrontSide(imtextAttribute, !flag);
            seamExternalData1.AnchorGuid = imtextAttributeGuid;
            seams.Add(seamExternalData1);
          }
          seamExternalData1.ConfigurationNames.Add(name);
          if (flag)
          {
            WeldingSeamExternalData seamExternalData2 = seams.Find((Predicate<WeldingSeamExternalData>) (item => item.AnchorGuid == imtextAttributeGuid && item.IsOnBackSide));
            if (seamExternalData2 == null)
            {
              seamExternalData2 = this.ReadSeamDataFromBackSide(imtextAttribute);
              seamExternalData2.AnchorGuid = imtextAttributeGuid;
              seamExternalData2.IsOnBackSide = true;
              seams.Add(seamExternalData2);
            }
            seamExternalData2.ConfigurationNames.Add(name);
          }
        }
      }
    }
    finally
    {
      if (attributeManagerProxy != null)
        Marshal.FinalReleaseComObject((object) attributeManagerProxy.RawObject);
      if (textDocumentProxy != null)
        Marshal.FinalReleaseComObject((object) textDocumentProxy.RawObject);
    }
  }

  private bool IsVisibleInModelConfiguration(IMTextFaceAttributeProxy imtextAttribute)
  {
    IMTextFaceProxy[] faces = imtextAttribute.Faces;
    if (faces.Length != 0)
    {
      foreach (IMTextFaceProxy imTextFaceProxy in faces)
      {
        if (imTextFaceProxy.GeEntity != null)
          return true;
      }
    }
    return false;
  }

  private WeldingSeamExternalData ReadSeamDataFromFrontSide(
    IMTextFaceAttributeProxy imtextAttribute,
    bool includeBackSideMarks)
  {
    WeldingSeamExternalData seamExternalData = new WeldingSeamExternalData();
    seamExternalData.Number = this.ParseString(imtextAttribute.GetProperty("Number"), seamExternalData.Number);
    seamExternalData.Count = this.ParseString(imtextAttribute.GetProperty("Count"), seamExternalData.Count);
    seamExternalData.Length = this.ParseString(imtextAttribute.GetProperty("WeldLeng"), seamExternalData.Length);
    seamExternalData.MakeAtInstallationStage = this.ParseBool(imtextAttribute.GetProperty("Mount"), seamExternalData.MakeAtInstallationStage);
    seamExternalData.MakeClosed = this.ParseBool(imtextAttribute.GetProperty("Close"), seamExternalData.MakeClosed);
    seamExternalData.StandardName = this.ParseString(imtextAttribute.GetProperty("GOST"), seamExternalData.StandardName);
    seamExternalData.DesignationByStandard = this.ParseString(imtextAttribute.GetProperty("Designation"), seamExternalData.DesignationByStandard);
    seamExternalData.WeldingMethodDesignationByStandard = this.ParseString(imtextAttribute.GetProperty("Type"), seamExternalData.WeldingMethodDesignationByStandard);
    seamExternalData.LegSizeByStandard = this.ParseString(imtextAttribute.GetProperty("Leg"), seamExternalData.LegSizeByStandard);
    CadmechWeldingSeamsReader.LegTolerance legTolerance = CadmechWeldingSeamsReader.LegTolerance.Parse(this.ParseString(imtextAttribute.GetProperty("LegTolerance"), (string) null));
    seamExternalData.LegUpperTolerance = legTolerance.UpperValue;
    seamExternalData.LegLowerTolerance = legTolerance.LowerValue;
    seamExternalData.ExtraDimensions = this.ParseString(imtextAttribute.GetProperty("ExtraValue"), seamExternalData.ExtraDimensions);
    seamExternalData.Note = this.ParseString(imtextAttribute.GetProperty("Comment"), seamExternalData.Note);
    seamExternalData.ControlComplexDesignation = this.ParseString(imtextAttribute.GetProperty("Control"), seamExternalData.ControlComplexDesignation);
    seamExternalData.GeometryType = (WeldingSeamGeometryType) this.ParseInt64(imtextAttribute.GetProperty("WJTYPE"), 1);
    seamExternalData.FullLength = this.ParseDouble(imtextAttribute.GetProperty("WLENGTH"), 0.0).ToString();
    seamExternalData.LeftOffset = this.ParseDouble(imtextAttribute.GetProperty("OFFSETLEFT"), 0.0).ToString();
    seamExternalData.RightOffset = this.ParseDouble(imtextAttribute.GetProperty("OFFSETRIGHT"), 0.0).ToString();
    seamExternalData.SegmentationType = (WeldingSeamSegmentationType) this.ParseInt64(imtextAttribute.GetProperty("SEGSTYLE"), 0);
    seamExternalData.SegmentStep = this.ParseDouble(imtextAttribute.GetProperty("SEG1"), 0.0).ToString();
    seamExternalData.SegmentLength = this.ParseDouble(imtextAttribute.GetProperty("SEG2"), 0.0).ToString();
    seamExternalData.Gap = this.ParseDouble(imtextAttribute.GetProperty("MAXDISTK"), 0.0).ToString();
    seamExternalData.FirstPartThickness = this.ParseDouble(imtextAttribute.GetProperty("S"), 0.0).ToString();
    seamExternalData.SecondPartThickness = this.ParseDouble(imtextAttribute.GetProperty("S1"), 0.0).ToString();
    seamExternalData.ConnectionKind = this.ParseString(imtextAttribute.GetProperty("CONNECT"), seamExternalData.ConnectionKind);
    seamExternalData.RemoveReinforcementOnFrontSide = this.ParseBool(imtextAttribute.GetProperty("WS"), seamExternalData.RemoveReinforcementOnFrontSide);
    seamExternalData.ProcessIrregularitiesOnFrontSide = this.ParseBool(imtextAttribute.GetProperty("WN"), seamExternalData.ProcessIrregularitiesOnFrontSide);
    seamExternalData.MakeOpenOnFrontSide = this.ParseBool(imtextAttribute.GetProperty("WC"), seamExternalData.MakeOpenOnFrontSide);
    if (includeBackSideMarks)
    {
      seamExternalData.RemoveReinforcementOnBackSide = new bool?(this.ParseBool(imtextAttribute.GetProperty("WS_u"), false));
      seamExternalData.ProcessIrregularitiesOnBackSide = new bool?(this.ParseBool(imtextAttribute.GetProperty("WN_u"), false));
      seamExternalData.MakeOpenOnBackSide = new bool?(this.ParseBool(imtextAttribute.GetProperty("WC_u"), false));
    }
    seamExternalData.DxfSketch = this.ToByteArray(this.ParseStringArray(imtextAttribute.GetProperty("DXF"), CadmechWeldingSeamsReader.emptyStringArray));
    seamExternalData.Components.AddRange((IEnumerable<WeldingSeamComponent>) this.ParseComponentCollection(imtextAttribute.GetProperty("COMP1"), 1));
    seamExternalData.Components.AddRange((IEnumerable<WeldingSeamComponent>) this.ParseComponentCollection(imtextAttribute.GetProperty("COMP2"), 2));
    return seamExternalData;
  }

  private WeldingSeamExternalData ReadSeamDataFromBackSide(IMTextFaceAttributeProxy imtextAttribute)
  {
    WeldingSeamExternalData seamExternalData = new WeldingSeamExternalData();
    seamExternalData.Number = this.ParseString(imtextAttribute.GetProperty("Number_u"), seamExternalData.Number);
    seamExternalData.Count = this.ParseString(imtextAttribute.GetProperty("Count_u"), seamExternalData.Count);
    seamExternalData.Length = this.ParseString(imtextAttribute.GetProperty("WeldLeng_u"), seamExternalData.Length);
    seamExternalData.MakeAtInstallationStage = this.ParseBool(imtextAttribute.GetProperty("Mount"), seamExternalData.MakeAtInstallationStage);
    seamExternalData.MakeClosed = this.ParseBool(imtextAttribute.GetProperty("Close"), seamExternalData.MakeClosed);
    seamExternalData.StandardName = this.ParseString(imtextAttribute.GetProperty("GOST_u"), seamExternalData.StandardName);
    seamExternalData.DesignationByStandard = this.ParseString(imtextAttribute.GetProperty("Designation_u"), seamExternalData.DesignationByStandard);
    seamExternalData.WeldingMethodDesignationByStandard = this.ParseString(imtextAttribute.GetProperty("Type_u"), seamExternalData.WeldingMethodDesignationByStandard);
    seamExternalData.LegSizeByStandard = this.ParseString(imtextAttribute.GetProperty("Leg_u"), seamExternalData.LegSizeByStandard);
    CadmechWeldingSeamsReader.LegTolerance legTolerance = CadmechWeldingSeamsReader.LegTolerance.Parse(this.ParseString(imtextAttribute.GetProperty("LegTolerance_u"), (string) null));
    seamExternalData.LegUpperTolerance = legTolerance.UpperValue;
    seamExternalData.LegLowerTolerance = legTolerance.LowerValue;
    seamExternalData.ExtraDimensions = this.ParseString(imtextAttribute.GetProperty("ExtraValue_u"), seamExternalData.ExtraDimensions);
    seamExternalData.Note = string.Empty;
    seamExternalData.ControlComplexDesignation = this.ParseString(imtextAttribute.GetProperty("Control"), seamExternalData.ControlComplexDesignation);
    seamExternalData.GeometryType = (WeldingSeamGeometryType) this.ParseInt64(imtextAttribute.GetProperty("WJTYPE"), 1);
    seamExternalData.FullLength = this.ParseDouble(imtextAttribute.GetProperty("WLENGTH"), 0.0).ToString();
    seamExternalData.LeftOffset = this.ParseDouble(imtextAttribute.GetProperty("OFFSETLEFT"), 0.0).ToString();
    seamExternalData.RightOffset = this.ParseDouble(imtextAttribute.GetProperty("OFFSETRIGHT"), 0.0).ToString();
    seamExternalData.SegmentationType = (WeldingSeamSegmentationType) this.ParseInt64(imtextAttribute.GetProperty("SEGSTYLE"), 0);
    seamExternalData.SegmentStep = this.ParseDouble(imtextAttribute.GetProperty("SEG1"), 0.0).ToString();
    seamExternalData.SegmentLength = this.ParseDouble(imtextAttribute.GetProperty("SEG2"), 0.0).ToString();
    seamExternalData.Gap = this.ParseDouble(imtextAttribute.GetProperty("MAXDISTK"), 0.0).ToString();
    seamExternalData.FirstPartThickness = this.ParseDouble(imtextAttribute.GetProperty("S"), 0.0).ToString();
    seamExternalData.SecondPartThickness = this.ParseDouble(imtextAttribute.GetProperty("S1"), 0.0).ToString();
    seamExternalData.ConnectionKind = this.ParseString(imtextAttribute.GetProperty("CONNECT"), seamExternalData.ConnectionKind);
    seamExternalData.RemoveReinforcementOnFrontSide = this.ParseBool(imtextAttribute.GetProperty("WS_u"), false);
    seamExternalData.ProcessIrregularitiesOnFrontSide = this.ParseBool(imtextAttribute.GetProperty("WN_u"), false);
    seamExternalData.MakeOpenOnFrontSide = this.ParseBool(imtextAttribute.GetProperty("WC_u"), false);
    seamExternalData.Components.AddRange((IEnumerable<WeldingSeamComponent>) this.ParseComponentCollection(imtextAttribute.GetProperty("COMP1"), 1));
    seamExternalData.Components.AddRange((IEnumerable<WeldingSeamComponent>) this.ParseComponentCollection(imtextAttribute.GetProperty("COMP2"), 2));
    return seamExternalData;
  }

  private Guid ParseGuid(object value, Guid defaultValue)
  {
    return Guid.Parse(TextServices.Trim((string) value));
  }

  private string ParseString(object value, string defaultValue)
  {
    return value == null || !(value is string) ? defaultValue : TextServices.Trim((string) value);
  }

  private long ParseInt64(object value, int defaultValue)
  {
    return value == null || !this.IsIntegerType(value) ? (long) defaultValue : Convert.ToInt64(value);
  }

  private bool IsIntegerType(object value)
  {
    switch (value)
    {
      case long _:
      case int _:
      case short _:
        return true;
      default:
        return value is byte;
    }
  }

  private double ParseDouble(object value, double defaultValue)
  {
    return value == null || !this.IsRealType(value) ? defaultValue : Convert.ToDouble(value);
  }

  private bool IsRealType(object value) => value is double || value is float;

  private bool ParseBool(object value, bool defaultValue)
  {
    switch (value)
    {
      case null:
        return defaultValue;
      case byte _:
      case int _:
      case long _:
        return (int) Convert.ChangeType(value, typeof (int)) != 0;
      case bool flag:
        return flag;
      default:
        return defaultValue;
    }
  }

  private string[] ParseStringArray(object value, string[] defaultValue)
  {
    return value != null && value is string[] ? (string[]) value : defaultValue;
  }

  private byte[] ToByteArray(string[] textLines)
  {
    if (textLines.Length == 0)
      return CadmechWeldingSeamsReader.emptyByteArray;
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      using (StreamWriter streamWriter = new StreamWriter((Stream) imChunkedStream, Encoding.UTF8, 4096 /*0x1000*/, true))
      {
        foreach (string textLine in textLines)
          streamWriter.WriteLine(textLine);
      }
      imChunkedStream.Flush();
      return imChunkedStream.ToArray();
    }
  }

  private ICollection<WeldingSeamComponent> ParseComponentCollection(object value, int groupId)
  {
    switch (value)
    {
      case object[] _:
        return this.ParseComponentCollection((ICollection<object>) (object[]) value, groupId);
      case string[] _:
        return this.ParseComponentCollectionOld((ICollection<string>) (string[]) value, groupId);
      default:
        return (ICollection<WeldingSeamComponent>) CadmechWeldingSeamsReader.emptyComponentArray;
    }
  }

  /// <summary>
  /// Выполняет разбор коллекции свариваемых компонентов, где каждый компонент задан массивом ключем, начиная от корня сборки.
  /// Такой формат кодирования компонентов устарел, его обработка оставлена только в целях совместимости.
  /// </summary>
  /// <param name="componentKeyCollection">Коллекция ключей компонентов</param>
  /// <param name="groupId">Номе группы компонентов (1 или 2)</param>
  /// <returns>Коллекция свариваемых компонентов</returns>
  private ICollection<WeldingSeamComponent> ParseComponentCollection(
    ICollection<object> componentKeyCollection,
    int groupId)
  {
    List<WeldingSeamComponent> componentCollection = new List<WeldingSeamComponent>(componentKeyCollection.Count);
    foreach (object componentKey in (IEnumerable<object>) componentKeyCollection)
    {
      if (componentKey is string[] keyAsStringArray && keyAsStringArray.Length != 0)
      {
        (ModelConfigurationProxy componentConfiguration, string str) = this.GetComponentConfigurationAndCompareKey(keyAsStringArray);
        if (!componentCollection.Exists((Predicate<WeldingSeamComponent>) (x => string.Equals(x.CompareKey, str, StringComparison.CurrentCultureIgnoreCase))))
        {
          string componentFilePath = componentConfiguration.Document.FullName;
          string componentExternalKey = this.TryGetComponentExternalKey(componentConfiguration);
          if (!componentCollection.Exists((Predicate<WeldingSeamComponent>) (x => PathUtils.IsSamePath(x.FilePath, componentFilePath) && string.Equals(x.ArticleExternalKey, componentExternalKey, StringComparison.CurrentCultureIgnoreCase))))
            componentCollection.Add(new WeldingSeamComponent(str, componentFilePath, componentExternalKey, groupId));
        }
      }
    }
    return (ICollection<WeldingSeamComponent>) componentCollection;
  }

  /// <summary>
  /// Выполняет разбор коллекции свариваемых компонентов, где каждый компонент задан единственным ключем.
  /// Такой формат кодирования компонентов устарел, его обработка оставлена только в целях совместимости.
  /// </summary>
  /// <param name="componentKeyCollection">Коллекция ключей компонентов</param>
  /// <param name="groupId">Номе группы компонентов (1 или 2)</param>
  /// <returns>Коллекция свариваемых компонентов</returns>
  private ICollection<WeldingSeamComponent> ParseComponentCollectionOld(
    ICollection<string> componentKeyCollection,
    int groupId)
  {
    List<WeldingSeamComponent> componentCollectionOld = new List<WeldingSeamComponent>(componentKeyCollection.Count);
    foreach (string componentKey1 in (IEnumerable<string>) componentKeyCollection)
    {
      string componentKey = componentKey1;
      if (!string.IsNullOrEmpty(componentKey) && !componentCollectionOld.Exists((Predicate<WeldingSeamComponent>) (x => string.Equals(x.CompareKey, componentKey, StringComparison.CurrentCultureIgnoreCase))))
      {
        ModelConfigurationProxy configuration = this.CurrentDocumentConfiguration.GetComponent(componentKey).GetConfiguration();
        string componentFilePath = configuration.Document.FullName;
        string componentExternalKey = this.TryGetComponentExternalKey(configuration);
        if (!componentCollectionOld.Exists((Predicate<WeldingSeamComponent>) (x => PathUtils.IsSamePath(x.FilePath, componentFilePath) && string.Equals(x.ArticleExternalKey, componentExternalKey, StringComparison.CurrentCultureIgnoreCase))))
          componentCollectionOld.Add(new WeldingSeamComponent(componentKey, componentFilePath, componentExternalKey, groupId));
      }
    }
    return (ICollection<WeldingSeamComponent>) componentCollectionOld;
  }

  private string TryGetComponentExternalKey(ModelConfigurationProxy componentConfiguration)
  {
    StringKey[] keyNames = CADArticleExternalKeys.GetKeyNames();
    return CADArticleExternalKeys.GetExternalKey(CADDocumentHelper.ReadAttributes((IServiceProvider) this.CurrentIntegrator, componentConfiguration, (ICollection<StringKey>) keyNames, DecodeAttributesOptions.Empty).Bag, (string) componentConfiguration.Name) ?? string.Empty;
  }

  private (ModelConfigurationProxy, string) GetComponentConfigurationAndCompareKey(
    string[] keyAsStringArray)
  {
    ModelConfigurationProxy parentModelConfiguration = this.CurrentDocumentConfiguration;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < keyAsStringArray.Length; ++index)
    {
      string keyAsString = keyAsStringArray[index];
      parentModelConfiguration = this.GetComponentOrFail(parentModelConfiguration, keyAsString).GetConfiguration();
      if (stringBuilder.Length != 0)
        stringBuilder.Append(",");
      stringBuilder.Append(keyAsString);
    }
    return (parentModelConfiguration, stringBuilder.ToString());
  }

  /// <summary>
  /// Получает и возвращает компонент сборочной моделий CAD-системы по указанному ключу.
  /// В случае неудачи метод бросает исключение.
  /// </summary>
  /// <param name="parentModelConfiguration">Конфигурация родительской модели CAD-системы</param>
  /// <param name="componentKey">Ключ компонента в дереве построения модели</param>
  /// <returns>Компонент CAD-системы в дереве построения родительской модели CAD-системы</returns>
  /// <exception cref="T:System.Exception">Не удалось получить компонент сборочной модели CAD-системы</exception>
  private ModelComponentProxy GetComponentOrFail(
    ModelConfigurationProxy parentModelConfiguration,
    string componentKey)
  {
    ModelComponentProxy component;
    try
    {
      component = parentModelConfiguration.GetComponent(componentKey);
    }
    catch (Exception ex)
    {
      throw new Exception($"Не удалось получить компонент сборочной модели из конфигурации '{parentModelConfiguration.Name}' по ключу компонента '{componentKey}'.", ex);
    }
    return component != null ? component : throw new Exception($"Не удалось найти компонент сборочной модели в конфигурации '{parentModelConfiguration.Name}' по ключу компонента '{componentKey}'.");
  }

  private enum CADSystemSelector
  {
    Default,
    Inventor,
    SolidWorks,
    Unigraphics,
  }

  private sealed class LegTolerance
  {
    private LegTolerance(string upperValue, string lowerValue)
    {
      this.UpperValue = upperValue;
      this.LowerValue = lowerValue;
    }

    public string UpperValue { get; private set; }

    public string LowerValue { get; private set; }

    public static CadmechWeldingSeamsReader.LegTolerance Parse(string value)
    {
      string upperValue = (string) null;
      string lowerValue = (string) null;
      if (!string.IsNullOrEmpty(value))
      {
        string[] strArray = value.Split('/');
        for (int index = 0; index < strArray.Length; ++index)
          strArray[index] = TextServices.Trim(strArray[index]);
        if (strArray.Length >= 1 && !string.IsNullOrEmpty(strArray[0]))
          upperValue = strArray[0];
        if (strArray.Length >= 2 && !string.IsNullOrEmpty(strArray[1]))
          lowerValue = strArray[1];
      }
      return new CadmechWeldingSeamsReader.LegTolerance(upperValue, lowerValue);
    }
  }
}
