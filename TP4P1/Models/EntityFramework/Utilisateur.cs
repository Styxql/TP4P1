﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
namespace TP4P1.Models.EntityFramework;

    [Table("t_e_utilisateur_utl")]
    public partial class Utilisateur
    {

    [Key]
    [Column("utl_id")]
    public int Id { get; set; }

    [Column("utl_nom")]
    [StringLength(50)]
    public string? Nom { get; set; }

    [Column("utl_prenom")]
    [StringLength(50)]
    public string? Prenom { get; set; }

    [Column("utl_mobile", TypeName = "char(10)")]
    public string? Mobile { get; set; }

    [Required]

    [Column("utl_mail")]
    [EmailAddress]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d’un email doit être comprise entre 6 et 100 caractères.")]
    public string Mail { get; set; } = null!;


    [Column("utl_pwd")]
    [StringLength(100)]
    public string Pwd { get; set; } = null!;

    [Column("utl_rue")]
    [StringLength(200)]
    public string? Rue { get; set; }


    [Column("utl_cp",TypeName ="char(10)")]
    [StringLength(100)]
    public string? CodePostal { get; set; }


    [Column("utl_ville")]
    [StringLength(50)]
    public string? Ville { get; set; }


    [Column("utl_pays")]
    [StringLength(100)]
    [DefaultValue("France")]
    public string? Pays { get; set; }



    [Column("utl_latitude")]
    public float? Latitude { get; set; }
    [Column("utl_longitude")]
    public float? Longitude { get; set; }

    [Column("utl_datecreation", TypeName = "Date")]
    
    public DateTime DateCreation { get; set; }


    [InverseProperty("UtilisateurNotant")]
    public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();
}

