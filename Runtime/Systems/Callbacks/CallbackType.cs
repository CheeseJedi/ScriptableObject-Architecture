namespace ScriptableObjectArchitecture
{
    [System.Flags]
    public enum CallbackType { None = 0, Everything = ~0, Start = 1, Update = 2, FixedUpdate = 4, LateUpdate = 8, OnGUI = 16, OnAwake = 32, OnQuit = 64, }
}