
namespace ScriptableObjectArchitecture
{
    public static class SerializerExtensions
    {
        /// <summary>
        /// Loads a PersistenceSystem with a template using the specified serializer.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="system"></param>
        public static void PersistenceLoad(this AbstractSerializer serializer, PersistenceSystem system)
        {
            PersistenceDataTemplate template = serializer.Load<PersistenceDataTemplate>
                (system.GetCachedPath(createMissingDirectories: true));
            system.FromPersistenceTemplate(template);
        }
        /// <summary>
        /// Saves a PersistenceSystem with a template using the specified serializer.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="system"></param>
        public static void PersistenceSave(this AbstractSerializer serializer, PersistenceSystem system)
        {
            PersistenceDataTemplate _persistenceData = system.ToPersistenceTemplate() as PersistenceDataTemplate;
            serializer.Save(system.GetCachedPath(createMissingDirectories: true), _persistenceData);
        }
    }
}
