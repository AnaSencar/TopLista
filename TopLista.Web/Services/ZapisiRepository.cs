using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLista.Web.Interfaces;
using TopLista.Web.ViewModels;

namespace TopLista.Web.Services
{
    public class ZapisiRepository : IZapisiRepository
    {
        private readonly ZapisiContext _context;

        public ZapisiRepository(ZapisiContext context)
        {
            _context = context;
        }

        public void AddZapis(Zapis zapis)
        {
            if(zapis == null)
            {
                throw new ArgumentNullException(nameof(zapis));
            }
            _context.Zapisi.Add(zapis);
        }

        public void UpdateApprovalOfZapis(Zapis zapis, bool odobreno)
        {
            zapis.Odobreno = odobreno;
        }

        public void DeleteZapis(Zapis zapis)
        {
            _context.Zapisi.Remove(zapis);
        }

        public IEnumerable<Zapis> GetUnapprovedZapisi()
        {
            return _context.Zapisi
                .OrderBy(x => x.VrijemeUSekundama)
                .ThenBy(x => x.DatumUnosa)
                .Where(x => !x.Odobreno)
                .ToList();
        }

        public Zapis GetZapis(int id)
        {
            return _context.Zapisi
                .SingleOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Zapis> GetFilteredZapisi(bool selectApproved, FilterViewModel filter)
        {
            IQueryable<Zapis> zapisi = _context.Zapisi
                .OrderBy(x => x.VrijemeUSekundama)
                .ThenBy(x => x.DatumUnosa)
                .Where(x => x.Odobreno == selectApproved);
            if (filter.NumberOfRecords.HasValue)
            {
                zapisi = zapisi.Take(filter.NumberOfRecords.Value);
            }
            return zapisi.ToList();
        }
    }
}
