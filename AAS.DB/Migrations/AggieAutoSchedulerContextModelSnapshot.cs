﻿// <auto-generated />
using System;
using AAS.DB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AAS.DB.Migrations
{
    [DbContext(typeof(AggieAutoSchedulerContext))]
    partial class AggieAutoSchedulerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AggieAutoSchedulerDB.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CRN");

                    b.Property<int>("CourseNumber");

                    b.Property<int>("Credits");

                    b.Property<int>("SectionNumber");

                    b.Property<string>("Subject");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("AggieAutoSchedulerDB.Exam", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseID");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Location");

                    b.Property<string>("Professor");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("AggieAutoSchedulerDB.Period", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseID");

                    b.Property<string>("DayOfWeek");

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Location");

                    b.Property<string>("Professor");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("Period");
                });

            modelBuilder.Entity("AggieAutoSchedulerDB.Exam", b =>
                {
                    b.HasOne("AggieAutoSchedulerDB.Course")
                        .WithMany("Exams")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AggieAutoSchedulerDB.Period", b =>
                {
                    b.HasOne("AggieAutoSchedulerDB.Course")
                        .WithMany("Periods")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
