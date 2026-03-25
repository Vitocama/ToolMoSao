using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ToolModiSAO.Models
{
    public partial class Attestati
    {
        public int Id { get; set; }
        public string Attivo { get; set; }
        public string MatricolaDipendente { get; set; }
        public string Dipendente { get; set; }
        public string TitoloCorso { get; set; }
        public string AttivitaFormativa { get; set; }
        public string MateriaCorso { get; set; }
        public string TipologiaCorso { get; set; }
        public string EnteFormatore { get; set; }
        public string DenominazioneEnteFormatore { get; set; }
        public string DenominazioneEnteCertificatore { get; set; }
        public string DataInizioCorso { get; set; }
        public string DataFineCorso { get; set; }
        public string DataNormalizzata { get; set; }
        public string AnnoCorso { get; set; }
        public string ValiditaAnni { get; set; }
        public string DataScadenzaCorso { get; set; }
        public string LinkAttestato { get; set; }
        public string Note { get; set; }
        public string CodUfficio { get; set; }
        public string Reparto { get; set; }
        public string Sezione { get; set; }
        public string Nucleo { get; set; }
    }
}
