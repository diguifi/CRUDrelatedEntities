using System.Linq;
using Abp.Application.Editions;
using CRUDreborn.Editions;
using CRUDreborn.EntityFramework;

namespace CRUDreborn.Migrations.SeedData
{
    public class DefaultEditionsCreator
    {
        private readonly CRUDrebornDbContext _context;

        public DefaultEditionsCreator(CRUDrebornDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition == null)
            {
                defaultEdition = new Edition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
                _context.Editions.Add(defaultEdition);
                _context.SaveChanges();

                //TODO: Add desired features to the standard edition, if wanted!
            }   
        }
    }
}