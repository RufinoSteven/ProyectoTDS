using MainServiceNew.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using MainService.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MainServiceNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemsController : Controller
    {
        private readonly AppDb_Context context;
        private readonly Detalles detalles = new Detalles();

        public ItemsController(AppDb_Context context)
        {
            this.context = context;

        }

        // GET: api/<ItemsController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.Items.ToList());
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
            
        // GET api/<ItemsController>/5
        [HttpGet("{id}", Name ="GetItems")]
        public ActionResult Get(int id)
        {

            
            try
            {
                var Items = context.Items.FirstOrDefault(g => g.id == id);
                    return Ok(Items);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("GetDetalle/{idFactura}")]

        public ActionResult GetDetalle(int idFactura)
        {
            var detallesList = new List<Detalles>();
            try
            {
               detallesList = detalles.GetDetalles(idFactura);
               return Ok(detallesList);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<ItemsController>
        [HttpPost]
        public ActionResult Post([FromBody] Items Items)
        {
            try
            {
                context.Items.Add(Items);
                context.SaveChanges();
                return CreatedAtRoute("GetItems", new { id = Items.id }, Items);
            }catch(Exception ex)
            {
             return BadRequest(ex.Message);
            }
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Items Items)
        {
            try
            {

                if (Items.id == id)
                {
                    context.Entry(Items).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetItems", new { id = Items.id }, Items);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var gItems = context.Items.FirstOrDefault(X => X.id == id);
                if (gItems != null)
                {
                    var itemList = context.Detalles.Where(x => x.item_id == id).Select(c => new
                    {
                        detalle = c.id,
                        item = c.item_id,
                        qty = c.qty
                    }).ToList();
                    foreach (var item in itemList)
                    {
                        var gItem = await context.Detalles.FirstOrDefaultAsync(X => X.id == item.detalle);
                        context.Detalles.Remove(gItem);
                        await context.SaveChangesAsync();
                    }
                    context.Items.Remove(gItems);
                    await context.SaveChangesAsync();
                    return Ok(id);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
