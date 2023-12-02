using Apartments.Domain;
using Apartments.Domain.Entities;
using Apartments.Domain.Interfaces;
using Apartments.Infrastructure.Db;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Infrastructure.Repositories
{
    public class ApartmentsStatisticsRepository : IApartmentsStatisticsRepository
    {
        private IMongoCollection<ApartmentsHourStatistics> _apartmentsStatisticsCollection;
        public ApartmentsStatisticsRepository(IOptions<MongoSettings> apartmentsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
    apartmentsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                apartmentsDatabaseSettings.Value.DatabaseName);

            _apartmentsStatisticsCollection = mongoDatabase.GetCollection<ApartmentsHourStatistics>(
                apartmentsDatabaseSettings.Value.CollectionStatisticsName);

        }
        public async Task CreateOrUpdateApartmentStatistics(ApartmentsHourStatistics apartmentsStatistics)
        {
            await _apartmentsStatisticsCollection.ReplaceOneAsync(x => x.ApartmentsStatisticsId == apartmentsStatistics.ApartmentsStatisticsId, apartmentsStatistics, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public async Task<ApartmentsHourStatistics> GetApartmentStatisticsById(string apartmentStatisticsId)
        {
            return await _apartmentsStatisticsCollection.FindAsync(x => x.ApartmentsStatisticsId == apartmentStatisticsId).Result.FirstAsync();

        }
    }
}
