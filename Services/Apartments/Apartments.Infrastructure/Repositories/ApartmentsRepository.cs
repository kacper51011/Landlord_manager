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

        public Task<Apartment> CreateApartment(Apartment apartment)
        {
            throw new NotImplementedException();
        }

        public Task<Apartment> DeleteApartment(string landlordId)
        {
            throw new NotImplementedException();
        }

        public Task<Apartment> GetApartmentsById(string landlordId)
        {
            throw new NotImplementedException();
        }

        public Task<Apartment> UpdateApartment(Apartment apartment)
        {
            throw new NotImplementedException();
        }
    }
}
