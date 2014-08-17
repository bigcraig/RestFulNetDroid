using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestFulNetDroid.Models;
using System.Threading.Tasks;

namespace RestFulNetDroid.Controllers
{
    public class randomController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/random
        public IQueryable<Treasure> GetTreasures()
        {
            return db.Treasures;
        }

        // GET: api/random/5
         [ResponseType(typeof(TreasureDto))]
                public async Task<IHttpActionResult> GetGerbilDto(string id)
        {
            Treasure treasure = await db.Treasures.Include(c => c.Coordinates).SingleOrDefaultAsync(p => p.Id == id);
         //         treasure   = Treasure.I 
            if (treasure == null)
            {
                return NotFound();
            } 
            var treasureDto = new TreasureDto(treasure);
        
            

            return Ok(treasureDto);
        }


        // PUT: api/random/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTreasure(string id, Treasure treasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != treasure.Id)
            {
                return BadRequest();
            }

            db.Entry(treasure).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TreasureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/random
        [ResponseType(typeof(Treasure))]
        public IHttpActionResult PostTreasure(Treasure treasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treasures.Add(treasure);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TreasureExists(treasure.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = treasure.Id }, treasure);
        }

        // DELETE: api/random/5
        [ResponseType(typeof(Treasure))]
        public IHttpActionResult DeleteTreasure(string id)
        {
            Treasure treasure = db.Treasures.Find(id);
            if (treasure == null)
            {
                return NotFound();
            }

            db.Treasures.Remove(treasure);
            db.SaveChanges();

            return Ok(treasure);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TreasureExists(string id)
        {
            return db.Treasures.Count(e => e.Id == id) > 0;
        }
    }
}