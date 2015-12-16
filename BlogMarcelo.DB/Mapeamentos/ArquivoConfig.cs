using BlogMarcelo.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMarcelo.DB.Mapeamentos
{
    public class ArquivoConfig :EntityTypeConfiguration<Arquivo>
    {
        public ArquivoConfig()
        {
         ToTable("ARQUIVO");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDARQUIVO")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Bytes)
               .HasColumnName("BYTES")
               .IsRequired();

            Property(x => x.IdPost)
               .HasColumnName("IDPOST")
               .IsRequired();

            Property(x => x.Extensao)
               .HasColumnName("EXTENSAO")
               .HasMaxLength(100)
               .IsRequired();

             HasRequired(X => X.Post)
                .WithMany()
                .HasForeignKey(x => x.IdPost);    

        }
    }
}
