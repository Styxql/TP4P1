﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using TP4P1.Models.DataManager;
using TP4P1.Models.EntityFramework;
using TP4P1.Models.Repository;
using static TP4P1.Models.DataManager.UtilisateurManager;

namespace TP4P1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly IDataRepository<Utilisateur> dataRepository;
        public UtilisateursController(IDataRepository<Utilisateur> dataRepo)
            {
            dataRepository = dataRepo;
            }
    // GET: api/Utilisateurs
    [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            return dataRepository.GetAll();
        }
        // GET: api/Utilisateurs/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(int id)
        {
            var utilisateur = dataRepository.GetById(id);
            //var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return utilisateur;
        }
        // GET: api/Utilisateurs/toto@titi.fr
        [HttpGet]
        [Route("[action]/{email}")]
        [ActionName("GetByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurByEmail(string email)
        {
            var utilisateur = await dataRepository.GetByStringAsync(email);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return utilisateur;
        }
        // PUT: api/Utilisateurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest();
            }
            var userToUpdate = dataRepository.GetById(id);
            if (userToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                dataRepository.UpdateAsync(userToUpdate.Value, utilisateur);
                return NoContent();
            }
        }
        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dataRepository.Add(utilisateur);
            return CreatedAtAction("GetById", new { id = utilisateur.Id }, utilisateur); // GetById : nom de l’action
        }
        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = dataRepository.GetById(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            dataRepository.Delete(utilisateur.Value);
            return NoContent();
        }
        //private bool UtilisateurExists(int id)
        //{
        // return _context.Utilisateurs.Any(e => e.UtilisateurId == id);
        //}
    }
}

