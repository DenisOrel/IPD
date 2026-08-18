using System.Drawing;


namespace Intermech.Map
{
    public interface IMapLayerCollectionContainer : IMapLayerAbilities
    {
      void RaiseChanged(
        int hint,
        int subhint,
        object obj,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect);

      MapLayerCollection Layers { get; }
    }
}
