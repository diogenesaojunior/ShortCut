using SisGerenciador.src.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SisGerenciador.src.Data
{
    public class MeuContexto : IdentityDbContext<Usuario>
    {
        public MeuContexto(DbContextOptions<MeuContexto> options) : base(options)
        {

        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<DisciplinaOfertada> DisciplinaOfertadas { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<GradeCurricular> GradeCurriculars { get; set; }
        public DbSet<HoraAula> HoraAulas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<MatriculaDisciplina> MatriculaDisciplinas { get; set; }
        public DbSet<PeriodoCurricular> PeriodoCurriculares { get; set; }
        public DbSet<PeriodoLetivo> PeriodoLetivos { get; set; }
        public DbSet<PreRequisito> PreRequisitos { get; set; }
        public DbSet<Restricao> Restricoes { get; set; }
        //public DbSet<SugestaoMatricula> SugestaoMatriculas { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<TurmaHorario> TurmaHorarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*Rela��es muitos para muitos - Grade Curricular*/
            modelBuilder.Entity<GradeCurricular>()
                .HasKey(m => new { m.Id });

            modelBuilder.Entity<GradeCurricular>()
                .HasOne(am => am.Curso)
                .WithMany(a => a.GradeCurriculares)
                .HasForeignKey(am => am.CursoId);

            modelBuilder.Entity<GradeCurricular>()
                .HasOne(am => am.PeriodoCurricular)
                .WithMany(m => m.GradeCurriculares)
                .HasForeignKey(am => am.PeriodoCurricularId);

            modelBuilder.Entity<GradeCurricular>()
                .HasOne(am => am.Disciplina)
                .WithMany(m => m.GradeCurriculares)
                .HasForeignKey(am => am.DisciplinaId);

            /*Rela��es muitos para muitos - DisciplinaOfertadas*/
            modelBuilder.Entity<DisciplinaOfertada>()
                .HasKey(t => new { t.Id });

            modelBuilder.Entity<DisciplinaOfertada>()
                .HasOne(at => at.Disciplina)
                .WithMany(a => a.DisciplinaOfertadas)
                .HasForeignKey(at => at.DisciplinaId);

            modelBuilder.Entity<DisciplinaOfertada>()
                .HasOne(at => at.PeriodoLetivo)
                .WithMany(t => t.DisciplinaOfertadas)
                .HasForeignKey(at => at.PeriodoLetivoId);

            /*Rela��es muitos para muitos - PreRequisitos*/
            modelBuilder.Entity<PreRequisito>()
                .HasKey(m => new { m.Id });

            modelBuilder.Entity<PreRequisito>()
                .HasOne(im => im.Liberada)
                .WithMany(i => i.Liberadas)
                .HasForeignKey(im => im.LiberadaId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<PreRequisito>()
                .HasOne(im => im.Liberadora)
                .WithMany(i => i.Liberadoras)
                .HasForeignKey(im => im.LiberadoraId)
                .OnDelete(DeleteBehavior.Restrict);


            /*Rela��es muitos para muitos - Turma*/
            modelBuilder.Entity<Turma>()
               .HasKey(m => new { m.Id });

            modelBuilder.Entity<Turma>()
                .HasOne(pm => pm.DisciplinaOfertada)
                .WithMany(p => p.Turmas)
                .HasForeignKey(pm => pm.DisciplinaOfertadaId);

            modelBuilder.Entity<Turma>()
                 .HasOne(pm => pm.Docente)
                 .WithMany(p => p.Turmas)
                 .HasForeignKey(pm => pm.DocenteId);

            /*Rela��es muitos para muitos - TurmaHorario*/
            modelBuilder.Entity<TurmaHorario>()
               .HasKey(a => new { a.Id });

            modelBuilder.Entity<TurmaHorario>()
                .HasOne(pa => pa.Turma)
                .WithMany(p => p.TurmaHorarios)
                .HasForeignKey(pa => pa.TurmaId);

            modelBuilder.Entity<TurmaHorario>()
                .HasOne(pa => pa.Horario)
                .WithMany(p => p.TurmaHorarios)
                .HasForeignKey(pa => pa.HorarioId);

            /*Rela��es muitos para muitos - Restricao*/
            modelBuilder.Entity<Restricao>()
               .HasKey(a => new { a.Id });

            modelBuilder.Entity<Restricao>()
                .HasOne(pa => pa.Horario)
                .WithMany(p => p.Restricoes)
                .HasForeignKey(pa => pa.HorarioId);

            modelBuilder.Entity<Restricao>()
                .HasOne(pa => pa.Aluno)
                .WithMany(p => p.Restricoes)
                .HasForeignKey(pa => pa.AlunoId);

            /*Rela��es muitos para muitos - MatriculaDisciplina*/
            modelBuilder.Entity<MatriculaDisciplina>()
               .HasKey(a => new { a.Id });

            modelBuilder.Entity<MatriculaDisciplina>()
                .HasOne(pa => pa.Aluno)
                .WithMany(p => p.MatriculaDisciplinas)
                .HasForeignKey(pa => pa.AlunoId);

            modelBuilder.Entity<MatriculaDisciplina>()
                .HasOne(pa => pa.Disciplina)
                .WithMany(p => p.MatriculaDisciplinas)
                .HasForeignKey(pa => pa.DisciplinaId);

            ///*Rela��es muitos para muitos - SugestaoMatricula*/
            //modelBuilder.Entity<SugestaoMatricula>()
            //   .HasKey(a => new { a.Id });

            //modelBuilder.Entity<SugestaoMatricula>()
            //    .HasOne(pa => pa.Aluno)
            //    .WithMany(p => p.SugestaoMatriculas)
            //    .HasForeignKey(pa => pa.AlunoId);

            //modelBuilder.Entity<SugestaoMatricula>()
            //    .HasOne(pa => pa.Turma)
            //    .WithMany(p => p.SugestaoMatriculas)
            //    .HasForeignKey(pa => pa.TurmaId);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Integrated Security=SSPI; Password=123; User ID=sa; Initial Catalog=Shortcut; Data Source=DESKTOP-T2I87RH\\SQLEXPRESS");
            }
        }
    }
}