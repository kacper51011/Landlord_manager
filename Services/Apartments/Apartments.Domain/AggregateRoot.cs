using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apartments.Domain
{
    public abstract class AggregateRoot
    {
        public string Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime LastModifiedDate { get; private set;}

        public void SetCreationDate() { CreationDate = DateTime.Now; }
        public void SetLastModifiedDate() {  LastModifiedDate = DateTime.Now; }

    }

}
