namespace ScriptableObjectArchitecture
{
    public static class SOArchitecture_Utility
    {
        public const int ASSET_MENU_ORDER_VARIABLES = 121;
        public const int ASSET_MENU_ORDER_EVENTS = 122;
        public const int ASSET_MENU_ORDER_COLLECTIONS = 123;
        public const int ASSET_MENU_ORDER_SYSTEMS = 124;

        public const string VARIABLE_SUBMENU = "Variables/";
        public const string COLLECTION_SUBMENU = "Collections/";
        public const string VARIABLE_COLLECTION_SUBMENU = COLLECTION_SUBMENU + VARIABLE_SUBMENU;
        public const string GAME_EVENTS_SUBMENU = "Game Events/";
        public const string SYSTEMS_SUBMENU = "Systems/";
        public const string PERSISTENCE_SUBMENU = SYSTEMS_SUBMENU + "Persistence/";
        public const string SERIALIZER_SUBMENU = PERSISTENCE_SUBMENU + "Serializers/";

        public const string ADVANCED_GAME_EVENT = GAME_EVENTS_SUBMENU + "Advanced/";
        public const string ADVANCED_VARIABLE_SUBMENU = VARIABLE_SUBMENU + "Advanced/";
        public const string ADVANCED_COLLECTION_SUBMENU = COLLECTION_SUBMENU + "Advanced/";

        // Add Component Menus
        public const string ADD_COMPONENT_ROOT_MENU = "SO Architecture/";
        public const string EVENT_LISTENER_SUBMENU = ADD_COMPONENT_ROOT_MENU + "Event Listeners/";
    }
}
