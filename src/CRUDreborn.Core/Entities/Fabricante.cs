using Abp.Domain.Entities.Auditing;
using System;

namespace CRUDreborn.Entities
{
    public class Fabricante : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Fabricante()
        {
            this.CreationTime = DateTime.Now;
        }

        public Fabricante(string name, string description)
        {
            this.Name = name;
            this.Description = description;
            CreationTime = DateTime.Now;
        }

    }
}
