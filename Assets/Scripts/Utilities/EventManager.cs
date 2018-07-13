using System;
public class EventManager{
    public static Action OnCameraMoveAway;

    public static void CameraMovedAway()
    {
        if (OnCameraMoveAway != null)
        {
            OnCameraMoveAway();
        }
    }
}
