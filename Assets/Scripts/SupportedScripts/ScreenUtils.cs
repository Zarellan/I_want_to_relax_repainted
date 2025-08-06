using UnityEngine;

public static class ScreenUtils
{
    // Private Properties
    private static float _screenLeft;
    private static float _screenRight;
    private static float _screenTop;
    private static float _screenBottom;
    private static Vector2 _bottomLeftCorner;
    private static Vector2 _topRightCorner;

    // Public Properties
    public static float ScreenLeft { get {CheckResolutionChange(); return _screenLeft;} }
    public static float ScreenRight { get {CheckResolutionChange(); return _screenRight;} }
    public static float ScreenTop { get {CheckResolutionChange(); return _screenTop;} }
    public static float ScreenBottom { get {CheckResolutionChange(); return _screenBottom;} }

    private static void CheckResolutionChange()
    {
        _bottomLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
        _topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.z));

        _screenLeft = _bottomLeftCorner.x;
        _screenBottom = _bottomLeftCorner.y;
        _screenRight = _topRightCorner.x;
        _screenTop = _topRightCorner.y;
    }
}