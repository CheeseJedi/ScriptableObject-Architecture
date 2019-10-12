using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "ObjectGameEvent.asset",
        menuName = SOArchitecture_Utility.GAME_EVENTS_SUBMENU + "Object",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 1)]
    public class ObjectGameEvent : GameEventBase<Object>
    {
    } 
}