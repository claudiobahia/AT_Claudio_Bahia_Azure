using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebApplication.Models;

using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Repository.Domain;

namespace WebApplication.Controllers
{
    public class AmigoController : Controller
    {
        readonly string _linkApi = "https://localhost:5003/api/";

        // GET: AmigoController
        public ActionResult Index()
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "amigo");
            var response = client.Get<List<Amigo>>(request);
            return View(response.Data);
        }

        // GET: AmigoController/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "amigo/" + id);
            var response = client.Get<Amigo>(request);

            var request2 = new RestRequest(_linkApi + "amigo/pessoa/" + id);
            var persons = client.Get<List<Amigo>>(request2);
            ViewData["pessoa"] = persons.Data;

            var request3 = new RestRequest(_linkApi + "amizade/" + id,DataFormat.Json);
            var amigos = client.Get<List<Amizade>>(request3);
            ViewData["amigo"] = amigos.Data;

            var amigosIds = new List<int>();
            foreach (var data in amigos.Data)
            {
                amigosIds.Add(data.AmigoId);
            }
            if (amigosIds.Count == 0) amigosIds = new List<int>();

            ViewData["AmigosIds"] = amigosIds;

            return View(response.Data);
        }

        // GET: AmigoController/Create
        public ActionResult Create()
        {
            return View();
        }

        private string UploadFotoPessoa(IFormFile Foto, int id)
        {
            var reader = Foto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=redesocialinfnet;AccountKey=vZcfcl7NqHfcIQJXfUXZRe4U1ePNyU3D4A9mkbj4yoWNFWl/2f78zN9gDFfbg05n1tKvp6QlTTT5jRIVFtTqmg==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("fotos-amigos");
            container.CreateIfNotExists();
            //Substituir imagem de mesmo user
            var blob = container.GetBlockBlobReference(id.ToString());
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }

        // POST: AmigoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Amigo amigo, IFormFile foto)
        {
            if (foto != null)
            {
                var urlFoto = UploadFotoPessoa(foto, amigo.Nome);
                amigo.Foto = urlFoto;
            }
            try
            {
                if (ModelState.IsValid == false)
                    return View(amigo);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "amigo", DataFormat.Json);
                request.AddJsonBody(amigo);

                var response = client.Post<Amigo>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro, por favor tente mais tarde.");
                return View(amigo);
            }
        }

        // GET: AmigoController/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "amigo/" + id, DataFormat.Json);
            var response = client.Get<Amigo>(request);

            return View(response.Data);
        }

        // POST: AmigoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Amigo amigo)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View(amigo);

                var client = new RestClient();
                var request = new RestRequest(_linkApi + "amigo/" + id, DataFormat.Json);
                request.AddJsonBody(amigo);

                var response = client.Put<Amigo>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AmigoController/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "amigo/" + id, DataFormat.Json);
            var response = client.Get<Amigo>(request);

            return View(response.Data);
        }

        // POST: AmigoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Amigo amigo)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_linkApi + "amigo/" + id, DataFormat.Json);
                request.AddJsonBody(amigo);

                var response = client.Delete<Amigo>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Amizade()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Amizade(int pessoaId, int amigoId)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_linkApi + "amizade", DataFormat.Json);
                AmizadeWeb model = new AmizadeWeb(pessoaId,amigoId);
                request.AddJsonBody(model);
                var response = client.Post<AmizadeWeb>(request);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured, please try again later!");
                return View();
            }

        }

        public class AmizadeWeb
        {
            public int PessoaId { get; set; }
            public int AmigoId { get; set; }

            public AmizadeWeb(int wPessoaId, int wAmigoId)
            {
                PessoaId = wPessoaId;
                AmigoId = wAmigoId;
            }
        }

        public ActionResult DeleteAmizade(int PessoaId, int AmigoId)
        {
            var client = new RestClient();
            var request = new RestRequest(_linkApi + "amizade/" + PessoaId + "/" + AmigoId);
            _ = new Amizade { PessoaId = PessoaId, AmigoId = AmigoId };
            _ = client.Delete<Amizade>(request);

            return RedirectToAction("Details", "amigo", new { id = PessoaId });
        }

        private string UploadFotoPessoa(IFormFile urlFoto, string nomeAmigo)
        {
            var reader = urlFoto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=atclaudiobahia;AccountKey=nCfPRYc6QAYEGAw2/aOhDJrJdsqZ33j0GowNLM1RecYLpp1yQR0OaX8zVawig8gPcnJ/uSvXY5XYn34Gu632JA==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("atclaudiobahia");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(nomeAmigo);
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();

            return destinoDaImagemNaNuvem;
        }
    }
}
