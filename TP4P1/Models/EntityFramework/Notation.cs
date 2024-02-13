using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TP4P1.Models.EntityFramework;

[Table("t_j_notation_not")]
public partial class Notation
{

    [Key]
    [ForeignKey("t_e_utilisateur_utl")]
    public int UtilisateurId { get; set; }

    [ForeignKey("t_e_film_flm")]
    public int FilmId { get; set; }

    [Column("not_note")]
    [Range(0,5)]
    public int Note { get; set; }


    [ForeignKey("UtilisateurId")]
    [InverseProperty("NotesUtilisateur")]
    public virtual Utilisateur UtilisateurNotant { get; set; }

    [ForeignKey("FilmId")]
    [InverseProperty("NotesFilm")]
    public virtual Film FilmNote { get; set; }




}

