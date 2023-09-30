using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages the input for the game
    /// </summary>
    public static class InputManager
    {
        private const string ScrollWheel = "Mouse ScrollWheel";

        public static bool PanUp()      => Input.GetKey(KeyCode.W);
                                        
        public static bool PanLeft()    => Input.GetKey(KeyCode.A);

        public static bool PanDown()    => Input.GetKey(KeyCode.S);                                
                                        
        public static bool PanRight()   => Input.GetKey(KeyCode.D);
                                        
        public static float Scroll()    => Input.GetAxis(ScrollWheel);
                                        
        public static bool PauseGame()  => Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape);

        public static bool LockCamera() => Input.GetKeyDown(KeyCode.Q);

        public static bool NextWave()   => Input.GetKeyDown(KeyCode.Space);
    }
}
