using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using RestSharp;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class PaisController : Controller
    {
        readonly string _linkApi = "https://localhost:44355/api/";
        string BLOB { get; set; }
        public PaisController(IConfiguration configuration)
        {
            BLOB = configuration.GetConnectionString("blob");
        }
        // GET: PaisController
        public ActionResult Index()
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "pais", DataFormat.Json);

            var response = client.Get<List<Pais>>(request);

            if (response.Data == null)
                response.Data = new List<Pais>();

            return View(response.Data);
        }

        // GET: PaisController/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "pais/" + id, DataFormat.Json);

            var response = client.Get<Pais>(request);

            return View(response.Data);
        }

        // GET: PaisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pais pais, IFormFile foto)
        {
            if(foto != null)
            {
                var urlFoto = UploadFotoPessoa(foto, pais.Nome);
                pais.Foto = urlFoto;
            }

            try
            {
                if (ModelState.IsValid == false)
                    return View(pais);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "pais", DataFormat.Json);

                request.AddJsonBody(pais);

                var response = client.Post<Pais>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(pais);
            }
        }

        // GET: PaisController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "pais/" + id, DataFormat.Json);


            var response = client.Get<Pais>(request);

            return View(response.Data);
        }

        // POST: PaisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pais pais)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(pais);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "pais/" + id, DataFormat.Json);

                request.AddJsonBody(pais);

                var response = client.Put<Pais>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaisController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "pais/" + id, DataFormat.Json);

            var response = client.Get<Pais>(request);

            return View(response.Data);
        }

        // POST: PaisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pais pais)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_linkApi + "pais/" + id, DataFormat.Json);

                request.AddJsonBody(pais);

                var response = client.Delete<Pais>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string UploadFotoPessoa(IFormFile urlFoto, string nomePais)
        {
            var reader = urlFoto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=atclaudiobahia;AccountKey=nCfPRYc6QAYEGAw2/aOhDJrJdsqZ33j0GowNLM1RecYLpp1yQR0OaX8zVawig8gPcnJ/uSvXY5XYn34Gu632JA==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("atclaudiobahia");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(nomePais);
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }
    }
}
