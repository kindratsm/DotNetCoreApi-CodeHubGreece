using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DotNetCoreApi_CodeHubGreece.Helpers
{
    public class GenericODataController<T> : ODataController where T: Models.CommonModel
    {

        private readonly ModelContext _db;

        public GenericODataController(ModelContext db)
        {
            _db = db;
        }

        private bool Exists(UInt64 id)
        {
            if (id <= 0)
            {
                return false;
            }

            return _db.Set<T>().Any((m) => m.Id == id);
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_db.Set<T>());
        }

        [EnableQuery]
        public IActionResult Get([FromODataUri] UInt64 key)
        {
            T model = _db.Set<T>().FirstOrDefault(c => c.Id == key);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        public IActionResult Post([FromBody] T model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id > 0)
            {
                if (Exists(model.Id))
                {
                    _db.Set<T>().Update(model);
                    _db.SaveChanges();

                    return Ok(model);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                _db.Set<T>().Add(model);
                _db.SaveChangesAsync();

                return Created(model);
            }
        }

        public IActionResult Delete([FromODataUri] UInt64 key)
        {
            T model = _db.Set<T>().Find(key);
            if (model == null)
            {
                return NotFound();
            }

            _db.Set<T>().Remove(model);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
