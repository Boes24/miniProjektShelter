using System;
using miniProjektShelter.Shared;

namespace miniProjektShelter.Server.Models{
    public class IShelterRepository{
        public IShelterRepository()
        {

            List<Shelter> GetAllItems();

            Shelter FindItem(int id);

            void AddItem(Shelter item);
            bool DeleteItem(int id);
            bool UpdateItem(Shelter item);
        }
    }
}

