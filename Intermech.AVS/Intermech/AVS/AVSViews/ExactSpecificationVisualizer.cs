// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSViews.ExactSpecificationVisualizer
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core.Visualizers;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Map;
using System;

#nullable disable
namespace Intermech.AVS.AVSViews;

/// <summary>
/// Визуализатор для точных спецификаций, которые генерируются для сконфигурированного состава без сохранения в БД
/// </summary>
public class ExactSpecificationVisualizer : IVisualizerEx, IVisualizer
{
  internal static void Initialize(IServiceProvider serviceProvider)
  {
    if (!(serviceProvider.GetService(typeof (IVisualizerService)) is IVisualizerService service))
      return;
    ExactSpecificationVisualizer specificationVisualizer = new ExactSpecificationVisualizer();
    service.AddVisualizer(ExtensionsConsts.ExactSpecificationExtension, (IVisualizer) specificationVisualizer);
  }

  public MapObject GetViewObject(VisualizerExParams visualizerExParams)
  {
    FiltrationSettings filtrationSettings = (FiltrationSettings) null;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IFiltrationService service = ServiceUtils.GetService<IFiltrationService>((object) ServicesManager.ServiceContainer, true);
        IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService<IVersionRulesCacheService>();
        filtrationSettings = customService.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, service.FiltrationServiceOwnerID, true);
        filtrationSettings = filtrationSettings.Clone() as FiltrationSettings;
        filtrationSettings.OwnerID = Guid.NewGuid().ToString();
        customService.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, filtrationSettings.OwnerID, filtrationSettings);
        int topObjectType = MetaDataHelper.IsPdmRootObjectType(visualizerExParams.ObjectTypeId) ? visualizerExParams.ObjectTypeId : -1;
        long objectId = MetaDataHelper.IsPdmRootObjectType(visualizerExParams.ObjectTypeId) ? visualizerExParams.ObjectId : 0L;
        RelationPair configureCompositionRoot = visualizerExParams.RelationPair ?? new RelationPair(0L, objectId, topObjectType, 0L, sessionKeeper.Session.UserID, visualizerExParams.ObjectId, -1, visualizerExParams.ObjectTypeId);
        AVSSpecification avsSpecification = new AVSSpecification(visualizerExParams.ObjectTypeId, visualizerExParams.ObjectId, AVSDocumentForm.Single, configureCompositionRoot, filtrationSettings.OwnerID, true);
        ImDocument document = avsSpecification.Document;
        avsSpecification.Document = (ImDocument) null;
        return (MapObject) new ImDocumentShowObject(document);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return (MapObject) null;
    }
    finally
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        if (filtrationSettings != null)
          customService?.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, filtrationSettings.OwnerID, (FiltrationSettings) null);
      }
    }
  }

  public MapObject GetViewObject(long objectId, int valueIndex, string fileName, byte[] data)
  {
    throw new NotImplementedException();
  }
}
