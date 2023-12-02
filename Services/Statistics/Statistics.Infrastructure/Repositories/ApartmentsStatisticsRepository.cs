using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Statistics.Domain.Entities;
using Statistics.Domain.Interfaces;
using Statistics.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Infrastructure.Repositories
{
    public class ApartmentsStatisticsRepository : IApartmentsStatisticsRepository
    {
        private IMongoCollection<ApartmentsStatistics> _apartmentsStatisticsCollection;
        public ApartmentsStatisticsRepository(IOptions<MongoSettings> databaseSettings)

        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _apartmentsStatisticsCollection = mongoDatabase.GetCollection<ApartmentsStatistics>(
                databaseSettings.Value.ApartmentsCollectionName);
        }
        public async Task CreateOrUpdateApartmentStatistics(ApartmentsStatistics apartmentsStatistics)
        {
            var result = await _apartmentsStatisticsCollection.ReplaceOneAsync(x => x.ApartmentsStatisticsId == apartmentsStatistics.ApartmentsStatisticsId, apartmentsStatistics, new ReplaceOptions()
            {
                IsUpsert = true
            });

        }

        public async Task<ApartmentsStatistics> GetApartmentStatisticsById(string apartmentStatisticsId)
        {
            return await _apartmentsStatisticsCollection.FindAsync(x => x.ApartmentsStatisticsId == apartmentStatisticsId).Result.FirstAsync();

        }
    }
}
