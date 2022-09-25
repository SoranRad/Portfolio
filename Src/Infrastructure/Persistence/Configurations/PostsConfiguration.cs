using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public  class PostsConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .ToTable                ("tbl_Post", "Posts")
                .HasKey                 (x => x.Id)
                .IsClustered            (false);

            builder
                .Property               (x => x.Id)
                .ValueGeneratedOnAdd    ();


            builder
                .Property               (x => x.Content)
                .IsRequired             (false);

            builder
                .Property               (x => x.FileName)
                .IsRequired             (false);

            builder
                .Property               (x => x.Title)
                .IsFixedLength          ()
                .HasMaxLength           (Post.MAX_TITLE_LENGTH)
                .IsRequired             (true);

             builder
                .Property               (x => x.Tags)
                .IsFixedLength          ()
                .HasMaxLength           (500)
                .IsRequired             (false);


        }
    }
}
