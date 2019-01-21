using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.BusinessLayer
{
    /// <summary>
    /// An abstract class to generalized the BusinessEntities
    /// </summary>
    public abstract class BusinessEntities
    {
        /// <summary>
        /// Id property to uniqely identifies the Business Entities
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Adds the Business Entity in repository
        /// </summary>
        public virtual void Add()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Deletes the Business Entity from repository
        /// </summary>
        public virtual void Delete()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Updates the Business Entity in repository
        /// </summary>
        public virtual void Update()
        {
            throw new NotSupportedException();
        }

    }
}
    