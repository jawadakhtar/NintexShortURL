using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.BusinessLayer
{
    public abstract class BusinessEntities
    {
        public int Id { get; set; }

        public virtual void Add()
        {
            throw new NotSupportedException();
        }

        public virtual void Delete()
        {
            throw new NotSupportedException();
        }

        public virtual void Update()
        {
            throw new NotSupportedException();
        }

    }
}
    