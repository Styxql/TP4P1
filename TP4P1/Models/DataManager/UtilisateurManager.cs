using TP4P1.Models.Repository;
using TP4P1.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TP4P1.Models.DataManager
{
    
    public class UtilisateurManager : IDataRepository<Utilisateur>
    {
        readonly FilmRatingDBContext? filmsDbContext;
        public UtilisateurManager() { }
        public UtilisateurManager(FilmRatingDBContext context)
        {
            filmsDbContext = context;
        }
        public ActionResult<IEnumerable<Utilisateur>> GetAll()
        {
            return filmsDbContext.Utilisateurs.ToList();
        }
        public ActionResult<Utilisateur> GetById(int id)
        {
            return filmsDbContext.Utilisateurs.FirstOrDefault(u => u.Id == id);
        }
        public async Task<ActionResult<Utilisateur>> GetByStringAsync(string mail)
        {
            return await filmsDbContext.Utilisateurs.FirstOrDefaultAsync(u => u.Mail.ToUpper() == mail.ToUpper());
        }



        public async Task AddAsync(Utilisateur entity)
        {
            await filmsDbContext.Utilisateurs.AddAsync(entity);
            await filmsDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Utilisateur utilisateur, Utilisateur entity)
        {
            filmsDbContext.Entry(utilisateur).State = EntityState.Modified;
            utilisateur.Id = entity.Id;
            utilisateur.Nom = entity.Nom;
            utilisateur.Prenom = entity.Prenom;
            utilisateur.Mail = entity.Mail;
            utilisateur.Rue = entity.Rue;
            utilisateur.CodePostal = entity.CodePostal;
            utilisateur.Ville = entity.Ville;
            utilisateur.Pays = entity.Pays;
            utilisateur.Latitude = entity.Latitude;
            utilisateur.Longitude = entity.Longitude;
            utilisateur.Pwd = entity.Pwd;
            utilisateur.Mobile = entity.Mobile;
            utilisateur.NotesUtilisateur = entity.NotesUtilisateur;
            await filmsDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Utilisateur utilisateur)
        {
                 filmsDbContext.Utilisateurs.Remove(utilisateur);
           await filmsDbContext.SaveChangesAsync();
        }
    }
}

