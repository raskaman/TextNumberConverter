using Microsoft.Practices.Unity;
using MyApp.DependencyResolution;
using MyApp.ServiceContracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyApp.Website.API
{
    public class TextConvertController : ApiController
    {
        private IUnityContainer _container;

        ISentenceConverterService _textConverterService;
        public TextConvertController()
        {
            _container = new UnityContainerFactory().BuildUnityContainer();
            _textConverterService = _container.Resolve<ISentenceConverterService>();
        }

        // POST: api/TextConvert
        public string Post([FromBody]string value)
        {
            try
            {
                return _textConverterService.TextToNumberWords(value);
            }
            catch (Exception)
            {
                // TODO: log the exception
                return "An error occured!";
            }
            
        }

    }
}
