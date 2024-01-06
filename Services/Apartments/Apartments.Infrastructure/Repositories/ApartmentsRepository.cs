using Apartments.Domain.Entities;
using Apartments.Domain.Interfaces;
using Apartments.Infrastructure.Db;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Apartments.Infrastructure.Repositories
{
    public class ApartmentsRepository : IApartmentsRepository
    {
        private IMongoCollection<Apartment> _apartmentsCollection;
        public ApartmentsRepository(IOptions<MongoSettings> apartmentsDatabaseSettings)

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

        public async Task<int> GetMostApartmentsOwnedByOneUserCount(DateTime endDate)
        {
            var filter = Builders<Apartment>.Filter.Lte(a => a.CreationDate, endDate);
            var aggregation = _apartmentsCollection.Aggregate()
            .Match(filter)
            .Group(
                key => key.LandlordId,
                group => new
                {
                    LandlordId = group.Key,
                    objectsCount = group.Count()
                }
                )
            .SortBy(x => x.objectsCount);
            var result = await aggregation.ToListAsync();
            return result[0].objectsCount;
        }
        public async Task<int> GetUpdatedApartmentsCount(DateTime startDate, DateTime endDate)
        {
            var builder = Builders<Apartment>.Filter;
            var dateRangeFilter = builder.Gte(a => a.UpdateDates.Min(), startDate) & builder.Lte(a => a.UpdateDates.Max(), endDate);
            var versionFilter = builder.Gt(a => a.Version, 1);
            var combinedFilter = versionFilter & dateRangeFilter;

            var result = await _apartmentsCollection.CountDocumentsAsync(combinedFilter);

            return (int)result;
        }
        public async Task<int> GetCreatedApartmentsCount(DateTime startDate, DateTime endDate)
        {
            //filter for number of created apartments from start - end date
            var builder = Builders<Apartment>.Filter;
            var filter = builder.Gte(a => a.CreationDate, startDate) & builder.Lt(a => a.CreationDate, endDate);

            var result = await _apartmentsCollection.CountDocumentsAsync(filter);


            return (int)result;

        }
        public async Task DeleteApartment(string apartmentId)
        {

            await _apartmentsCollection.FindOneAndDeleteAsync(x => x.ApartmentId == apartmentId);
        }

        public async Task<Apartment> GetApartmentByIdAndLandlordId(string landlordId, string apartmentId)
        {
            var builder = Builders<Apartment>.Filter;
            var filter = builder.Eq(a => a.ApartmentId, apartmentId) & builder.Eq(a => a.LandlordId, landlordId);

            return await _apartmentsCollection.Find(filter).FirstAsync();


        }

        public async Task<Apartment> GetApartmentById(string apartmentId)
        {

            return await _apartmentsCollection.Find(x => x.ApartmentId == apartmentId).FirstOrDefaultAsync();


        }

        public async Task<List<Apartment>> GetApartmentsByUserId(string landlordId)
        {
            return await _apartmentsCollection.FindAsync(x => x.LandlordId == landlordId).Result.ToListAsync();
        }
    }
}
