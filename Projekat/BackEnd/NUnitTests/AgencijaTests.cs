﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using WebTemplate.Controllers;

namespace NUnitTests
{
    public class AgencijaTests
    {
        private WebTemplate.Controllers.AgencijaContoller _agencijaController;
        private Context _context;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("IspitCS");

            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new Context(options);

            _agencijaController = new WebTemplate.Controllers.AgencijaContoller(_context);
        }

        //DodajAgencija
        //1.Test: VracaOKStatus
        [Test]
        public async Task DodajAgenciju_UspesnoDodavanje_VracaOkStatus()
        {
            Agencija novaAgencija = new Agencija
            {
                Naziv = "Nova Agencija",
                Adresa = "Nova Adresa",
                Grad = "Novi Grad",
                Email = "nova@email.com",
                BrojTelefona = "123456789"
            };

            var result = await _agencijaController.DodajAgenciju(novaAgencija);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(novaAgencija, "novaAgencija nije instancirana.");

            Assert.IsNotNull(novaAgencija.Id, "Id nije postavljen nakon dodavanja agencije.");
            Assert.AreEqual($"ID nove agencije je = {novaAgencija.Id}", okResult?.Value);
        }

        //2.Test: VracaBadRequest jer nema Email i BrojTelefona
        [Test]
        public async Task DodajAgenciju_NeuspesnoDodavanje_VracaBadRequest()
        {
            Agencija agencijaSaGreskom = new Agencija
            {
                Naziv = "Nova agencija",
                Adresa = "Nova Adresa",
                Grad = "Novi Grad"
            };

            var result = await _agencijaController.DodajAgenciju(agencijaSaGreskom);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        //3.Test: BadRequest za Duplikate Naziva
        [Test]
        public async Task DodajAgenciju_DuplikatNaziva_BadRequest()
        {
            var postojećaAgencija = new Agencija
            {
                Naziv = "VecPostojecaAgencija",
                Adresa = "Test Adresa",
                Grad = "Test Grad",
                Email = "test@example.com",
                BrojTelefona = "123456789"
            };

            await _context.Agencije.AddAsync(postojećaAgencija);
            await _context.SaveChangesAsync();

            var novaAgencija = new Agencija
            {
                Naziv = "VecPostojecaAgencija",
                Adresa = "Nova Adresa",
                Grad = "Novi Grad",
                Email = "nova@test.com",
                BrojTelefona = "987654321"
            };

            var result = await _agencijaController.DodajAgenciju(novaAgencija);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Agencija sa istim nazivom već postoji.", badRequestResult.Value);
        }

        //ObrisiAgenciju
        //1.Test: Uspesno brisanje
        [Test]
        public async Task ObrisiAgenciju_UspesnoBrisanje_VracaOkStatus()
        {
            var agencija = new Agencija
            {
                Naziv = "Test Agencija",
                Adresa = "Test Adresa",
                Grad = "Test Grad",
                Email = "test@example.com",
                BrojTelefona = "123456789"
            };

            await _context.Agencije.AddAsync(agencija);
            await _context.SaveChangesAsync();

            var result = await _agencijaController.ObrisiAgenciju(agencija.Id);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual($"Izbrisana je agencija: {agencija.Naziv}", okResult.Value);
        }

        //2.Test: Brisanje nepostojece agencije
        [Test]
        public async Task ObrisiAgenciju_NepostojecaAgencija_BadRequest()
        {
            var nepostojeciId = 9999;

            var result = await _agencijaController.ObrisiAgenciju(nepostojeciId);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Nije uspelo brisanje agencije!", badRequestResult.Value);
        }

        //3.Test: da li se obrisao iz baze
        [Test]
        public async Task ObrisiAgenciju_ProveraBrisanjaIzBaze()
        {
            var agencija = new Agencija
            {
                Naziv = "Test Agencija",
                Adresa = "Test Adresa",
                Grad = "Test Grad",
                Email = "test@example.com",
                BrojTelefona = "123456789"
            };

            await _context.Agencije.AddAsync(agencija);
            await _context.SaveChangesAsync();

            await _agencijaController.ObrisiAgenciju(agencija.Id);
            var agencijaIzBaze = await _context.Agencije.FindAsync(agencija.Id);

            Assert.IsNull(agencijaIzBaze, "Agencija nije obrisana iz baze.");
        }

        //PreuzmiAgencije
        //1.Test
        [Test]
        public async Task PrezumiAgencije_UspesnoPreuzimanje_VracaOKStatus()
        {
            var agencije = new List<Agencija>
            {
                new Agencija { Naziv = "Agencija1", Adresa = "Adresa1", Grad = "Grad1", Email = "email1@example.com", BrojTelefona = "123456789" },
                new Agencija { Naziv = "Agencija2", Adresa = "Adresa2", Grad = "Grad2", Email = "email2@example.com", BrojTelefona = "987654321" }
            };

            await _context.Agencije.AddRangeAsync(agencije);
            await _context.SaveChangesAsync();

            var result = await _agencijaController.PrezumiAgencije() as OkObjectResult;

            Assert.IsNotNull(result);
            var preuzeteAgencije = result.Value as List<Agencija>;
            Assert.IsNotNull(preuzeteAgencije);

            Assert.AreEqual(_context.Agencije.Count(), preuzeteAgencije.Count);
        }

        //2.Test:
        //3.Test:

        //PreuzmiAgenciju
        //1.Test:
        [Test]
        public async Task PrezumiAgenciju_Uspeh_VracaOkStatusSaTacnimPodacima()
        {
            var novaAgencija = new Agencija
            {
                Naziv = "NovaAgencija",
                Adresa = "Test Adresa",
                Grad = "Novi Grad",
                BrojTelefona = "123456789",
                Email = "test@example.com"
            };

            await _context.Agencije.AddAsync(novaAgencija);
            await _context.SaveChangesAsync();

            var result = await _agencijaController.PrezumiAgenciju(novaAgencija.Id) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(result.Value);

            var preuzetaAgencija = result.Value as Agencija;
            Assert.IsNotNull(preuzetaAgencija);
            Assert.AreEqual(novaAgencija.Naziv, preuzetaAgencija.Naziv);
            Assert.AreEqual(novaAgencija.Adresa, preuzetaAgencija.Adresa);
        }

        //2.Test:
    }
}