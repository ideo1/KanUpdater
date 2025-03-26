using KanUpdater.Services.RedgeMigration.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;

namespace KanUpdater.Services.RedgeMigration
{
    public interface IRedgeMigrationService
    {
        SubclassMigrationModel GetMigratedSubclassIds();
    }
    public class RedgeMigrationService : IRedgeMigrationService
    {
        private readonly MigrationConfiguration _migrationConfiguration;
        public RedgeMigrationService(IOptions<MigrationConfiguration> migrationConfiguration)
        {
            _migrationConfiguration = migrationConfiguration.Value;
        }
        public SubclassMigrationModel GetMigratedSubclassIds()
        {
            var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                $"Services\\RedgeMigration\\Configurations\\{_migrationConfiguration.SubclassFileName}.json");
            using var reader = new StreamReader(fileName);
            var responseBody = reader.ReadToEnd();

            if (responseBody == null)
            {
                return new SubclassMigrationModel();
            }

            var subclasses = JsonConvert.DeserializeObject<SubclassMigrationModel>(responseBody);

            return subclasses;
        }
    }
}
