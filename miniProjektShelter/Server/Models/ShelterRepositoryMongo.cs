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


        public void AddItem(CostumerBooking costumerInfo)
        {

            Console.WriteLine("Tilføj booking" + costumerInfo);

            /*
            var newBooking = new BsonDocument
            {
                {"KundeNavn",costumerInfo.CostumerName },
                {"Telefon",costumerInfo.PhoneNumber },
                {"Email",costumerInfo.Email },
                { "date1", costumerInfo.Date1},
                { "date2", costumerInfo.Date2},
                { "ShelterID", costumerInfo.ShelterID}
            };
            */

            db.bookinger.InsertOne(costumerInfo);
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


        //return item with id = -1 if not found
        public Shelter FindItem(string id)
        {

            FilterDefinition<Shelter> filter = Builders<Shelter>.Filter.Eq("MongoId", id);
            return db.Items.Find(filter).FirstOrDefault();

        }

        public List<Shelter> GetAllItems()
        {
            return db.Items.Find(_ => true).ToList();
        }
    }
}

