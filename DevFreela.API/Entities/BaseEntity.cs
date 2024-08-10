﻿namespace DevFreela.API.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatdAt = DateTime.Now;
            IsDeleted = false;
        }

        public int Id { get; set; }
        public DateTime CreatdAt { get; set; }
        public bool IsDeleted { get; set; }

        public void SetAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
