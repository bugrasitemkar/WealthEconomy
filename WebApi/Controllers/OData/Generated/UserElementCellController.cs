//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace forCrowd.WealthEconomy.WebApi.Controllers.OData
{
    using forCrowd.WealthEconomy.BusinessObjects;
    using forCrowd.WealthEconomy.Facade;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.OData;
    using WebApi.Controllers.Extensions;

    public abstract class BaseUserElementCellController : BaseODataController
    {
        public BaseUserElementCellController()
		{
			MainUnitOfWork = new UserElementCellUnitOfWork();		
		}

		protected UserElementCellUnitOfWork MainUnitOfWork { get; private set; }

        // GET odata/UserElementCell
        //[Queryable]
        public virtual IQueryable<UserElementCell> Get()
        {
			var userId = this.GetCurrentUserId();
			if (!userId.HasValue)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);	

			var list = MainUnitOfWork.AllLive;
			list = list.Where(item => item.UserId == userId.Value);
            return list;
        }

        // GET odata/UserElementCell(5)
        //[Queryable]
        public virtual SingleResult<UserElementCell> Get([FromODataUri] int elementCellId)
        {
            return SingleResult.Create(MainUnitOfWork.AllLive.Where(userElementCell => userElementCell.ElementCellId == elementCellId));
        }

        // PUT odata/UserElementCell(5)
        public virtual async Task<IHttpActionResult> Put([FromODataUri] int elementCellId, UserElementCell userElementCell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (elementCellId != userElementCell.ElementCellId)
            {
                return BadRequest();
            }

            try
            {
                await MainUnitOfWork.UpdateAsync(userElementCell);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await MainUnitOfWork.All.AnyAsync(item => item.ElementCellId == userElementCell.ElementCellId))
                {
                    return Conflict();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(userElementCell);
        }

        // POST odata/UserElementCell
        public virtual async Task<IHttpActionResult> Post(UserElementCell userElementCell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MainUnitOfWork.InsertAsync(userElementCell);
            }
            catch (DbUpdateException)
            {
                if (await MainUnitOfWork.All.AnyAsync(item => item.ElementCellId == userElementCell.ElementCellId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(userElementCell);
        }

        // PATCH odata/UserElementCell(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public virtual async Task<IHttpActionResult> Patch([FromODataUri] int elementCellId, Delta<UserElementCell> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userElementCell = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.ElementCellId == elementCellId);
            if (userElementCell == null)
            {
                return NotFound();
            }

            var patchEntity = patch.GetEntity();

            // TODO How is passed ModelState.IsValid?
            if (patchEntity.RowVersion == null)
                throw new InvalidOperationException("RowVersion property of the entity cannot be null");

            if (!userElementCell.RowVersion.SequenceEqual(patchEntity.RowVersion))
            {
                return Conflict();
            }

            patch.Patch(userElementCell);
            await MainUnitOfWork.UpdateAsync(userElementCell);

            return Ok(userElementCell);
        }

        // DELETE odata/UserElementCell(5)
        public virtual async Task<IHttpActionResult> Delete([FromODataUri] int elementCellId)
        {
            var userElementCell = await MainUnitOfWork.AllLive.SingleOrDefaultAsync(item => item.ElementCellId == elementCellId);
            if (userElementCell == null)
            {
                return NotFound();
            }

            await MainUnitOfWork.DeleteAsync(userElementCell.ElementCellId);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }

    public partial class UserElementCellController : BaseUserElementCellController
    {
	}
}