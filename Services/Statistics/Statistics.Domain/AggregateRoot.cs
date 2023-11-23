using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Statistics.Domain
{
    public abstract class AggregateRoot
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public DateTime CreationDate { get; private set; }
        public DateTime LastModifiedDate { get; private set; }

        public void SetCreationDate() { CreationDate = DateTime.Now; }
        public void SetLastModifiedDate() { LastModifiedDate = DateTime.Now; }
    }
}
