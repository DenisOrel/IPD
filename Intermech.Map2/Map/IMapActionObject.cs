namespace Intermech.Map
{
    public interface IMapActionObject
    {
      void OnAction(MapView view, MapInputEventArgs e);

      void OnActionAdjusted(MapView view, MapInputEventArgs e);

      bool ActionActivated { get; set; }

      bool ActionEnabled { get; set; }
    }
}
