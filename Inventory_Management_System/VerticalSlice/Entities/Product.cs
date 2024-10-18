﻿namespace Inventory_Management_System.VerticalSlice.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set;}
         public int Quantity { get; set; } 
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }
    
    }
}
