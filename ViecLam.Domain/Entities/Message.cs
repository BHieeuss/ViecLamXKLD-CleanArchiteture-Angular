﻿namespace ViecLam.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}