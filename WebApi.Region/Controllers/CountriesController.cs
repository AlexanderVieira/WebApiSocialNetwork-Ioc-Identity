using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Region.Models;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Services;

namespace WebApi.Region.Controllers
{
    [RoutePrefix("api/countries")]
    public class CountriesController : ApiController
    {
        private readonly ICountryService _countryService;
        private readonly IBlobStorageService _blobStorageService;

        public CountriesController(ICountryService countryService,
                                    IBlobStorageService blobStorageService)
        {
            _countryService = countryService;
            _blobStorageService = blobStorageService;
        }

        public CountriesController()
        {

        }

        // GET: api/Countries
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetCountries()
        {
            var countryBM = Mapper.Map<IEnumerable<Country>,
                            IEnumerable<CountryBindingModel>>(_countryService.GetAll());

            return Ok(countryBM.ToList());
        }

        // GET: api/Countries/country/info/5
        [HttpGet]
        [Route("country/info/{id}")]
        //[ResponseType(typeof(Country))]
        public IHttpActionResult GetCountry(Guid id)
        {
            var countryBM = Mapper.Map<Country, CountryBindingModel>(_countryService.GetById(id));
            if (countryBM != null)
            {
                return Ok(countryBM);
            }
            return Content(HttpStatusCode.NotFound, "Country not found!");
        }

        // PUT: api/Countries/update/5
        [HttpPut]
        [Route("update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCountry(CountryBindingModel countryBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var country = Mapper.Map<CountryBindingModel, Country>(countryBM);
                    _countryService.Update(country);

                    countryBM = Mapper.Map<Country, CountryBindingModel>(country);
                    return Ok(countryBM);
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

        // POST: api/Countries/save
        [HttpPost]
        [Route("save")]
        //[ResponseType(typeof(Country))]
        public IHttpActionResult PostCountry(CountryBindingModel countryBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var country = Mapper.Map<CountryBindingModel, Country>(countryBM);
                    _countryService.Save(country);

                    country = _countryService.GetById(country.Id);

                    countryBM = Mapper.Map<Country, CountryBindingModel>(country);
                    return Ok(countryBM);
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

        // DELETE: api/Countries/5
        [HttpDelete]
        [Route("country/del/{id}")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult DeleteCountry(Guid id)
        {
            try
            {
                var countryBM = new CountryBindingModel()
                {
                    Id = id
                };
                var country = Mapper.Map<CountryBindingModel, Country>(countryBM);
                _countryService.Delete(country.Id);
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
                _countryService.Dispose();
            }
            base.Dispose(disposing);
        }

        //POST: api/Countries/upload
        [HttpPost]
        [Route("upload")]
        public IHttpActionResult Upload()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var photoUrl = String.Empty;                   
                    var files = HttpContext.Current.Request.Files;

                    for (int i = 0; i < files.Count; i++)
                    {
                        var imageFile = files[i];
                        photoUrl = _blobStorageService
                            .UploadImage("countries", Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName),
                                          imageFile.InputStream, imageFile.ContentType);
                        
                    }
                    return Ok(photoUrl);
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

        private bool CountryExists(Guid id)
        {
            return _countryService.GetAll().Count(c => c.Id == id) > 0;
        }

    }
}