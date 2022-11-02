﻿using System;
using miniProjektShelter.Shared;

namespace miniProjektShelter.Server.Models{


    public interface IShelterRepository{
        List<Shelter> GetAllItems();

        Shelter FindItem(string id);

        void AddItem(CostumerBooking costumerInfo);
        bool DeleteItem(int id);
        bool UpdateItem(Shelter item);
        
    }
}

