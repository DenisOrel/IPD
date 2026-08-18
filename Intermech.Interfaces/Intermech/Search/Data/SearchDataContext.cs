
// Type: Intermech.Search.Data.SearchDataContext
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Search.Data.Repositories;
using System;


namespace Intermech.Search.Data
{
    public sealed class SearchDataContext
    {
      public SearchDataContext(IServiceProvider serviceProvider)
      {
        this.Applicabilities = serviceProvider != null ? serviceProvider.GetService(typeof (IApplicabilityRepository)) as IApplicabilityRepository : throw new ArgumentNullException(nameof (serviceProvider));
        this.AttributeTypes = serviceProvider.GetService(typeof (IAttributeTypeRepository)) as IAttributeTypeRepository;
        this.Compositions = serviceProvider.GetService(typeof (ICompositionRepository)) as ICompositionRepository;
        this.LifecycleLevels = serviceProvider.GetService(typeof (ILifecycleLevelRepository)) as ILifecycleLevelRepository;
        this.LifecycleSteps = serviceProvider.GetService(typeof (ILifecycleStepRepository)) as ILifecycleStepRepository;
        this.Objects = serviceProvider.GetService(typeof (IObjectRepository)) as IObjectRepository;
        this.ObjectTypes = serviceProvider.GetService(typeof (IObjectTypeRepository)) as IObjectTypeRepository;
        this.Relations = serviceProvider.GetService(typeof (IRelationRepository)) as IRelationRepository;
        this.RelationTypes = serviceProvider.GetService(typeof (IRelationTypeRepository)) as IRelationTypeRepository;
      }

      public IApplicabilityRepository Applicabilities { get; private set; }

      public IAttributeTypeRepository AttributeTypes { get; private set; }

      public ICompositionRepository Compositions { get; private set; }

      public ILifecycleLevelRepository LifecycleLevels { get; private set; }

      public ILifecycleStepRepository LifecycleSteps { get; private set; }

      public IObjectRepository Objects { get; private set; }

      public IObjectTypeRepository ObjectTypes { get; private set; }

      public IRelationRepository Relations { get; private set; }

      public IRelationTypeRepository RelationTypes { get; private set; }
    }
}
