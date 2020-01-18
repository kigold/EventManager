using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventManagement.Core.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? UserId { get; set; }
        public bool IsRecuring { get; set; }
        public DateTime DateCreated { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int RequestId { get; set; }

    }

    public class EventMap : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            /*builder.Property(x => x.RequestId)
                .ValueGeneratedOnAdd();
            SetupData(builder);*/
        }

        private void SetupData(EntityTypeBuilder<Event> builder)
        {

        }
    }
}
