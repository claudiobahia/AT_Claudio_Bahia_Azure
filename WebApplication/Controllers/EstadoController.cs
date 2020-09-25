using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class EstadoController : Controller
    {
        readonly string _linkApi = "https://localhost:44355/api/";

        // GET: EstadoController
        public ActionResult Index()
        {

            var client = new RestClient();
            var request = new RestRequest(_linkApi + "estado", DataFormat.Json);


            var response = client.Get<List<Estado>>(request);

            if (response.Data == null)
                response.Data = new List<Estado>();

            return View(response.Data);
        }

        // GET: EstadoController/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "estado/" + id, DataFormat.Json);

            var response = client.Get<Estado>(request);

            return View(response.Data);
        }

        // GET: EstadoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Estado estado, IFormFile foto)
        {
            if (foto != null)
            {
                var urlFoto = UploadFotoPessoa(foto, estado.Nome);
                estado.Foto = urlFoto;
            }
            try
            {
                if (ModelState.IsValid == false)
                    return View(estado);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "estado", DataFormat.Json);

                request.AddJsonBody(estado);

                var response = client.Post<Estado>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(estado);
            }
        }

        // GET: EstadoController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "estado/" + id, DataFormat.Json);


            var response = client.Get<Estado>(request);

            return View(response.Data);
        }

        // POST: EstadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Estado estado)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(estado);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "estado/" + id, DataFormat.Json);

                request.AddJsonBody(estado);

                var response = client.Put<Estado>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "estado/" + id, DataFormat.Json);

            var response = client.Get<Estado>(request);

            return View(response.Data);
        }

        // POST: EstadoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Estado estado)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_linkApi + "estado/" + id, DataFormat.Json);

                request.AddJsonBody(estado);

                var response = client.Delete<Estado>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        private string UploadFotoPessoa(IFormFile urlFoto, string nomeEstado)
        {
            var reader = urlFoto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=atclaudiobahia;AccountKey=nCfPRYc6QAYEGAw2/aOhDJrJdsqZ33j0GowNLM1RecYLpp1yQR0OaX8zVawig8gPcnJ/uSvXY5XYn34Gu632JA==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("atclaudiobahia");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(nomeEstado);
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }
    }
}
