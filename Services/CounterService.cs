using Eapproval.Models;
using MongoDB.Driver;

namespace Eapproval.Services
{
    public class CounterService
    {
        private readonly IMongoCollection<Counter> _counter;
        private readonly string Id;
        private readonly string TravelId;


        public CounterService()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase("eapproval");
            _counter = mongoDatabase.GetCollection<Counter>("counter");
            Id = "64db3d107009062c716b3156";
            TravelId = "65080d989747676e0b241195";
        }

     

        public async Task<int> GetOrCreateCounterAsync()
        {
            var existingCounter = await _counter.Find(d => d.Id == Id).FirstOrDefaultAsync();

            if (existingCounter != null)
            {
                existingCounter.Count++;

                await _counter.ReplaceOneAsync(x => x.Id == Id, existingCounter);
                
                return existingCounter.Count;

                
            }

            var newCounter = new Counter
            {
                Id = Id,
                Count = 0,
                // Set other properties
            };

            newCounter.Count++;

            await _counter.InsertOneAsync(newCounter);

            return newCounter.Count;
        }





         public async Task<int> GetOrCreateCounterTravelAsync()
        {
            var existingCounter = await _counter.Find(d => d.Id == TravelId).FirstOrDefaultAsync();

            if (existingCounter != null)
            {
                existingCounter.Count++;

                await _counter.ReplaceOneAsync(x => x.Id == TravelId, existingCounter);
                
                return existingCounter.Count;

                
            }

            var newCounter = new Counter
            {
                Id = Id,
                Count = 0,
                // Set other properties
            };

            newCounter.Count++;

            await _counter.InsertOneAsync(newCounter);

            return newCounter.Count;
        }

    }
}
