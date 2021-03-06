﻿// <auto-generated />
using System;
using APIorm.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APIorm.Migrations
{
    [DbContext(typeof(CompraContext))]
    partial class CompraContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APIorm.Models.Compra", b =>
                {
                    b.Property<long>("IdCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdLoja")
                        .HasColumnType("int");

                    b.Property<string>("ObservacaoCompra")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioSolicitante")
                        .HasColumnType("bigint");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("IdCompra");

                    b.HasIndex("UsuarioSolicitante");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("APIorm.Models.ItensCompra", b =>
                {
                    b.Property<long>("IdItemCompra")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("IdCompra")
                        .HasColumnType("bigint");

                    b.Property<long>("IdProduto")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("float");

                    b.Property<double>("ValorUnit")
                        .HasColumnType("float");

                    b.HasKey("IdItemCompra");

                    b.HasIndex("IdCompra");

                    b.HasIndex("IdProduto");

                    b.ToTable("ItensCompras");
                });

            modelBuilder.Entity("APIorm.Models.Produto", b =>
                {
                    b.Property<long>("IdProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("IdProduto");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("APIorm.Models.Usuario", b =>
                {
                    b.Property<long>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("SenhaLogin")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Sexo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("APIorm.Models.Compra", b =>
                {
                    b.HasOne("APIorm.Models.Usuario", "Usuario")
                        .WithMany("Compra")
                        .HasForeignKey("UsuarioSolicitante")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APIorm.Models.ItensCompra", b =>
                {
                    b.HasOne("APIorm.Models.Compra", "Compra")
                        .WithMany("ItensCompra")
                        .HasForeignKey("IdCompra")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APIorm.Models.Produto", "Produto")
                        .WithMany("ItensCompra")
                        .HasForeignKey("IdProduto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
