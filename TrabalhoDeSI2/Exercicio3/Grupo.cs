//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Exercicio3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grupo
    {
        public Grupo()
        {
            this.Precario = new HashSet<Precario>();
            this.Veiculo = new HashSet<Veiculo>();
        }
    
        public string designacao { get; set; }
        public string descrição { get; set; }
        public bool freesale { get; set; }
    
        public virtual ICollection<Precario> Precario { get; set; }
        public virtual ICollection<Veiculo> Veiculo { get; set; }
    }
}