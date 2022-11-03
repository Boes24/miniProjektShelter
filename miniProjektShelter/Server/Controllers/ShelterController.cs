using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using miniProjektShelter.Server.Models;
using miniProjektShelter.Shared;


namespace miniProjektShelter.Server.Controllers{
    [ApiController]
    [Route("api/shelterAPI")]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterRepository Repository = new ShelterRepositoryMongo();

        //contructor tjekker om repository er tom og hvis den er, laves et nyt repository 

        public ShelterController(IShelterRepository shelterRepository)
        {
            if (Repository == null && shelterRepository != null)
            {
                Repository = shelterRepository;
                Console.WriteLine("Repository initialized");
            }
        }


        [HttpGet]
        public IEnumerable<Shelter> GetAllItems()
        {
            return Repository.GetAllItems();
        }


        [HttpGet]
        [Route("getAllBookings")] //route er til for at lave flere get metoder i samme controller
        public IEnumerable<CustomerBooking> GetAllBookings()
        {
            return Repository.GetAllBookings();
        }

        
        [HttpDelete("{id:int}")]
        public StatusCodeResult DeleteItem(int id)
        {
            Console.WriteLine("Server: Delete item called: id = " + id);

            bool deleted = Repository.DeleteItem(id);
            if (deleted)
            {
                Console.WriteLine("Server: Item deleted succces");
                int code = (int)HttpStatusCode.OK;
                return new StatusCodeResult(code);
            }
            else
            {
                Console.WriteLine("Server: Item deleted fail - not found");
                int code = (int)HttpStatusCode.NotFound;
                return new StatusCodeResult(code);
            }
        }

        [HttpPost]
        public void AddItem(CustomerBooking item)
        {
            Repository.AddItem(item);
        }


        [HttpGet("{id:int}")]
        public Shelter FindItem(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public void Update(CustomerBooking item)
        {
            throw new NotImplementedException();
        }

    }
}

