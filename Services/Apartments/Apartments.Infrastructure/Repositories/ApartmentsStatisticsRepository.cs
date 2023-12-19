﻿using Apartments.Domain;
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
        private IMongoCollection<ApartmentsStatistics> _apartmentsStatisticsCollection;
        public ApartmentsStatisticsRepository(IOptions<MongoSettings> apartmentsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
    apartmentsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                apartmentsDatabaseSettings.Value.DatabaseName);

            _apartmentsStatisticsCollection = mongoDatabase.GetCollection<ApartmentsStatistics>(
                apartmentsDatabaseSettings.Value.CollectionStatisticsName);

        }
        public async Task CreateOrUpdateApartmentStatistics(ApartmentsStatistics apartmentsStatistics)
        {
            await _apartmentsStatisticsCollection.ReplaceOneAsync(x => x.ApartmentsStatisticsId == apartmentsStatistics.ApartmentsStatisticsId, apartmentsStatistics, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public async Task<ApartmentsStatistics> GetApartmentStatisticsById(string apartmentStatisticsId)
        {
            return await _apartmentsStatisticsCollection.FindAsync(x => x.ApartmentsStatisticsId == apartmentStatisticsId).Result.FirstAsync();

        }
        public async Task<ApartmentsStatistics> GetUnproccessedApartmentStatistics()
        {
            var builder = Builders<ApartmentsStatistics>.Filter;
            var filter = builder.Eq(x => x.AreInformationsSubmitted, false);
            var sort = Builders<ApartmentsStatistics>.Sort.Descending(x => x.LastModifiedDate);

            var returnValue = await _apartmentsStatisticsCollection.Find(filter).Sort(sort).FirstOrDefaultAsync();
            return returnValue;
        }
        public async Task<ApartmentsStatistics> GetNotSendApartmentsStatistics()
        {
            var builder = Builders<ApartmentsStatistics>.Filter;
            var filter = builder.Eq(x=> x.IsSendToStatisticsService, false);

            var sort = Builders<ApartmentsStatistics>.Sort.Descending(x => x.LastModifiedDate);
            var returnValue = await _apartmentsStatisticsCollection.Find(filter).Sort(sort).FirstOrDefaultAsync();
            return returnValue;
        }
    }
}
