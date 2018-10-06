using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Region.Models;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Services;

namespace WebApi.Region.Controllers
{
    [RoutePrefix("api/addresses")]
    public class AddressController : ApiController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // GET: api/addresses
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAddresses()
        {
            var countryBM = Mapper.Map<IEnumerable<Address>,
                            IEnumerable<AddressBindingModel>>(_addressService.GetAll());

            return Ok(countryBM.ToList());
        }

        // GET: api/addresses/address/info/5
        [HttpGet]
        [Route("address/info/{id}")]
        [ResponseType(typeof(Address))]
        public IHttpActionResult GetAddress(Guid id)
        {
            var addressBM = Mapper.Map<Address, AddressBindingModel>(_addressService.GetById(id));
            if (addressBM != null)
            {
                return Ok(addressBM);
            }
            return Content(HttpStatusCode.NotFound, "Address not found!");
        }

        // PUT: api/addresses/update/5
        [HttpPut]
        [Route("update")]
        public IHttpActionResult PutAddress(AddressBindingModel addressBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var address = Mapper.Map<AddressBindingModel, Address>(addressBM);
                    _addressService.Update(address);

                    addressBM = Mapper.Map<Address, AddressBindingModel>(address);
                    return Ok(addressBM);
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        // POST: api/addresses/save
        [HttpPost]
        [Route("save")]
        [ResponseType(typeof(Address))]
        public IHttpActionResult PostAddress(AddressBindingModel addressBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var address = Mapper.Map<AddressBindingModel, Address>(addressBM);
                    _addressService.Save(address);

                    address = _addressService.GetById(address.Id);

                    addressBM = Mapper.Map<Address, AddressBindingModel>(address);
                    return Ok(addressBM);
                }
                catch (Exception ex)
                {
                    var result = ex.Message;
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        // DELETE: api/addresses/address/del/5
        [HttpDelete]
        [Route("address/del/{id}")]
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(Guid id)
        {
            try
            {
                var addressBM = new AddressBindingModel
                {
                    Id = id
                };
                var address = Mapper.Map<AddressBindingModel, Address>(addressBM);
                _addressService.Delete(address.Id);
                return Ok();

            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _addressService.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(Guid id)
        {
            return _addressService.GetAll().Count(c => c.Id == id) > 0;
        }
    }
}
