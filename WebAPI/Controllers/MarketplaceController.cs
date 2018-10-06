using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Services;
using WebAPI.BLL.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/marketplaces")]
    public class MarketplaceController : ApiController
    {
        private readonly IMarketplaceService _marketplaceService;
        private readonly IImageService _imageService;
        private readonly IBlobStorageService _blobStorageService;

        public MarketplaceController(IMarketplaceService marketplaceService, 
                                      IImageService imageService, IBlobStorageService blobStorageService)
        {
            _marketplaceService = marketplaceService;
            _imageService = imageService;
            _blobStorageService = blobStorageService;
        }

        //GET: api/images/all        
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAll()
        {
            var marketplaces = Mapper.Map<IEnumerable<Marketplace>,
                            IEnumerable<MarketplaceBindingModel>>(_marketplaceService.GetAll());

            return Ok(marketplaces.ToList());
        }

        //GET: api/marketplaces/marketplace/info/5
        [HttpGet]
        [Route("marketplace/info/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var marketplaceBM = Mapper.Map<Marketplace, MarketplaceBindingModel>(_marketplaceService.GetById(id));
            if (marketplaceBM != null)
            {
                return Ok(marketplaceBM);
            }
            return Content(HttpStatusCode.NotFound, "Marketplace not found!");
        }

        //POST: api/marketplaces/save        
        [HttpPost]
        [Route("save")]
        public IHttpActionResult Save(MarketplaceBindingModel marketplaceBM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var marketplace = Mapper.Map<MarketplaceBindingModel, Marketplace>(marketplaceBM);
                    _marketplaceService.Save(marketplace);

                    marketplaceBM = Mapper.Map<Marketplace, MarketplaceBindingModel>(marketplace);
                    return Ok(marketplaceBM);
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

        //PUT: api/marketplaces/update/5
        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(MarketplaceBindingModel marketplaceBM)
        {
            try
            {
                var marketplace = Mapper.Map<MarketplaceBindingModel, Marketplace>(marketplaceBM);
                _marketplaceService.Update(marketplace);

                marketplaceBM = Mapper.Map<Marketplace, MarketplaceBindingModel>(marketplace);
                return Ok(marketplaceBM);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }

        //DELETE: api/marketplaces/marketplace/del/5
        [HttpDelete]
        [Route("marketplace/del/{id}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                var marketplaceBM = new MarketplaceBindingModel()
                {
                    Id = id
                };
                var marketplace = Mapper.Map<MarketplaceBindingModel, Marketplace>(marketplaceBM);
                _marketplaceService.Delete(marketplace.Id);
                return Ok();

            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return Ok(StatusCode(HttpStatusCode.BadRequest));
        }
    }
}
