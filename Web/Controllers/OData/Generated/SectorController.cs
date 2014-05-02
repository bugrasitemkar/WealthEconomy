//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Controllers.OData
{
    using BusinessObjects;
    using Facade;
    using Microsoft.AspNet.Identity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.OData;

    public partial class SectorController : ODataController
    {
        SectorUnitOfWork unitOfWork = new SectorUnitOfWork();

        // GET odata/Sector
        [Queryable]
        public IQueryable<Sector> GetSector()
        {
			var list = unitOfWork.AllLive;
            return list;
        }

        // GET odata/Sector(5)
        [Queryable]
        public SingleResult<Sector> GetSector([FromODataUri] short key)
        {
            return SingleResult.Create(unitOfWork.AllLive.Where(sector => sector.Id == key));
        }

        // PUT odata/Sector(5)
        public async Task<IHttpActionResult> Put([FromODataUri] short key, Sector sector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != sector.Id)
            {
                return BadRequest();
            }

            unitOfWork.Update(sector);

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!unitOfWork.Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(sector);
        }

        // POST odata/Sector
        public async Task<IHttpActionResult> Post(Sector sector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.Insert(sector);

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateException)
            {
                if (unitOfWork.Exists(sector.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(sector);
        }

        // PATCH odata/Sector(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] short key, Delta<Sector> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sector = await unitOfWork.FindAsync(key);
            if (sector == null)
            {
                return NotFound();
            }

            patch.Patch(sector);
            unitOfWork.Update(sector);

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!unitOfWork.Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(sector);
        }

        // DELETE odata/Sector(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] short key)
        {
            var sector = await unitOfWork.FindAsync(key);
            if (sector == null)
            {
                return NotFound();
            }

            unitOfWork.Delete(sector.Id);
            await unitOfWork.SaveAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
