using Apartments.Domain.Entities;
using Apartments.Domain.Interfaces;
using Apartments.Infrastructure.Db;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task<int> GetMostApartmentsOwnedByOneUserCount()
        {
            var aggregation = _apartmentsCollection.Aggregate()
            .Group(
                key => key.LandlordId,
                group => new
                {
                    LandlordId = group.Key,
                    IloscObiektów = group.Count()
                }
                ).SortBy(x => x.IloscObiektów);
            var result = await aggregation.ToListAsync();
            if (result == null)
            {
                throw new Exception("problem with GetMostApartmentsOwnedByOneUser");
            }
            return result[0].IloscObiektów;
        }
        public async Task<int> GetUpdatedApartmentsCount()
        {
            var builder = Builders<Apartment>.Filter;
            var filter = builder.Gt(a => a.Version, 1) & builder.Where(x => x.IsUpdateSubmitted == false);
            //var update = Builders<Apartment>.Update.Set(x => x.IsUpdateSubmitted, true).Set(x => x.LastModifiedDate, DateTime.UtcNow);
            var result = await _apartmentsCollection.FindAsync(filter);
            if (result == null)
            {
                throw new Exception("problem with GetUpdatedApartments");
            }
            return result.ToList().Count();
        }
        public async Task<int> GetCreatedApartmentsCount(DateTime startDate, DateTime endDate)
        {
            //filter for number of created apartments from start - end date
            var builder = Builders<Apartment>.Filter;
            var filter = builder.Gte(a => a.CreationDate, startDate) & builder.Lt(a => a.CreationDate, endDate);
            
            var result = await _apartmentsCollection.FindAsync(filter).Result.ToListAsync();
            if(result == null)
            {
                throw new Exception("problem with GetCreatedApartments");
            }
            return result.Count();

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
