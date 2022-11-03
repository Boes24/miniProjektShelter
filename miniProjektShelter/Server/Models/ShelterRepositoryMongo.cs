using System;
using MongoDB.Driver;
using miniProjektShelter.Server.Models;
using miniProjektShelter.Shared;
using System.Net.Http;
using MongoDB.Bson;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace miniProjektShelter.Server.Models
{

    internal class ShelterRepositoryMongo : IShelterRepository
    {
        ShelterDBContext db = new ShelterDBContext();

        public ShelterRepositoryMongo()
        {
        }


        public void AddItem(CustomerBooking item)
        {
            Console.WriteLine("Tilføj booking" + item);
            db.Bookinger.InsertOne(item);
        }

        public bool DeleteItem(int id)
        {
            FilterDefinition<Shelter> item = Builders<Shelter>.Filter.Eq("id", id);
            var deletedItem = db.Items.FindOneAndDelete(item);
            if (deletedItem != null)
                return true;
            else
                return false;
        }

        public bool UpdateItem(Shelter updatedItem)
        {
            var oldItem = FindItem(updatedItem.MongoId!);
            if (oldItem != null)
            {
                //db.Items.ReplaceOne(filter: it => it.MongoId == updatedItem.MongoID, replacement: updatedItem);

                return true;
            }
            else
                return false;
        }

        public bool UpdateBooking(CustomerBooking updatedBooking)
        {
            var oldItem = FindBooking(updatedBooking.MongoId!);
            db.Bookinger.ReplaceOne(filter: it => it.MongoId == updatedBooking.MongoId, replacement: updatedBooking);
            if (oldItem != null)
            {
                return true;
            }
            else
                return false;
        }

        
        //return item with id = -1 if not found
        public Shelter FindItem(string id)
        {

            throw new NotImplementedException();
            FilterDefinition<Shelter> filter = Builders<Shelter>.Filter.Eq("_id", id);
            return db.Items.Find(filter).FirstOrDefault();

        }
        

        public CustomerBooking FindBooking(string id)
        {
              
            FilterDefinition<CustomerBooking> filter = Builders<CustomerBooking>.Filter.Eq("_id", id);
            return db.Bookinger.Find(filter).FirstOrDefault();

        }

        public List<Shelter> GetAllItems()
        {
            return db.Items.Find(_ => true).ToList();
        }

        public List<CustomerBooking> GetAllBookings()
        {
            return db.Bookinger.Find(_ => true).ToList();
        }
    }
}

