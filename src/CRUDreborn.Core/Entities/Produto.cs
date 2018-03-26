using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDreborn.Entities
{
    public class Produto : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("AssignedManufacturer")]
        public long AssignedManufacturer_Id { get; set;  }
        public Fabricante AssignedManufacturer { get; set; }
        public bool Consumable { get; set; }

        public Produto()
        {
            CreationTime = DateTime.Now;
        }

        public Produto(string name, string description, Fabricante assignedManufacturer, bool consumable)
        {
            this.Name = name;
            this.Description = description;
            this.AssignedManufacturer = assignedManufacturer;
            this.Consumable = consumable;
            CreationTime = DateTime.Now;
        }
    }
}
