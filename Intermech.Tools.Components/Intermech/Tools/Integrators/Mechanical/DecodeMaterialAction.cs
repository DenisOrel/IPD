// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.DecodeMaterialAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Text;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class DecodeMaterialAction : TransferValueRecordAction
{
  private StringKey materialIDKey;

  public DecodeMaterialAction(
    ValueBag source,
    StringKey sourceKey,
    ValueBag target,
    StringKey targetKey,
    StringKey materialIDKey = null)
    : base(source, sourceKey, target, targetKey)
  {
    this.materialIDKey = materialIDKey;
  }

  public override void Perform()
  {
    if (this.materialIDKey != (StringKey) null)
    {
      ValueRecord valueRecord = this.Source.Find(this.materialIDKey);
      if (valueRecord != null && !valueRecord.IsNull && valueRecord.DataType == typeof (string))
      {
        Tuple<long, int, string> createImbaseObject = ImbaseHelper.FindOrCreateImbaseObject(valueRecord.Read<string>((string) null), (string) null, (string) null, (string) null);
        if (createImbaseObject != null)
        {
          this.Target.Update(this.TargetKey, (object) createImbaseObject.Item1);
          this.Target.CopyFlag(this.TargetKey, valueRecord.Flags, NamedFlags.ThrowSetException);
          return;
        }
      }
    }
    ValueRecord materialItem = this.Source.Find(this.SourceKey);
    if (materialItem == null)
      return;
    if (materialItem.DataType == typeof (long) || materialItem.DataType == typeof (int) || materialItem.DataType == typeof (short))
      this.ObjectIdToObjectLink(materialItem);
    else if (materialItem.DataType == typeof (string))
      this.TextToObjectLink(materialItem);
    else
      this.ReportBadTypedItem(materialItem);
  }

  private void ObjectIdToObjectLink(ValueRecord materialItem)
  {
    try
    {
      this.Target.Update(this.TargetKey, materialItem.IsNull ? (object) TypedNull.Int64 : (object) Convert.ToInt64(materialItem.Value));
      this.Target.CopyFlag(this.TargetKey, materialItem.Flags, NamedFlags.ThrowSetException);
    }
    catch (InvalidCastException ex)
    {
      this.ReportBadValuedItem(materialItem, (Exception) ex);
    }
    catch (FormatException ex)
    {
      this.ReportBadValuedItem(materialItem, (Exception) ex);
    }
  }

  private void TextToObjectLink(ValueRecord materialItem)
  {
    string str = TextServices.Trim(materialItem.Read<string>((string) null));
    if (string.IsNullOrEmpty(str))
      this.Target.Update(this.TargetKey, (object) TypedNull.Int64);
    else if (this.IsTableDrivenValue(str))
    {
      this.Target.Update(this.TargetKey, (object) TypedNull.Int64).Flags.Set(MechanicalNamedFlags.TableDrivenValue);
    }
    else
    {
      IArticleService service = ServiceUtils.GetService<IArticleService>((object) ServicesManager.ServiceContainer, true);
      VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
      long newValue;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        newValue = service.FindArticleID(string.Empty, string.Empty, str, editorRule.OwnerId, (object) sessionKeeper.Session, true);
        if (newValue == 0L)
          newValue = service.FindArticleID(str, string.Empty, string.Empty, editorRule.OwnerId, (object) sessionKeeper.Session, true);
        if (newValue == 0L)
          newValue = TechcardHelper.FindTechBlankId(str);
        if (newValue == 0L)
          newValue = service.GetMaterialID(str, editorRule.OwnerId, (object) sessionKeeper.Session);
      }
      if (newValue != 0L)
        this.Target.Update(this.TargetKey, (object) newValue);
      else
        this.ReportBadValuedItem(materialItem, new Exception("Unable to find material object by name."));
    }
  }

  private bool IsTableDrivenValue(string materialText)
  {
    return materialText.StartsWith("см. табл", StringComparison.CurrentCultureIgnoreCase) || materialText.StartsWith("см.табл", StringComparison.CurrentCultureIgnoreCase) || materialText.Equals("см. тт", StringComparison.CurrentCultureIgnoreCase) || materialText.Equals("см.тт", StringComparison.CurrentCultureIgnoreCase) || materialText.StartsWith("изделие-заготовка", StringComparison.CurrentCultureIgnoreCase);
  }
}
