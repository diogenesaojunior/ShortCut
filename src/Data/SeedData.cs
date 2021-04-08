using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SisGerenciador.src.Models;

namespace SisGerenciador.src.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MeuContexto(
                    serviceProvider.GetRequiredService<
                        DbContextOptions<MeuContexto>>()))
            {
                if (!context.Departamentos.Any())
                {
                    var departamentos = new List<Departamento>
                    {
                        new Departamento { Sigla = "BSI", Descricao = "Bacharelado de Sistema de Informação"}
                    };

                    context.Departamentos.AddRange(departamentos);
                    context.SaveChanges();
                }
                if (!context.Instituicoes.Any())
                {
                    var instituicoes = new List<Instituicao>
                    {
                        new Instituicao { Sigla = "IFS", Descricao = "Instituto Federal de Ciência e Tecnologia de Sergipe"},
                        new Instituicao { Sigla = "UFS", Descricao = "Universidade Federal de Sergipe"}
                    };

                    context.Instituicoes.AddRange(instituicoes);
                    context.SaveChanges();
                }

                if (!context.Disciplinas.Any())
                {
                    var disciplinas = new List<Disciplina>
                    {
                        new Disciplina { Descricao = "Prática de Ensino Orientada", Credito = 8 },
                        new Disciplina { Descricao = "Web 2", Credito = 6 },
                        new Disciplina { Descricao = "Teste de Software", Credito = 4 }
                    };

                    context.Disciplinas.AddRange(disciplinas);
                    context.SaveChanges();
                }

                if (!context.PeriodoCurriculares.Any())
                {
                    var periodosCurriculares = new List<PeriodoCurricular>
                    {
                        new PeriodoCurricular { NumOrdinal = 1},
                        new PeriodoCurricular { NumOrdinal = 2},
                        new PeriodoCurricular { NumOrdinal = 3},
                        new PeriodoCurricular { NumOrdinal = 4},
                        new PeriodoCurricular { NumOrdinal = 5},
                        new PeriodoCurricular { NumOrdinal = 6},
                        new PeriodoCurricular { NumOrdinal = 7},
                        new PeriodoCurricular { NumOrdinal = 8},
                        new PeriodoCurricular { NumOrdinal = 9},
                        new PeriodoCurricular { NumOrdinal = 10},
                        new PeriodoCurricular { NumOrdinal = 11},
                        new PeriodoCurricular { NumOrdinal = 12},
                    };

                    context.PeriodoCurriculares.AddRange(periodosCurriculares);
                    context.SaveChanges();
                }

            }
        }
    }
}
