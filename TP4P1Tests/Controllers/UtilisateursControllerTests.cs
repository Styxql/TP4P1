using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP4P1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP4P1.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TP4P1.Models.DataManager;
using TP4P1.Models.Repository;
using System.Security.Cryptography;

namespace TP4P1.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private UtilisateursController _controller;
        private FilmRatingDBContext _context;
        private IDataRepository<Utilisateur> dataRepository;
        public UtilisateursControllerTests()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingDBContext>().UseNpgsql("Server = localhost; port = 5432; Database = TP4; uid = postgres; \npassword = postgres;");
            _context = new FilmRatingDBContext(builder.Options);
            dataRepository = new UtilisateurManager(_context);
            _controller = new UtilisateursController(dataRepository);
        }

        [TestMethod()]
        public async Task GetUtilisateursTest()
        {
            // Chargez les utilisateurs de la base de données
            var utilisateurbd = await _context.Utilisateurs.ToListAsync();

            // Appelez la méthode GetUtilisateurs du contrôleur
            var result = await _controller.GetUtilisateurs();
            var utilisateur = result.Value.ToList();

            // Assurez-vous que les utilisateurs récupérés par la méthode GetUtilisateurs correspondent à ceux de la base de données
            CollectionAssert.AreEqual(utilisateurbd, utilisateur);
        }

        [TestMethod()]
        public async Task GetById()
        {
            var userId = 1;
            var utilisateur = _context.Utilisateurs.FirstOrDefault(c => c.Id == userId);

            var result = await _controller.GetUtilisateurById(1);
            var resultUtilisateur = result.Value;

            var resultfalse = await _controller.GetUtilisateurById(2);

            Assert.AreEqual(utilisateur, resultUtilisateur);
            Assert.AreNotEqual(utilisateur, resultfalse);
        }
        [TestMethod()]
        public async Task GetByEmail()
        {
            string email = "clilleymd@last.fm";
            var utilisateur = _context.Utilisateurs.FirstOrDefault(c => c.Mail == email);

            var result = await _controller.GetUtilisateurByEmail(email);
            var resultUtilisateur = result.Value;

            var resultfalse = await _controller.GetUtilisateurByEmail("salut@gmail.com");

            Assert.AreEqual(utilisateur, resultUtilisateur);
            Assert.AreNotEqual(utilisateur, resultfalse);
        }


        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API
            
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = _controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de manière
            // Assert
            Utilisateur? userRecupere = _context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail
            userAtester.Id = userRecupere.Id;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }


        [TestMethod]
        public void Pututilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API
            int iduseramodif = 2;
            Utilisateur userAtester = new Utilisateur()
            {
                Id = iduseramodif,
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = _controller.PutUtilisateur(iduseramodif,userAtester).Result; // .Result pour appeler la méthode async de manière
            // Assert
            Utilisateur? userRecupere = _context.Utilisateurs.FirstOrDefault(u => u.Id == userAtester.Id);// On récupère l'utilisateur créé directement dans la BD grace à son mail
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod]
        public void DeleteUtilisateurTest()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            Utilisateur userAtester = new Utilisateur()
            {
                
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            _context.Add(userAtester);
            
            _context.SaveChanges();
            int idUserDelete = userAtester.Id;
            _controller.DeleteUtilisateur(idUserDelete);

            Assert.IsNull(_context.Utilisateurs.FirstOrDefault(u=>u.Id== idUserDelete));
        }

    }
}

