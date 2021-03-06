﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TransactionInfo
    {
        [Required]
        [DisplayName("登録No")]
        public int Id { get; set; }

        [Required]
        [DisplayName("取引銘柄")]
        public string TransactionTitle { get; set; }

        [Required]
        [DisplayName("東証コード")]
        public int? ToushouCode { get; set; }

        [DisplayName("買付金額")]
        public int? PurchaseFund { get; set; }
        
        [DisplayName("売付金額")]
        public int? TransactionFund { get; set; }

        [DisplayName("買付日")]
        public string PurchaseDate { get; set; }

        [DisplayName("売付日")]
        public string TransactionDate { get; set; }

        [DisplayName("買付数")]
        public int? PurchaseCount { get; set; }

        [DisplayName("売付数")]
        public int? TransactionCount { get; set; }

        [DisplayName("損益率")]
        public int? ReturnOnInvestment { get; set; }

        [DisplayName("test")]
        public string Test { get; set; }

        public virtual User User { get; set; }

    }
}