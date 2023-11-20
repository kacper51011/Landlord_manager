﻿using Apartments.Domain.Entities;
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
    public class ApartmentsRepository : IApartmentsRepository
    {
        private IMongoCollection<Apartment> _apartmentsCollection;
        public ApartmentsRepository( IOptions<MongoSettings> apartmentsDatabaseSettings)
   
        {
            var mongoClient = new MongoClient(
                apartmentsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                apartmentsDatabaseSettings.Value.DatabaseName);

            _apartmentsCollection = mongoDatabase.GetCollection<Apartment>(
                apartmentsDatabaseSettings.Value.CollectionName);
        }

        public async Task CreateOrUpdateApartment(Apartment apartment)
        {

            await _apartmentsCollection.ReplaceOneAsync(x => x.ApartmentId == apartment.ApartmentId, apartment, new ReplaceOptions()
            {
                IsUpsert = true
            });
   
        }

        public async Task DeleteApartment(string apartmentId)
        {

            await _apartmentsCollection.FindOneAndDeleteAsync(x=> x.ApartmentId == apartmentId);
        }

        public async Task<Apartment> GetApartmentByIdAndLandlordId(string landlordId, string apartmentId)
        {
            var builder = Builders<Apartment>.Filter;
            var filter = builder.Eq(a => a.ApartmentId, apartmentId) & builder.Eq(a => a.LandlordId, landlordId);

            return await _apartmentsCollection.FindAsync(filter).Result.FirstAsync();


        }

        public async Task<Apartment> GetApartmentById(string apartmentId)
        {
            try
            {
                return await _apartmentsCollection.FindAsync(x => x.ApartmentId == apartmentId).Result.FirstAsync();
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<List<Apartment>> GetApartmentsByUserId(string landlordId)
        {
            return await _apartmentsCollection.FindAsync(x => x.LandlordId == landlordId).Result.ToListAsync();
        }
    }
}