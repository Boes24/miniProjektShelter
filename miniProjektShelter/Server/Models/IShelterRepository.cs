using System;
using miniProjektShelter.Shared;

namespace miniProjektShelter.Server.Models{


    public interface IShelterRepository{
        List<Shelter> GetAllItems();
        List<CostumerBooking> GetAllBookings();
        Shelter FindItem(string id);

        void AddItem(CostumerBooking item);
        bool DeleteItem(int id);
        bool UpdateItem(Shelter item);
        
    }
}

