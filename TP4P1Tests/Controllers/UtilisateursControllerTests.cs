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
using Moq;

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
            var utilisateurbd = await _context.Utilisateurs.ToListAsync();

            var result = await _controller.GetUtilisateurs();
            var utilisateur = result.Value.ToList();
            
            CollectionAssert.AreEqual(utilisateurbd, utilisateur);
        }



        [TestMethod]
        public void GetUtilisateurById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
         {
         // Arrange
             Utilisateur user = new Utilisateur
             {
                 Id = 1,
                 Nom = "Calida",
                 Prenom = "Lilley",
                 Mobile = "0653930778",
                 Mail = "clilleymd@last.fm",
                 Pwd = "Toto12345678!",
                 Rue = "Impasse des bergeronnettes",
                 CodePostal = "74200",
                 Ville = "Allinges",
                 Pays = "France",
                 Latitude = 46.344795F,
                 Longitude = 6.4885845F
             };
                    var mockRepository = new Mock<IDataRepository<Utilisateur>>();
                    mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
                    var userController = new UtilisateursController(mockRepository.Object);
                    // Act
                    var actionResult = userController.GetUtilisateurById(1).Result;
                    // Assert
                    Assert.IsNotNull(actionResult);
             Assert.IsNotNull(actionResult.Value);
             Assert.AreEqual(user, actionResult.Value as Utilisateur);
         }
        [TestMethod()]
        public async Task GetByEmail()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);


            //    string email = "clilleymd@last.fm";
            //    var utilisateur = _context.Utilisateurs.FirstOrDefault(c => c.Mail == email);

            //    var result = await _controller.GetUtilisateurByEmail(email);
            //    var resultUtilisateur = result.Value;

            //    var resultfalse = await _controller.GetUtilisateurByEmail("salut@gmail.com");

            //    Assert.AreEqual(utilisateur, resultUtilisateur);
            //    Assert.AreNotEqual(utilisateur, resultfalse);*


            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };

            var utilisateur = mockRepository.Setup(x => x.GetByStringAsync("poisson@gmail.com").Result).Returns(user);

            var actionResult=userController.GetUtilisateurByEmail("poisson@gmail.com").Result;

            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);






        }


        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var actionResult = userController.PostUtilisateur(user).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
            user.Id = ((Utilisateur)result.Value).Id;
            Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
        }

        [TestMethod]
        public async Task Pututilisateur_ModelValidated_CreationOK()
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
        public async Task DeleteUtilisateurTest()
        {
            //Random rnd = new Random();
            //int chiffre = rnd.Next(1, 1000000000);
            //Utilisateur userAtester = new Utilisateur()
            //{

            //    Nom = "MACHIN",
            //    Prenom = "Luc",
            //    Mobile = "0606070809",
            //    Mail = "machin" + chiffre + "@gmail.com",
            //    Pwd = "Toto1234!",
            //    Rue = "Chemin de Bellevue",
            //    CodePostal = "74940",
            //    Ville = "Annecy-le-Vieux",
            //    Pays = "France",
            //    Latitude = null,
            //    Longitude = null
            //};
            //_context.Add(userAtester);

            //_context.SaveChanges();
            //int idUserDelete = userAtester.Id;
            //await _controller.DeleteUtilisateur(idUserDelete);

            //Assert.IsNull(_context.Utilisateurs.FirstOrDefault(u=>u.Id== idUserDelete));



            Utilisateur user = new Utilisateur
            {
                Id = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);


            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteUtilisateur(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour

        }

    }
}

