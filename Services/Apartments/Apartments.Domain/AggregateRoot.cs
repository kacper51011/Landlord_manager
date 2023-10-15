﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain
{
    public abstract class AggregateRoot
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreationDate { get; private set; }
        public DateTime LastModifiedDate { get; private set;}

        public void SetCreationDate() { CreationDate = DateTime.Now; }
        public void SetLastModifiedDate() {  LastModifiedDate = DateTime.Now; }

    }

}
