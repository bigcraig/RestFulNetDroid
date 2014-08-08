using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestFulNetDroid.Models;

namespace RestFulNetDroid.Controllers
{
    public class TreasuresController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Treasures
        public IQueryable<Treasure> GetTreasures()
        {
            return db.Treasures.Include(c => c.Coordinates);
           //  return db.Treasures;


        }

        // GET: api/Treasures/5
     /*   [ResponseType(typeof(Treasure))]
        public async Task<IHttpActionResult> GetTreasure(string id)
        {
            Treasure treasure = await db.Treasures.FindAsync(id);
            if (treasure == null)
            {
                return NotFound();
            }

            return Ok(treasure);
        }  */

        // GET: api/TreasuresDto/5
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



        // PUT: api/Treasures/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTreasure(string id, Treasure treasure)
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
                await db.SaveChangesAsync();
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

        // POST: api/Treasures
        [ResponseType(typeof(Treasure))]
        public async Task<IHttpActionResult> PostTreasure(Treasure treasure)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Treasures.Add(treasure);

            try
            {
                await db.SaveChangesAsync();
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

        // DELETE: api/Treasures/5
        [ResponseType(typeof(Treasure))]
        public async Task<IHttpActionResult> DeleteTreasure(string id)
        {
            Treasure treasure = await db.Treasures.FindAsync(id);
            if (treasure == null)
            {
                return NotFound();
            }

            db.Treasures.Remove(treasure);
            await db.SaveChangesAsync();

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