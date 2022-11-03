using System;
using miniProjektShelter.Shared;

namespace miniProjektShelter.Server.Models{


    public interface IShelterRepository{
        List<Shelter> GetAllItems();
        List<CustomerBooking> GetAllBookings();
        Shelter FindItem(string id);

        void AddItem(CustomerBooking item);
        bool DeleteItem(int id);
        bool UpdateItem(Shelter item);
        bool UpdateBooking(CustomerBooking updatedBooking);


    }
}

