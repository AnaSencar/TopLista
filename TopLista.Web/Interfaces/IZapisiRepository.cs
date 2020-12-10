using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopLista.Web.ViewModels;

namespace TopLista.Web.Interfaces
{
    public interface IZapisiRepository
    {
        IEnumerable<Zapis> GetFilteredZapisi(bool selectApproved, FilterViewModel filter);
        void AddZapis(Zapis zapis);
        void Save();
        IEnumerable<Zapis> GetUnapprovedZapisi();
        Zapis GetZapis(int id);
        void DeleteZapis(Zapis zapis);
        void UpdateApprovalOfZapis(Zapis zapis, bool odobreno);
    }
}
