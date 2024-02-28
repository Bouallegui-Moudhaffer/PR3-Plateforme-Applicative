namespace PA.ApplicationCore.Domain
{
    public class Log
    {
        public int LogId { get; set; } // Primary Key
        public virtual DateTime Timestamp { get; set; } // Time of the event
        public string EventType { get; set; } // Type of event (e.g., Maintenance, Shutdown, StartUp)
        public string Description { get; set; } // Detailed description of the event

        // Optional associations:
        public int? EtablissementId { get; set; } // Foreign Key (Nullable) - if the log is related to an establishment
        public int? SallesId { get; set; } // Foreign Key (Nullable) - if the log is related to a specific room
        public int? PostesId { get; set; } // Foreign Key (Nullable) - if the log is related to a specific workstation
        public int? UtilisateurId { get; set; } // Foreign Key (Nullable) - if the log is related to a user action

        // Navigation properties
        public virtual Etablissement Etablissement { get; set; }
        public virtual Salles Salle { get; set; }
        public virtual Postes Poste { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
    }
}
