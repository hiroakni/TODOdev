﻿using System.Data.Entity;

namespace TodoApp.Models
{
    public class AppContext : DbContext
    {
        // Azure上のDBに接続しに行く処理。ローカルで開発する場合はここをコメントアウト
        public AppContext() : base("name=TodoDbConnection")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Todo> Todoes { get; set; }
        public DbSet<TransactionInfo> TransactionInfos { get; set; }

    }
}
